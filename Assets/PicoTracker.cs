using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.Rendering;

public class PicoTracker : MonoBehaviour
{
    public Transform trackerObj;

    private bool updateOT = true;
    private int objectTrackersMaxNum = 1;

    bool externalDataFound = false;
    int realLength = 0;
    ExtDevTrackerPassDataArray passDataArray = new ExtDevTrackerPassDataArray();

    // Start is called before the first frame update
    void Start()
    {

        int res = -1;
        res = PXR_MotionTracking.CheckMotionTrackerModeAndNumber(MotionTrackerMode.MotionTracking, MotionTrackerNum.ONE);
        if (res == 0)
        {
            updateOT = true;
        }

        // Enable data passthrough-related APIs
        PXR_MotionTracking.SetExtDevTrackerPassDataState(true);
        PXR_MotionTracking.ExtDevPassDataAction += ExtDevPassDataAction;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current tracking mode, either body tracking or motion tracking.
        MotionTrackerMode trackingMode = PXR_MotionTracking.GetMotionTrackerMode();
        if (trackingMode != MotionTrackerMode.MotionTracking)
        {
            PXR_MotionTracking.CheckMotionTrackerModeAndNumber(MotionTrackerMode.MotionTracking);
        }


        // Update the poses of motion trackers
        if (updateOT && trackingMode == MotionTrackerMode.MotionTracking)
        {
            // Get the serial numbers of the motion trackers to identify them.
            MotionTrackerConnectState connectState = new MotionTrackerConnectState();
            int connectStateFailed = PXR_MotionTracking.GetMotionTrackerConnectStateWithSN(ref connectState);

            if (connectStateFailed == 0 && connectState.trackersSN.Length > 0)
            {

                string sn = connectState.trackersSN[0].value.ToString().Trim();
                if (!string.IsNullOrEmpty(sn))
                {
                    // Get the position and rotation estimates of each motion tracker.
                    MotionTrackerLocations locations = new MotionTrackerLocations();
                    MotionTrackerConfidence confidence = new MotionTrackerConfidence();
                    int getLocationsFailed = PXR_MotionTracking.GetMotionTrackerLocations(connectState.trackersSN[0], ref locations, ref confidence);

                    // if the return is success
                    if (getLocationsFailed == 0)
                    {
                        MotionTrackerLocation localLocation = locations.localLocation;

                        trackerObj.position = localLocation.pose.Position.ToVector3();
                        trackerObj.rotation = localLocation.pose.Orientation.ToQuat();
                    }
                }
            }
        }

        //Debug.Log(externalDataFound);
        if (externalDataFound)
        {
            int result = PXR_MotionTracking.GetExtDevTrackerByPassData(ref passDataArray, ref realLength);
            Debug.Log(realLength);
            for (int i = 0; i < realLength; i++)
            {
                ExtDevTrackerPassData passData = passDataArray.passDatas[i];
                Debug.Log("passData(" + i + "): " + passData);
            }
        }
    }

    public void OnDestroy()
    {
        PXR_MotionTracking.ExtDevPassDataAction -= ExtDevPassDataAction;
        // Disable data passthrough-related APIs
        PXR_MotionTracking.SetExtDevTrackerPassDataState(false);
    }

    private void ExtDevPassDataAction(int value)
    {
        Debug.Log("data received");
        if (value == 1) // when receiving `1`, call GetExtDevTrackerByPassData to obtain the data passed through
        {
            externalDataFound = true;
        }
        else if (value == 0) // When receiving `0`, stop calling GetExtDevTrackerByPassData
        {
            externalDataFound = false;
        }
    }
}
