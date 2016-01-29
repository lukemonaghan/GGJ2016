using UnityEngine;
using System.Collections;

public class PortraitSpin : MonoBehaviour {

    public int turnSpeed = 3;
    private int spinSpeed;
    private Transform myTransform;
    private Transform target;

    // Use this for initialization
    void Start ()
    {
        spinSpeed = 360;
        myTransform = transform;
        target = GameObject.FindWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), turnSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, (transform.position - target.position).normalized * 4.0f + target.position, Time.deltaTime * 3.0f);
    }
}
