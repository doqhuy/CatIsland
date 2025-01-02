using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    private GeneralInformation GeneralInformation = GeneralInformation.Instance;
    public float moveSpeed = 2f;
    public float chaseDistance = 2f;
    public float returnDistance = 4f; // Distance at which the enemy returns to starting position
    public float cooldownDuration = 2f; // Cooldown period before resuming patrolling
    public float stepDistance = 1f;
    public float stepPauseDuration = 1f;

    private Vector2[] patrolDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private Vector2 currentPatrolDirection;
    private Vector3 startingPosition;
    private Vector3 targetPosition;
    private bool isChasing = false;
    private bool isCooldown = false;

    private Animator animator;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        startingPosition = transform.position;
        animator = GetComponent<Animator>();
        ChooseRandomDirection();
        targetPosition = transform.position;
        StartCoroutine(PatrolRoutine());

    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);


        if (distanceToPlayer < chaseDistance && GeneralInformation.Instance.Actioning == "Playing")
        {
            isChasing = true;
            isCooldown = false;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else if (!isCooldown && Vector3.Distance(transform.position, startingPosition) > returnDistance)
        {
            ReturnToStartingPosition();
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        UpdateAnimatorDirection(direction);
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * 2 * Time.deltaTime);

        if (Vector3.Distance(transform.position, player.position) > chaseDistance || GeneralInformation.Actioning != "Playing")
        {
            isChasing = false;
            StartCoroutine(StartCooldown());
        }
    }

    void ReturnToStartingPosition()
    {
        isChasing = false;
        targetPosition = startingPosition;
    }

    IEnumerator StartCooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        isCooldown = false;
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            if (!isChasing && !isCooldown)
            {

                if (Vector2.Distance(transform.position, targetPosition) <= 0.1f)
                {
                    animator.speed = 0;
                    yield return new WaitForSeconds(stepPauseDuration);
                    ChooseRandomDirection();

                    targetPosition = transform.position + (Vector3)currentPatrolDirection * stepDistance;
                }
                else
                {

                    Vector3 direction = (targetPosition - transform.position).normalized;
                    UpdateAnimatorDirection(direction);
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                }
            }
            else
            {

                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }
    }

    void UpdateAnimatorDirection(Vector3 direction)
    {
        animator.speed = 1f;
        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);
    }

    void ChooseRandomDirection()
    {

        currentPatrolDirection = patrolDirections[Random.Range(0, patrolDirections.Length)];
    }
}