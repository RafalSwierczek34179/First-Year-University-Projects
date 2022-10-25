using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGyroData : MonoBehaviour
{
    [SerializeField] GameObject gyroDataObject;
    private Text gyroDataText;
    private float x, y, z = 0f;
    private string error;
    private string result;
    private void Awake()
    {
        gyroDataText = GetComponent<Text>();
    }
    private void Update()
    {
        ReadGyroData();
        UpdateDisplay();
    }
    private void ReadGyroData()
    {
        x = gyroDataObject.GetComponent<GyroData>().x;
        y = gyroDataObject.GetComponent<GyroData>().y;
        z = gyroDataObject.GetComponent<GyroData>().z;
        error = gyroDataObject.GetComponent<GyroData>().GetError();
        result = gyroDataObject.GetComponent<GyroData>().GetResult();
    }
    private void UpdateDisplay()
    {
        gyroDataText.text = string.Format("X: {0}\nY: {1}\nZ: {2}\n{3}\n{4}", x, y, z, error, result);
    }
}
