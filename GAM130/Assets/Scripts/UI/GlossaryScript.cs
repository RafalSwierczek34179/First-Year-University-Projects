using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlossaryScript : MonoBehaviour
{
    public GameObject Glossary;

    public void OpenGlossary()
        {
        Glossary.SetActive(true);
        }

    public void CloseGlossary()
    {
        Glossary.SetActive(false);
    }

    public Text GlossaryText;
    public Text GlossaryDescription;

    public Image GlossaryIcon;

    public void SetGlos(GlossaryInfo newGlos)
    {
        GlossaryText.text = newGlos.GlosName;
        GlossaryDescription.text = newGlos.GlosDescription;
        GlossaryIcon.sprite = newGlos.GlosIcon;
    }
}
