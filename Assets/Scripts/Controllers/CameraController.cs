using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float pitch = 2f;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float yawSpeed = 100f;
    private float currentZoom = 10f;
    private float currentYaw = 0f;
    private void Start()
    {
    }
    private void Update()
    {
        //滚轮缩放
       currentZoom-= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }
    void LateUpdate()
    {
        //摄像机跟随和看向角色
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position+Vector3.up*pitch);

        //
        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
}
