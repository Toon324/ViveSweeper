using UnityEngine;
using System.Collections;

public class ConfettiFollower : MonoBehaviour {

    [SerializeField]
    private Transform toFollow;

    [SerializeField]
    private float yHeight;

    private Vector3 pos;

    void Start()
    {
        pos = new Vector3(0, yHeight, 0);
    }

	// Update is called once per frame
	void Update () {

        pos.x = toFollow.position.x;
        pos.z = toFollow.position.z;

        this.transform.position = pos;

	}
}
