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
        case ActorStates.DEAD:
            return new ActorStateDead();            
        default: 
            return new ActorStateIdle();
        }
    }
}
