using UnityEngine;
using UnityEngine.AI;

public class MoveToState : State
{
    private Vector3 _finalPosition;
    //range to assume in which character has reached destination
    private float toleranceValue = 0.2f;

    private NavMeshPath _path;

    public override void StateEnter()
    {
        if(character.path == null)
        {
            //stateMachine.ChangeState(**);
        }
        Debug.Log("setting destination");
        character.Agent.SetPath(character.path);
    }

    public override void StateUpdate()
    {
        if(Vector3.SqrMagnitude(character.transform.position - _finalPosition) <= toleranceValue)
        {
            //changeState
        }
    }    
}
