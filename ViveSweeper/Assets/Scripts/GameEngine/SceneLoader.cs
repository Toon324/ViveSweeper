using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.Scripts.World;

public class SceneLoader : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        //Check if there already is a Scene Loader, there should only be one.
        if (GameObject.Find("SceneLoader") != null)
        {
            Destroy(this.gameObject);
        }
        this.gameObject.name = "SceneLoader";
        DontDestroyOnLoad(this.gameObject);

    }

    public void LoadNewGame(int index, WorldConstants.Difficulties difficulty)
    {
        WorldConstants.CurrentDifficulty = difficulty;

        StartCoroutine(LoadNewGame(index));


    }


    public IEnumerator LoadNewGame(int index)
    {
        for (int i = 0; i < 100; i++)
            yield return new WaitForFixedUpdate();

        SceneManager.LoadScene(index);

        

    }


    void OnLevelWasLoaded(int level)
    {
        //Do nothing in the main menu
        if (level == 0)
            return;

        GameEngine engine = (GameEngine)GameObject.Find("GameEngine").GetComponent("GameEngine");

        engine.NewGame();
    }

}
