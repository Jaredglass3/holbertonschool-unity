using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Toggle invertYAxis;
    int Inverted = -1;

    // Reference to the BGMSlider in the UI
    public Slider bgmSlider;
    // Reference to the SFXSlider in the UI
    public Slider sfxSlider;
    // Reference to the MasterMixer
    public AudioMixer masterMixer;

    private float initialVolumeBGM;
    private float initialVolumeSFX;

    // Start is called before the first frame update
    void Start()
    {
        // Load saved Invert Y Axis value from PlayerPrefs
        if (PlayerPrefs.HasKey("InvertYToggle"))
            invertYAxis.isOn = PlayerPrefs.GetInt("InvertYToggle") == 0 ? false : true;

        // Load saved BGM volume value from PlayerPrefs
        float savedBGMVolume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        bgmSlider.value = savedBGMVolume;
        initialVolumeBGM = savedBGMVolume;

        // Load saved SFX volume value from PlayerPrefs
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        sfxSlider.value = savedSFXVolume;
        initialVolumeSFX = savedSFXVolume;

        // Set initial volumes based on saved values
        SetBGMVolume(savedBGMVolume);
        SetRunningVolume(savedSFXVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        SetBGMVolume(initialVolumeBGM);
        SetRunningVolume(initialVolumeSFX);

        string lastScene = PlayerPrefs.GetString("PreviousScene");
        SceneManager.LoadScene(lastScene);
    }

    public void Apply()
    {
        // Save the Invert Y Axis setting to PlayerPrefs
        PlayerPrefs.SetInt("InvertYToggle", invertYAxis.isOn ? 1 : 0);

        // Save the BGM and SFX volume values to PlayerPrefs
        PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);

        Back();
    }

    // Called when the BGMSlider value changes
    public void OnBGMSliderValueChanged(float volume)
    {
        // Update BGM volume based on slider value
        SetBGMVolume(volume);
    }

    // Called when the SFXSlider value changes
    public void OnSFXSliderValueChanged(float volume)
    {
        // Update SFX volume based on slider value
        SetRunningVolume(volume);
    }

    private void SetBGMVolume(float volume)
    {
        // Set the BGM volume using the Audio Mixer
        masterMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
    }

    private void SetRunningVolume(float volume)
    {
        // Set the SFX volume using the Audio Mixer
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }
}
