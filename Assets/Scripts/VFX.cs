using UnityEngine;

public class VFX : MonoBehaviour
{
    [SerializeField] private GameObject musicPrefab;
    
    [SerializeField] private GameObject soundsPrefab;

    private void Awake()
    {
        SpawnMusic();
        
        SpawnSounds();
    }

    private void SpawnMusic()
    {
        //Спавн объекта музыки
        
        if (PlayerPrefs.HasKey("music")) return;

        Instantiate(musicPrefab);

        PlayerPrefs.SetString("music", "true");
    }
    
    private void SpawnSounds()
    {
        //Спавн объекта звуков
        
        if (PlayerPrefs.HasKey("sounds")) return;

        Instantiate(soundsPrefab);

        PlayerPrefs.SetString("sounds", "true");
    }
}
