using UnityEngine;
using System.Collections;
using Assets.Scripts.Controllers;

public class MainController : MonoBehaviour {

    private Valve.VR.EVRButtonId dpadup = Valve.VR.EVRButtonId.k_EButton_DPad_Up;

    private Valve.VR.EVRButtonId touchPad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

    private Valve.VR.EVRButtonId ax0 = Valve.VR.EVRButtonId.k_EButton_Axis0;
    private Valve.VR.EVRButtonId ax1 = Valve.VR.EVRButtonId.k_EButton_Axis1;
    private Valve.VR.EVRButtonId ax2 = Valve.VR.EVRButtonId.k_EButton_Axis2;
    private Valve.VR.EVRButtonId ax3 = Valve.VR.EVRButtonId.k_EButton_Axis3;
    private Valve.VR.EVRButtonId ax4 = Valve.VR.EVRButtonId.k_EButton_Axis4;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    private ShovelController shovelC;
    //private PointerController pointerC;
    //private HandController handC;
    [SerializeField] private TeleportVive teleportC;

    //Controller GUI
    [SerializeField] private TextMesh contText;

    private enum ControllerType {Shovel,Teleport};
    private ControllerType curContType = ControllerType.Shovel;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

        shovelC = (ShovelController)gameObject.GetComponent("ShovelController");
    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null)
        {
            Debug.Log("Controller not initialized.");
            return;
        }

        //Debug.Log("updating...");

        if (controller.GetPress(touchPad))
        {
            updateTouchPad(controller.GetAxis(ax0));
        }

    }


    private void updateTouchPad(Vector2 pos)
    {
        float y = Mathf.Abs(pos.y);
        float x = Mathf.Abs(pos.x);

        //Up/Down
        if(y >= x)
        {
            //Up
            if (pos.y >= 0)
            {
                switchToShovelCont();
            }
            //Down
            else
            {
                switchToTeleportCont();
            }

        }
        //Right/Left
        else
        {
            //Right
            if(pos.x >= 0)
            {

            }
            //Left
            else
            {
              
            }
        }

    }

    private void switchToShovelCont()
    {
        teleportC.disableController(trackedObj);
        shovelC.enabled = true;
        shovelC.enable();
        contText.text = "";
    }



    private void switchToTeleportCont()
    {
        shovelC.disable();
        shovelC.enabled = false;
        teleportC.enableController(trackedObj);
        curContType = ControllerType.Teleport;
        contText.text = "Teleport Mode";
    }




}
