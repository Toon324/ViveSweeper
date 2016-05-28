using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour
{

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    private static GameObject toGrab;

    private bool holdingSomething;

    private bool holdingHat; 

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        holdingHat = false;
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
            toGrab.transform.parent = this.transform;

            if (toGrab.GetComponent("Hat") != null)
            {
                Debug.Log("Hat Detected!");
                holdingHat = true;
            }
            
        }

        if (controller.GetPressUp(triggerButton))
        {
            drop();
        }

    }

    public void letGo()
    {
        holdingSomething = false;
        toGrab = null;
    }

    private void drop()
    {
        holdingSomething = false;

        toGrab.transform.parent = null;
        holdingHat = false;
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
    }

    public void disableController()
    {
        if (holdingSomething)
            drop();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (holdingSomething)
            return;

        if (toGrab.GetComponent("Grabbable") == null)
            return;

        toGrab = collider.gameObject;
    }

    void OnTriggerExit(Collider collider)
    {
        if (holdingSomething)
            return;

        toGrab = null;
    }

}
