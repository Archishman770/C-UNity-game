using UnityEngine;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float healthRegenRate = 0.5f;
    public float healthRegenDelay = 3f;
    private float lastDamageTime;

    [Header("Hunger/Thirst")]
    public float maxHunger = 100f;
    public float currentHunger = 100f;
    public float hungerDecayRate = 0.1f;
    
    public float maxThirst = 100f;
    public float currentThirst = 100f;
    public float thirstDecayRate = 0.15f;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float currentStamina = 100f;
    public float staminaRegenRate = 8f;
    public float staminaDrainRate = 15f;
    public float staminaRegenDelay = 1f;
    private float lastStaminaUseTime;

    void Start()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentThirst = maxThirst;
        currentStamina = maxStamina;
    }

    void Update()
    {
        // Health regeneration when not recently damaged
        if (Time.time > lastDamageTime + healthRegenDelay && currentHealth < maxHealth)
        {
            currentHealth = Mathf.Min(maxHealth, currentHealth + (healthRegenRate * Time.deltaTime));
        }

        // Hunger/thirst decay
        currentHunger = Mathf.Max(0, currentHunger - (hungerDecayRate * Time.deltaTime));
        currentThirst = Mathf.Max(0, currentThirst - (thirstDecayRate * Time.deltaTime));

        // Stamina regeneration when not recently used
        if (Time.time > lastStaminaUseTime + staminaRegenDelay && currentStamina < maxStamina)
        {
            currentStamina = Mathf.Min(maxStamina, currentStamina + (staminaRegenRate * Time.deltaTime));
        }

        // Starvation/dehydration damage
        if (currentHunger <= 0 || currentThirst <= 0)
        {
            TakeDamage(0.5f * Time.deltaTime);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        lastDamageTime = Time.time;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public bool UseStamina(float amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            lastStaminaUseTime = Time.time;
            return true;
        }
        return false;
    }

    public void RestoreHunger(float amount)
    {
        currentHunger = Mathf.Min(maxHunger, currentHunger + amount);
    }

    public void RestoreThirst(float amount)
    {
        currentThirst = Mathf.Min(maxThirst, currentThirst + amount);
    }

    void Die()
    {
        Debug.Log("Player died - implement respawn logic");
        // TODO: Add respawn system
    }
}