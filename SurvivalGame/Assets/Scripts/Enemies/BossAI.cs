using UnityEngine;

public class BossAI : MonoBehaviour
{
    [Header("Combat")]
    public int maxHealth = 100;
    public int currentHealth;
    public float attackCooldown = 2f;
    public float attackRange = 3f;
    public int damage = 10;
    
    [Header("Movement")] 
    public float moveSpeed = 2f;
    public float chaseDistance = 8f;
    
    private Transform player;
    private float lastAttackTime;
    private bool isEnraged = false;
    
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (distanceToPlayer < chaseDistance)
        {
            // Chase behavior
            if (distanceToPlayer > attackRange)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position, 
                    player.position, 
                    moveSpeed * Time.deltaTime
                );
            }
            // Attack behavior
            else if (Time.time > lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
        
        // Enrage at 30% health
        if (!isEnraged && currentHealth < maxHealth * 0.3f)
        {
            Enrage();
        }
    }
    
    void Attack()
    {
        // Implement boss-specific attack pattern
        Debug.Log("Boss attacks!");
    }
    
    void Enrage()
    {
        isEnraged = true;
        moveSpeed *= 1.5f;
        attackCooldown *= 0.7f;
        Debug.Log("Boss is enraged!");
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        Debug.Log("Boss defeated!");
        Destroy(gameObject);
    }
}