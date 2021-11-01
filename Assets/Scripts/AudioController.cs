using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioClip defaultMusic;
    [SerializeField] List<AudioSource> audioSources;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddAudioSource(AudioSource newAudioSource)
    {
        audioSources.Add(newAudioSource);

        newAudioSource.clip = defaultMusic;

        ResetMusic();
    }

    void ResetMusic()
    {
        for (int i = 0; i < audioSources.Count; ++i)
        {
            audioSources[i].Stop();
            audioSources[i].Play();
        }
    }
}
