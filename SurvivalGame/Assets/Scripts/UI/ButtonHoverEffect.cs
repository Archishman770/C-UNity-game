using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Settings")]
    public float hoverScale = 1.1f;
    public float transitionSpeed = 5f;

    private Vector3 originalScale;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable)
        {
            // Scale up on hover
            LeanTween.scale(gameObject, originalScale * hoverScale, transitionSpeed * Time.deltaTime);
            
            // Play hover sound
            SoundManager.Instance.PlaySound("ButtonHover");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Return to original scale
        LeanTween.scale(gameObject, originalScale, transitionSpeed * Time.deltaTime);
    }
}