using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTextMainMenu;
    [SerializeField] private TextMeshProUGUI scoreTextCustomize;
    [SerializeField] private TextMeshProUGUI maxScoreTextMainMenu;
    [SerializeField] private TextMeshProUGUI earnedScoreTextPause;
    [SerializeField] private TextMeshProUGUI earnedScoreTextGame;
    
    private bool _isEarnedScoreTextPauseNotNull;
    
    private bool _isEarnedScoreTextGameNotNull;
    
    private bool _isMaxScoreTextMainMenuNotNull;

    private void Start()
    {
        _isMaxScoreTextMainMenuNotNull = maxScoreTextMainMenu != null;
        
        _isEarnedScoreTextGameNotNull = earnedScoreTextGame != null;
        
        _isEarnedScoreTextPauseNotNull = earnedScoreTextPause != null;
        
        UpdateAll();
    }

    private void UpdateAll()
    {
        UpdateScoreText();
        
        DisplayMaxScoreTextMainMenu();
    }

    public void UpdateScoreText()
    {
        DisplayScoreTextMainMenu();

        DisplayScoreTextCustomize();
    }

    public void UpdatePauseScoreText()
    {
        DisplayEarnedScoreTextPause();
        
        DisplayMaxScoreTextMainMenu();
    }

    private void DisplayEarnedScoreTextPause()
    {
        if (_isEarnedScoreTextPauseNotNull && _isEarnedScoreTextGameNotNull)
            earnedScoreTextPause.text = "Заработанные очки: " + "\n" + earnedScoreTextGame.text;
    }

    private void DisplayScoreTextMainMenu()
    {
        if (scoreTextMainMenu != null) scoreTextMainMenu.text = "Твои очки:" + "\n" + PlayerPrefs.GetInt("score");
    }
    
    private void DisplayScoreTextCustomize()
    {
        if (scoreTextCustomize != null) scoreTextCustomize.text = "Твои очки: " + PlayerPrefs.GetInt("score");
    }

    private void DisplayMaxScoreTextMainMenu()
    {
        if (_isMaxScoreTextMainMenuNotNull) 
            maxScoreTextMainMenu.text = "Максимальные очки: " + "\n" + PlayerPrefs.GetInt("maxScore");
    }
}
