using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class Skins : MonoBehaviour
{
    [SerializeField] private List<Sprite> skinsSprites;
    
    [SerializeField] private List<int> skinsCosts;
    
    [SerializeField] private Image reviewSkinImage;

    [SerializeField] private TextMeshProUGUI coastSkinText;
    
    [SerializeField] private DisplayScore displayScore;

    [SerializeField] private Animator scoreTextAnimator;

    private string[] _mySkins;

    private int _reviewSkinIndex;

    private bool _reviewSkinIsTaken;
    
    private static readonly int NotEnoughScore = Animator.StringToHash("notEnoughScore");

    private void Start()
    {
        GetMySkins();
        
        _reviewSkinIndex = PlayerPrefs.GetInt("skin");

        UpdateSkin(0);
    }

    public void NextSkin()
    {
        UpdateSkin(1);
    }

    public void PreviousSkin()
    {
        UpdateSkin(-1);
    }

    private void UpdateSkin(int pos)
    { 
        _reviewSkinIsTaken = false;

        switch (pos)
        {
            case 1:
                _reviewSkinIndex++;
                
                break;
            case -1:
                _reviewSkinIndex--;
                
                break;
            default:
                _reviewSkinIsTaken = true;
                
                break;
        }
        
        if (_reviewSkinIndex < 0) _reviewSkinIndex = skinsSprites.Count - 1;
        else if (_reviewSkinIndex > skinsSprites.Count - 1) _reviewSkinIndex = 0;
        
        if (PlayerPrefs.GetInt("skin") == _reviewSkinIndex) _reviewSkinIsTaken = true;
        
        reviewSkinImage.sprite = skinsSprites[_reviewSkinIndex];

        UpdateSkinText(_mySkins[_reviewSkinIndex], _reviewSkinIsTaken, skinsCosts[_reviewSkinIndex]);
    }
    
    private void GetMySkins()
    {
        _mySkins = PlayerPrefs.GetString("mySkins").Split(" ".ToCharArray());

        if (!_mySkins[0].Equals("")) return;
        
        _mySkins = new string[skinsSprites.Count];
            
        InitializeSkins();
    }
    
    private void InitializeSkins()
    {
        _mySkins[0] = "true";
            
        for (var i = 1; i < skinsSprites.Count; i++)
        {
            _mySkins[i] = "false";
        }
    }
    
    public void GetOrBuySkin()
    {
        if (_mySkins[_reviewSkinIndex].Equals("false")) BuySkin(_reviewSkinIndex);
        else TakeSkin(_reviewSkinIndex);
    }
    
    private void BuySkin(int index)
    {
        if (PlayerPrefs.GetInt("score") >= skinsCosts[index])
        {
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") - skinsCosts[index]);
            
            PlayerPrefs.Save();

            _mySkins[index] = "true";
            
            UpdateSkinText(_mySkins[index], _reviewSkinIsTaken, skinsCosts[index]);

            SetMySkins();
            
            displayScore.UpdateScoreText();
        } else scoreTextAnimator.SetTrigger(NotEnoughScore);
    }
    
    private void TakeSkin(int index)
    {
        PlayerPrefs.SetInt("skin", index);
        
        _reviewSkinIsTaken = true;
        
        UpdateSkinText(_mySkins[index], _reviewSkinIsTaken, skinsCosts[index]);
    }
    
    private void SetMySkins()
    {
        var str = "";
        
        for (var i = 1; i < skinsSprites.Count; i++)
        {
            str += _mySkins[i] + " ";
        }
        
        PlayerPrefs.SetString("mySkins", str);
    }
    
    private void UpdateSkinText(string isMySkin, bool isTaken, int cost)
    {
        if (isTaken) coastSkinText.text = "Скин взят";
        else coastSkinText.text = isMySkin.Equals("true") ? "Взять скин" : "Купить за: " + cost;
    }
}
