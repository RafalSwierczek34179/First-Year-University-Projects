using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTilemapRotation : MonoBehaviour
{
    public void RotateWorldTilemap(Vector3 cameraPosition, float amountToRotate)
    {
        this.transform.RotateAround(cameraPosition, Vector3.forward, amountToRotate);
    }
}
