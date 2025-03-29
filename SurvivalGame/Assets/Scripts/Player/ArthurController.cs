using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerStats))]
public class ArthurController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float acceleration = 10f;
    public float deceleration = 15f;
    public float dodgeSpeed = 12f;
    public float dodgeDuration = 0.3f;
    public float dodgeCooldown = 1f;
    private float lastDodgeTime;
    private bool isDodging;
    
    [Header("Combat")] 
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackStaminaCost = 15f;
    public float dodgeStaminaCost = 20f;
    public LayerMask enemyLayers;
    
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastDirection;
    private PlayerStats stats;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();
    }
    
    void Update()
    {
        if (isDodging) return;
        
        // Input handling
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        if (movement != Vector2.zero) 
        {
            lastDirection = movement;
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > lastDodgeTime + dodgeCooldown)
        {
            TryDodge();
        }
    }
    
    void FixedUpdate()
    {
        if (isDodging) return;
        
        // Movement physics
        if (movement.magnitude > 0)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, movement * moveSpeed, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }
    }
    
    void TryDodge()
    {
        if (stats.UseStamina(dodgeStaminaCost))
        {
            StartCoroutine(Dodge());
            lastDodgeTime = Time.time;
        }
    }
    
    IEnumerator Dodge()
    {
        isDodging = true;
        rb.velocity = lastDirection.normalized * dodgeSpeed;
        SoundManager.Instance.PlaySound("PlayerDodge");
        
        yield return new WaitForSeconds(dodgeDuration);
        
        isDodging = false;
    }
    
    [Header("Combat Stats")]
    public CharacterStats characterStats = new CharacterStats();
    
    void Attack()
    {
        if (!stats.UseStamina(attackStaminaCost)) return;
        
        // Play attack sound
        SoundManager.Instance.PlaySound("PlayerAttack");
        
        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        // Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            BossAI boss = enemy.GetComponent<BossAI>();
            if (boss != null)
            {
                bool isCritical = Random.value < characterStats.criticalChance;
                DamageSystem.CalculateDamage(characterStats, boss, isCritical);
                
                // Apply knockback
                Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
                DamageSystem.ApplyKnockback(enemy.GetComponent<Rigidbody2D>(), knockbackDirection, 5f);
                
                // Play hit sound if critical
                if (isCritical)
                {
                    SoundManager.Instance.PlaySound("PlayerCriticalHit");
                }
            }
        }
    }
    
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
    public void TakeDamage(float amount)
    {
        if (!isDodging)
        {
            stats.TakeDamage(amount);
            SoundManager.Instance.PlaySound("PlayerHurt");
        }
    }
}
