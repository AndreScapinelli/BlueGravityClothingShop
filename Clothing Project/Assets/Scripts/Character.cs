using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Unity Ref")]
    public Animator animator;
    public Rigidbody2D rigidbody2d;
    [Header("Character Main Variables")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private float runSpeedMultiplier = 1.5f;
    public float maxStamina = 100f;
    [SerializeField]
    protected float Stamina;
    public virtual float stamina
    {
        get { return Stamina; }
        set 
        {
            if (value <= 0) value = 0;

            Stamina = value; 
        }
    }
    [SerializeField]
    private bool isRunning;
    [SerializeField]
    private int Health;
    public int MaxHealth = 3;
    public virtual int health
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
    public virtual int goldCoin
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
        return stamina > 0;
    }

    private void Run(Vector2 direction)
    {
        Vector2 velocity = direction * speed * runSpeedMultiplier;
        rigidbody2d.velocity = velocity;

        stamina -= Time.deltaTime;

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
        if (stamina < maxStamina)
        {
            stamina += Time.deltaTime;
        }
    }
}
