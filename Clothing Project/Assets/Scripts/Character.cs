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
            animator.SetFloat("Health", Health);
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

    [Header("Attack Att")]
    public Transform attackCircleOrigin;
    public Vector3 attackCircleOffset = Vector3.zero;
    public float attackRadius;

    public void Move(Vector2 direction, bool wantRun = false)
    {
        if (health <= 0) return;

        if (IsAttacking) return;

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

    private bool attackBlocked = false;
    public bool IsAttacking { get; private set; }

    public void ResetIsAttacking()
    {
        IsAttacking = false;
        attackBlocked = false;

    }
    public void Attack()
    {
        rigidbody2d.velocity = new Vector2(0, 0);

        if (attackBlocked) return;

        attackBlocked = true;
        animator.SetTrigger("Attack");
        IsAttacking = true;
    }

    public void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        rigidbody2d.velocity = Vector2.zero;
    }

    private bool onHit = false;
    public void ResetOnHit()
    {
        onHit = false;
    }
    public void GetHit(int amount, GameObject sender)
    {
        if (onHit) return;

        if (sender == this.gameObject) return;

        Debug.Log(gameObject.name + "get hi amount: " + amount);

        rigidbody2d.velocity = Vector2.zero;

        onHit = true;

        if (health <= 0)
        {
            Die();
            return;
        }

        health -= amount;

        if (health > 0)
        {
            animator.SetTrigger("Hit");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = attackCircleOrigin == null ? Vector3.zero: attackCircleOrigin.position + attackCircleOffset;
        Gizmos.DrawWireSphere(position,attackRadius);

    }

    public void AttackDetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(attackCircleOrigin.position,attackRadius))
        {
            if (collider.gameObject.tag == "Enemy" && gameObject.tag != "Enemy")
            {
                collider.GetComponent<Character>().GetHit(10,this.gameObject);
            }
            else if (collider.gameObject.tag == "Player" && gameObject.tag != "Player")
            {
                collider.GetComponent<Character>().GetHit(1,this.gameObject);
            }
        }
    }
}
