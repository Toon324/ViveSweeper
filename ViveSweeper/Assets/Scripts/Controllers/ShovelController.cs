using UnityEngine;
using System.Collections;

public class ShovelController : MonoBehaviour {

    [SerializeField]
    private GameObject shovel;


	// Use this for initialization
	void Start () {
	
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
        //DIG!
    }

    //void OnTriggerExit(Collider collider)
    //{

    //}
}
