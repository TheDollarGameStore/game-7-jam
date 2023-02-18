using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemy : MonoBehaviour
{
    private MeshRenderer mr;
    private Material defaultMaterial;
    [SerializeField] private Material damageMaterial;

    [SerializeField] private int hp;

    [SerializeField] private AudioClip explosion;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        defaultMaterial = mr.material;
    }

    // Update is called once per frame
    public void TakeDamage()
    {
        mr.material = damageMaterial;
        CancelInvoke("ResetMaterial");
        Invoke("ResetMaterial", 0.075f);

        hp -= 1;

        if (hp <= 0)
        {
            PPEffectManager.instance.Flash(20f);
            GameManager.instance.cameraBehaviour.Shake(0.5f);
            SoundManager.instance.PlayRandomized(explosion);
            Destroy(gameObject);
        }
    }

    private void ResetMaterial()
    {
        mr.material = defaultMaterial;
    }
}
