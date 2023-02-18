using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;

    private bool isStrafing = false;

    [SerializeField] private GameObject bulletPrefab;



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
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
            float strafeX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float strafeZ = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            //Tilt Camera
            GameManager.instance.cameraBehaviour.gameObject.transform.localRotation = Quaternion.Euler(GameManager.instance.cameraBehaviour.transform.localRotation.eulerAngles + new Vector3(0f, 0f, strafeX / -2f));

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
        ClampPlayer();
    }

    void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();

        bullet.bulletDir = transform.rotation.eulerAngles;

        GameManager.instance.cameraBehaviour.Nudge();
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
