using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstaclesObjects;

    [SerializeField] private float spawnPoint = 15;
    
    [SerializeField] private float destroyPoint = -15;

    [SerializeField] private float maxMoveSpeed;

    [SerializeField] private float moveSpeed = 5f;

    private GameObject _obstacleObject;

    private Rigidbody2D _obstacleRigidbody2D;

    private void Start()
    {
        SpawnObstacle();
    }

    private void FixedUpdate()
    {
        if (_obstacleObject.transform.position.x <= destroyPoint) DestroyObstacle();
    }
    
    void DestroyObstacle()
    {
        Destroy(_obstacleObject);
            
        SpawnObstacle();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void SpawnObstacle()
    {
        var index = Random.Range(0, obstaclesObjects.Count);
        var pos = Random.Range(1, 4);
        
        _obstacleObject = Instantiate(obstaclesObjects[index], 
            Vector3.zero, new Quaternion(0, 0, 0, 0));

        _obstacleRigidbody2D = _obstacleObject.GetComponent<Rigidbody2D>();

        switch (pos)
        {
            case 1:
            {
                _obstacleObject.transform.localScale = new Vector3(Random.Range(0.4f, 0.6f), Random.Range(0.4f, 0.8f), 0);
                
                _obstacleObject.transform.position = new Vector3(spawnPoint,
                    5 - (_obstacleObject.transform.localScale.y / 2) * 10, 0);

                if (index == 1) _obstacleObject.transform.rotation = new Quaternion(0, 0, 180, 0);
                
                break;
            }
            case 2:
                _obstacleObject.transform.localScale = new Vector3(Random.Range(0.4f, 0.6f), Random.Range(0.4f, 0.8f), 0);
                
                _obstacleObject.transform.position = new Vector3(spawnPoint, 
                    -5 + (_obstacleObject.transform.localScale.y / 2) * 10, 0);
                
                break;
            default:
            {
                _obstacleObject.transform.localScale = new Vector3(Random.Range(0.2f, 0.6f), Random.Range(0.4f, 0.6f), 0);
                
                _obstacleObject.transform.position = new Vector3(spawnPoint,
                    0, 0);
            
                _obstacleObject.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                
                break;
            }
        }
        
        _obstacleRigidbody2D.velocity = new Vector2(-moveSpeed, 0);
    }

    public float GETMaxMoveSpeed()
    {
        return maxMoveSpeed;
    }

    public float GETMoveSpeed()
    {
        return moveSpeed;
    }

    public void UpdateMoveSpeed(float speed)
    {
        _obstacleRigidbody2D.velocity = new Vector2(-speed, 0);
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
