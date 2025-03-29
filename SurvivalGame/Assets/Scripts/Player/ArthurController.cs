using UnityEngine;

public class ArthurController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float acceleration = 10f;
    public float deceleration = 15f;
    
    [Header("Combat")] 
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastDirection;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
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
    }
    
    void FixedUpdate()
    {
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
    
    void Attack()
    {
        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        // Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);
        }
    }
    
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}