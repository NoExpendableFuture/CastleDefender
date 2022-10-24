using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ActorInput
{
    // TODO: Abstract functions for all kinds of directions etc. we'll want

    Vector2 getMoveDirection ();
    bool isDoMeleeAttack();
}
