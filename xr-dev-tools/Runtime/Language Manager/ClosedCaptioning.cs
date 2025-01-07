using UnityEngine;
using TMPro;

public class ClosedCaptioning : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    public static Canvas canvas { get; private set; }

    [SerializeField] private TextMeshProUGUI _text;
    public static TextMeshProUGUI text { get; private set; }


    private void Awake()
    {
        canvas = _canvas;
        text = _text;
    }

	private void Reset()
	{
		_canvas = GetComponentInChildren<Canvas>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
	}

    private void OnEnable()
    {
        LanguageManager.instance.toggleClosedCaptioning += ToggleClosedCaptioning;

    }

	private void OnDisable()
	{
		LanguageManager.instance.toggleClosedCaptioning -= ToggleClosedCaptioning;
	}

	public void ToggleClosedCaptioning(bool enabled)
    {
        canvas.enabled = enabled;
    }
}