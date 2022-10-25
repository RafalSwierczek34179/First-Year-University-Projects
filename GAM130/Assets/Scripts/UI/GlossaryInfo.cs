using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GlossaryInfo : ScriptableObject
{
    public string GlosName;
    [TextArea]
    public string GlosDescription;
    public Sprite GlosIcon;
}
