using UnityEngine;

public class Quit : MonoBehaviour
{
    [SerializeField] private Score score;
    
    public void QuitApplication()
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        //Выход из приложения и очистка памяти
        
        PlayerPrefs.DeleteKey("music");
        
        PlayerPrefs.DeleteKey("sounds");
        
        if (score != null) score.SaveScore();
    }
}
