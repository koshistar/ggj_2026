using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : PersistentSingleton<AudioSystem>
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void SetListenerVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public float GetMusicVolume()
    {
        return musicSource.volume;
    }

    public float GetSfxVolume()
    {
        return sfxSource.volume;
    }
    public float GetListenerVolume()
    {
        return AudioListener.volume;
    }
}
