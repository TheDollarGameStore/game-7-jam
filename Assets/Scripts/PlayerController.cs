using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    bool isStrafing = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            isStrafing = true;
        }
        else
        {
            isStrafing = false;
        }

        if (isStrafing)
        {
            float strafeX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float strafeZ = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            Vector3 strafeDirection = new Vector3(strafeX, 0, strafeZ);
            Vector3 moveDirection = transform.TransformDirection(strafeDirection);

            transform.position += moveDirection;
        }
        else
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 5f * Time.deltaTime;
            float moveZ = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            transform.Rotate(Vector3.up * mouseX);

            Vector3 moveDirection = transform.forward * moveZ;

            transform.position += moveDirection;
        }

        //Clamp player
        float clampedX = transform.position.x;
        float clampedZ = transform.position.z;

        if (clampedX < -20f)
        {
            clampedX = -20f;
        }

        if (clampedX > 20f)
        {
            clampedX = 20f;
        }

        if (clampedZ < -20f)
        {
            clampedZ = -20f;
        }

        if (clampedZ > 20f)
        {
            clampedZ = 20f;
        }

        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }
}
