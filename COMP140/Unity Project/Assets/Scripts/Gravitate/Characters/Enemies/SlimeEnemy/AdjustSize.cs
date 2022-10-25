using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustSize : MonoBehaviour
{
    public void AdjustSlimeGFXSize(int size)
    {
        switch (size)
        {
            case 2:
                transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            case 1:
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                break;
            default:
                transform.localScale = new Vector3(2f, 2f, 2f);
                break;
        }
    }
}
