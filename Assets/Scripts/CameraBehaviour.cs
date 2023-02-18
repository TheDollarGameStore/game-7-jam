using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Vector3 basePosition;

    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        basePosition = transform.localPosition;
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, basePosition, 10f * Time.deltaTime);

        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 70f, 8f * Time.deltaTime);

        camera.transform.localRotation = Quaternion.Lerp(camera.transform.localRotation, Quaternion.Euler(Vector3.zero), 10f * Time.deltaTime);
    }

    public void Nudge()
    {
        camera.fieldOfView = 72f;
    }
}
