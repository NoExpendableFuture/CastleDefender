using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAttack : MonoBehaviour
{
    private float swingInitialRotation = 90f;
    private float swingEndRotation = -90f;
    
    private float duration;
    private float elapsedTime = 0f;
    private bool completed = false;
    private float zRotationInitial, zRotationEnd;
    private Quaternion currentRotation;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    public void initialise(ActorFacing facing, float timeToDespawn) {
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

    private void setRotationFromZPosition(float newZRotation) {    
        Vector3 facingVector = new Vector3(0f, 0f, newZRotation);
        currentRotation.eulerAngles = facingVector;

        this.transform.rotation = currentRotation;
    }
}
