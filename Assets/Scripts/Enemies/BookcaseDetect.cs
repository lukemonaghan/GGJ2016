﻿using UnityEngine;

public class BookcaseDetect : Entity
{
    public int moveSpeed = 6;
    public int turnSpeed = 3;
    public bool canFall;

	public float distance = 10;
	public float damage = 10;
	
	private Transform target;
	private Rigidbody rb;

	private bool hasLockedOn = false;

	// Use this for initialization
	void Start ()
	{
        target = GameObject.FindWithTag("Player").transform;
        canFall = false;
        rb = GetComponentInChildren<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (target == null)
		{
			return;
		}

		if (Vector3.Distance(target.transform.position, transform.position) < distance)
		{
			hasLockedOn = true;
		}

		if (hasLockedOn == false)
		{
			return;
		}

		if (canFall)
		{
			if (transform.eulerAngles.x > 89 && transform.eulerAngles.x < 91)
			{
				OnDeath();
			}
			return;
		}

		rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(target.position - rb.position), turnSpeed * Time.deltaTime);
		rb.position += transform.forward * moveSpeed * Time.deltaTime;
	}

    void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag == "Player")
        {
            canFall = true;
            rb.AddRelativeForce(Vector3.forward * 3500.0f);
        }
    }

    void OnCollisionEnter(Collision other)
    {
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<PlayerController>().health -= damage;
			OnDeath();
		}
    }

	public override void OnDeath()
	{
		Destroy(gameObject);

		for (var i = 0; i < Random.Range(5, 10); i++)
		{
			Instantiate(GameParameters.Instance.woodBits[Random.Range(0, GameParameters.Instance.woodBits.Length)], transform.position, Quaternion.identity);
		}
	}
}