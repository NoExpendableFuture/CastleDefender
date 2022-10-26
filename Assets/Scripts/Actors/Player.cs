using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public PlayerSwordAttack meleeAttack;
    
    public LevelEndPopUp gameOverPrompt;

    protected override void DoMelee() {
        if(actorState.AllowMelee() && input.isDoMeleeAttack()) {
            actorState.StateDeactivate();

            PlayerSwordAttack attackInst = Instantiate(meleeAttack, transform.position, Quaternion.identity);
            attackInst.initialise(facing, meleeAttackDuration);

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
}
