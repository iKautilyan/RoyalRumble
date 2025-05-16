using UnityEngine;

public class StateMachine:MonoBehaviour
{
    public State CurrentState { get; private set; }
    private Character character => GetComponent<Character>();

    public void Initialize(/*State startState*/)
    {      
        ChangeState(new MoveToState());
    }

    public void ChangeState(State newState)
    {
        CurrentState?.StateExit();
        CurrentState = newState;        
        if(CurrentState != null)
        {
            CurrentState.stateMachine = this;
            CurrentState.character = character;
            CurrentState.character.activeState = CurrentState.ToString();
            CurrentState.StateEnter();
        }
    }

    public void Update()
    {
        CurrentState?.StateUpdate();
    }
}
