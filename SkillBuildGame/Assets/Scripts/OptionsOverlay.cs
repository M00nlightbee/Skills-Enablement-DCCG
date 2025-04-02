using UnityEngine;
using UnityEngine.UI;

public class OptionsOverlay : MonoBehaviour
{
    public GameObject optionsPanel;

    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Dropdown frameRateDropdown;

    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;

    Resolution[] resolutions;

    void Start()
    {
        optionsPanel.SetActive(false);

        // Set up resolution dropdown
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionDropdown.options.Add(new Dropdown.OptionData(option));
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();

        // Quality dropdown
        qualityDropdown.ClearOptions();
        foreach (string q in QualitySettings.names)
        {
            qualityDropdown.options.Add(new Dropdown.OptionData(q));
        }
        qualityDropdown.value = QualitySettings.GetQualityLevel();

        // Frame rate dropdown
        frameRateDropdown.ClearOptions();
        frameRateDropdown.AddOptions(new System.Collections.Generic.List<string> { "30", "60", "120" });

        // Sliders
        masterVolumeSlider.value = AudioListener.volume;
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void ApplyResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, true);
    }

    public void ApplyQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void ApplyFrameRate(int index)
    {
        int[] fps = { 30, 60, 120 };
        Application.targetFrameRate = fps[index];
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}
