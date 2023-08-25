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
    [SerializeField]
    private float baseStamina = 15;
    [SerializeField]
    private float maxStamina = 100f;
    public float GetMaxStamina()
    {
        maxStamina = baseStamina + +(outfit.hood.staminaBonus + outfit.armour.healthBonus + outfit.pelvis.staminaBonus);
        return maxStamina;
    }
    [SerializeField]
    private float Stamina;
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
    private float Health;
    [SerializeField]
    private int baseMaxHealth = 3;
    [SerializeField]
    private float maxHealth = 3;
    public float GetMaxHealth()
    {
        maxHealth = baseMaxHealth + (outfit.hood.healthBonus + outfit.armour.healthBonus + outfit.pelvis.healthBonus);
        return maxHealth;
    }
    public virtual float health
    {
        get { return Health; }
        set 
        {
            if (value <= 0)
            {
                Health = 0;

                Die();
            }
            else if (value >= maxHealth)
            {
                Health = GetMaxHealth(); 
            }
            else
            {
                Health = value;
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
            else
            {
                GoldCoin = value;
            }
            Debug.Log("Current amount of goild coins: " + goldCoin);
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

        if (stamina < GetMaxStamina())
        {
            stamina += Time.deltaTime;
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }
}
