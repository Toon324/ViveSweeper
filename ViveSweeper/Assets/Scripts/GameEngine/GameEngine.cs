using UnityEngine;
using System.Collections;
using Assets.Scripts.World.GridWorld;

public class GameEngine : MonoBehaviour
{

    //static Player player;
    private static GridWorld World { get; set; }

    // Use this for initialization
    void Start()
    {

        WorldConstants.LoadObjects();

        WorldConstants.CurrentDifficulty = WorldConstants.Difficulties.Easy;

        NewGame();
    }

    void NewGame()
    {
        switch (WorldConstants.CurrentDifficulty)
        {
            case WorldConstants.Difficulties.Medium:
                World = new GridWorld(16);
                break;
            case WorldConstants.Difficulties.Expert:
                World = new GridWorld(21);
                break;
            default:
                World = new GridWorld(9);
                break;
        }

        WorldConstants.World = World;
        //player = new Player();

    }

}
