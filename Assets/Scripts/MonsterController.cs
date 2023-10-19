using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private int detectionRange;
    [SerializeField] private float velocity;

    private Transform target;

    private bool isHiding;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        isHiding = true;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        // TODO make sure Monster does not spawn inside of an object/wall
    }

    // Update is called once per frame
    void Update()
    {
        if (isHiding)
        {
            
        }
        else
        {
            
        }
        // TODO change so if player within range, chase player
        // move towards player
        Vector3 move = (target.position - transform.position).normalized;
        rb.velocity = move * velocity;
    }
}
