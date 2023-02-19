using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Vector3 basePosition;

    private Camera camera;

    private float shakeIntensity;

    [SerializeField] private Transform gameOverCameraRail;
    // Start is called before the first frame update
    void Start()
    {
        basePosition = transform.localPosition;
        camera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        transform.localPosition += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f) * shakeIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameOver)
        {
            transform.position = Vector3.Lerp(transform.position, gameOverCameraRail.position, 5f * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, gameOverCameraRail.rotation, 3f * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, basePosition, 10f * Time.deltaTime);

            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 70f, 8f * Time.deltaTime);

            camera.transform.localRotation = Quaternion.Lerp(camera.transform.localRotation, Quaternion.Euler(Vector3.zero), 10f * Time.deltaTime);

            shakeIntensity = Mathf.Lerp(shakeIntensity, 0f, 10f * Time.deltaTime);
        }
    }

    public void Nudge()
    {
        camera.fieldOfView = 72f;
    }

    public void Shake(float intensity)
    {
        shakeIntensity = Mathf.Max(shakeIntensity, intensity);
    }
}
