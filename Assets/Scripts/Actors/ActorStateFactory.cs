public class ActorStateFactory
{
    private ActorState idleState = new ActorStateIdle();
    private ActorState walkState = new ActorStateWalk();


    public ActorStateFactory() {

    }

    public ActorState Build(ActorStates toBuild) {
        switch(toBuild) 
        {
        case ActorStates.IDLE:
            return idleState;
        case ActorStates.WALK:
            return walkState;
        default: 
            return idleState;
        }
    }
}
