using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [Header("UI References")]
    public Button startButton;
    public Button optionsButton;
    public Button quitButton;
    public TextMeshProUGUI titleText;
    public Image backgroundImage;

    [Header("Settings")]
    public string gameSceneName = "World";
    public Color buttonNormalColor = new Color(0.2f, 0.2f, 0.2f, 0.8f);
    public Color buttonHoverColor = new Color(0.3f, 0.3f, 0.3f, 1f);

    void Start()
    {
        // Initialize UI elements
        SetupButtons();
        
        // Set cursor state
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    void SetupButtons()
    {
        // Add button listeners
        startButton.onClick.AddListener(StartGame);
        optionsButton.onClick.AddListener(OpenOptions);
        quitButton.onClick.AddListener(QuitGame);

        // Setup button appearance
        ColorBlock colors = startButton.colors;
        colors.normalColor = buttonNormalColor;
        colors.highlightedColor = buttonHoverColor;
        
        startButton.colors = colors;
        optionsButton.colors = colors;
        quitButton.colors = colors;
    }

    public void StartGame()
    {
        SoundManager.Instance.PlaySound("ButtonClick");
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenOptions()
    {
        SoundManager.Instance.PlaySound("ButtonClick");
        // TODO: Implement options menu
        Debug.Log("Options menu opened");
    }

    public void QuitGame()
    {
        SoundManager.Instance.PlaySound("ButtonClick");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}