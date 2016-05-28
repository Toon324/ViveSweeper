using UnityEngine;
using System.Collections;

public class Grabbable : MonoBehaviour
{

    private bool beingHeld = false;

    private HandController curHolding = null;

    [SerializeField]
    public bool isFlag;

    public void grab(HandController cont)
    {
        if (beingHeld)
        {
            curHolding.letGo();
            return;
        }

        //Set Physics settings
        Rigidbody toChange = (Rigidbody)gameObject.GetComponent("Rigidbody");
        toChange.useGravity = false;
        toChange.isKinematic = true;
        curHolding = cont;
        beingHeld = true;

    }

    public void drop()
    {
        beingHeld = false;
        curHolding = null;
    }


}
