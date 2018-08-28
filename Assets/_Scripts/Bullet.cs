using UnityEngine;

public class Bullet : MonoBehaviour
{
	public TowerBehaviour tower;
	Transform parent;
	Vector3 direction;

	private void Start()
	{
		parent = tower.gameObject.transform.GetChild(0).GetChild(1);
		direction = parent.forward;
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
