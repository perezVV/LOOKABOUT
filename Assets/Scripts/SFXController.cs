using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SFXController : MonoBehaviour
{
    public static SFXController instance;

    [SerializeField] private AudioSource sfxObject;
    [SerializeField] private AudioSource monsterSfxObject;
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

        sounds.RemoveAll(s => s == null); 

        // // Remove the null sounds after the loop.
        // foreach (var soundToRemove in soundsToRemove)
        // {
        //     sounds.Remove(soundToRemove);
        // }
    }

    public AudioSource PlaySFX(AudioClip audioClip, Transform pos, float vol)
    {
        AudioSource audioSource;
        if (audioClip.name == "monster_inhale_alt" || audioClip.name == "monster_exhale_alt")
        {
            audioSource = Instantiate(monsterSfxObject, pos.position, Quaternion.identity);
        }
        else
        {
            audioSource = Instantiate(sfxObject, pos.position, Quaternion.identity);
        }
        sounds.Add(audioSource);
        audioSource.clip = audioClip;
        audioSource.volume = vol;
        audioSource.Play();
        if(audioSource.gameObject != null) 
        {
            float clipLength = audioSource.clip.length; 
            Destroy(audioSource.gameObject, audioSource.clip.length);
        }
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
        if (flashlightOff)
        {
            Debug.Log("music sequence already playing :( cant go to chase");
            return;
        }
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
        if (flashlightOff)
        {
            Debug.Log("music sequence already playing :( cant go to idle");
            return;
        }
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

    public void SetChaseBool()
    {
        isChase = true;
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

    public void FlashlightOn()
    {
        if (isChase)
        {
            Debug.Log("music sequence already happening :(");
            return;
        }
        Debug.Log("flashlight off thing stopped");
        flashlightOff = false;
        StopCoroutine("FadeVolumeInFlashlightOff");
        musicObject.clip = idle;
        musicObject.Stop();
    }

    IEnumerator FadeVolumeInFlashlightOn()
    {
        Debug.Log("waiting to start idle music...");
        yield return new WaitForSeconds(5.0f);
        Debug.Log("starting idle music again");
        musicObject.Play();
        musicObject.volume = 0.1f;
    }
    
    public void FlashlightOff()
    {
        musicObject.clip = chase;
        musicObject.volume = 0;
        musicObject.Play();
        flashlightOff = true;
        StartCoroutine("FadeVolumeInFlashlightOff");
    }
    
    IEnumerator FadeVolumeInFlashlightOff()
    {
        Debug.Log("flashlight off thing begun");
        yield return new WaitForSeconds(2f);
        while (flashlightOff)
        {
            Debug.Log("fading in music...");
            if (musicObject.volume >= 1f)
            {
                flashlightOff = false;
            }
            musicObject.volume += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    
}