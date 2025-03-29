using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Player Stats")]
    public Slider healthSlider;
    public Slider hungerSlider;
    public Slider thirstSlider;
    public Slider staminaSlider;
    
    [Header("Boss Info")] 
    public GameObject bossHealthPanel;
    public Slider bossHealthSlider;
    public Text bossNameText;
    
    private PlayerStats playerStats;
    
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        
        if (playerStats == null)
        {
            Debug.LogError("No PlayerStats found in scene!");
            return;
        }
        
        // Initialize sliders
        healthSlider.maxValue = playerStats.maxHealth;
        hungerSlider.maxValue = playerStats.maxHunger;
        thirstSlider.maxValue = playerStats.maxThirst;
        staminaSlider.maxValue = playerStats.maxStamina;
        
        // Hide boss health by default
        bossHealthPanel.SetActive(false);
    }
    
    void Update()
    {
        if (playerStats == null) return;
        
        // Update player stat displays
        healthSlider.value = playerStats.currentHealth;
        hungerSlider.value = playerStats.currentHunger;
        thirstSlider.value = playerStats.currentThirst;
        staminaSlider.value = playerStats.currentStamina;
    }
    
    public void ShowBossHealth(string bossName, float currentHealth, float maxHealth)
    {
        bossNameText.text = bossName;
        bossHealthSlider.maxValue = maxHealth;
        bossHealthSlider.value = currentHealth;
        bossHealthPanel.SetActive(true);
    }
    
    public void HideBossHealth()
    {
        bossHealthPanel.SetActive(false);
    }
    
    public void UpdateBossHealth(float currentHealth)
    {
        bossHealthSlider.value = currentHealth;
    }
}