using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject tutorial;

    [SerializeField] private Begin begin;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("isTrained")) tutorial.SetActive(true);
        else begin.StartGame();
    }

    public void SetIsTrained(string isTrained) {
        PlayerPrefs.SetString("isTrained", isTrained);

        PlayerPrefs.Save();

        begin.StartGame();
    }
}
