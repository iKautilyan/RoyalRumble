using UnityEngine;

public class StateMachine:MonoBehaviour
{
    public State CurrentState { get; private set; }
    public MoveToState moveToState;
    private Character character => GetComponent<Character>();

    public void Initialize(/*State startState*/)
    {
        moveToState = new MoveToState();
        character.SetDestination(Vector3.forward * 9f);
        ChangeState(moveToState);
    }

    public void ChangeState(State newState)
    {
        CurrentState?.StateExit();
        CurrentState = newState;        
        if(CurrentState != null)
        {
            CurrentState.stateMachine = this;
            CurrentState.character = character;
            CurrentState.StateEnter();
        }
    }

    public void Update()
    {
        CurrentState?.StateUpdate();
    }
}
