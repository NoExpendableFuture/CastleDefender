using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ActorState
{
   string StateName();
   
   bool AllowMove();
   bool Completed();

   void StateActivate();
   void StateDeactivate();   
}

public enum ActorStates {
    IDLE,
    WALK,
    INACTIVE
}

// TODO: Actor state factory using the states pls
