using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStateInactive: ActorState
{
   public string StateName() {
      return "Inactive";
   }

   public bool AllowMove() {
    return false;
   }

   public bool Completed(){
    return false;
   }

   public void StateActivate() {

   }

   public void StateDeactivate() {

   }

}
