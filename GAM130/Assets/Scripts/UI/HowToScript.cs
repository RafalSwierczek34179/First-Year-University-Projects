using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToScript : MonoBehaviour
{
    public GameObject HowToPanel;

    public void OpenHowToPanel()
    {
        HowToPanel.SetActive(true);
    }

    public void CloseHowToPanel()
    {
        HowToPanel.SetActive(false);
    }

    public Text HowPanelText;
    public Text HowPanelDescription;

    public Image HowPanelIcon;

    public void SetHow(HowToScriptableObject newHow)
    {
        HowPanelText.text = newHow.HowToName;
        HowPanelDescription.text = newHow.HowToDescription;
        HowPanelIcon.sprite = newHow.HowToIcon;
    }
}
