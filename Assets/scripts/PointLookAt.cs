using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLookAt : MonoBehaviour
{
    public Transform target;
    private Camera camera;

    public float offsetX = 0;
    public float offsetY = 0;
    public float offsetZ = 0;

    void Start()
    {
        camera = Camera.main;
    }

   
    void Update()
    {
        Vector3 screenPos = camera.WorldToScreenPoint(new Vector3(target.position.x + offsetX, target.position.y + offsetY, target.position.z + offsetZ));
        transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
    }
}
