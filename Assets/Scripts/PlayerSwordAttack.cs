using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAttack : MeleeAttack
{
    private float swingInitialRotation = 90f;
    private float swingEndRotation = -90f;
    
    private float zRotationInitial, zRotationEnd;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    public override void initialise(ActorFacing facing, float timeToDespawn) {
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

        zRotationInitial = playerRotation + swingInitialRotation;
        zRotationEnd = playerRotation + swingEndRotation;
        setRotationFromZPosition(zRotationInitial);

        this.duration = timeToDespawn;

        StartCoroutine(swingSword());
    }
    
    public IEnumerator swingSword()
    {
        while(!completed) {

            float zRotationCurrent = Mathf.Lerp(zRotationInitial, zRotationEnd, (elapsedTime / duration)); 
            setRotationFromZPosition(zRotationCurrent);

            if(elapsedTime >= duration){
                completed = true;
                Destroy(gameObject);
            } 

            yield return null;
        }
    }

}
