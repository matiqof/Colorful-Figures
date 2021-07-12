using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    [SerializeField] private Obstacles obstacles;
    
    [SerializeField] private int score;

    [SerializeField] private int increaseTime = 15;

    [SerializeField] private int increaseValue = 1;
    
    [SerializeField] private float followHeight = 1f;
    
    private Sounds _sounds;

    private Vector2 _playerPosition;

    private TextMeshProUGUI _scoreText;

    private bool _work = true;

    private void Start()
    {
        _scoreText = gameObject.GetComponent<TextMeshProUGUI>();
        
        InvokeRepeating(nameof(ReceiveScore), 1f, 1f);
        
        if (PlayerPrefs.GetString("vfx").Equals("true")) _sounds = GameObject.FindWithTag("Sounds").GetComponent<Sounds>();
    }

    private void FixedUpdate()
    {
        _playerPosition = player.transform.position;

        transform.position = new Vector2(_playerPosition.x, _playerPosition.y + followHeight); 
    }

    private void ReceiveScore()
    {
        if (!_work) return;
        
        if (score != 0 && score % increaseTime == 0 && obstacles.GETMoveSpeed() + increaseValue
            <= obstacles.GETMaxMoveSpeed())
        {
            if (PlayerPrefs.GetString("vfx").Equals("true")) _sounds.PlaySound(5);
                
            obstacles.SetMoveSpeed(obstacles.GETMoveSpeed() + increaseValue);
        }

        score += 1;

        _scoreText.SetText(score.ToString());
    }

    public void SetTimerWork(bool w)
    {
        _work = w;
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + score);
        
        if (PlayerPrefs.GetInt("maxScore") < score) PlayerPrefs.SetInt("maxScore", score);
        
        PlayerPrefs.Save();
    }
}
