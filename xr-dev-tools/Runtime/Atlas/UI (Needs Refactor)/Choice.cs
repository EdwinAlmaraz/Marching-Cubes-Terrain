using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RTL.Atlas;
public class Choice : MonoBehaviour
{
    public static Choice instance = null;
    public static string[] triggerID;

    public TextMeshProUGUI Prompt { get; private set; } = null;
    public GameObject ChoiceUI;
    public GameObject[] Buttons;
    public TextMeshProUGUI[] BtnsText;

    [SerializeField] private TextMeshProUGUI prompt;

    private void Awake()
    {
        instance = this;
        Prompt = prompt;
    }
    private void Start()
    {
        //TurnOff();
    }
    public void SetChoiceText(string[] txt)
    {
        if (txt.Length > Buttons.Length)
        {
            Debug.LogError("Not enough ui buttons.  Use different UI card.");
        }

        for (int i = 0; i < txt.Length; i++)
        {
            Buttons[i].SetActive(true);
            BtnsText[i].text = txt[i];
        }
    }
    public string[] Register(SequenceController sequenceController, int choiceCount)
    {
        string[] result = new string[choiceCount];
        for(int i = 0; i < choiceCount;)
        {
            string tmp = "option" + (i + 1);
            Buttons[i].GetComponent<Button>().onClick.AddListener(() => sequenceController.OnTrigger(tmp));
            result[i++] = tmp;
        }

        return result;
    }
    public void Deregister()
    {
        int i = 0;
        foreach (GameObject button in Buttons)
        {
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            i++;
        }
    }

    public void TurnOff()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].SetActive(false);
        }
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        TurnOff();
    }

}


