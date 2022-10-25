using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    // 90 degrees per second
    private const float rotationPerSecond = 90f;
    private Vector3 zOffset = new Vector3(0f, 0f, -10f);
    [Header("Game Objects")]
    [SerializeField] GameObject worldTilemap;
    [SerializeField] GameObject player;
    private void Update()
    {
        FollowPlayer(player.transform.position, zOffset);
        RotateCameraAndWorldTilemap(rotationPerSecond);
    }
    private void FollowPlayer(Vector3 playersPosition, Vector3 zOffset)
    {
        this.transform.position = playersPosition + zOffset;
    }
    private void RotateCameraAndWorldTilemap(float rotationPerSecond)
    {
        float amountToRotate = GrabRotationInput(rotationPerSecond) * Time.deltaTime;
        // Rotates camera
        this.transform.RotateAround(this.transform.position, Vector3.forward, amountToRotate);
        // Rotates world tilemap by the same amount
        worldTilemap.GetComponent<WorldTilemapRotation>().RotateWorldTilemap(this.transform.position, amountToRotate);
    }
    private float GrabRotationInput(float rotationPerSecond)
    {
        if (Input.GetButton("Horizontal"))
        {
            return rotationPerSecond * Input.GetAxisRaw("Horizontal");
        }
        else
        {
            return 0f;
        }
    }
}
