using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
