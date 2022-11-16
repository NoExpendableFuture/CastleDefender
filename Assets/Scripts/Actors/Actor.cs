using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public float walkSpeed = 1f;
    protected ActorInput input;
    protected Rigidbody2D rb;
    protected ActorState actorState;
    public ActorState ActorState {
        get {return actorState;}
    }
    protected ActorStateFactory actorStateFactory = new ActorStateFactory();
    public ActorType actorType;
    
    private ActorAnimator actorAnimator;

    protected ActorFacing facing = ActorFacing.TOP;
    public ActorFacing Facing {
        get {return facing;}
    }

    public float meleeAttackDuration = 1f;

    public float meleeAttackWindUpDuration = 0.25f;
    private float meleeAttackWindUpTimeElapsed = 0f;
    private bool meleeAttackWindUpComplete = false;
    
    public float meleeAttackCoolDownDuration = 0.5f;
    private float meleeAttackCoolDownTimeElapsed = 0f;
    private bool meleeAttackOnCoolDown = false;

    public MeleeAttack meleeAttack;
    public bool hasMeleeAttack = true;

    public RespawnPoint spawnFromPoint;

    public bool destroyAfterKill = true;

    private Vector2 knockbackForce;

    private Vector2 targetPos = Vector2.zero;

    public Vector2 MovingDirection {get {return direction;}}
    Vector2 direction = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<ActorInput>();
        actorAnimator = GetComponent<ActorAnimator>();
        actorState = new ActorStateIdle();
    }

    void Update() {
        DoMelee();
    }

    void FixedUpdate()
    {
        direction = input.getMoveDirection().normalized;
        
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

            float targetX = transform.position.x + direction.x * walkSpeed * Time.fixedDeltaTime;
            float targetY = transform.position.y + direction.y * walkSpeed * Time.fixedDeltaTime;
            targetPos = new Vector3(targetX, targetY, transform.position.z);
            rb.MovePosition(targetPos);
        }

        if(actorState.StateName() == ActorStateName.KNOCKEDBACK) {            
            float targetX = transform.position.x + knockbackForce.x * Time.fixedDeltaTime;
            float targetY = transform.position.y + knockbackForce.y * Time.fixedDeltaTime;   
            targetPos = new Vector3(targetX, targetY, transform.position.z);
            rb.MovePosition(targetPos);
        }
    }

    // void LateUpdate() {
    //     rb.MovePosition(targetPos);
    // }

    protected virtual void DoMelee() { 
        /** Basic method only - Override this in subclasses*/
        // Check cooldown
        if(meleeAttackOnCoolDown) {
            meleeAttackCoolDownTimeElapsed += Time.deltaTime;
            if(meleeAttackCoolDownTimeElapsed >= meleeAttackCoolDownDuration) {
                meleeAttackOnCoolDown = false;
            }
        }
        if(actorState.AllowMelee() && input.isDoMeleeAttack() && hasMeleeAttack && !meleeAttackOnCoolDown) {
            // Commence attack
            actorState.StateDeactivate();

            actorState = actorStateFactory.Build(ActorStates.MELEE_WINDUP);
            actorState.StateActivate(this);

            meleeAttackWindUpTimeElapsed = 0f;
            meleeAttackWindUpComplete = false;
            StartCoroutine(WindUpMeleeAttack());
        }
    }

    // Call this from the health component when hit to initiate knockback
    public void StartKnockback(Vector2 force, float duration) 
    {
        actorState = actorStateFactory.Build(ActorStates.KNOCKEDBACK);
        knockbackForce = force;
        actorState.StateActivate(this, EndKnockback, duration);       
    }

    // Pass this to the knockback component to end the knockback state and reset to idle
    void EndKnockback() 
    {
        if (actorState.StateName() == ActorStateName.KNOCKEDBACK) {
            actorState = actorStateFactory.Build(ActorStates.IDLE);
            actorState.StateActivate(this);
        }
    }

    public IEnumerator WindUpMeleeAttack()
    {
        while(!meleeAttackWindUpComplete) {
            meleeAttackWindUpTimeElapsed += Time.deltaTime;
            if(meleeAttackWindUpTimeElapsed >= meleeAttackWindUpDuration && actorState.StateName() == ActorStateName.MELEE_WINDUP){
                actorState.StateDeactivate();
                actorState = actorStateFactory.Build(ActorStates.MELEE);
                actorState.StateActivate(this, () => {
                    meleeAttackOnCoolDown = true;
                    meleeAttackCoolDownTimeElapsed = 0f;
                    this.CallbackStateDeactivating();
                }, meleeAttackDuration);
                
                meleeAttackWindUpComplete = true;                
                MeleeAttack attackInst = Instantiate(meleeAttack, transform.position, Quaternion.identity);
                attackInst.initialise(this, facing, actorType, meleeAttackDuration);
            } 

            yield return null;
        }
    }

    public virtual void Kill() {
        actorState.StateDeactivate();
        actorState = actorStateFactory.Build(ActorStates.DEAD);
        actorState.StateActivate(this);
        if(spawnFromPoint != null) {
            spawnFromPoint.SignalSpawnedActorDestroyed(this);
        }
        if(destroyAfterKill){
            // TODO: Set it up so this only happens after the death animation plays out...
            Destroy(gameObject);
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

    private void setFacing(UnityEngine.Vector2 moveDirection) {
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
    }
}

public enum ActorType {
    Player,
    Enemy
}

public enum ActorFacing {
    TOP,
    LEFT,
    RIGHT,
    BOTTOM
}