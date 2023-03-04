using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;

    [SerializeField] private float maxPlayerMovementSpeed;

    private bool isStrafing = false;

    private float configuredSensitivity;

    [SerializeField] private Slider slider;



    void Start()
    {
        configuredSensitivity = PlayerPrefs.GetFloat("ConfiguredSensitivity", 0.25f);
        Cursor.lockState = CursorLockMode.Locked;
        slider.value = configuredSensitivity;
    }

    void ModifySensitivity(float value)
    {
        value /= 50f;

        configuredSensitivity += value;

        if (configuredSensitivity < 0.05f)
        {
            configuredSensitivity = 0.05f;
        }

        if (configuredSensitivity > 1f)
        {
            configuredSensitivity = 1f;
        }

        PlayerPrefs.SetFloat("ConfiguredSensitivity", configuredSensitivity);
        slider.value = configuredSensitivity;
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            ModifySensitivity(Input.mouseScrollDelta.y);
        }

        if (GameManager.instance.gameOver)
        {
            return;
        }

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
            float strafeX = Input.GetAxis("Mouse X") * mouseSensitivity * configuredSensitivity;
            float strafeZ = Input.GetAxis("Mouse Y") * mouseSensitivity * configuredSensitivity;

            //Tilt Camera
            GameManager.instance.cameraBehaviour.gameObject.transform.localRotation = Quaternion.Euler((GameManager.instance.cameraBehaviour.transform.localRotation.eulerAngles + new Vector3(0f, 0f, strafeX * -1f * Time.deltaTime)));

            Vector3 strafeDirection = new Vector3(strafeX, 0, strafeZ);
            Vector3 moveDirection = transform.TransformDirection(strafeDirection);

            moveDirection = ClampMoveDirection(moveDirection);

            transform.position += moveDirection * Time.deltaTime;
        }
        else
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 5f * configuredSensitivity;
            float moveZ = Input.GetAxis("Mouse Y") * mouseSensitivity * configuredSensitivity;

            transform.Rotate(Vector3.up * mouseX * Time.deltaTime);

            Vector3 moveDirection = transform.forward * moveZ;

            moveDirection = ClampMoveDirection(moveDirection);

            transform.position += moveDirection * Time.deltaTime;
        }

        //Clamp player
        ClampPlayer();
    }

    Vector3 ClampMoveDirection(Vector3 moveDirection)
    {
        if (moveDirection.magnitude >= maxPlayerMovementSpeed)
        {
            return moveDirection.normalized * maxPlayerMovementSpeed;
        }

        return moveDirection;
    }
    

    void ClampPlayer()
    {
        if (Vector3.Distance(new Vector3(0f, 1.5f, 0f), transform.position) >= 20f)
        {
            Vector3 directionVector = transform.position - new Vector3(0f, 1.5f, 0f);

            Vector3 maxDistance = new Vector3(directionVector.x, 1.5f, directionVector.z).normalized * 20f;

            transform.position = maxDistance;
        }
    }
}
