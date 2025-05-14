using UnityEngine;
using UnityEngine.UIElements;

public class State
{
    public Character character;
    public StateMachine stateMachine;

    public virtual void StateEnter() { }
    public virtual void StateUpdate() { }
    public virtual void StateExit() { }
}
