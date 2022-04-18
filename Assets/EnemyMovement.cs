using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AIController : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed;

    public void MoveTowardsTarget(Vector2 targetPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }




    void Start()
    {

    }

    void Update()
    {

    }

}