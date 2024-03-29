using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IEnemy : MonoBehaviour
{
    private MeshRenderer mr;
    private Material defaultMaterial;
    [SerializeField] private Material damageMaterial;

    [SerializeField] private float hp;

    [SerializeField] private AudioClip explosion;

    [SerializeField] private GameObject deathParticles;

    [SerializeField] private int scoreValue;

    [SerializeField] private GameObject starPrefab;

    private Text hpText;

    [SerializeField] private GameObject hpCounterPrefab;

    private GameObject hpCounter;

    [SerializeField] private float hpCounterYOfffset;

    // Start is called before the first frame update
    public void Start()
    {
        mr = GetComponent<MeshRenderer>();
        defaultMaterial = mr.material;

        hp *= GameManager.instance.enemyLevel;

        if (hpCounterPrefab != null)
        {
            hpCounter = Instantiate(hpCounterPrefab, transform.position, Quaternion.identity);
            hpText = hpCounter.GetComponentInChildren<Text>();
            hpText.text = Mathf.CeilToInt(hp).ToString();
        }
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        mr.material = damageMaterial;
        CancelInvoke("ResetMaterial");
        Invoke("ResetMaterial", 0.075f);

        hp -= damage * GameManager.instance.gunLevel;

        hp = Mathf.Max(hp, 0);

        if (hpText != null)
        {
            hpText.text = Mathf.CeilToInt(hp).ToString();
        }
    }

    private void Die()
    {
        PPEffectManager.instance.Flash(20f);
        GameManager.instance.cameraBehaviour.Shake(0.5f);
        SoundManager.instance.PlayRandomized(explosion);
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        GameManager.instance.AddScore(scoreValue);

        int amount = Random.Range(1, 6);

        Random.InitState(System.DateTime.Now.Millisecond);

        for (int i = 0; i < amount; i++)
        {
            Instantiate(starPrefab, new Vector3(transform.position.x, 1.75f, transform.position.z), Quaternion.identity).GetComponent<Star>().direction = new Vector3(Random.Range(-1, 1f), 0f, Random.Range(-1, 1f)).normalized * 10f;
        }

        Destroy(hpCounter);
        Destroy(gameObject);
    }

    private void ResetMaterial()
    {
        mr.material = defaultMaterial;
    }

    public void Update()
    {
        if (hpCounter != null)
        {
            hpCounter.transform.position = transform.position + (Vector3.up * hpCounterYOfffset);
        }

        if (Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), GameManager.instance.playerObject.transform.position) <= 1.75f)
        {
            GameManager.instance.playerObject.GetComponent<PlayerBehaviour>().Die();
        }

        if (hp <= 0)
        {
            Die();
        }
    }
}
