using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [HideInInspector] public Vector3 direction;
    [SerializeField] private AudioClip collectSound;

    bool destroying;

    private void Start()
    {
        if (Vector3.Distance(new Vector3(0f, 1.5f, 0f), transform.position) >= 18f)
        {
            Vector3 directionVector = transform.position - new Vector3(0f, 1.5f, 0f);

            Vector3 maxDistance = new Vector3(directionVector.x, 1.5f, directionVector.z).normalized * 18f;

            transform.position = maxDistance;
        }

        Invoke("DestroySelf", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        direction = Vector3.Lerp(direction, Vector3.zero, 5f * Time.deltaTime);
        transform.position += direction * Time.deltaTime;

        if (destroying)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 5f * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(new Vector3(GameManager.instance.playerObject.transform.position.x, 0f, GameManager.instance.playerObject.transform.position.z), new Vector3(transform.position.x, 0f, transform.position.z)) <= 2f)
        {
            GameManager.instance.AddMultiplier(1);
            SoundManager.instance.PlayRandomized(collectSound);
            Destroy(gameObject);
        }
    }

    void DestroySelf()
    {
        destroying = true;
        Invoke("DestroyForReal", 0.25f);
    }

    void DestroyForReal()
    {
        Destroy(gameObject);
    }
}
