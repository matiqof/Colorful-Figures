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

        _reviewColorIsTaken = PlayerPrefs.GetInt("color") == _reviewColorIndex;

        reviewColorImage.color = colorsValues[col];
        
        UpdateColorText(_myColors[_reviewColorIndex], _reviewColorIsTaken, colorsCosts[_reviewColorIndex]);
    }
    
    private void GetMyColors()
    {
        _myColors = PlayerPrefs.GetString("myColors").Split(" ".ToCharArray());

        if (!_myColors[0].Equals("")) return;
        
        _myColors = new string[colorsValues.Count];
            
        InitializeColors(); //Надо задать массив по умолчанию
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
        if (_myColors[_reviewColorIndex].Equals("false")) BuyColor(); //Фукнкиця покупки
        else TakeColor(); //Фукнция взятия
    }
    
    private void BuyColor()
    {
        if (PlayerPrefs.GetInt("score") >= colorsCosts[_reviewColorIndex])
        {
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") - colorsCosts[_reviewColorIndex]); //Подсчет очков
            
            PlayerPrefs.Save();

            _myColors[_reviewColorIndex] = "true"; //Задание выбранного цвета
            
            UpdateColorText(_myColors[_reviewColorIndex], _reviewColorIsTaken, colorsCosts[_reviewColorIndex]);

            SetMyColors();
            
            displayScore.UpdateScoreText(); //Обновление очков
        } else scoreTextAnimator.SetTrigger(NotEnoughScore); //Анимация малого количества очков
    }
    
    private void TakeColor()
    {
        PlayerPrefs.SetInt("color", _reviewColorIndex); //Задание выбранного цвета
        
        PlayerPrefs.Save();
        
        _reviewColorIsTaken = true;
        
        UpdateColorText(_myColors[_reviewColorIndex], _reviewColorIsTaken, colorsCosts[_reviewColorIndex]);
    }
    
    private void SetMyColors()
    {
        //Сохранения цветов
        
        var str = "";
        
        for (var i = 0; i < colorsValues.Count; i++)
        {
            str += _myColors[i] + " ";
        }
        
        PlayerPrefs.SetString("myColors", str);
        
        PlayerPrefs.Save();
    }
    
    private void UpdateColorText(string isMyColor, bool isTaken, int cost)
    {
        if (isTaken) coastColorText.text = "Цвет взят";
        else coastColorText.text = isMyColor.Equals("true") ? "Взять цвет" : "Купить за: " + cost;
    }
}
