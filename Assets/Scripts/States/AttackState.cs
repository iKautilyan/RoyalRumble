using UnityEngine;

public class AttackState : State
{
    private float tolerance = 25f;

    public override void StateEnter()
    {
        
    }
    public override void StateUpdate()
    {
        if(character.enemySighted && character.spottedEnemy)
        {
            RotateTowardsTarget();
            if (SigthsOnEnemy())
            {
                if (character.characterWeapon.loaded)
                    character.characterWeapon.PullTrigger();
            }
        }
    }

    public bool SigthsOnEnemy()
    {
        float viewAngle = character.AngleToCharacter(character.spottedEnemy?.transform);
        if (viewAngle < tolerance && viewAngle >-tolerance)
        {
            return true;
        }
        return false;
    }    

    public void RotateTowardsTarget()
    {
        float maxRotationThisFrame = character.CharacterRotationSpeed * Time.deltaTime;
        float rotationThisFrame = Mathf.Clamp(character.AngleToCharacter(character.spottedEnemy?.transform), -maxRotationThisFrame, maxRotationThisFrame);
        character.transform.Rotate(Vector3.up, rotationThisFrame, Space.World);
    }
}
