using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private int detectionRange;
    [SerializeField] private float velocity;

    private Transform target;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        // TODO make sure Monster does not spawn inside of an object/wall
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= detectionRange)
        {
            // move towards player if within detection range
            Vector3 move = (target.position - transform.position).normalized;
            rb.velocity = move * velocity;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{other}");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Game Over :(");
            // TODO implement game over screen from here
        }
    }
}
