using UnityEngine;

public class SpellController : MonoBehaviour
{
	public Animator animator { get { return _animator ?? (_animator = GetComponentInChildren<Animator>()); } }
	private Animator _animator;

	public void Update()
	{
		// Either Trigger
		var l = Mathf.Abs(Input.GetAxisRaw("TriggersL_1"));
		var r = Mathf.Abs(Input.GetAxisRaw("TriggersR_1"));

        if (l > 0.01f || r > 0.01f)
        {
	        GameObject spellObject = null;
			var type = UIManager.Instance.inGameMenu.ActivateSpell(out spellObject);
	        if (spellObject != null)
	        {
				Physics.IgnoreCollision(GetComponent<Collider>(), spellObject.GetComponent<Collider>());
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
}
