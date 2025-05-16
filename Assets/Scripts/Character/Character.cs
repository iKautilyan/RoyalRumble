using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

public class Character : MonoBehaviour
{
    private StateMachine stateMachine => GetComponent<StateMachine>();
    private NavMeshAgent agent => GetComponent<NavMeshAgent>();

    public WeaponController characterWeapon;

    public NavMeshAgent Agent { get => agent;}
    private Transform _transform => transform;

    [SerializeField]
    private int health = 100;

    [SerializeField]
    private float detectionRange = 15f;

    [SerializeField]
    private float _fov = 135f;

    private float fov =>  _fov;

    [SerializeField]
    private float characterMovementSpeed = 2.5f;

    [SerializeField]
    private float characterRotationSpeed = 1f;

    public float CharacterRotationSpeed => characterRotationSpeed;

    [SerializeField]
    public string activeState;

    [SerializeField]
    public GameObject spottedEnemy;

    public NavMeshPath path;

    public bool enemySighted = false;

    void Start()
    {        
        stateMachine.Initialize();
    }

    private void Update()
    {
        if(health<=0)
            CharacterDeath();
        if (activeState == "AttackState" && (!enemySighted || spottedEnemy == null))
        {
            enemySighted = false;
            spottedEnemy = null;
            stateMachine.ChangeState(new MoveToState());
        }
        if (!enemySighted)
        {
            WatchForEnemy();
        }
        else if(spottedEnemy != null)
        {
            TrackEnemy();
        }
        
        
    }

    public void CharacterDeath()
    {
        SimulationManager.Instance.simCharacters.Remove(this.gameObject);
        Destroy(gameObject);
    }

    public void SetDestination()
    {
        SetDestination(SetRandomPointOnPlane());
    }

    public Vector3 SetRandomPointOnPlane()
    {
        Vector3 randomDestination = new Vector3(Random.Range(-25f, 25f), 0, Random.Range(-25f, 25f));
        return randomDestination;
    }

    public void DamageToCharacter(int damage)
    {
        health -= damage;
    }

    public void SetDestination(Vector3 finalPosition)
    {
        Agent.speed = characterMovementSpeed;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(finalPosition, out hit, 2f, NavMesh.AllAreas))
        {
            path = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, hit.position, NavMesh.AllAreas, path);
        }
    }

    public void TrackEnemy()
    {
        if (CheckVisibility(spottedEnemy?.transform))
        {
            return;
        }
        if (spottedEnemy != null) 
        {
            SetDestination(spottedEnemy.transform.position);
            enemySighted = false;
            spottedEnemy = null;
            stateMachine.ChangeState(new MoveToState());
        }
    }

    public bool WatchForEnemy()
    {
        enemySighted = false;
        Collider[] charactersInRange = Physics.OverlapSphere(transform.position, detectionRange, 1 << 3);
        for(int i = 0; i < charactersInRange.Length; i++)
        {
            if (charactersInRange[i].gameObject != gameObject && (AngleToCharacter(charactersInRange[i].transform) < fov / 2 && AngleToCharacter(charactersInRange[i].transform) > -fov / 2))
            {
                if (CheckVisibility(charactersInRange[i].transform))
                {
                    enemySighted = true;
                    spottedEnemy = charactersInRange[i].gameObject;
                    stateMachine.ChangeState(new AttackState());
                    continue;
                }
            }
        }
        return enemySighted;
    }

    public bool CheckVisibility(Transform characterOfInterest)
    {
        bool result = false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, GetDirectionVector(characterOfInterest), out hit, detectionRange))
        {
            result = SimulationManager.Instance.simCharacters.Contains(hit.collider.gameObject);
        }
        return (result);
    }

    public void CharacterPathComplete()
    {
        stateMachine.ChangeState(new MoveToState());
    }

    public float AngleToCharacter(Transform characterOfInterest)
    {
        Vector3 forward = transform.forward;
        return Vector3.SignedAngle(forward, GetDirectionVector(characterOfInterest), Vector3.up);
    }

    public Vector3 GetDirectionVector(Transform characterOfInterest)
    {
        Vector3 directionToTarget = characterOfInterest.position - transform.position;
        directionToTarget.y = 0f;
        return directionToTarget.normalized;
    }

    public float GetDistance(Transform characterOfInterest)
    {
        return (transform.position - characterOfInterest.position).magnitude;
    }
}
