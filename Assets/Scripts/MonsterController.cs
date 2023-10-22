using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private int detectionRange;
    [SerializeField] private float velocity;
    [SerializeField] private int maxStamina;

    private Transform target;

    private bool drainingStamina;
    
    private int currentStamina;
    
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        // TODO make sure Monster spawns in right place
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(target.position, transform.position) <= detectionRange)
        {
            // move towards player if within detection range
            Vector2 move = (target.position - transform.position).normalized;
            rb.velocity = move * velocity;
            StartCoroutine("DrainStamina");
        }
        else
        {
            rb.velocity = Vector2.zero;
            currentStamina = maxStamina;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log($"monster collided with {other}");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Game Over :(");
            // TODO implement game over screen from here
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
                Debug.Log("Monster is tired :/");
                currentStamina = maxStamina;
                //TODO respawn monster at proper location
            }
            drainingStamina = false;
        }
    }
}
