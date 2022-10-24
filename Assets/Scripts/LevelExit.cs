using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelExit : MonoBehaviour
{
    public LevelEndPopUp levelCompletePrompt;

    public void OnTriggerEnter2D(Collider2D other) {
        Actor otherActor = other.GetComponent<Actor>();
        if (otherActor && otherActor.actorType == ActorType.Player) {
            otherActor.SetInactive();
            showLevelCompletePrompt();
        }
    }

    private void showLevelCompletePrompt() {
        levelCompletePrompt.ShowPopup();
    }
}
