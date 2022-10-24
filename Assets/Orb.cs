using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : Puzzle
{
    bool orbDestroyed = false;

    private void OnTriggerEnter2D(Collider2D other) {
        UnityEngine.Debug.Log("Something entered orb collider!" + other.name);
        PlayerSwordAttack sword = other.GetComponent<PlayerSwordAttack>();
        if(sword != null) {
            orbDestroyed = true;
            // TODO: Animate 'dis
            gameObject.SetActive(false);
            // Once complete, do 'dis
            door.Open();
        }
    }

    public override bool PuzzleComplete() {
        return orbDestroyed;
    }
}
