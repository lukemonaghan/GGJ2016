﻿using UnityEngine;
using System.Collections;

public class BookcaseDetect : MonoBehaviour {

    public int moveSpeed = 3;
    public int turnSpeed = 3;
    private Rigidbody rb;
    public bool canFall;
    private Transform myTransform;
    private Transform target;
    
	// Use this for initialization
	void Start () {
        myTransform = transform;
        target = GameObject.FindWithTag("Player").transform;
        canFall = false;
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (canFall == false)
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), turnSpeed * Time.deltaTime);
            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        }
	}

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player")
        {
            print("Triggered");
            canFall = true;
            rb.AddForce(Vector3.back * 1500.0f);
        }
    }

    void OnCollisionEnter (Collision other)
    {
        foreach (ContactPoint contact in other.contacts)
        {
            if (other.gameObject.tag == "Player")
            {
                print("Hit");
            }
        }
    }
}
