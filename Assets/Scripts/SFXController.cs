using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SFXController : MonoBehaviour
{
    public static SFXController instance;

    [SerializeField] private AudioSource sfxObject;
    [SerializeField] private float volume;

    [SerializeField] private AudioSource musicObject;

    [Header("Music")]
    [SerializeField] private AudioClip idle;
    [SerializeField] private AudioClip chase;
    private bool isFadeOut = true;
    private bool isFadeIn = true;
    private bool isChase = false;

    private bool flashlightOff = false;
    
    
    private List<AudioSource> sounds;
    
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            GameObject music = GameObject.FindWithTag("Music");
            if (music != null)
            {
                musicObject = music.GetComponent<AudioSource>();
            }
            sounds = new List<AudioSource>();
        }
    }

    List<AudioSource> soundsToRemove = new List<AudioSource>();

    void Update()
    {

        foreach (var sound in sounds)
        {
            if (sound == null)
            {
                soundsToRemove.Add(sound);
            }
        }

        // Remove the null sounds after the loop.
        foreach (var soundToRemove in soundsToRemove)
        {
            sounds.Remove(soundToRemove);
        }
    }

    public AudioSource PlaySFX(AudioClip audioClip, Transform pos, float vol)
    {
        AudioSource audioSource = Instantiate(sfxObject, pos.position, Quaternion.identity);
        sounds.Add(audioSource);
        audioSource.clip = audioClip;
        audioSource.volume = vol;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, audioSource.clip.length);
        return audioSource;
    }

    public void StopAll()
    {
        foreach (var sound in sounds)
        {
            sound.Stop();
        }
        musicObject.Stop();
    }

    public void SetChaseMusic()
    {
        isFadeOut = true;
        isFadeIn = true;
        isChase = true;
        musicObject.Stop();
        musicObject.clip = chase;
        musicObject.volume = 1f;
        musicObject.Play();
    }

    public void SetIdleMusic()
    {
        flashlightOff = true;
        StartCoroutine("FadeVolumeOut");
    }

    void SetIdleMusic2()
    {
        StopCoroutine("FadeVolumeOut");
        musicObject.Stop();
        musicObject.clip = idle;
        musicObject.Play();
        musicObject.volume = 0f;
        StartCoroutine("FadeVolumeIn");
        isChase = false;
    }

    public bool IsChaseMusic()
    {
        return isChase;
    }

    IEnumerator FadeVolumeOut()
    {
        while (isFadeOut)
        {
            if (musicObject.volume <= 0)
            {
                isFadeOut = false;
                SetIdleMusic2();
            }
            musicObject.volume -= 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeVolumeIn()
    {
        while (isFadeIn)
        {
            if (musicObject.volume <= 0)
            {
                musicObject.volume += 0.1f;
                yield return new WaitForSeconds(5f);
            }
            if (musicObject.volume >= 0.1f)
            {
                isFadeIn = false;
            }

            musicObject.volume += 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void StopMusic()
    {
        musicObject.Stop();
    }
    
    // public void FlashlightOff()
    // {
    //     musicObject.clip = chase;
    //     musicObject.volume = 0;
    //     musicObject.Play();
    //     StartCoroutine("FadeVolumeInFlashlightOff");
    // }
    //
    // IEnumerator FadeVolumeInFlashlightOff()
    // {
    //     yield return new WaitForSeconds(2f);
    //     while (flashlightOff)
    //     {
    //         if (musicObject.volume >= 1f)
    //         {
    //             flashlightOff = false;
    //         }
    //         musicObject.volume += 0.01f;
    //         yield return new WaitForSeconds(0.1f);
    //     }
    // }
    
    
}