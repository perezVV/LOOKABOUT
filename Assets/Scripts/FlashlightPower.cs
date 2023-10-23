using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightPower : MonoBehaviour
{
    [Header("Script Necessities")]
    [SerializeField] private Light2D light;
    [SerializeField] private UnityEngine.UI.Image batteryUI;
    
    [Header("Customize")]
    [SerializeField] private float batteryLifeLength;
    
    [Header("UI Images")] 
    [SerializeField] private Sprite[] batterySprites;
    
    [Header("SFX")]
    [SerializeField] private AudioClip flashlightOnOff;
    [SerializeField] private AudioClip flashlightDead;
    
    // private vars
    private bool isCurrentlyOn;
    private bool hasBatteries;
    
    private float time;
    private int batteryAmt;

    // private bool isLightDying = false;
    // private bool isLightBackOn = false;
    // private bool isLerping = false;
    
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
        batteryUI = GameObject.Find("BatteryUI").GetComponent<UnityEngine.UI.Image>();
        batteryAmt = 5;
        batteryUI.sprite = batterySprites[batteryAmt];
        isCurrentlyOn = true;
        time = batteryLifeLength;
        hasBatteries = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && hasBatteries)
        {
            FlickFlashlight();
        }

        if (isCurrentlyOn)
        {
            BatteryTimer();
        }

        // if (isLightDying || isLightBackOn || isLerping)
        // {
        //     LerpLight();
        // }
    }

    void SFX()
    {
        if (hasBatteries)
        {
            SFXController.instance.PlaySFX(flashlightOnOff, transform, 0.5f);
            return;
        }
        SFXController.instance.PlaySFX(flashlightDead, transform, 0.1f);
    }

    void BatteryTimer()
    {
        if (batteryAmt == 0 && isCurrentlyOn)
        {
            SetFlashlightDead();
        }
        if (time <= 0 && batteryAmt != 0)
        {
            batteryAmt -= 1;
            batteryUI.sprite = batterySprites[batteryAmt];
           // Debug.Log("RESTART! Battery AMT: " + batteryAmt);
            time = batteryLifeLength;
        }

        if (hasBatteries)
        {
            time -= Time.deltaTime;
            Debug.Log(Mathf.Ceil(time));
        }
    }

    void FlickFlashlight()
    {
        isCurrentlyOn = !isCurrentlyOn;
        light.enabled = isCurrentlyOn;
        SFX();
    }

    void SetFlashlightDead()
    {
        isCurrentlyOn = false;
        light.enabled = false;
        // isLightDying = true;
        hasBatteries = false;
        SFX();
    }

    void SetFlashlightAlive()
    {
        isCurrentlyOn = true;
        light.enabled = true;
        // isLightBackOn = true;
        hasBatteries = true;
        time = batteryLifeLength;
    }

    // private float startPoint = 0;
    // private float endPoint = 0;
    // private float smoothTime = 0f;
    //
    // void LerpLight()
    // {
    //
    //     float maxSpeed = 0.5f;
    //     isLerping = true;
    //     
    //     if (isLightDying)
    //     {
    //         startPoint = 0.6f;
    //         endPoint = 1f;
    //         isLightDying = false;
    //     }
    //     else if (isLightBackOn)
    //     {
    //         light.enabled = true;
    //         startPoint = 1f;
    //         endPoint = 0.6f;
    //         isLightBackOn = false;
    //     }
    //
    //     if (isLerping)
    //     {
    //         light.falloffIntensity = Mathf.SmoothDamp(startPoint, endPoint, ref smoothTime, maxSpeed);
    //     }
    //
    //     if (light.falloffIntensity == 0.6f || light.falloffIntensity == 1f)
    //     {
    //         isLerping = false;
    //         if (light.falloffIntensity == 0)
    //         {
    //             light.enabled = false;
    //         }
    //     }
    //     
    // }

    public void PickupBattery()
    {
        batteryAmt += 1;
        batteryUI.sprite = batterySprites[batteryAmt];
        if (!hasBatteries)
        {
            SetFlashlightAlive();
        }
    }

    public bool CanPickupBattery()
    {
        if (batteryAmt < 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
