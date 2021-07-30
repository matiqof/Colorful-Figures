using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = .2f;
    
    [SerializeField] private Score score;

    [SerializeField] private LoadScenes sceneLoader;

    [SerializeField] private SceneTransition sceneTransition;

    [SerializeField] private Pause pause;

    [SerializeField] private List<Sprite> skins;

    [SerializeField] private List<Color> colors;

    private Sounds _sounds;

    private SpriteRenderer _spriteRenderer;

    private bool _playerAlive = true;

    private bool _die;
    
    private Camera _mainCamera;

    private Vector3 _playerPosition;

    private void Start()
    {
        if (PlayerPrefs.GetString("vfx").Equals("true")) _sounds = GameObject.FindWithTag("Sounds").GetComponent<Sounds>();
    
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _spriteRenderer.sprite = skins[PlayerPrefs.GetInt("skin")];

        _spriteRenderer.color = colors[PlayerPrefs.GetInt("color")];
        
        _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (_playerAlive && !pause.GETPause()) PlayerMove();
        else if (!_die && !pause.GETPause()) PlayerDie();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle")) _playerAlive = false;
    }

    private void PlayerMove()
    {
        _playerPosition = Vector2.Lerp(transform.position,
            _mainCamera.ScreenToWorldPoint(Input.mousePosition), moveSpeed); //Плавное перемещение объекта за пальцем(мышкой)

        transform.position = _playerPosition;
    }

    private void PlayerDie()
    {
        if (PlayerPrefs.GetString("vfx").Equals("true")) _sounds.PlaySound(4);

        sceneTransition.Appear();
        
        _die = true;
            
        score.SetTimerWork(false);
        
        score.SaveScore();
        
        sceneLoader.LoadScene(0);
    }
}
