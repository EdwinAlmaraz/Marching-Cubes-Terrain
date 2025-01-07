using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class RenderOnTop : MonoBehaviour
{
    public Image[] arrImages;
    public TextMeshProUGUI[] arrText;
    private UnityEngine.Rendering.CompareFunction comparison = UnityEngine.Rendering.CompareFunction.Always;
    private Material existingGlobalMat;
    private Material updatedMaterial;

    public List<GameObject> Objects;

    public List<Image> Images;
    public List<TextMeshProUGUI> Text;

    // Start is called before the first frame update
    void Awake()
    {
        arrImages = GetComponentsInChildren<Image>();
        arrText = GetComponentsInChildren<TextMeshProUGUI>();

        foreach (Image i in arrImages)
        {
            Images.Add(i);
        }
        foreach (TextMeshProUGUI t in arrText)
        {
            Text.Add(t);
        }
        BringUIToFront();
    }


    void BringUIToFront()
    {
        foreach (Image image in Images)
        {
            existingGlobalMat = image.materialForRendering;
            updatedMaterial = new Material(existingGlobalMat);
            updatedMaterial.SetInt("unity_GUIZTestMode", (int)comparison);
            image.material = updatedMaterial;
        }

        foreach (TextMeshProUGUI text in Text)
        {
            text.isOverlay = true;
        }   
        TurnOffElements();
    }

    public void TurnOffElements()
    {
        foreach (GameObject myobject in Objects){
            myobject.SetActive(false);
        }

    }

}
