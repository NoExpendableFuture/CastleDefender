using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool pushable = true;
    public float playerPushToMoveTime = 1f;
    public float moveSpeed = 1f;
    private Vector2 moveTarget;
    private Vector2 moveDirection;

    private bool moving = false;
    private float height;
    private float width;
    private float playerPushTime = 0f;
    
    private bool pushSinceLastFrame = false;

    private Rigidbody2D rb;

    void Start()
    {
        BoxCollider2D bc2d = GetComponent<BoxCollider2D>();
        width = bc2d.bounds.size.x;
        height = bc2d.bounds.size.y;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), moveTarget) < 0.075f) {
            transform.position = moveTarget;
            moving = false;            
        }
        if (moving) {            
            float targetX = transform.position.x + moveDirection.x * moveSpeed * Time.fixedDeltaTime;
            float targetY = transform.position.y + moveDirection.y * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(new Vector3(targetX, targetY, transform.position.z));
        }
        if (!pushSinceLastFrame && playerPushTime > 0f) {
            playerPushTime = 0f;
            moving = false;
        }

        pushSinceLastFrame = false;
    }

    private bool willHitWall(Vector2 targetedMoveDirection) {
        // Cast box toward the move direction, extending out from the edge of the box by size of the block
        // Note: Assuming square block
        
        // TODO: Set collision layer mask - don't want it blocked by enemies etc.
        RaycastHit2D[] hits = Physics2D.BoxCastAll(this.transform.position, new Vector2(width - 0.1f, height - 0.1f), 0f, targetedMoveDirection, Mathf.Max(width, height));
        for (int i = 0; i < hits.Length; i++) {
            if (hits[i].transform == this.transform) {
                continue;
            } else {
                Debug.Log("Cannot push - hitting " + hits[i].transform.gameObject.name);
                return true;
            }
        }
        return false;        
    }

    public void OnCollisionStay2D(Collision2D other) {
        Player player = other.gameObject.GetComponent<Player>();
        // TODO: Should be a push state, not walk
        if (pushable && !moving && player != null && player.ActorState.StateName() == ActorStateName.WALK) {
            playerPushTime += Time.deltaTime;
            pushSinceLastFrame = true;
            if(playerPushTime >= playerPushToMoveTime) 
            {
                Vector2 distToPlayer = this.transform.position - player.transform.position;
                Debug.Log(distToPlayer);
                if (player.Facing == ActorFacing.BOTTOM && distToPlayer.y < 0f && Mathf.Abs(distToPlayer.y) > Mathf.Abs(distToPlayer.x) ) {
                    // Debug.Log("Push down");
                    moveDirection = new Vector2(0, -1);
                    if(!willHitWall(moveDirection)) {
                        moveTarget = new Vector2(transform.position.x, transform.position.y) + moveDirection;
                        moving = true;  
                    }
                } else if (player.Facing == ActorFacing.TOP && distToPlayer.y > 0f && Mathf.Abs(distToPlayer.y) > Mathf.Abs(distToPlayer.x) ) {
                    // Debug.Log("Push Up");
                    moveDirection = new Vector2(0, 1);
                    if(!willHitWall(moveDirection)) {
                        moveTarget = new Vector2(transform.position.x, transform.position.y) + moveDirection;
                        moving = true;  
                    }   
                } else if (player.Facing == ActorFacing.LEFT && distToPlayer.x < 0f && Mathf.Abs(distToPlayer.y) < Mathf.Abs(distToPlayer.x) ) {
                    // Debug.Log("Push Left");
                    moveDirection = new Vector2(-1, 0);
                    if(!willHitWall(moveDirection)) {
                        moveTarget = new Vector2(transform.position.x, transform.position.y) + moveDirection;
                        moving = true;  
                    }
                } else if (player.Facing == ActorFacing.RIGHT && distToPlayer.x > 0f && Mathf.Abs(distToPlayer.y) < Mathf.Abs(distToPlayer.x) ) {
                    // Debug.Log("Push Right");
                    moveDirection = new Vector2(1, 0);
                    if(!willHitWall(moveDirection)) {
                        moveTarget = new Vector2(transform.position.x, transform.position.y) + moveDirection;
                        moving = true;          
                    }
                }
                playerPushTime = 0f;
            }
        }
    }
}
