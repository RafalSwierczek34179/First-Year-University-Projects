using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class HowToScriptableObject : ScriptableObject
{
    public string HowToName;
    [TextArea]
    public string HowToDescription;
    public Sprite HowToIcon;
}
