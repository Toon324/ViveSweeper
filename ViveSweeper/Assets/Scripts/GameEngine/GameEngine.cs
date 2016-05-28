using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour {

    //static Player player;
    static GridWorld world;

	// Use this for initialization
	void Start () {

        WorldConstants.loadObjects();

        newGame();

	}



    void newGame()
    {
        world = new GridWorld();
        //player = new Player();

    }


}
