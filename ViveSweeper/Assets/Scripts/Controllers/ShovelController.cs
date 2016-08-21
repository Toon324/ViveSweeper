using UnityEngine;
using System.Collections;
using Assets.Scripts.World;
using Assets.Scripts.World.GridWorld;
using UnityEngine.SceneManagement;

public class ShovelController : MonoBehaviour
{

    [SerializeField]
    private GameObject shovel;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    SphereCollider sc;

    [SerializeField]
    private float colliderRadius;

    [SerializeField]
    private Vector3 colliderCenter;

    private bool isEnabled;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    public void disable()
    {
        Destroy(sc);

        isEnabled = false;
        shovel.SetActive(false);
    }

    public void enable()
    {
        if (sc == null)
        {
            sc = gameObject.AddComponent<SphereCollider>();
            sc.radius = colliderRadius;
            sc.center = colliderCenter;
            sc.isTrigger = true;
        }
        shovel.SetActive(true);
        isEnabled = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!isEnabled)
            return;

        //Debug.Log("Digging: " + collider.gameObject.name);

        //DIG!
        int y;

        string name = collider.gameObject.name;

        //Debug.Log("Attempting to parse: " + name);

        if (int.TryParse(name, out y))
        {
            if (isValidGridSpaceName(y))
            {
                GridSpace interactSpace = WorldConstants.World.GetSpaceFromWorldIndex(y);
                interactSpace.Dig();
                Debug.Log("DUG!");
                StartCoroutine(controller.LongVibration(1, 1));
            }
        }

        //Menu Interaction
        else if (name.Equals("New Game - Easy"))
        {
            SceneLoader sl = (SceneLoader)GameObject.Find("SceneLoader").GetComponent("SceneLoader");
            sl.LoadNewGame(WorldConstants.Scenes.Easy, WorldConstants.Difficulties.Easy);
            Destroy(collider.gameObject);
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
