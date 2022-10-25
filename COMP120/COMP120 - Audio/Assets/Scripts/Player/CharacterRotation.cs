using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    float cameraYAxisRotation;

    private void FixedUpdate()
    {
        cameraYAxisRotation = GameObject.Find("Main Camera").gameObject.transform.rotation.eulerAngles.y;
        this.transform.localRotation = Quaternion.Euler(0f, cameraYAxisRotation, 0f);
    }
}
