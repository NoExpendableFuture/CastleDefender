using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public float walkSpeed = 1f;
    private ActorInput input;
    private Rigidbody2D rb;
    private ActorState actorState;
    private ActorStateFactory actorStateFactory = new ActorStateFactory();
    public ActorType actorType;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<ActorInput>();
        actorState = new ActorStateIdle();
    }

    void Update()
    {
        Vector2 direction = input.getMoveDirection().normalized;
        
        if(actorState.AllowMove()) {
            if(direction.magnitude > 0f) {
               actorState.StateDeactivate();
               actorState = actorStateFactory.Build(ActorStates.WALK);
               actorState.StateActivate();
            } else {                
               actorState.StateDeactivate();
               actorState = actorStateFactory.Build(ActorStates.IDLE);
               actorState.StateActivate();
            }

            float targetX = transform.position.x + direction.x * walkSpeed * Time.deltaTime;
            float targetY = transform.position.y + direction.y * walkSpeed * Time.deltaTime;
            
            Vector3 targetPos = new Vector3(targetX, targetY, transform.position.z);
            rb.MovePosition(targetPos);
        }
    }

    public void SetInactive() {
        actorState.StateDeactivate();
        actorState = actorStateFactory.Build(ActorStates.INACTIVE);
        actorState.StateActivate();
    }
}

public enum ActorType {
    Player
}