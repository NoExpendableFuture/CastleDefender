using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ActorState
{
   string StateName();
   
   bool AllowMove();
   bool AllowMelee();

   bool Completed();

   void StateActivate(MonoBehaviour caller, Action onCompletedState = null, float stateDuration = 0f);
   void StateDeactivate();   
}

public enum ActorStates {
    IDLE,
    WALK,
    INACTIVE,
    MELEE
}

// TODO: Actor state factory using the states pls
