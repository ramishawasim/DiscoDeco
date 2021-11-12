using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MouseLock : MonoBehaviour, AxisState.IInputAxisProvider
{
    // private CinemachineFreeLook _cinemachineFreeLook;
    // private string _inputAxisNameX;
    // private string _inputAxisNameY;
 
    // void Awake()
    // {
    //     _cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
    //     _inputAxisNameX = _cinemachineFreeLook.m_XAxis.m_InputAxisName;
    //     _inputAxisNameY = _cinemachineFreeLook.m_YAxis.m_InputAxisName;
    // }
 
    // void Update()
    // {
    //     Cursor.lockState = CursorLockMode.Locked;
    //     Cursor.lockState = CursorLockMode.Confined;
    //     _cinemachineFreeLook.m_XAxis.m_InputAxisName = Input.GetMouseButton(2) ? _inputAxisNameX : "";
    //     _cinemachineFreeLook.m_YAxis.m_InputAxisName = Input.GetMouseButton(2) ? _inputAxisNameY : "";
    // }
    public string HorizontalInput = "Mouse X";
    public string VerticalInput = "Mouse Y";
 
    public float GetAxisValue(int axis)
    {
        // No input unless right mouse is down
        if (!Input.GetMouseButton(2))
            return 0;
 
        switch (axis)
        {
            case 0: return Input.GetAxis(HorizontalInput);
            case 1: return Input.GetAxis(VerticalInput);
            default: return 0;
        }
    }
}
