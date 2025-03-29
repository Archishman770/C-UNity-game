using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Settings")]
    public float hoverScale = 1.1f;
    public float transitionSpeed = 5f;
    public AudioClip hoverSound;

    private Vector3 originalScale;
    private AudioSource audioSource;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        originalScale = transform.localScale;
        
        // Add AudioSource if hover sound is specified
        if (hoverSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = hoverSound;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable)
        {
            // Scale up on hover
            LeanTween.scale(gameObject, originalScale * hoverScale, transitionSpeed * Time.deltaTime);
            
            // Play sound if available
            if (audioSource != null && hoverSound != null)
            {
                audioSource.Play();
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Return to original scale
        LeanTween.scale(gameObject, originalScale, transitionSpeed * Time.deltaTime);
    }
}