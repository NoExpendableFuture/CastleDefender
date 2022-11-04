using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStateMeleeWindUp: ActorState
{
    private bool completed = false;
    private float startTime;
    private float duration;
    
    public string StateName() {
        return ActorStateName.MELEE_WINDUP;
    }

    public bool AllowMove() {
        return false;
    }

    public bool AllowMelee() {
        return false;
    }

    public bool Completed(){
        return completed;
    }

    public void StateActivate(MonoBehaviour caller, Action onCompletedState = null, float stateDuration = 0f) {
        startTime = Time.time;
        duration = stateDuration;
        caller.StartCoroutine(checkComplete(onCompletedState));
    }

    public void StateDeactivate() {
        completed = true;
    }
        
    public IEnumerator checkComplete(Action onCompletedState)
    {
        while(!completed) {
            if(Time.time > startTime + duration){
                if(onCompletedState != null) {
                    onCompletedState();
                }
            } 
            yield return new WaitForSeconds(duration);
        }
    }
}
