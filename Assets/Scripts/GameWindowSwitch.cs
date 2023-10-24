using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWindowSwitch : MonoBehaviour
{

    [Header("Main Menu")] 
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject helpScreen;

    [Header("Game Scene")]
    [SerializeField] private GameObject winObject;
    [SerializeField] private GameObject loseObject;

    [SerializeField] private UnityEngine.UI.Image winImg;
    [SerializeField] private UnityEngine.UI.Image loseImg;

    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI loseText;

    [Header("SFX")] 
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip lose;
    [SerializeField] private AudioClip select;
    
    // Start is called before the first frame update
    void Start()
    {
        mainScreen = GameObject.FindWithTag("MenuScreen");
        helpScreen = GameObject.FindWithTag("HelpScreen");
        if (mainScreen != null && helpScreen != null)
        {
            helpScreen.SetActive(false);
        }
        winObject = GameObject.FindWithTag("WinScreen");
        loseObject = GameObject.FindWithTag("LoseScreen");
        if (winObject != null && loseObject != null)
        {
            winImg = winObject.GetComponent<UnityEngine.UI.Image>();
            loseImg = loseObject.GetComponent<UnityEngine.UI.Image>();
            winText = winObject.GetComponentInChildren<TextMeshProUGUI>();
            loseText = loseObject.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private bool startWinImg = false;
    private bool startLoseImg = false;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Win();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Lose();
        }
        
        if (startWinImg)
        {
            WinGameGraphic();
        }

        if (startLoseImg)
        {
            LoseGameGraphics();
        }
    }

    public void Win()
    {
        startWinImg = true;
        StartCoroutine("WinGame");
    }

    public void Lose()
    {
        startLoseImg = true;
        StartCoroutine("LoseGame");
    }

    private float maxSpeedWin = 0;
    void WinGameGraphic()
    {
        float alpha = Mathf.SmoothDamp(0f, 255f, ref maxSpeedWin, 1f);
        winImg.color = new Color(winImg.color.r, winImg.color.g, winImg.color.b, alpha);
        winText.color = new Color(winText.color.r, winText.color.g, winText.color.b, alpha);
        if (winImg.color.a >= 1)
        {
            startWinImg = false;
        }
    }

    IEnumerator WinGame()
    {
        SFXController.instance.StopAll();
        SFXController.instance.PlaySFX(win, Camera.main.transform, 0.5f);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Scenes/MENU");
    }

    private float maxSpeedLose = 0f;
    void LoseGameGraphics()
    {
        float alpha = Mathf.SmoothDamp(0f, 255f, ref maxSpeedLose, 0.2f);
        loseImg.color = new Color(loseImg.color.r, loseImg.color.g, loseImg.color.b, alpha);
        loseText.color = new Color(loseText.color.r, loseText.color.g, loseText.color.b, alpha);
        if (loseImg.color.a >= 1)
        {
            startLoseImg = false;
        }
    }

    IEnumerator LoseGame()
    {
        SFXController.instance.StopAll();
        SFXController.instance.PlaySFX(lose, Camera.main.transform, 0.5F);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Scenes/MENU");
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Scenes/map");
    }

    public void HelpButton()
    {
        SFXController.instance.PlaySFX(select, Camera.main.transform, 0.5f);
        mainScreen.SetActive(false);
        helpScreen.SetActive(true);
    }

    public void BackButton()
    {
        SFXController.instance.PlaySFX(select, Camera.main.transform, 0.5f);
        helpScreen.SetActive(false);
        mainScreen.SetActive(true);
    }
    
}
