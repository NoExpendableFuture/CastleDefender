using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorInputPlayer : MonoBehaviour, ActorInput
{
    private Vector2 change;

    public Vector2 getMoveDirection () {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        // TODO: change to binary on/off movement? try like this first

        return change;
    }
}
