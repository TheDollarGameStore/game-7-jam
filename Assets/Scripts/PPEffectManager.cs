using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PPEffectManager : MonoBehaviour
{
    private PostProcessVolume ppV;
    private Bloom bloomLayer;
    public static PPEffectManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ppV = GetComponent<PostProcessVolume>();
        ppV.profile.TryGetSettings(out bloomLayer);
    }

    // Update is called once per frame
    void Update()
    {
        bloomLayer.intensity.value = Mathf.Lerp(bloomLayer.intensity.value, 0f, 10f * Time.deltaTime);
    }

    public void Flash(float intensity)
    {
        bloomLayer.intensity.value = Mathf.Max(bloomLayer.intensity.value, intensity);
    }
}
