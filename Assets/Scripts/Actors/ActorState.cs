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
    MELEE,
    DEAD,
    KNOCKEDBACK
}

public static class ActorStateName
{
    public static String IDLE { get { return "Idle";} } 
    public static String WALK { get { return "Walk";} } 
    public static String INACTIVE { get { return "Inactive";} } 
    public static String MELEE { get { return "Melee";} } 
    public static String DEAD { get { return "Dead";} } 
    public static String KNOCKEDBACK { get { return "Knocked back";} } 
}

