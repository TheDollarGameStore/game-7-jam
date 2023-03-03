using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;

    [SerializeField] private float maxPlayerMovementSpeed;

    private bool isStrafing = false;

    private int phase = 1;

    [SerializeField] private List<Sprite> phaseSprites;

    [SerializeField] private Image tutorialImage;

    private float phaseProgress;

    [SerializeField] private Image progressBar;

    [SerializeField] private GameObject standardBulletPrefab;

    private bool loaded = true;

    [SerializeField] private AudioClip standardSound;

    private bool tutorialFinished;

    private float configuredSensitivity;

    [SerializeField] private Slider slider;

    [SerializeField] private GameObject sensitivityIndicator;


    void Shoot()
    {
        loaded = false;
        Bullet bulletComponent;
        bulletComponent = Instantiate(standardBulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        bulletComponent.bulletDir = transform.rotation.eulerAngles;
        Invoke("Reload", 0.2f);
        SoundManager.instance.PlayRandomized(standardSound);
        GameManager.instance.cameraBehaviour.Nudge();
        phaseProgress += 10;
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

    void Start()
    {
        configuredSensitivity = PlayerPrefs.GetFloat("ConfiguredSensitivity", 0.25f);
        phase = 1;
        Cursor.lockState = CursorLockMode.Locked;
        slider.value = configuredSensitivity;
    }

    void IncreasePhase()
    {
        if (phase != 5)
        {
            phase++;
            tutorialImage.sprite = phaseSprites[phase - 1];
            progressBar.fillAmount = 0f;
            phaseProgress = 0f;

            if (phase == 5)
            {
                sensitivityIndicator.SetActive(true);
            }
        }
        else if (!tutorialFinished)
        {
            tutorialFinished = true;
            Invoke("GoToMenu", 2f);
        }
    }

    void Update()
    {
        if (phase == 5 && Input.mouseScrollDelta.y != 0)
        {
            phaseProgress += Mathf.Abs(Input.mouseScrollDelta.y);
            ModifySensitivity(Input.mouseScrollDelta.y);
        }

        if (phase == 4 && Input.GetMouseButton(0) && loaded)
        {
            Shoot();
        }

        if (phase >= 3)
        {
            if (Input.GetMouseButton(1))
            {
                isStrafing = true;
            }
            else
            {
                isStrafing = false;
            }
        }
        
        if (isStrafing)
        {
            float strafeX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * configuredSensitivity;
            float strafeZ = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * configuredSensitivity;

            //Tilt Camera
            GameManager.instance.cameraBehaviour.gameObject.transform.localRotation = Quaternion.Euler(GameManager.instance.cameraBehaviour.transform.localRotation.eulerAngles + new Vector3(0f, 0f, strafeX / -2f));

            Vector3 strafeDirection = new Vector3(strafeX, 0, strafeZ);
            Vector3 moveDirection = transform.TransformDirection(strafeDirection);

            moveDirection = ClampMoveDirection(moveDirection);

            transform.position += moveDirection;
            
            if (phase == 3)
            {
                phaseProgress += moveDirection.magnitude / 2f;
            }
        }
        else
        {
            if (phase == 3)
            {
                return;
            }
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 5f * Time.deltaTime * configuredSensitivity;
            float moveZ = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * configuredSensitivity;

            if (phase != 2)
            {
                transform.Rotate(Vector3.up * mouseX);
            }

            Vector3 moveDirection = transform.forward * moveZ;

            moveDirection = ClampMoveDirection(moveDirection);

            if (phase != 1)
            {
                transform.position += moveDirection;
            }

            if (phase == 2)
            {
                phaseProgress += moveDirection.magnitude / 2f;
            }

            if (phase == 1)
            {
                phaseProgress += Mathf.Abs(mouseX) / 8f;
            }
        }

        progressBar.fillAmount = phaseProgress / 100f;

        if (phaseProgress >= 100f)
        {
            IncreasePhase();
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

    void Reload()
    {
        loaded = true;
    }

    void GoToMenu()
    {
        Transitioner.Instance.TransitionToScene("Menu");
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
