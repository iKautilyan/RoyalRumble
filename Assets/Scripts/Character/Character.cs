using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour
{
    private StateMachine stateMachine => GetComponent<StateMachine>();
    private NavMeshAgent agent => GetComponent<NavMeshAgent>();

    public WeaponController characterWeapon;

    public NavMeshAgent Agent { get => agent;}
    private Transform _transform => transform;

    public MoveToState moveToState;

    [SerializeField]
    private float detectionRange = 15f;

    [SerializeField]
    private float fov = 135f;

    [SerializeField]
    private float characterMovementSpeed = 2.5f;

    [SerializeField]
    private float characterRotationSpeed = 1f;

    [SerializeField]
    string activeState;

    public NavMeshPath path;

    public bool enemySighted = false;

    NavMeshHit hitPoint;
    bool hitTrue = false;

    void Start()
    {        
        stateMachine.Initialize();
    }

    public void SetDestination(Vector3 finalPosition)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(finalPosition, out hit, 0.1f, NavMesh.AllAreas))
        {
            path = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, hit.position, NavMesh.AllAreas, path);
        }
    }

    public void WatchForEnemy()
    {
        Collider[] charactersInRange = Physics.OverlapSphere(transform.position, detectionRange, 3 << 8);
        for(int i = 0; i < charactersInRange.Length; i++)
        {
            Vector3 targetDirection = charactersInRange[i].transform.position - transform.position;
            if (Vector3.Angle(transform.forward, targetDirection) < fov/2)
            {
                enemySighted = true;
                continue;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(hitTrue)
        {
            Gizmos.DrawSphere(hitPoint.position, 1f);
        }
    }
}
