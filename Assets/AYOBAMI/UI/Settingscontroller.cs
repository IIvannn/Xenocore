using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

/// <summary>
/// Settings Controller
/// This handles Quality, Music Volume, SFX Volume, Fullscreen, Resolution, and VSync.
/// All settings are saved/loaded via PlayerPrefs automatically.
/// 
/// Attach this script to the Settings Panel GameObject.
/// An AudioMixer with exposed parameters is required "MusicVolume" and "SFXVolume".
/// </summary>
public class SettingsController : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Inspector References
    // -------------------------------------------------------------------------

    [Header("Audio")]
    [Tooltip("Assign your project's AudioMixer here.")]
    [SerializeField] private AudioMixer audioMixer;

    [Tooltip("Slider controlling music volume (0–1 range).")]
    [SerializeField] private Slider musicSlider;

    [Tooltip("Slider controlling SFX volume (0–1 range).")]
    [SerializeField] private Slider sfxSlider;

    [Header("Graphics")]
    [Tooltip("Dropdown for Unity Quality levels (auto-populated at runtime).")]
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [Tooltip("Dropdown listing available screen resolutions (auto-populated at runtime).")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    [Tooltip("Toggle for fullscreen mode.")]
    [SerializeField] private Toggle fullscreenToggle;

    [Tooltip("Toggle for VSync (On = 1 vblank, Off = 0).")]
    [SerializeField] private Toggle vsyncToggle;

    // -------------------------------------------------------------------------
    // PlayerPrefs Keys
    // -------------------------------------------------------------------------

    private const string PREF_MUSIC_VOL      = "MusicVolume";
    private const string PREF_SFX_VOL        = "SFXVolume";
    private const string PREF_QUALITY        = "QualityLevel";
    private const string PREF_RESOLUTION_IDX = "ResolutionIndex";
    private const string PREF_FULLSCREEN     = "Fullscreen";
    private const string PREF_VSYNC          = "VSync";

    // -------------------------------------------------------------------------
    // Private State
    // -------------------------------------------------------------------------

    /// <summary>All resolutions supported by the current display.</summary>
    private Resolution[] _resolutions;

    // -------------------------------------------------------------------------
    // Unity Lifecycle
    // -------------------------------------------------------------------------

    public void CloseSettings()
    {
        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        // Re-apply saved settings every time the panel opens
        LoadAndApplyAllSettings();
    }

    private void Start()
    {
        PopulateResolutionDropdown();
        PopulateQualityDropdown();
        LoadAndApplyAllSettings();
    }

    // -------------------------------------------------------------------------
    // Initialisation Helpers
    // -------------------------------------------------------------------------

    /// <summary>
    /// Fills the resolution dropdown with all resolutions the display supports,
    /// filtered to unique width×height pairs at the display's refresh rate.
    /// </summary>
    private void PopulateResolutionDropdown()
    {
        if (resolutionDropdown == null) return;

        _resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        var options   = new System.Collections.Generic.List<string>();
        int savedIdx  = PlayerPrefs.GetInt(PREF_RESOLUTION_IDX, -1);
        int currentIdx = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string label = $"{_resolutions[i].width} x {_resolutions[i].height}";
            if (!options.Contains(label))
                options.Add(label);

            // Default to the closest match to the current screen resolution
            if (savedIdx == -1 &&
                _resolutions[i].width  == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height)
            {
                currentIdx = options.Count - 1;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = savedIdx >= 0 ? savedIdx : currentIdx;
        resolutionDropdown.RefreshShownValue();
    }

    /// <summary>
    /// Fills the quality dropdown with Unity's Quality Settings names.
    /// </summary>
    private void PopulateQualityDropdown()
    {
        if (qualityDropdown == null) return;

        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new System.Collections.Generic.List<string>(QualitySettings.names));
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();
    }

    // -------------------------------------------------------------------------
    // Load & Apply
    // -------------------------------------------------------------------------

    /// <summary>
    /// Reads all values from PlayerPrefs and applies them to the UI controls
    /// and actual engine/system settings.
    /// </summary>
    private void LoadAndApplyAllSettings()
    {
        // Music
        float music = PlayerPrefs.GetFloat(PREF_MUSIC_VOL, 0.75f);
        if (musicSlider != null) musicSlider.value = music;
        ApplyMusicVolume(music);

        // SFX
        float sfx = PlayerPrefs.GetFloat(PREF_SFX_VOL, 0.75f);
        if (sfxSlider != null) sfxSlider.value = sfx;
        ApplySFXVolume(sfx);

        // Quality
        int quality = PlayerPrefs.GetInt(PREF_QUALITY, QualitySettings.GetQualityLevel());
        if (qualityDropdown != null) qualityDropdown.value = quality;
        ApplyQuality(quality);

        // Resolution
        int resIdx = PlayerPrefs.GetInt(PREF_RESOLUTION_IDX, -1);
        if (resolutionDropdown != null && resIdx >= 0)
            resolutionDropdown.value = resIdx;

        // Fullscreen
        bool fs = PlayerPrefs.GetInt(PREF_FULLSCREEN, Screen.fullScreen ? 1 : 0) == 1;
        if (fullscreenToggle != null) fullscreenToggle.isOn = fs;
        ApplyFullscreen(fs);

        // VSync
        bool vs = PlayerPrefs.GetInt(PREF_VSYNC, QualitySettings.vSyncCount > 0 ? 1 : 0) == 1;
        if (vsyncToggle != null) vsyncToggle.isOn = vs;
        ApplyVSync(vs);
    }

    // -------------------------------------------------------------------------
    // Public Setters — call these from UI OnValueChanged / OnClick events
    // -------------------------------------------------------------------------

    /// <summary>
    /// Called by the Music Slider's OnValueChanged event.
    /// value: 0.0001 – 1  (slider min should be 0.0001 to avoid -80 dB silence)
    /// </summary>
    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat(PREF_MUSIC_VOL, value);
        ApplyMusicVolume(value);
    }

    /// <summary>
    /// Called by the SFX Slider's OnValueChanged event.
    /// </summary>
    public void SetSFXVolume(float value)
    {
        PlayerPrefs.SetFloat(PREF_SFX_VOL, value);
        ApplySFXVolume(value);
    }

    /// <summary>
    /// Called by the Quality Dropdown's OnValueChanged event.
    /// index maps directly to QualitySettings.names[].
    /// </summary>
    public void SetQuality(int index)
    {
        PlayerPrefs.SetInt(PREF_QUALITY, index);
        ApplyQuality(index);
    }

    /// <summary>
    /// Called by the Resolution Dropdown's OnValueChanged event.
    /// </summary>
    public void SetResolution(int index)
    {
        PlayerPrefs.SetInt(PREF_RESOLUTION_IDX, index);
        ApplyResolution(index);
    }

    /// <summary>
    /// Called by the Fullscreen Toggle's OnValueChanged event.
    /// </summary>
    public void SetFullscreen(bool isFullscreen)
    {
        PlayerPrefs.SetInt(PREF_FULLSCREEN, isFullscreen ? 1 : 0);
        ApplyFullscreen(isFullscreen);
    }

    /// <summary>
    /// Called by the VSync Toggle's OnValueChanged event.
    /// </summary>
    public void SetVSync(bool enabled)
    {
        PlayerPrefs.SetInt(PREF_VSYNC, enabled ? 1 : 0);
        ApplyVSync(enabled);
    }

    /// <summary>
    /// Saves all current UI values to PlayerPrefs immediately.
    /// Call from an explicit "Apply" or "Save" button if desired;
    /// settings are also auto-saved on each individual change.
    /// </summary>
    public void SaveSettings()
    {
        PlayerPrefs.Save();
        Debug.Log("[SettingsController] All settings saved.");
    }

    /// <summary>
    /// Resets every setting to its default value, updates the UI, and saves.
    /// Wire this to a "Reset to Defaults" button.
    /// </summary>
    public void ResetToDefaults()
    {
        PlayerPrefs.DeleteKey(PREF_MUSIC_VOL);
        PlayerPrefs.DeleteKey(PREF_SFX_VOL);
        PlayerPrefs.DeleteKey(PREF_QUALITY);
        PlayerPrefs.DeleteKey(PREF_RESOLUTION_IDX);
        PlayerPrefs.DeleteKey(PREF_FULLSCREEN);
        PlayerPrefs.DeleteKey(PREF_VSYNC);

        LoadAndApplyAllSettings();
        PopulateResolutionDropdown();
        Debug.Log("[SettingsController] Settings reset to defaults.");
    }

    // -------------------------------------------------------------------------
    // Private Apply Helpers — separate from setters so LoadAndApply can call them
    // -------------------------------------------------------------------------

    private void ApplyMusicVolume(float value)
    {
        if (audioMixer == null) return;
        // Convert linear (0–1) to logarithmic dB. Clamp to avoid log(0).
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f);
    }

    private void ApplySFXVolume(float value)
    {
        if (audioMixer == null) return;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f);
    }

    private void ApplyQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    private void ApplyResolution(int index)
    {
        if (_resolutions == null || index < 0 || index >= _resolutions.Length) return;
        Resolution r = _resolutions[index];
        Screen.SetResolution(r.width, r.height, Screen.fullScreen);
    }

    private void ApplyFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    private void ApplyVSync(bool enabled)
    {
        QualitySettings.vSyncCount = enabled ? 1 : 0;
    }
}