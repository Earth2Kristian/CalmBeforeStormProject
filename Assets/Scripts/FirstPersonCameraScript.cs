using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCameraScript : MonoBehaviour
{
    public float mouseSensitivity = 50f;
    public float gameSensitivity = 50f;
    public Transform playerBody;

    private Vector2 rotationInput;

    void Start()
    {
        Cursor.visible = false  ;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        //if (GameManager.Instance.ableToClick == true)
        //{
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
        //}
        //if (GameManager.Instance.ableToClick == false)
        //{
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        //}

        if (Gamepad.current != null)
        {
            // If Gamepad is being used
            rotationInput = Gamepad.current.rightStick.ReadValue() * gameSensitivity * Time.deltaTime;
        }
        else if (Gamepad.current == null)
        {
            // If Mouse is being used and not the gamepad
            rotationInput = Mouse.current.delta.ReadValue() * mouseSensitivity * Time.deltaTime;
        }


        playerBody.Rotate(Vector3.up * rotationInput.x);

        float xRotation = transform.localRotation.eulerAngles.x;
        xRotation -= rotationInput.y;
        xRotation = Mathf.Clamp(xRotation, -360f, 360f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
