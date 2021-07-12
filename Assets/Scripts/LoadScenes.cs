using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    [SerializeField] private float time = 1f;

    public void LoadScene(int scene)
    {
        var async = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        StartCoroutine(UpdateScene(async));
    }

    private IEnumerator UpdateScene(AsyncOperation async)
    {
        async.allowSceneActivation = false;

        yield return new WaitForSeconds(time);

        async.allowSceneActivation = true;
    }
}
