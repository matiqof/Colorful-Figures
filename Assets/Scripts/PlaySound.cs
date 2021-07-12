using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySound : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int sound;
    
    private Sounds _sounds;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (PlayerPrefs.GetString("vfx").Equals("true")) _sounds = GameObject.FindWithTag("Sounds").GetComponent<Sounds>();
        
        if (PlayerPrefs.GetString("vfx").Equals("true")) _sounds.PlaySound(sound);
    }
}
