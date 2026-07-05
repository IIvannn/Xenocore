using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

/// <summary>
/// Pause Menu Controller
/// Handles opening/closing the pause menu (via button or Escape key),
/// pausing/resuming gameplay via Time.timeScale, and navigating to
/// Settings, Controls, and Main Menu panels.
///
/// Attach this script to a persistent GameObject in your game scene
/// (e.g. "PauseMenuManager"). It does NOT need to be on the panel itself.
/// </summary>
public class PauseMenuController : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Inspector References
    // -------------------------------------------------------------------------

    [Header("Panels")]
    [Tooltip("The root pause menu panel GameObject.")]
    [SerializeField] private GameObject pauseMenuPanel;

    [Tooltip("The settings panel to open from within the pause menu.")]
    [SerializeField] private GameObject settingsPanel;

    [Tooltip("The controls panel (key bindings / controller layout).")]
    [SerializeField] private GameObject controlsPanel;

    [Header("Scene")]
    [Tooltip("Exact name of the Main Menu scene (must be in Build Settings).")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    [Header("Buttons (optional — can also wire via OnClick in Inspector)")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button closeSettingsButton;
    [SerializeField] private Button closeControlsButton;

    [Header("Input")]
    [Tooltip("Key used to toggle the pause menu. Defaults to Escape.")]
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

    // -------------------------------------------------------------------------
    // State
    // -------------------------------------------------------------------------

    /// <summary>True when the pause menu (or a sub-panel) is currently open.</summary>
    public bool IsPaused { get; private set; }

    // -------------------------------------------------------------------------
    // Unity Lifecycle
    // -------------------------------------------------------------------------

    private void Awake()
    {
        // Ensure all panels start hidden
        SetPanelActive(pauseMenuPanel,  false);
        SetPanelActive(settingsPanel,   false);
        SetPanelActive(controlsPanel,   false);
    }

    private void Start()
    {
        // Register button listeners programmatically
        // (safe even if you also wired them in the Inspector — listeners are additive,
        //  so pick ONE method: either wire here or wire in Inspector, not both)
        if (pauseButton        != null) pauseButton       .onClick.AddListener(TogglePause);
        if (resumeButton       != null) resumeButton      .onClick.AddListener(ResumeGame);
        if (settingsButton     != null) settingsButton    .onClick.AddListener(OpenSettings);
        if (controlsButton     != null) controlsButton    .onClick.AddListener(OpenControls);
        if (mainMenuButton     != null) mainMenuButton    .onClick.AddListener(ReturnToMainMenu);
        if (closeSettingsButton != null) closeSettingsButton.onClick.AddListener(CloseSettings);
        if (closeControlsButton != null) closeControlsButton.onClick.AddListener(CloseControls);
    }

    private void Update()
    {
        // Toggle pause on Escape (or whichever key is set in the Inspector)
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            { TogglePause(); }
    }

    // -------------------------------------------------------------------------
    // Core Pause / Resume
    // -------------------------------------------------------------------------

    /// <summary>
    /// Toggles the pause state. Opens the pause panel if not paused; resumes if paused.
    /// Sub-panels (Settings, Controls) are closed before the main panel is hidden.
    /// </summary>
    public void TogglePause()
    {
        if (IsPaused)
            ResumeGame();
        else if (!settingsPanel.activeSelf)
            PauseGame();
    }

    /// <summary>
    /// Pauses gameplay: freezes Time.timeScale, enables mouse cursor,
    /// and shows the pause menu panel.
    /// </summary>
    public void PauseGame()
    {
        IsPaused = true;

        Time.timeScale = 0f;
        SetCursorState(visible: true, locked: false);

        // Close sub-panels in case they were left open
        SetPanelActive(settingsPanel, false);
        SetPanelActive(controlsPanel, false);

        SetPanelActive(pauseMenuPanel, true);

        Debug.Log("[PauseMenuController] Game paused.");
    }

    /// <summary>
    /// Resumes gameplay: restores Time.timeScale, hides cursor (re-locks for
    /// first-person games — adjust CursorLockMode below as needed),
    /// and hides all pause panels.
    /// </summary>
    public void ResumeGame()
    {
        IsPaused = false;

        Time.timeScale = 1f;

        // Adjust CursorLockMode to match your game type:
        //   CursorLockMode.Locked  → first-person / action games
        //   CursorLockMode.None    → top-down / RTS / point-and-click
        SetCursorState(visible: true, locked: false);

        SetPanelActive(pauseMenuPanel, false);
        SetPanelActive(settingsPanel,  false);
        SetPanelActive(controlsPanel,  false);

        Debug.Log("[PauseMenuController] Game resumed.");
        gameObject.SetActive(false);
    }

    // -------------------------------------------------------------------------
    // Button Handlers
    // -------------------------------------------------------------------------

    /// <summary>
    /// Opens the settings panel and hides the main pause panel so they don't overlap.
    /// </summary>
    /// 

    public void sett()
    {
        settingsPanel.SetActive(true);
    }

    public void OpenSettings()
    {
        SetPanelActive(pauseMenuPanel, false);
        SetPanelActive(controlsPanel,  false);
        SetPanelActive(settingsPanel,  true);
        Debug.Log("[PauseMenuController] Settings panel opened.");
        settingsPanel.SetActive(true);
    }

    /// <summary>
    /// Closes the settings panel and returns to the main pause panel.
    /// </summary>
    public void CloseSettings()
    {
        SetPanelActive(settingsPanel,  false);
        SetPanelActive(pauseMenuPanel, true);
        Debug.Log("[PauseMenuController] Settings panel closed.");
    }

    /// <summary>
    /// Opens the controls panel and hides the main pause panel.
    /// </summary>
    public void OpenControls()
    {
        SetPanelActive(pauseMenuPanel, false);
        SetPanelActive(settingsPanel,  false);
        SetPanelActive(controlsPanel,  true);
        Debug.Log("[PauseMenuController] Controls panel opened.");
    }

    /// <summary>
    /// Closes the controls panel and returns to the main pause panel.
    /// </summary>
    public void CloseControls()
    {
        SetPanelActive(controlsPanel,  false);
        SetPanelActive(pauseMenuPanel, true);
        Debug.Log("[PauseMenuController] Controls panel closed.");
    }

    /// <summary>
    /// Returns to the Main Menu: restores time scale and cursor before loading the scene.
    /// Failing to reset timeScale would cause the main menu to load in a frozen state.
    /// </summary>
    public void ReturnToMainMenu()
    {
        // Always restore state before a scene load
        Time.timeScale = 1f;
        SetCursorState(visible: true, locked: false);

        Debug.Log($"[PauseMenuController] Returning to main menu: {mainMenuSceneName}");
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuSceneName);
    }

    // -------------------------------------------------------------------------
    // Helpers
    // -------------------------------------------------------------------------

    /// <summary>
    /// Null-safe wrapper for SetActive to avoid NullReferenceExceptions when
    /// a panel slot is intentionally left unassigned in the Inspector.
    /// </summary>
    private void SetPanelActive(GameObject panel, bool active)
    {
        if (panel != null)
            panel.SetActive(active);
    }

    /// <summary>
    /// Sets cursor visibility and lock state together — they should always change as a pair.
    /// </summary>
    /// <param name="visible">Whether the cursor sprite is shown.</param>
    /// <param name="locked">If true, locks cursor to the center of the screen (Locked mode).</param>
    private void SetCursorState(bool visible, bool locked)
    {
        Cursor.visible   = visible;
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
    }

    // -------------------------------------------------------------------------
    // Application Focus Safety Net
    // -------------------------------------------------------------------------

    /// <summary>
    /// Restores the correct cursor state when the application regains focus.
    /// Without this, tabbing out and back in can leave the cursor in the wrong state.
    /// </summary>
    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) return;

        if (IsPaused)
            SetCursorState(visible: true,  locked: false);
        else
            SetCursorState(visible: true, locked: false);
    }
}