using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStateIdle: ActorState
{
   public string StateName() {
      return "Idle";
   }

   public bool AllowMove() {
    return true;
   }

   public bool AllowMelee() {
      return true;
   }

   public bool Completed(){
    return false;
   }

   public void StateActivate(MonoBehaviour caller, Action onCompletedState = null, float stateDuration = 0f) {

   }

   public void StateDeactivate() {

   }

}
