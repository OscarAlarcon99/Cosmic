using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineControllerCamera : MonoBehaviour
{
    public Cinemachine.CinemachineFreeLook freeLook;
    public  float sencivilidad;
    public float lookspeed;

    void Start()
    {
        freeLook = (Cinemachine.CinemachineFreeLook)FindObjectOfType(typeof(Cinemachine.CinemachineFreeLook));
    }

    public void FixedUpdateCameraController()
    {
        if (freeLook.gameObject.activeInHierarchy)
        {
            InputCamera();
        }
    }

    void InputCamera()
    {
        Vector2 delta = Player.Instance.input.deltaLook;

        freeLook.m_XAxis.Value += delta.x * sencivilidad * lookspeed * Time.deltaTime;
        freeLook.m_YAxis.Value += delta.y * lookspeed * Time.deltaTime;
    }
}
