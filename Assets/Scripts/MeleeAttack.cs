using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{    
    protected float duration;
    protected float elapsedTime = 0f;
    protected bool completed = false;
    // private float zRotationInitial, zRotationEnd;
    protected Quaternion currentRotation;

    public ActorType attackerType;

    public float physicalDamage = 1f;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    public virtual void initialise(ActorFacing facing, ActorType attackerType, float timeToDespawn) {
        this.attackerType = attackerType;
        float playerRotation = 0f;
        if(facing == ActorFacing.TOP) {
            playerRotation = 0f;
        } else if (facing == ActorFacing.LEFT) {
            playerRotation = 90f;            
        } else if (facing == ActorFacing.RIGHT) {
            playerRotation = 270f;            
        } else if (facing == ActorFacing.BOTTOM) {
            playerRotation = 180f;
        }

        // zRotationInitial = playerRotation + swingInitialRotation;
        // zRotationEnd = playerRotation + swingEndRotation;
        setRotationFromZPosition(playerRotation);

        this.duration = timeToDespawn;

        StartCoroutine(checkAttackComplete());
    }
    
    public IEnumerator checkAttackComplete()
    {
        while(!completed) {

            if(elapsedTime >= duration){
                completed = true;
                Destroy(gameObject);
            } 

            yield return null;
        }
    }

    protected void setRotationFromZPosition(float newZRotation) {    
        Vector3 facingVector = new Vector3(0f, 0f, newZRotation);
        currentRotation.eulerAngles = facingVector;

        this.transform.rotation = currentRotation;
    }

    public float getPhysicalDamage() {
        return physicalDamage;
    }
}

public enum AttackSource {
    PLAYER,
    ENEMY
}