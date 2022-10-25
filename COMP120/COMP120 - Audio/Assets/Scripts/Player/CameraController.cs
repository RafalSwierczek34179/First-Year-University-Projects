using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    GameObject player;

    float mouseX;
    float mouseY;

    float yAxisRotation = 0f;
    float xAxisRotation = 0f;

    [SerializeField]
    float mouseSensitivity = 1f;

    private void Start()
    {
        player = GameObject.Find("Player").gameObject;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        UpdateCameraRotation();
        BindCameraToPlayer();
    }

    // Modified verion of Brackeys camera controller from https://www.youtube.com/watch?v=_QajrabyTJc
    void UpdateCameraRotation()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yAxisRotation += mouseX;
        xAxisRotation = Mathf.Clamp(xAxisRotation - mouseY, -90f, 90f);

        this.transform.localRotation = Quaternion.Euler(xAxisRotation, yAxisRotation, 0f);
    }
    // Didn't want to parent the camera to the player so that the camera can rotate freely relative to the world space
    void BindCameraToPlayer()
    {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.8f, player.transform.position.z);
    }
}
