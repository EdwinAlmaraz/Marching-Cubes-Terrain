
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using RTL.Atlas;
public class Indicator : MonoBehaviour
{
    public static Indicator instance = null;
    public static string triggerID { get; private set; } = "indicatorbtnpressed";
    public TextMeshProUGUI Instructions { get; private set; } = null;
    public GameObject IndicatorCanvas;
    public GameObject DownArrow;
    public GameObject btn;
    public TextMeshProUGUI BtnText;
    [SerializeField] private TextMeshProUGUI instructions_;

    private void Awake() 
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }        
        else
        {
            instance = this;
            Instructions = instructions_;
            DontDestroyOnLoad(gameObject);        
        }
    }
    private void Start() 
    {
        // gameObject.SetActive(false);
    }

    public void Register(SetIndicatorSequence setIndicatorSequence)
    {
        btn.GetComponent<Button>().onClick.AddListener(() => setIndicatorSequence.OnTrigger(triggerID));
    }

    public void Deregister(SetIndicatorSequence setIndicatorSequence)
    {
        btn.GetComponent<Button>().onClick.RemoveListener(() => setIndicatorSequence.OnTrigger(triggerID));
    }


    public void SetIndicator(SequenceController seq, string msg, GameObject target, float yOffset = 0.45f, bool showarrow = true, bool showbtn = false, string btntxt = "Okay")
    {
        instance.gameObject.SetActive(false);
        this.transform.position = target.transform.position + new Vector3(0, yOffset, 0);
        this.instructions_.text = msg;
        instance.gameObject.SetActive(true);
        if (showarrow)
            this.DownArrow.SetActive(true);
        else
            this.DownArrow.SetActive(false);
        if (showbtn)
        {
            this.BtnText.text = btntxt;
            this.btn.SetActive(true);
            btn.GetComponent<Button>().onClick.AddListener(() => 
            { 
                seq.OnTrigger(triggerID);
                this.gameObject.SetActive(false);            
            });
            Debug.LogError("Must use Triggerstate to trigger button press... and close indicator manually");
        }
        else
        {
            this.btn.SetActive(false);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        instance.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}