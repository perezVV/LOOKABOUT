using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private int detectionRange;
    [SerializeField] private float velocity;
    [SerializeField] private int maxStamina;

    [SerializeField] private GameObject windowSwitcher;
    private GameWindowSwitch screen;
    
    [Header("SFX")] 
    [SerializeField] private AudioClip monsterInhale;
    [SerializeField] private AudioClip monsterExhale;
    [SerializeField] private AudioClip monsterStep;
    [SerializeField] private AudioClip monsterCry;

    private Transform target;
    private bool drainingStamina;
    private int currentStamina;
    private Rigidbody2D rb;
    private Vector2 move;
    private SpawnController spawner;

    private bool isSleeping;
    private bool isChasing;

    private bool playCryOnce = true;
    private bool startStepSfx = true;
    private bool isWalking = false;
    private bool resetMusic = true;
    
    // Start is called before the first frame update
    void Start()
    {
        windowSwitcher = GameObject.FindGameObjectWithTag("WindowSwitch");
        if (windowSwitcher != null)
        {
            screen = windowSwitcher.GetComponent<GameWindowSwitch>();
        }
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnController>();
        currentStamina = maxStamina;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        isSleeping = true;
        StartCoroutine("MonsterSleepSFX");
    }

    void Update()
    {
        if (!isChasing && rb.velocity == Vector2.zero)
        {
            isSleeping = true;
            if (SFXController.instance.IsChaseMusic() && resetMusic)
            {
                SFXController.instance.SetIdleMusic();
                resetMusic = false;
            }
        }
        else
        {
            isSleeping = false;
            resetMusic = true;
        }

        if (rb.velocity != Vector2.zero)
        {
            isWalking = true;
            if (startStepSfx)
            {
                StartCoroutine("MonsterStepSFX");
                startStepSfx = false;
            }
        }
        else
        {
            startStepSfx = true;
            isWalking = false;
        }
    }

    IEnumerator MonsterSleepSFX()
    {
        while (isSleeping)
        {
            SFXController.instance.PlaySFX(monsterInhale, transform, 0.1f);
            yield return new WaitForSeconds(monsterInhale.length + 1.5f);
            SFXController.instance.PlaySFX(monsterExhale, transform, 0.1f);
            yield return new WaitForSeconds(monsterExhale.length + 1.5f);
        }
    }

    IEnumerator MonsterStepSFX()
    {
        while (isWalking)
        {
            SFXController.instance.PlaySFX(monsterStep, transform, 0.5f);
            yield return new WaitForSeconds(monsterStep.length + 0.3f);
        }
    }

    void StartChasingSFX()
    {
        if (playCryOnce)
        {
            StopCoroutine("MonsterSleepSFX");
            SFXController.instance.PlaySFX(monsterCry, transform, 0.5f);
            SFXController.instance.SetChaseMusic();
            playCryOnce = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(target.position, transform.position) <= detectionRange)
        {
            StartChasingSFX();
            isChasing = true;
        }
        if (isChasing)
        {
            // move towards player
            move = (target.position - transform.position);
            
            rb.velocity = move.normalized * velocity;
            StartCoroutine("DrainStamina");
        }
        else
        {
            rb.velocity = Vector2.zero;
            currentStamina = maxStamina;
        }
    }

    public bool GetIsChasing()
    {
        return isChasing;
    }

    private void RespawnMonster()
    {
        playCryOnce = true;
        isChasing = false;
        Vector2 pos = spawner.AssignRandomLocation();
        currentStamina = maxStamina;
        transform.position = pos;
        spawner.UnassignLocation(pos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Game Over :(");
            // TODO implement game over screen from here
            screen.Lose();
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Flashlight"))
        {
            isChasing = true;
            StartChasingSFX();
            Debug.Log(("flashlight hit monster"));
        }
    }

    private IEnumerator DrainStamina()
    {
        if (!drainingStamina)
        {
            drainingStamina = true;
            yield return new WaitForSeconds(1);
            currentStamina = Mathf.Clamp(currentStamina - 1, 0, maxStamina);
            if (currentStamina == 0)
            {
                RespawnMonster(); // "respawn" the monster
            }
            drainingStamina = false;
        }
    }
    
    
}
