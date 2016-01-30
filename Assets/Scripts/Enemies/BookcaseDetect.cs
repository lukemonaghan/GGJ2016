using UnityEngine;

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

	void Start ()
	{
        target = GameObject.FindWithTag("Player").transform;
        canFall = false;
        rb = GetComponentInChildren<Rigidbody>();
	}
	
	void Update()
	{
		if (target == null)
		{
			//Debug.Log("no targert");
			return;
		}

		if (Vector3.Distance(target.transform.position, transform.position) < distance)
		{
			//Debug.Log("in range");
			hasLockedOn = true;
		}

		if (hasLockedOn == false)
		{
			//Debug.Log("hasLockedOn false");
			return;
		}

		if (canFall)
		{
			if ((transform.eulerAngles.x > 85 && transform.eulerAngles.x < 95) || (transform.eulerAngles.x > 265 && transform.eulerAngles.x < 275))
			{
				OnDeath();
			}
			//Debug.Log("Fall");
		}
		else
		{
			rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(target.position - rb.position), turnSpeed * Time.deltaTime);
			rb.position += transform.forward * moveSpeed * Time.deltaTime;
			//Debug.Log("Move");
		}
	}

    void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag == "Player")
        {
            canFall = true;
            rb.AddForce(transform.forward * 3500.0f);
        }
    }

    void OnCollisionEnter(Collision other)
    {
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<PlayerController>().health -= damage;
			UIManager.Instance.inGameMenu.DamagePopup(transform, (int)-damage);
			OnDeath();
		}
	}

	public override void OnDeath()
	{
		Destroy(gameObject);

		for (var i = 0; i < deathBits.Length; i++)
		{
			Instantiate(deathBits[i], transform.position, transform.rotation);
		}
	}
}
