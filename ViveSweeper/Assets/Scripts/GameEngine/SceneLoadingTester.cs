using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoadingTester : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(DelayLoad());
    }

    public IEnumerator DelayLoad()
    {
        for (int i = 0; i < 1000; i++)
            yield return new WaitForFixedUpdate();

        loadScene();

    }
    
    private void loadScene()
    {
        SceneManager.LoadScene(1);
    }

}