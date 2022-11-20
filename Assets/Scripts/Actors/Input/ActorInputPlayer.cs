using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorInputPlayer : MonoBehaviour, ActorInput
{
    private Vector2 change;

    public Vector2 getMoveDirection () {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal") > 0f ? 1f : Input.GetAxisRaw("Horizontal") < 0f ? -1f : 0f;
        change.y = Input.GetAxisRaw("Vertical") > 0f ? 1f : Input.GetAxisRaw("Vertical") < 0f ? -1f : 0f;
        
        return change.normalized;
    }

    public bool isDoMeleeAttack() {
        return Input.GetButtonDown("Fire1");
    }
}
