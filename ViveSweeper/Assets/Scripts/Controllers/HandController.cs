using UnityEngine;
using System.Collections;
using Assets.Scripts.World;
using Assets.Scripts.World.GridWorld;


public class HandController : MonoBehaviour
{

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    private static GameObject toGrab;

    private bool holdingSomething;

    [SerializeField]
    private Vector3 grabbedObjPos;
    [SerializeField]
    private Quaternion grabbedObjRot;

    private SphereCollider handCollider;
    [SerializeField]
    private float handColliderRadius;
    [SerializeField]
    private Vector3 handColliderPos;

    private BoxCollider grabbedCollider;
    [SerializeField]
    private Vector3 grabbedColliderSize;
    [SerializeField]
    private Vector3 grabbedColliderPos;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null)
        {
            Debug.Log("Controller not initialized.");
            return;
        }

        if (toGrab == null)
            return;

        if (controller.GetPressDown(triggerButton))
        {
            holdingSomething = true;

            Grabbable grabbed = (Grabbable)toGrab.GetComponent("Grabbable");
            grabbed.grab(this);

            //Rigidbody toChange = (Rigidbody)toGrab.GetComponent("Rigidbody");
            //toChange.useGravity = false;
            //toChange.isKinematic = true;

            toGrab.transform.parent = this.gameObject.transform;
            toGrab.transform.localRotation = grabbedObjRot;
            toGrab.transform.localPosition = grabbedObjPos;

            switchToGrabbedCollider();

        }

        if (controller.GetPressUp(triggerButton))
        {
            if(holdingSomething)
             drop();
        }

    }

    public void letGo()
    {

        holdingSomething = false;
        Grabbable curHolding = (Grabbable)toGrab.GetComponent("Grabbable");
        curHolding.drop();

        toGrab = null;
        switchToHandCollider();
    }

    private void drop()
    {
        holdingSomething = false;

        toGrab.transform.parent = null;
        Rigidbody toChange = (Rigidbody)toGrab.GetComponent("Rigidbody");
        toChange.useGravity = true;
        toChange.isKinematic = false;

        Vector3 vel = controller.velocity;
        vel.x *= -1;
        vel.z *= -1;

        toChange.velocity = vel;
        Vector3 ang = controller.angularVelocity;
        ang.x *= -1;
        ang.y *= -1;
        ang.z *= -1;
        toChange.angularVelocity = ang;
        toGrab = null;
        switchToHandCollider();
    }

    public void enableController()
    {
        switchToHandCollider();
    }

    public void disableController()
    {
        if (holdingSomething)
            drop();

        Destroy(handCollider);
        Destroy(grabbedCollider);

    }

    void OnTriggerEnter(Collider collider)
    {
        if (!holdingSomething)
        {
            if (collider.gameObject.GetComponent("Grabbable") == null)
                return;

            toGrab = collider.gameObject;
            return;
        }

        //Plant Marker
        int y;

        string name = collider.gameObject.name;

        Debug.Log("Attempting to parse/grab: " + name);

        if (int.TryParse(name, out y))
            if (isValidGridSpaceName(y))
            {
                GridSpace interactSpace = WorldConstants.World.GetSpaceFromWorldIndex(y);

                if (interactSpace.HasMarker)
                    return;

                Grabbable curHolding = (Grabbable)toGrab.GetComponent("Grabbable");

                curHolding.Plant(interactSpace);
                curHolding.drop();
               // if (curHolding.isFlag)
                interactSpace.PlantMarker(toGrab);
                //else
                //    interactSpace.PlantMarker()

                controller.TriggerHapticPulse(500, Valve.VR.EVRButtonId.k_EButton_Axis0);
                letGo();
            }

    }

    void OnTriggerExit(Collider collider)
    {
        if (holdingSomething)
            return;

        toGrab = null;
    }

    private bool isValidGridSpaceName(int y)
    {
        return (y >= 0 && y <= WorldConstants.World.TotalSize);
    }

    private void switchToHandCollider()
    {
        if (grabbedCollider != null)
            Destroy(grabbedCollider);

        if(handCollider == null)
        {
            handCollider = gameObject.AddComponent<SphereCollider>();
            handCollider.radius = handColliderRadius;
            handCollider.center = handColliderPos;
            handCollider.isTrigger = true;
        }
    }

    private void switchToGrabbedCollider()
    {
        if (handCollider != null)
            Destroy(handCollider);

        if (grabbedCollider == null)
        {
            grabbedCollider = gameObject.AddComponent<BoxCollider>();
            grabbedCollider.size = grabbedColliderSize;
            grabbedCollider.center = grabbedColliderPos;
            grabbedCollider.isTrigger = true;
        }
    }
}
