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

   public bool Completed(){
    return false;
   }

   public void StateActivate() {

   }

   public void StateDeactivate() {

   }

}
