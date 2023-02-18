using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] audioSources;

    public static SoundManager instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    public void PlayNormal(AudioClip clip)
    {
        audioSources[0].PlayOneShot(clip);
    }

    public void PlayRandomized(AudioClip clip)
    {
        audioSources[1].pitch = Random.Range(0.9f, 1.1f);
        audioSources[1].PlayOneShot(clip);
    }
}
