using System;
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
    public PlayerSwordAttack meleeAttack;

    // TODO: May be time to split off player and standard actor behaviour... shouldn't have this for general actors...
    public LevelEndPopUp gameOverPrompt;

    private ActorFacing facing = ActorFacing.TOP;

    public float meleeAttackDuration = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<ActorInput>();
        actorState = new ActorStateIdle();
    }

    void Update()
    {
        Vector2 direction = input.getMoveDirection().normalized;
        
        if(actorState.AllowMelee() && input.isDoMeleeAttack()) {
            actorState.StateDeactivate();

            PlayerSwordAttack attackInst = Instantiate(meleeAttack, transform.position, Quaternion.identity);
            attackInst.initialise(facing, meleeAttackDuration);

            actorState = actorStateFactory.Build(ActorStates.MELEE);
            actorState.StateActivate(this, () => {
                this.CallbackStateDeactivating();
            }, meleeAttackDuration);
        }

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

    public void Kill() {
        actorState.StateDeactivate();
        actorState = actorStateFactory.Build(ActorStates.DEAD);
        actorState.StateActivate(this);
        if(gameObject.tag == "Player") {
            gameOverPrompt.ShowPopup();
        }
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