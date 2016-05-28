using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridWorld {

    public static int size = 9;

    public static int ysize = size * size;

    private static GridSpace[] world;

    public static GameObject gridSpace;

    private float worldScaleFactor = 1f;

    private GameObject worldObj;

    private int yPos = -1;

    public GridWorld()
    {
        world = new GridSpace[size*size];

        gridSpace = WorldConstants.gridSpace;

        worldObj = new GameObject();//(GameObject)GameObject.Instantiate(new GameObject(), new Vector3(0, 0, 0), Quaternion.identity);
        worldObj.name = "World";

        for (int y = 0; y < size * size; y++)
        {
            int x = y / size;
            int z = y % size;

            float xPos = x * worldScaleFactor;
            float zPos = z * worldScaleFactor;

            GameObject space;

 
            space = (GameObject)GameObject.Instantiate(gridSpace, new Vector3(xPos, yPos, zPos), Quaternion.identity);
                space.name = "" + y;
                world[y] = new GridSpace(space,  y);

            space.transform.parent = worldObj.transform;

        }


    }

    public static GridSpace getSpaceFromWorldIndex(int y)
    {
        return world[y];
    }


}
