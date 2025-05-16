using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class MoveToState : State
{
    //range to assume in which character has reached destination
    private float toleranceValue = 2f;

    private NavMeshPath _path;

    public override void StateEnter()
    {
        if(character.path == null)
        {
            character.SetDestination();
            //stateMachine.ChangeState(**);
        }
        character.Agent.SetPath(character.path);
        character.Agent.isStopped = false;
    }

    public override void StateUpdate()
    {
        if (Vector3.SqrMagnitude(character.transform.position - character.path.corners[character.path.corners.Length - 1]) <= toleranceValue)
        { 
            character.CharacterPathComplete();
            //changeState
        }
    }    

    public override void StateExit()
    {
        character.Agent.isStopped = true;
        character.path = null;
    }
}
