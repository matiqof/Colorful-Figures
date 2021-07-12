using UnityEngine;
using UnityEngine.EventSystems;

public class Secret : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private DisplayScore displayScore;

    private Sounds _sounds;
    
    private float _lastTimeClick;

    private void Start()
    {
        if (PlayerPrefs.GetString("vfx").Equals("true")) _sounds = GameObject.FindWithTag("Sounds").GetComponent<Sounds>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var currentTimeClick = eventData.clickTime;
        
        if(Mathf.Abs(currentTimeClick - _lastTimeClick) < 0.75f) FindSecret();
        
        _lastTimeClick = currentTimeClick;
    }
    
    private void FindSecret()
    {
        if (PlayerPrefs.GetString("secret").Equals("true")) return;
        
        if (PlayerPrefs.GetString("vfx").Equals("true")) _sounds.PlaySound(4);

        PlayerPrefs.SetString("secret", "true");

        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 999);

        PlayerPrefs.Save();
            
        displayScore.UpdateScoreText();
    }
}
