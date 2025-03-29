using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "Audio/Audio Data")]
public class AudioData : ScriptableObject
{
    [Header("Player Sounds")]
    public AudioClip[] footstepSounds;
    public AudioClip attackSound;
    public AudioClip dodgeSound;
    public AudioClip damageSound;
    public AudioClip deathSound;
    public AudioClip hungerSound;
    public AudioClip thirstSound;

    [Header("Boss Sounds")]
    public AudioClip bossSpawnSound;
    public AudioClip bossAttackSound;
    public AudioClip bossDamageSound;
    public AudioClip bossDeathSound;
    public AudioClip bossEnrageSound;

    [Header("UI Sounds")]
    public AudioClip buttonHoverSound;
    public AudioClip buttonClickSound;
    public AudioClip menuOpenSound;
    public AudioClip menuCloseSound;

    [Header("Environment Sounds")]
    public AudioClip ambientDaySound;
    public AudioClip ambientNightSound;
    public AudioClip rainSound;
    public AudioClip windSound;
    public AudioClip resourceGatherSound;

    [Header("Music")]
    public AudioClip mainMenuMusic;
    public AudioClip explorationMusic;
    public AudioClip bossMusic;
    public AudioClip victoryMusic;
}