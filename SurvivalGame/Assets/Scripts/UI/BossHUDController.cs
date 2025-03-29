using UnityEngine;

public class BossHUDController : MonoBehaviour
{
    [SerializeField] private HUDManager hudManager;
    
    private void OnEnable()
    {
        BossAI.OnBossSpawned += OnBossSpawned;
        BossAI.OnBossDefeated += OnBossDefeated;
    }
    
    private void OnDisable()
    {
        BossAI.OnBossSpawned -= OnBossSpawned;
        BossAI.OnBossDefeated -= OnBossDefeated;
    }
    
    private void OnBossSpawned(BossAI boss)
    {
        hudManager.ShowBossHealth(boss.bossName, boss.currentHealth, boss.maxHealth);
    }
    
    private void OnBossDefeated(BossAI boss)
    {
        hudManager.HideBossHealth();
    }
    
    private void Update()
    {
        // Could add logic here to update boss health in real-time
    }
}