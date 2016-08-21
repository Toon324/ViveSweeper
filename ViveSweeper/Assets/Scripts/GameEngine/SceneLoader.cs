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
        LoadNewGame(WorldConstants.Scenes.Easy, WorldConstants.Difficulties.Easy);
    }

    public void LoadNewGame(WorldConstants.Scenes scene, WorldConstants.Difficulties difficulty)
    {
        WorldConstants.CurrentDifficulty = difficulty;

        StartCoroutine(LoadNewGame(scene));
    }


    public IEnumerator LoadNewGame(WorldConstants.Scenes scene)
    {
        for (int i = 0; i < 100; i++)
            yield return new WaitForFixedUpdate();

        SceneManager.LoadScene((int)scene);
    }

    public void OnLevelWasLoaded(int level)
    {
        //Do nothing in the main menu
        if (level == (int) WorldConstants.Scenes.MainMenu)
            return;

        GameEngine engine = (GameEngine)GameObject.Find("GameEngine").GetComponent("GameEngine");

        engine.NewGame();
    }
}
