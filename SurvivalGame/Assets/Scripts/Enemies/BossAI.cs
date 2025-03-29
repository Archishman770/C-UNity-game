using UnityEngine;

public class BossAI : MonoBehaviour
{
    public static event System.Action<BossAI> OnBossSpawned;
    public static event System.Action<BossAI> OnBossDefeated;
    [Header("Combat")]
    public string bossName = "Boss";
    public int maxHealth = 100;
    public int currentHealth;
    public float attackCooldown = 2f;
    public float attackRange = 3f;
    public int damage = 10;
    public int scoreValue = 1000;
    
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
        OnBossSpawned?.Invoke(this);
        SoundManager.Instance.PlaySound("BossSpawn");
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
        SoundManager.Instance.PlaySound("BossAttack");
        Debug.Log("Boss attacks!");
    }
    
    void Enrage()
    {
        isEnraged = true;
        moveSpeed *= 1.5f;
        attackCooldown *= 0.7f;
        SoundManager.Instance.PlaySound("BossEnrage");
        Debug.Log("Boss is enraged!");
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        SoundManager.Instance.PlaySound("BossHurt");
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        Debug.Log("Boss defeated!");
        SoundManager.Instance.PlaySound("BossDeath");
        OnBossDefeated?.Invoke(this);
        Destroy(gameObject);
    }
}