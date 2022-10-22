using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public float walkSpeed = 1f;
    private ActorInput input;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<ActorInput>();
    }

    void Update()
    {
        Vector2 direction = input.getMoveDirection();
        
        float targetX = transform.position.x + direction.x * walkSpeed * Time.deltaTime;
        float targetY = transform.position.y + direction.y * walkSpeed * Time.deltaTime;
        
        Vector3 targetPos = new Vector3(targetX, targetY, transform.position.z);

        rb.MovePosition(targetPos);
    }
}
