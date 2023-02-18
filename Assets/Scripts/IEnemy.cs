using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemy : MonoBehaviour
{
    private MeshRenderer mr;
    private Material defaultMaterial;
    [SerializeField] private Material damageMaterial;

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
    }

    private void ResetMaterial()
    {
        mr.material = defaultMaterial;
    }
}
