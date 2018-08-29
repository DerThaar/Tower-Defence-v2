using UnityEngine;

public class AoEBullet : MonoBehaviour
{
	public AoETowerBehaviour tower;
	public Transform parent;
	Vector3 direction;

	private void Start()
	{		
		direction = parent.up;
	}

	void Update()
	{
		GetComponent<Rigidbody>().velocity = direction * 10;
		float distance = (transform.position - parent.position).magnitude;
		if (distance >= tower.detectionRadius * 2)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			GameObject target = other.gameObject;
			if (target.GetComponent<EnemyBehaviour>().health > 0)
			{
				target.GetComponent<EnemyBehaviour>().health -= tower.damage;

				if (target.GetComponent<EnemyBehaviour>().health <= 0)
				{
					tower.enemiesInRange.Remove(target);
					tower.destroyedEnemies++;
					Destroy(gameObject);
				}
			}
		}
	}
}
