using UnityEngine;

public class SpellController : MonoBehaviour
{
	public Animator animator { get { return _animator ?? (_animator = GetComponentInChildren<Animator>()); } }
	private Animator _animator;

	public Renderer renderer { get { return _renderer ?? (_renderer = GetComponentInChildren<Renderer>()); } }
	private Renderer _renderer;
	public ParticleSystem[] particleSystems { get { return _particleSystems ?? (_particleSystems = GetComponentsInChildren<ParticleSystem>()); } }
	private ParticleSystem[] _particleSystems;

	Color EmissionColor = Color.black;

	void Start()
	{
		EmissionColor = Color.black;
		SetColor();
	}

	void Update()
	{
		// Either Trigger
		var l = Mathf.Abs(Input.GetAxisRaw("TriggersL_1"));
		var r = Mathf.Abs(Input.GetAxisRaw("TriggersR_1"));

		// Get the type from input
		var type = GameParameters.SpellType.NONE;
		if (l > 0.01f)
		{
			type = GameParameters.SpellType.Explode;
        }
		else if (r > 0.01f)
		{
			type = GameParameters.SpellType.Projectile;
		}

		// Did we press something?
		if (type != GameParameters.SpellType.NONE)
        {
			// Create the Spell
	        GameObject spellObject = null;
			UIManager.Instance.inGameMenu.ActivateSpell(out spellObject,EmissionColor, type);

			// Did we have a good mix?
			if (spellObject != null)
			{
				// Reset the color
				EmissionColor = Color.black;
				// Feed it to everything
				SetColor();

				// Dont collide with me
				Physics.IgnoreCollision(GetComponent<Collider>(), spellObject.GetComponent<Collider>());

				// Do the correct animation
				switch (type)
		        {
			        case GameParameters.SpellType.Explode:
				        animator.SetTrigger("Explode");
				        break;
			        case GameParameters.SpellType.Projectile:
				        animator.SetTrigger("Projectile");
				        break;
		        }
	        }
        }
	}

	public void AddColor(Color col)
	{
		// Add the colour to us
		EmissionColor += col;
		// Normalize it
		var color = new Vector3(EmissionColor.r, EmissionColor.g, EmissionColor.b).normalized;
		// Feed it back
		EmissionColor = new Color(color.x, color.y, color.z,1);
		// Feed it to everything
		SetColor();
	}

	void SetColor()
	{
		foreach (var p in particleSystems)
		{
			p.startColor = EmissionColor;
		}
		renderer.material.SetColor("_EmissionColor", EmissionColor);
	}
}
