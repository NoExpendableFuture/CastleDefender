using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStateInactive: ActorState
{
   public string StateName() {
      return ActorStateName.INACTIVE;
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

   }

   public void StateDeactivate() {

   }

}
