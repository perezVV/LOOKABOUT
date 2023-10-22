using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float velocity;

    [SerializeField] private Animator anim;
    private SpriteRenderer spr;

    private Vector2 input;
    private bool isWalking;
    private bool startSfx;

    [Header("GameObjects")]
    [SerializeField] private GameObject flashlight;

    private bool flashlightEnabled;

    [Header("SFX")] 
    [SerializeField] private AudioClip footstep;
    [SerializeField] private AudioClip flashOnOff;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        flashlight = GameObject.Find("Flashlight");
        startSfx = true;
        flashlightEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlightEnabled = !flashlightEnabled;
            flashlight.SetActive(flashlightEnabled);
        }
        
        Animation();
        SFX();
    }

    void SFX()
    {
        if (input != Vector2.zero)
        {
            isWalking = true;
            if (startSfx)
            {
                StartCoroutine("FootstepSFX");
                startSfx = false;
            }
            
        }
        else
        {
            isWalking = false;
            startSfx = true;
            StopCoroutine("FootstepSFX");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            SFXController.instance.PlaySFX(flashOnOff, transform, 0.5f);
        }
    }

    private IEnumerator FootstepSFX()
    {
        while (isWalking)
        {
            yield return new WaitForSeconds(0.1f);
            // Debug.Log("test");
            AudioSource footstepSfx = SFXController.instance.PlaySFX(footstep, transform, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Animation()
    {
        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
        
        if (input.x > 0)
        {
            spr.flipX = false;
            anim.Play("walk_horiz");
        }
        else if (input.x < 0)
        {
            spr.flipX = true;
            anim.Play("walk_horiz");
        }
        else if (input.y < 0)
        {
            spr.flipX = false;
            anim.Play("walk_down");
        }
        else if (input.y > 0)
        {
            spr.flipX = false;
            anim.Play("walk_up");
        }
        else
        {
            anim.Play(currentState.fullPathHash, 0, 0f);
        }
    }

    void FixedUpdate()
    {
        Vector2 movement = input.normalized * velocity;
        rb.velocity = movement;
    }
}
