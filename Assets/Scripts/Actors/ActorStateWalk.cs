using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStateWalk: ActorState
{
   public string StateName() {
      return "Walk";
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
