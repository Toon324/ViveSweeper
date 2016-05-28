using UnityEngine;
using System.Collections;

public class WorldConstants {

    //World Objects
    public static GameObject gridSpace;


    public static void loadObjects()
    {
        gridSpace = Resources.Load<GameObject>("GridSpace");

    }



}
