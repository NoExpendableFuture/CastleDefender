using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public float walkSpeed = 1f;
    protected ActorInput input;
    protected Rigidbody2D rb;
    protected ActorState actorState;
    protected ActorStateFactory actorStateFactory = new ActorStateFactory();
    public ActorType actorType;

    protected ActorFacing facing = ActorFacing.TOP;

    public float meleeAttackDuration = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<ActorInput>();
        actorState = new ActorStateIdle();
    }

    void Update()
    {
        DoMelee();

        Vector2 direction = input.getMoveDirection().normalized;
        
        if(actorState.AllowMove()) {
            if(direction.magnitude > 0f) {
               actorState.StateDeactivate();
               actorState = actorStateFactory.Build(ActorStates.WALK);
               actorState.StateActivate(this);
               setFacing(direction);
            } else {
               actorState.StateDeactivate();
               actorState = actorStateFactory.Build(ActorStates.IDLE);
               actorState.StateActivate(this);
            }

            float targetX = transform.position.x + direction.x * walkSpeed * Time.deltaTime;
            float targetY = transform.position.y + direction.y * walkSpeed * Time.deltaTime;
            
            Vector3 targetPos = new Vector3(targetX, targetY, transform.position.z);
            rb.MovePosition(targetPos);
        }
    }

    protected virtual void DoMelee() { /** Override in subclasses*/ }

    public virtual void Kill() {
        actorState.StateDeactivate();
        actorState = actorStateFactory.Build(ActorStates.DEAD);
        actorState.StateActivate(this);
    }

    public void SetInactive() {
        actorState.StateDeactivate();
        actorState = actorStateFactory.Build(ActorStates.INACTIVE);
        actorState.StateActivate(this);
    }
    
    public void CallbackStateDeactivating() {
        actorState.StateDeactivate();
        actorState = actorStateFactory.Build(ActorStates.IDLE);
        actorState.StateActivate(this);
    }

    private void setFacing(Vector2 moveDirection) {
        if(Mathf.Abs(moveDirection.y) > Mathf.Abs(moveDirection.x)) {
            // One of the verticals
            if(moveDirection.y >= 0f) {
                facing = ActorFacing.TOP;
            } else {
                facing = ActorFacing.BOTTOM;
            }
        } else {
            // One of the horizontals
            if(moveDirection.x >= 0f) {
                facing = ActorFacing.RIGHT;
            } else {
                facing = ActorFacing.LEFT;
            }
        }
        // TODO: Update facing animation!
    }
}

public enum ActorType {
    Player
}

public enum ActorFacing {
    TOP,
    LEFT,
    RIGHT,
    BOTTOM
}