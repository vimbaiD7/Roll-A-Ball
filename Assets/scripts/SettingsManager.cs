using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [Header("Volume Controls")]
    public Slider volumeSlider;
    public TextMeshProUGUI volumeValueText;
    
    [Header("Other Settings")]
    public Toggle fullscreenToggle;
    
    private const string VolumeKey = "MasterVolume";
    
    void Start()
    {
        LoadSettings();
    }
    
    void LoadSettings()
    {
        // Load volume
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 0.5f);
        
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
        
        UpdateVolumeDisplay(savedVolume);
        ApplyVolume(savedVolume);
        
        // Load fullscreen
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
        }
    }
    
    void OnVolumeChanged(float value)
    {
        UpdateVolumeDisplay(value);
        ApplyVolume(value);
        SaveVolume(value);
    }
    
    void UpdateVolumeDisplay(float value)
    {
        if (volumeValueText != null)
        {
            int percentage = Mathf.RoundToInt(value * 100);
            volumeValueText.text = percentage.ToString() + "%";
        }
    }
    
    void ApplyVolume(float value)
    {
        AudioListener.volume = value;
        
        // Also update MusicManager if it exists
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.SetVolume(value);
        }
    }
    
    void SaveVolume(float value)
    {
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
    }
    
    void OnFullscreenChanged(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    
    public void ResetSettings()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = 0.5f;
        }
        
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = true;
        }
    }
}
