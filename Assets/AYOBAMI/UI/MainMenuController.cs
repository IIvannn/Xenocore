using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Main Menu Controller
/// Handles Play, Settings, and Quit functionality from the main menu UI.
/// Attach this script to a GameObject in the main menu scene (e.g. "MainMenuManager").
/// </summary>
public class MainMenuController : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("The exact name of the scene to load when Play is pressed (must be added to Build Settings).")]
    [SerializeField] private string sceneToLoad = "GameScene";

    [Header("UI Panels")]
    [Tooltip("Assign your Settings Panel GameObject here.")]
    [SerializeField] private GameObject settingsPanel;

    [Header("Buttons (optional — wire up in Inspector or via OnClick)")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button closeSettingsButton;

    public Image problem;



    public void Update()
    {
        if (settingsPanel.activeSelf)
        {
            problem.gameObject.SetActive(false);
        }
        else
        {
            problem.gameObject.SetActive(true);
        }
    }

    // -------------------------------------------------------------------------
    // Unity Lifecycle
    // -------------------------------------------------------------------------

    private void Awake()
    {
        // This ensures the settings panel is hidden at startup
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    private void Start()
    {
        // This would wire up the buttons programmatically (safe if already wired in the Inspector — no duplicates)
        if (playButton     != null) playButton    .onClick.AddListener(OnPlayButtonPressed);
        if (settingsButton != null) settingsButton.onClick.AddListener(OnSettingsButtonPressed);
        if (quitButton     != null) quitButton    .onClick.AddListener(OnQuitButtonPressed);
        if (closeSettingsButton != null) closeSettingsButton.onClick.AddListener(CloseSettingsPanel);
    }

    // -------------------------------------------------------------------------
    // Button Handlers — call these from Button OnClick() events in the Inspector
    // -------------------------------------------------------------------------

    /// <summary>
    /// Called by the Play button.
    /// Loads the scene specified in sceneToLoad.
    /// Make sure the scene is added to File > Build Settings.
    /// </summary>
    public void OnPlayButtonPressed()
    {
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogWarning("[MainMenuController] sceneToLoad is empty. Please set a scene name in the Inspector.");
            return;
        }

        Debug.Log($"[MainMenuController] Loading scene: {sceneToLoad}");
        SceneManager.LoadScene(sceneToLoad);
    }

    /// <summary>
    /// Called by the Settings button.
    /// Opens (shows) the Settings panel.
    /// </summary>
    public void OnSettingsButtonPressed()
    {
        OpenSettingsPanel();
    }

    /// <summary>
    /// Called by the Quit button.
    /// Exits the application (works in builds; stops play mode in the Editor).
    /// </summary>
    public void OnQuitButtonPressed()
    {
        Debug.Log("[MainMenuController] Quitting application.");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // -------------------------------------------------------------------------
    // Settings Panel Helpers
    // -------------------------------------------------------------------------

    /// <summary>
    /// Shows the Settings panel.
    /// </summary>
    public void OpenSettingsPanel()
    {
        if (settingsPanel == null)
        {
            Debug.LogWarning("[MainMenuController] settingsPanel is not assigned in the Inspector.");
            return;
        }

        settingsPanel.SetActive(true);
        Debug.Log("[MainMenuController] Settings panel opened.");
    }

    /// <summary>
    /// Hides the Settings panel.
    /// Call this from a Close/Back button inside the panel.
    /// </summary>
    public void CloseSettingsPanel()
    {
        if (settingsPanel == null) return;

        settingsPanel.SetActive(false);
        Debug.Log("[MainMenuController] Settings panel closed.");
    }
}