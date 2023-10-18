using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float smoothing;

    private Vector3 targetPos;
    private Vector3 oldPos;
    private Vector3 newPos;

    private Vector3 velocity = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        targetPos = target.transform.position;
        transform.position = new Vector3(targetPos.x, targetPos.y, -10);
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = target.transform.position;
        oldPos = new Vector3(transform.position.x, transform.position.y, -10);
        newPos = new Vector3(targetPos.x, targetPos.y, -10);
        transform.position = Vector3.SmoothDamp(oldPos, newPos, ref velocity, smoothing);
    }
}
