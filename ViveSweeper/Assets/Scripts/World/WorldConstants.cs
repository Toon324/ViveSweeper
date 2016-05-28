using UnityEngine;
using System.Collections;
using Assets.Scripts.World.GridWorld;

public class WorldConstants {

    //World Objects
    public static GameObject GridSpace { get; set; }

    public static GridWorld World { get; set; }

    public enum Difficulties { Easy, Medium, Expert }

    public static Difficulties CurrentDifficulty { get; set; }

    public static void LoadObjects()
    {
        GridSpace = Resources.Load<GameObject>("GridSpace");

    }



}
