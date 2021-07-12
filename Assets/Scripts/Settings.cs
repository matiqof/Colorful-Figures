using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Toggle soundsToggle;
        
    [SerializeField] private Toggle graphicsToggle;

    [SerializeField] private Slider volumeSlider;
    
    private AudioSource _music;
    
    private AudioSource _sounds;

    private void Start()
    {
        _music = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
        
        _sounds = GameObject.FindWithTag("Sounds").GetComponent<AudioSource>();

        SettingVolume();
        
        SettingSounds();
        
        SettingGraphics();
    }

    private void SettingVolume()
    {
        if (!PlayerPrefs.HasKey("volume")) PlayerPrefs.SetFloat("volume", 0.5f);

        volumeSlider.value = PlayerPrefs.GetFloat("volume");

        _sounds.volume = PlayerPrefs.GetFloat("volume");

        _music.volume = PlayerPrefs.GetFloat("volume");
    }

    private void SettingSounds()
    {
        if (PlayerPrefs.GetString("vfx").Equals("")) PlayerPrefs.SetString("vfx", "true");
        else if (PlayerPrefs.GetString("vfx").Equals("true"))
        {
            soundsToggle.isOn = true;
            
            _sounds.enabled = true;

            _music.enabled = true;
        }
        else
        {
            soundsToggle.isOn = false;
            
            _sounds.enabled = false;

            _music.enabled = false;
        }
    }
    
    private void SettingGraphics()
    {
        if (PlayerPrefs.GetString("graphics").Equals("")) PlayerPrefs.SetString("graphics", "true");
        else if (PlayerPrefs.GetString("graphics").Equals("true"))
        {
            graphicsToggle.isOn = true;
            
            QualitySettings.SetQualityLevel(0, true);
        }
        else
        {
            graphicsToggle.isOn = false;
            
            QualitySettings.SetQualityLevel(1, true);
        }
    }
    
    public void SetVolume()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        
        SettingVolume();
    }

    public void SetSounds()
    {
        PlayerPrefs.SetString("vfx", soundsToggle.isOn ? "true" : "false");
        
        SettingSounds();
    }
    
    public void SetGraphics()
    {
        PlayerPrefs.SetString("graphics", graphicsToggle.isOn ? "true" : "false");
        
        SettingGraphics();
    }
}
