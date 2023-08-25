using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Unity Ref")]
    public Animator animator;
    public Rigidbody2D rigidbody2d;
    public Transform playerTransform;

    [Header("Enemy Parameters")]
    public float targetDistance;
    public float minDistanceToMove = 3f;
    public float minDistanceToAttack = 1f;
    public float idleDurationMin = 1f;
    public float idleDurationMax = 5f;
    [Header("Enemy Drops")]
    public GameObject potion;
    public GameObject coin;


    private float idleTimer = 0f;
    private Vector2 originalPosition;
    private bool isAttacking = false;
    private Character character;

    private void Start()
    {
        originalPosition = transform.position;
        idleTimer = Random.Range(idleDurationMin, idleDurationMax);
        character = GetComponent<Character>();
    }

    private void Update()
    {
        Vector2 playerDirection = playerTransform.position - transform.position;
        targetDistance = playerDirection.magnitude;

        if (targetDistance < minDistanceToAttack && !isAttacking)
        {
            isAttacking = true;
            Attack();
        }
        else if (targetDistance <= minDistanceToMove && !isAttacking) 
        {
            Move(playerDirection.normalized);
            isAttacking = false;
        }
        else
        {
            isAttacking = false;
            IdleOrMove();
        }
        if (!drop && character.health <= 0)
        {
            DieAndDropLoot();
        }
    }

    private void Move(Vector2 direction)
    {
        character.Move(direction);
        animator.SetFloat("Speed", character.rigidbody2d.velocity.magnitude);
    }

    private void Attack()
    {
        character.Attack();
    }

    private void IdleOrMove()
    {
        if (idleTimer <= 0)
        {
            Vector2 randomMove = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            Move(randomMove);
            idleTimer = Random.Range(idleDurationMin, idleDurationMax);
        }
        else
        {
            character.rigidbody2d.velocity = Vector2.zero;
            animator.SetFloat("Speed", 0f);
            idleTimer -= Time.deltaTime;
        }
    }

    private bool drop = false;
    private void DieAndDropLoot()
    {
        drop = true;
        Vector2 randomOffset = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Vector3 spawnPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0f) * 2f;
        GameObject lootPrefab = GetRandomLootPrefab();
        GameObject lootInstance = Instantiate(lootPrefab, spawnPosition, Quaternion.identity);
    }
    private GameObject GetRandomLootPrefab()
    {
        GameObject[] lootPrefabs = { potion, coin };

        int randomIndex = Random.Range(0, lootPrefabs.Length);

        return lootPrefabs[randomIndex];
    }
}
