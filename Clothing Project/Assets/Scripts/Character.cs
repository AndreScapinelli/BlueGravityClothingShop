using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigidbody2d;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float runSpeedMultiplier = 1.5f;
    [SerializeField]
    private float maxStamina = 100f;
    [SerializeField]
    private float currentStamina;
    [SerializeField]
    private bool isRunning;
    [SerializeField]
    private int Health;
    [SerializeField]
    private int MaxHealth = 3;
    public int health
    {
        get { return Health; }
        set 
        {
            if (value <= 0)
            {
                health = 0;
            }
            else if (value >= MaxHealth)
            {
                health = MaxHealth;
            }
        }
    }
    [SerializeField]
    private int GoldCoin;
    public int goldCoin
    {
        get { return GoldCoin; }
        set 
        {
            if (value <= 0)
            {
                GoldCoin = value;
            }
        }
    }

    public GameBase.CharacterOutfit outfit;

    public void Move(Vector2 direction, bool wantRun = false)
    {
        if (CanRun() && wantRun)
        {
            Run(direction);
        }
        else
        {
            Walk(direction);
        }

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Face right
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Face left
        }
    }
    private bool CanRun()
    {
        return currentStamina > 0;
    }

    private void Run(Vector2 direction)
    {
        Vector2 velocity = direction * speed * runSpeedMultiplier;
        rigidbody2d.velocity = velocity;

        currentStamina -= Time.deltaTime;

        animator.SetFloat("Speed", direction.magnitude);
        isRunning = true;
        animator.SetBool("Running", isRunning);
    }
    private void Walk(Vector2 direction)
    {
        Vector2 velocity = direction * speed;
        rigidbody2d.velocity = velocity;

        isRunning = false;
        animator.SetBool("Running", isRunning);

        animator.SetFloat("Speed", direction.magnitude);
        if (currentStamina < maxStamina)
        {
            currentStamina += Time.deltaTime;
        }
    }
}
