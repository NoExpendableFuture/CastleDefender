using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStateKnockedBack: ActorState
{
   private float knockbackTime = 9999f;
   private float startTime;
   private bool completed = false;
   private Action onCompletedState;

   public string StateName() {
      return ActorStateName.KNOCKEDBACK;
   }

   public bool AllowMove() {
    return false;
   }

   public bool AllowMelee() {
      return false;
   }

   public bool Completed(){
    return false;
   }

   public void StateActivate(MonoBehaviour caller, Action onCompletedState = null, float stateDuration = 0f) {
      knockbackTime = stateDuration;
      startTime = Time.time;
      this.onCompletedState = onCompletedState;

      caller.StartCoroutine(checkComplete());
   }

   public void StateDeactivate() {
      completed = true;
      onCompletedState();
   }

    public IEnumerator checkComplete()
    {
        while(!completed) {
            if(Time.time > startTime + knockbackTime){
               StateDeactivate();                
            } 
            yield return new WaitForSeconds(knockbackTime);
        }
    }
}
