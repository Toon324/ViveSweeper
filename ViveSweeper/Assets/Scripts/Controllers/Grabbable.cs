using UnityEngine;
using System.Collections;
using Assets.Scripts.World.GridWorld;

public class Grabbable : MonoBehaviour
{

    private bool beingHeld = false;

    private HandController curHolding = null;

    [SerializeField]
    public bool isFlag;

    private GridSpace plantedSpace;
    private bool isPlanted = false;

    public void grab(HandController cont)
    {
        if (beingHeld)
        {
            curHolding.objectTaken();
            curHolding = cont;
            return;
        }

        if(isPlanted)
        {
            plantedSpace.Grab();
            plantedSpace = null;
            isPlanted = false;
        }

        //Set Physics settings
        Rigidbody toChange = (Rigidbody)gameObject.GetComponent("Rigidbody");
        toChange.useGravity = false;
        toChange.isKinematic = true;
        curHolding = cont;
        beingHeld = true;

    }

    public void Plant(GridSpace space)
    {
        plantedSpace = space;
        isPlanted = true;
    }

    public void drop()
    {
        beingHeld = false;
        curHolding = null;
    }


}
