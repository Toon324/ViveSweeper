using UnityEngine;
using System.Collections;
using Assets.Scripts.World;
using Assets.Scripts.World.GridWorld;

public class ShovelController : MonoBehaviour {

    [SerializeField]
    private GameObject shovel;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    public void disable()
    {
        shovel.SetActive(false);
    }

    public void enable()
    {
        shovel.SetActive(true);
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Digging: " + collider.gameObject.name);

        //DIG!
        int y;

        string name = collider.gameObject.name;

        //Debug.Log("Attempting to parse: " + name);

        if (int.TryParse(name, out y))
            if (isValidGridSpaceName(y))
            {
                GridSpace interactSpace = WorldConstants.World.GetSpaceFromWorldIndex(y);
                interactSpace.Interact();
                controller.TriggerHapticPulse(500, Valve.VR.EVRButtonId.k_EButton_Axis0);
            }
    }

    //void OnTriggerExit(Collider collider)
    //{

    //}

    private bool isValidGridSpaceName(int y)
    {
        return (y >= 0 && y <= WorldConstants.World.TotalSize);
    }


}
