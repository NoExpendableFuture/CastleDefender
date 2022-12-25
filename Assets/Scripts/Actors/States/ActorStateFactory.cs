using System;

public class ActorStateFactory
{
    public ActorStateFactory() {

    }

    public ActorState Build(ActorStates toBuild) {
        switch(toBuild) 
        {
        case ActorStates.IDLE:
            return new ActorStateIdle();
        case ActorStates.WALK:
            return new ActorStateWalk();
        case ActorStates.INACTIVE:
            return new ActorStateInactive();
        case ActorStates.MELEE:
            return new ActorStateMelee();
        case ActorStates.MELEE_WINDUP:
            return new ActorStateMeleeWindUp();
        case ActorStates.DEAD:
            return new ActorStateDead();        
        case ActorStates.KNOCKEDBACK:
            return new ActorStateKnockedBack();          
        case ActorStates.PUSHING:
            return new ActorStatePushing();          
        default: 
            throw new ArgumentOutOfRangeException("Unknown actor state " + toBuild);
        }
    }
}
