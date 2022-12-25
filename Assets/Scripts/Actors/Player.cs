using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public PlayerSwordAttack playerSwordAttack;
    
    public LevelEndPopUp gameOverPrompt;

    protected override void DoMelee() {
        if(actorState.AllowMelee() && input.isDoMeleeAttack() && hasMeleeAttack) {
            actorState.StateDeactivate();

            PlayerSwordAttack attackInst = Instantiate(playerSwordAttack, transform.position, Quaternion.identity);
            attackInst.initialise(this, facing, actorType, meleeAttackDuration);

            actorState = actorStateFactory.Build(ActorStates.MELEE);
            actorState.StateActivate(this, () => {
                this.CallbackStateDeactivating();
            }, meleeAttackDuration);
        }
    }

    public override void Kill() {
        base.Kill();
        gameOverPrompt.ShowPopup();
    }

    public void OnCollisionStay2D(Collision2D other) {
        // If it's a pushable block, and we're pushing into it, want to use pushing animations
        if(other.gameObject.GetComponent<Block>() != null) {
            Vector2 distFromPlayer = other.transform.position - this.transform.position;
            if(Facing == ActorFacing.BOTTOM && distFromPlayer.y < 0f && Mathf.Abs(distFromPlayer.y) > Mathf.Abs(distFromPlayer.x) ) {
                pushingTimeElapsed = 0f;
                isPushingBlock = true;
            } else if (Facing == ActorFacing.TOP && distFromPlayer.y > 0f && Mathf.Abs(distFromPlayer.y) > Mathf.Abs(distFromPlayer.x) ) {
                pushingTimeElapsed = 0f;
                isPushingBlock = true;
            } else if (Facing == ActorFacing.LEFT && distFromPlayer.x < 0f && Mathf.Abs(distFromPlayer.y) < Mathf.Abs(distFromPlayer.x) ) {
                pushingTimeElapsed = 0f;
                isPushingBlock = true;
            } else if (Facing == ActorFacing.RIGHT && distFromPlayer.x > 0f && Mathf.Abs(distFromPlayer.y) < Mathf.Abs(distFromPlayer.x) ) {
                pushingTimeElapsed = 0f;
                isPushingBlock = true;
            }
        }
    }
}
