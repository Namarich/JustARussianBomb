using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public List<AudioClip> sounds;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(int index)
    {
        audioSource.clip = sounds[index];
        audioSource.Play();
    }
}
