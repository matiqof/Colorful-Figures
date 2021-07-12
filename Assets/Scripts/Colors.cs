using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Colors : MonoBehaviour
{
    [SerializeField] private List<Color> colorsValues;

    [SerializeField] private List<int> colorsCosts;
    
    [SerializeField] private Image reviewColorImage;

    [SerializeField] private TextMeshProUGUI coastColorText;
    
    [SerializeField] private DisplayScore displayScore;

    [SerializeField] private List<Toggle> colorsToggles;
    
    [SerializeField] private Animator scoreTextAnimator;

    private string[] _myColors;

    private int _reviewColorIndex;

    private bool _reviewColorIsTaken;
    
    private static readonly int NotEnoughScore = Animator.StringToHash("notEnoughScore");

    private void Start()
    {
        GetMyColors();
        
        _reviewColorIndex = PlayerPrefs.GetInt("color");

        colorsToggles[_reviewColorIndex].isOn = true;

        UpdateColor(_reviewColorIndex);
    }

    public void UpdateColor(int col)
    {
        _reviewColorIsTaken = false;
        
        _reviewColorIndex = col;

        if (PlayerPrefs.GetInt("color") == _reviewColorIndex) _reviewColorIsTaken = true;

        reviewColorImage.color = colorsValues[col];
        
        UpdateColorText(_myColors[_reviewColorIndex], _reviewColorIsTaken, colorsCosts[_reviewColorIndex]);
    }
    
    private void GetMyColors()
    {
        _myColors = PlayerPrefs.GetString("myColors").Split(" ".ToCharArray());

        if (!_myColors[0].Equals("")) return;
        
        _myColors = new string[colorsValues.Count];
            
        InitializeColors();
    }
    
    private void InitializeColors()
    {
        _myColors[0] = "true";
            
        for (var i = 1; i < colorsValues.Count; i++)
        {
            _myColors[i] = "false";
        }
    }
    
    public void GetOrBuyColor()
    {
        if (_myColors[_reviewColorIndex].Equals("false")) BuyColor(_reviewColorIndex);
        else TakeColor(_reviewColorIndex);
    }
    
    private void BuyColor(int index)
    {
        if (PlayerPrefs.GetInt("score") >= colorsCosts[index])
        {
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") - colorsCosts[index]);
            
            PlayerPrefs.Save();

            _myColors[index] = "true";
            
            UpdateColorText(_myColors[index], _reviewColorIsTaken, colorsCosts[index]);

            SetMyColors();
            
            displayScore.UpdateScoreText();
        } else scoreTextAnimator.SetTrigger(NotEnoughScore);
    }
    
    private void TakeColor(int index)
    {
        PlayerPrefs.SetInt("color", index);
        
        _reviewColorIsTaken = true;
        
        UpdateColorText(_myColors[index], _reviewColorIsTaken, colorsCosts[index]);
    }
    
    private void SetMyColors()
    {
        var str = "";
        
        for (var i = 1; i < colorsValues.Count; i++)
        {
            str += _myColors[i] + " ";
        }
        
        PlayerPrefs.SetString("myColors", str);
    }
    
    private void UpdateColorText(string isMyColor, bool isTaken, int cost)
    {
        if (isTaken) coastColorText.text = "Цвет взят";
        else coastColorText.text = isMyColor.Equals("true") ? "Взять цвет" : "Купить за: " + cost;
    }
}
