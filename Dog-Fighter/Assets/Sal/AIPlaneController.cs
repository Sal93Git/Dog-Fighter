using UnityEngine;

public class AIPlaneController : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 50f;
    public float turnSpeed = 2f;
    public float rollSpeed = 2f;

    public Vector3 startPosition;
    public Vector3 PatrolArea;

    public Vector3 evasionDestination;
    
    // public bool evading = false;

    public enum EnemyState{
        Patrolling,
        Attacking,
        Chasing,
        Evading
    }

    public EnemyState currentState;

    void Start()
    {
        startPosition = transform.position;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = EnemyState.Chasing;
    }
    void Update()
    {
        // Calculate distance to player target 
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        // Use value of distanceToPlayer to decide on which state to switch to
        if(distanceToPlayer > 180f)
        {
            currentState = EnemyState.Patrolling;
        }

        if((distanceToPlayer < 180f) && (distanceToPlayer >= 70f))
        {
            currentState = EnemyState.Chasing;
        }

        if(distanceToPlayer < 10f)
        {
            currentState = EnemyState.Evading;
        }

        // Call what behavior to run depending on what the currentState is
        HandleAiState();

        // Constantly keep the plane moving forward
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void HandleAiState()
    {
        switch(currentState)
        {
            case EnemyState.Chasing:
                Chase();
                break;
            case EnemyState.Patrolling:
                Patrol();
                break;
            case EnemyState.Evading:
                Evade();
                break;
        }
    }

    void Patrol()
    {
        // Pick a new target if none exists or we're close to it
        if (PatrolArea == Vector3.zero || Vector3.Distance(transform.position, PatrolArea) < 4f)
        {
            PatrolArea = GetRandomPositionFarFrom(startPosition);
        }

        // Calculate direction to target
        Vector3 directionToTarget = (PatrolArea - transform.position).normalized;

        MoveToDestination(directionToTarget);
    }

    private Vector3 chaseOffset;
    private float offsetResetTimer = 0f;
    private float offsetResetInterval = 3f;

    void Chase()
    {
        if (!target) return;

        // Periodically reset offset
        offsetResetTimer -= Time.deltaTime;
        if (offsetResetTimer <= 0f)
        {
            chaseOffset = target.right * Random.Range(-20f, 20f) + target.up * Random.Range(-5f, 5f);
            offsetResetTimer = offsetResetInterval;
        }

        Vector3 desiredPosition = target.position + chaseOffset;
        Vector3 targetDir = (desiredPosition - transform.position).normalized;

        MoveToDestination(targetDir);
    }

    void Evade()
    {
        if(evasionDestination == Vector3.zero || evasionDestination == null)
        //if(evading == false)
        {
            evasionDestination = GetRandomPositionFarFrom(transform.position);
        }

        MoveToDestination(evasionDestination);

        if(Vector3.Distance(transform.position,evasionDestination) < 4f)
        {
            evasionDestination = Vector3.zero;
            // currentState = EnemyState.Chasing;
        }
    }

    void Attack()
    {
        // Attack code - should maybe combine with chase?
    }


    Vector3 GetRandomPositionFarFrom(Vector3 origin, float minDistance = 40f, float maxDistance = 70f)
    {
        Vector2 randomDirection2D = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minDistance, maxDistance);

        Vector3 offset = new Vector3(randomDirection2D.x, 0f, randomDirection2D.y) * randomDistance;
        return origin + offset;
    }

    void MoveToDestination(Vector3 desination)
    {
        // Look and rotate toward target
        Quaternion targetRotation = Quaternion.LookRotation(desination, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        // Simulate banking (roll)
        float turnAmount = Vector3.Dot(transform.right, desination);
        // float desiredRoll = -turnAmount * 45f;
        float desiredRoll = Mathf.Clamp(-turnAmount * 45f, -45f, 45f);
        Quaternion rollRotation = Quaternion.AngleAxis(desiredRoll, transform.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, rollRotation * transform.rotation, rollSpeed * Time.deltaTime);
    }

}
