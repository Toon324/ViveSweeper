using UnityEngine;
using System.Collections;

public class GridSpace {

    private Transform transform;
    private GameObject spacePiece;

    int index;
    int up;
    int upright;
    int upleft;
    int down;
    int downright;
    int downleft;
    int right;
    int left;

    private bool interacting;

    public GridSpace(GameObject space, int index)
    {
        this.spacePiece = space;
        this.transform = space.transform;

        this.index = index;
        this.up = index - GridWorld.size;
        this.upright = up + 1;
        this.upleft = up - 1;
        this.down = index + GridWorld.size;
        this.downleft = down - 1;
        this.downright = down + 1;
        this.right = index + 1;
        this.left = index - 1;

        this.interacting = false;

    }




    public void interact()
    {
        if (interacting)
            return;

        interacting = true;
              
        //Debug.Log("Interacting with:"+index);
        this.spacePiece.GetComponent<MeshRenderer>().material.color = Color.green;
    }

    public void doneInteracting()
    {
        this.interacting = false;
        this.spacePiece.GetComponent<MeshRenderer>().material.color = Color.white;
    }

    public Transform getTransform()
    {
        return spacePiece.transform;
    }

}
