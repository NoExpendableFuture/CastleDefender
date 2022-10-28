using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Input to push an actor to chase after the player
public class ActorInputPlayerChase : MonoBehaviour, ActorInput
{
    public Player target;

    public float attackRange = 1.25f;

    private Vector2 change;

    void Start() {
        if (target == null) {
            UnityEngine.Debug.LogWarning("Missing player setup on player chasing script: " + gameObject.name);
        }
    }

    public Vector2 getMoveDirection () {
        Vector3 direction = target.transform.position - this.transform.position;
        if(new Vector2(direction.x, direction.y).magnitude < 1f) {
            return Vector2.zero;
        }
        Vector2 change = direction.normalized;
        return change;
    }

    public bool isDoMeleeAttack() {
        Vector3 direction = target.transform.position - this.transform.position;
        if(new Vector2(direction.x, direction.y).magnitude < 1.75f) {
            return true;
        }
        return false; 
    }
}
