public class ActorStateFactory
{
    private ActorState idleState = new ActorStateIdle();
    private ActorState walkState = new ActorStateWalk();
    private ActorState inactiveState = new ActorStateInactive();


    public ActorStateFactory() {

    }

    public ActorState Build(ActorStates toBuild) {
        switch(toBuild) 
        {
        case ActorStates.IDLE:
            return idleState;
        case ActorStates.WALK:
            return walkState;
        case ActorStates.INACTIVE:
            return inactiveState;
        default: 
            return idleState;
        }
    }
}
