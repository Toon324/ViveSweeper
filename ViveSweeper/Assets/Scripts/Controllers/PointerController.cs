using Assets.Scripts.World;
using Assets.Scripts.World.GridWorld;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class PointerController : MonoBehaviour
    {

        private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

        private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
        private SteamVR_TrackedObject trackedObj;

        private static GridSpace interactSpace;

        public GameObject pointer;
        public Color color;
        public float thickness = 0.002f;

        const int WorldLayer = 1 << 8;
        private const int rayDist = 50;

        // Use this for initialization
        void Start()
        {
            trackedObj = GetComponent<SteamVR_TrackedObject>();

            pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            pointer.transform.parent = this.transform;
            pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
            pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
            BoxCollider collider = pointer.GetComponent<BoxCollider>();
            Object.Destroy(collider);
            Material newMaterial = new Material(Shader.Find("Unlit/Color"));
            newMaterial.SetColor("_Color", color);
            pointer.GetComponent<MeshRenderer>().material = newMaterial;
        }

        // Update is called once per frame
        void Update()
        {
            if (controller == null)
            {
                Debug.Log("Controller not initialized.");
                return;
            }


            if (controller.GetPressDown(triggerButton))
            {
                Ray raycast = new Ray(transform.position, transform.forward);
                RaycastHit hit;
                bool bHit = Physics.Raycast(raycast, out hit, rayDist, WorldLayer);
       
                if (bHit)
                {
                    int y;

                    string name = hit.transform.gameObject.name;

                    //Debug.Log("Attempting to parse: " + name);

                    if (int.TryParse(name, out y))
                        if (isValidGridSpaceName(y))
                        {

                            interactSpace = WorldConstants.World.GetSpaceFromWorldIndex(y);
                            interactSpace.Dig();
                            controller.TriggerHapticPulse(500, Valve.VR.EVRButtonId.k_EButton_Axis0);
                        }
                }


            }


        }

        public void disablePointer()
        {
            pointer.SetActive(false);
        }

        public void enablePointer()
        {
            pointer.SetActive(true);
        }

        private bool isValidGridSpaceName(int y)
        {
            return (y >= 0 && y <= WorldConstants.World.TotalSize);
        }

    }
}
