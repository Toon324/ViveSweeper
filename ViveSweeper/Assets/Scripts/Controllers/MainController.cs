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

    private PointerController pointerC;
    private HandController handC;
    [SerializeField] private TeleportVive teleportC;

    //private teleporter teleportC;

    //Controller GUI
    [SerializeField] private TextMesh contText;

    private enum ControllerType {Hand,Pointer,Teleport};
    private ControllerType curContType = ControllerType.Pointer;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

        pointerC = (PointerController)this.gameObject.GetComponent("PointerController");
        handC = (HandController)this.gameObject.GetComponent("HandController");
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
                switchToPointerCont();
            }
            //Down
            else
            {
                switchToHandCont(); 
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
                switchToTeleportCont();
            }
        }

    }


    private void switchToHandCont()
    {
        //Disable other conts
        disablePointerC();
        teleportC.enabled = false;
        handC.enabled = true;
        curContType = ControllerType.Hand;
        contText.text = "Grab Mode";
    }

    private void switchToPointerCont()
    {
        //Disable other conts
        disableHandC();
        teleportC.enabled = false;
        enablePointerC();
        contText.text = "Point Mode";
    }

    private void switchToTeleportCont()
    {
        disableHandC();
        disablePointerC();
        teleportC.enabled = true;
        curContType = ControllerType.Teleport;
        contText.text = "Teleport Mode";
    }

    private void enablePointerC()
    {
        pointerC.enablePointer();
        pointerC.enabled = true;
        curContType = ControllerType.Pointer;
    }

    private void disablePointerC()
    {
        pointerC.disablePointer();
        pointerC.enabled = false;
    }

    private void disableHandC()
    {
        handC.disableController();
        handC.enabled = false;
    }



}
