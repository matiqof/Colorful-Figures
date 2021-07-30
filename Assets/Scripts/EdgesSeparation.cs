using UnityEngine;

public class EdgesSeparation : MonoBehaviour
{
    [SerializeField] private GameObject up;
    
    [SerializeField] private GameObject down;
    
    [SerializeField] private GameObject right;
    
    [SerializeField] private GameObject left;

    [SerializeField] private Camera mainCamera;
    
    private int _screenWidth;

    private int _screenHeight;

    private Vector2 _worldPosition;

    private void Awake()
    {
        //Задание положения границ в зависимости от разрешения экрана
        
        _screenWidth = Screen.width;
        
        _screenHeight = Screen.height;

        _worldPosition = mainCamera.ScreenToWorldPoint(new Vector2(_screenWidth, _screenHeight));

        up.transform.position = new Vector3(0, _worldPosition.y, 0);

        down.transform.position = new Vector3(0, -_worldPosition.y, 0);

        right.transform.position = new Vector3(_worldPosition.x, 0, 0);
        
        left.transform.position = new Vector3(-_worldPosition.x, 0, 0);
    }
}
