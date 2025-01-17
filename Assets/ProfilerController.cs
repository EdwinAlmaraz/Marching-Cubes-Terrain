using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using Unity.Profiling;
using Unity.Profiling.LowLevel.Unsafe;
using TMPro;

public class ProfilerController : MonoBehaviour
{
    ProfilerRecorder systemMemoryRecorder;
    ProfilerRecorder gcMemoryRecorder;
    ProfilerRecorder mainThreadTimeRecorder;
    ProfilerRecorder drawCallsCountRecorder;
    ProfilerRecorder batchesCountRecorder;
    ProfilerRecorder trianglesCountRecorder;
    ProfilerRecorder verticesCountRecorder;

    public TextMeshProUGUI m_StatsText;

    System.IO.StreamWriter fileStream = null;
    const string TRG_FILE_FMT = @"C:\profilerlogs\log-{0:yyyy-MM-ddTHHmmss}.csv";
    long frameCount = 0;

    static double GetRecorderFrameAverage(ProfilerRecorder recorder)
    {
        var samplesCount = recorder.Capacity;
        if (samplesCount == 0)
            return 0;

        double r = 0;

        var samples = new List<ProfilerRecorderSample>(samplesCount);
        recorder.CopyTo(samples);
        for (var i = 0; i < samples.Count; ++i)
            r += samples[i].Value;
        r /= samplesCount;

        return r;
    }

    void OnEnable()
    {
        fileStream = System.IO.File.CreateText(string.Format(TRG_FILE_FMT, DateTime.Now));

        var line = @"FPS,GC Memory (MB),System Memory (MB),Draw Calls,Batches,Triangles,Vertices";
        fileStream.WriteLine(line);

        systemMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
        gcMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");
        mainThreadTimeRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Internal, "Main Thread", 15);
        drawCallsCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
        trianglesCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Triangles Count");
        batchesCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Batches Count");
        verticesCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Vertices Count");
    }

    void OnDisable()
    {
        systemMemoryRecorder.Dispose();
        gcMemoryRecorder.Dispose();
        mainThreadTimeRecorder.Dispose();
        drawCallsCountRecorder.Dispose();
        trianglesCountRecorder.Dispose();
        batchesCountRecorder.Dispose();
        verticesCountRecorder.Dispose();

        if (fileStream != null)
        {
            fileStream.Flush();
            fileStream.Close();
        }
    }

    void Update()
    {
        var sb = new StringBuilder(500);

        sb.AppendLine($"Frame Time: {1000 / (GetRecorderFrameAverage(mainThreadTimeRecorder) * (1e-6f)):F1} FPS");// * (1e-6f):F1} ms");
        sb.AppendLine($"GC Memory: {gcMemoryRecorder.LastValue / (1024 * 1024)} MB");
        sb.AppendLine($"System Memory: {systemMemoryRecorder.LastValue / (1024 * 1024)} MB");
        sb.AppendLine($"Draw Calls: {drawCallsCountRecorder.LastValue}");
        sb.AppendLine($"Batches: {batchesCountRecorder.LastValue}");
        sb.AppendLine($"Triangles: {trianglesCountRecorder.LastValue}");
        sb.AppendLine($"Vertices: {verticesCountRecorder.LastValue}");

        m_StatsText.text = sb.ToString();

        if (fileStream != null)
        {
            if (++frameCount > 60)
            {
                var line = $"\"{1000 / (GetRecorderFrameAverage(mainThreadTimeRecorder) * (1e-6f)):F1}\", \"{gcMemoryRecorder.LastValue / (1024 * 1024)}\", \"{systemMemoryRecorder.LastValue / (1024 * 1024)}\", \"{drawCallsCountRecorder.LastValue}\", \"{batchesCountRecorder.LastValue}\", \"{trianglesCountRecorder.LastValue}\", \"{verticesCountRecorder.LastValue}\"";
                fileStream.WriteLine(line);
            }
        }
    }
}
