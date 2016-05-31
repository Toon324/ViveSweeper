using UnityEngine;
using System.Collections;
using Assets.Scripts.Controllers;
using Assets.Scripts.World.GridWorld;

public class MainController : MonoBehaviour {

    private Valve.VR.EVRButtonId dpadup = Valve.VR.EVRButtonId.k_EButton_DPad_Up;

    private Valve.VR.EVRButtonId touchPad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

    private Valve.VR.EVRButtonId gripBut = Valve.VR.EVRButtonId.k_EButton_Grip;


    private Valve.VR.EVRButtonId ax0 = Valve.VR.EVRButtonId.k_EButton_Axis0;
    private Valve.VR.EVRButtonId ax1 = Valve.VR.EVRButtonId.k_EButton_Axis1;
    private Valve.VR.EVRButtonId ax2 = Valve.VR.EVRButtonId.k_EButton_Axis2;
    private Valve.VR.EVRButtonId ax3 = Valve.VR.EVRButtonId.k_EButton_Axis3;
    private Valve.VR.EVRButtonId ax4 = Valve.VR.EVRButtonId.k_EButton_Axis4;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    private ShovelController shovelC;
    private HandController handC;
    [SerializeField] private TeleportVive teleportC;
    [SerializeField] private GameObject miniMap;

    [SerializeField]
    private GameObject teleportObj;

  
    private enum ControllerType {Shovel,Teleport,Hand};
    private ControllerType curContType = ControllerType.Hand;

    [SerializeField]
    private SpriteRenderer touchPadGUI;

    [SerializeField]
    private Sprite touchpadNoneSprite;
    [SerializeField]
    private Sprite touchpadShovelSprite;
    [SerializeField]
    private Sprite touchpadTeleportSprite;

    private bool miniMapOn = false;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();


        handC = (HandController)gameObject.GetComponent("HandController");
        shovelC = (ShovelController)gameObject.GetComponent("ShovelController");
        switchToHandCont();
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

        if (controller.GetPressDown(touchPad))
        {
            updateTouchPad(controller.GetAxis(ax0));
        }

        if (controller.GetPressDown(gripBut))
            toggleMiniMap();

    }


    private void updateTouchPad(Vector2 pos)
    {
        float y = Mathf.Abs(pos.y);
        float x = Mathf.Abs(pos.x);

            //UpIndex
            if (pos.y >= 0)
            {
                if (curContType == ControllerType.Shovel)
                    switchToHandCont();
                else
                    switchToShovelCont();
            }
            //DownIndex
            else
            {
            if (curContType == ControllerType.Teleport)
                switchToHandCont();
            else
                switchToTeleportCont();
            }
        
       
            //RightIndex
            //if(pos.x >= 0)
            //{
            //    if (curContType == ControllerType.MiniMap)
            //        switchToHandCont();
            //    else
           //         switchToMiniMap();
           // }
            //LeftIndex
           // else
           // {
           //     if (curContType == ControllerType.Teleport)
            //        switchToHandCont();
           //     else
           //         switchToTeleportCont();
           // }
   
    }

    private void switchToShovelCont()
    {
        teleportObj.SetActive(false);
        teleportC.disableController(trackedObj);
        handC.disableController();
        handC.enabled = false;
        shovelC.enabled = true;
        shovelC.enable();
        touchPadGUI.sprite = touchpadShovelSprite;
        curContType = ControllerType.Shovel;
    }

    private void toggleMiniMap()
    {
        miniMapOn = !miniMapOn;
        miniMap.SetActive(miniMapOn);
    }

    private void switchToTeleportCont()
    {
        shovelC.disable();
        shovelC.enabled = false;
        handC.disableController();
        handC.enabled = false;
        teleportC.enableController(trackedObj);
        teleportObj.SetActive(true);
        curContType = ControllerType.Teleport;
        touchPadGUI.sprite = touchpadTeleportSprite;
    }

    private void switchToHandCont()
    {
        teleportObj.SetActive(false);
        shovelC.disable();
        shovelC.enabled = false;
        teleportC.disableController(trackedObj);
        handC.enabled = true;
        handC.enableController();
        curContType = ControllerType.Hand;
        touchPadGUI.sprite = touchpadNoneSprite;
    }


}
