using UnityEngine;

public static class DamageSystem
{
    public static void CalculateDamage(CharacterStats attacker, CharacterStats defender, bool isCritical = false)
    {
        if (defender == null) return;

        float baseDamage = attacker.baseDamage;
        float damageMultiplier = 1f;
        float defenseReduction = Mathf.Clamp01(defender.defense / 100f);

        // Critical hit calculation
        if (isCritical)
        {
            damageMultiplier *= attacker.criticalMultiplier;
            Debug.Log("Critical hit!");
        }

        // Final damage calculation
        float finalDamage = baseDamage * damageMultiplier * (1f - defenseReduction);
        finalDamage = Mathf.Max(1, finalDamage); // Ensure at least 1 damage

        defender.TakeDamage(Mathf.RoundToInt(finalDamage));
    }

    public static void ApplyKnockback(Rigidbody2D targetRb, Vector2 direction, float force)
    {
        if (targetRb != null)
        {
            targetRb.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        }
    }
}

[System.Serializable]
public class CharacterStats
{
    public float baseDamage = 10f;
    public float defense = 5f;
    public float criticalChance = 0.1f;
    public float criticalMultiplier = 1.5f;
    
    public void TakeDamage(int amount)
    {
        // To be implemented by the character class
    }
}