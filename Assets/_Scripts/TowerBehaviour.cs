using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
	public int damage;
	public float detectionRadius;
	public float shootTimer;
	public int numberOfProjectiles = 1;
	public int destroyedEnemies;
	GameObject target;
	List<GameObject> enemiesInRange = new List<GameObject>();
	float timer;


	private void Awake()
	{
		GetComponent<SphereCollider>().radius = detectionRadius;
	}

	void Update()
	{
		FindTarget();
		if (target != null)
			Shoot();
	}

	void FindTarget()
	{
		if (enemiesInRange.Count > 0)
		{
			target = enemiesInRange[0];
			if (target == null)
				enemiesInRange.Remove(target);
			else
			{
				Vector3 lookTarget = target.transform.position;
				lookTarget.y = transform.GetChild(0).transform.position.y;
				transform.GetChild(0).LookAt(lookTarget);
			}
		}
	}

	void Shoot()
	{
		timer += Time.deltaTime;

		if (timer >= shootTimer)
		{
			timer = 0;
			if (target.GetComponent<EnemyBehaviour>().health > 0)
			{
				target.GetComponent<EnemyBehaviour>().health -= damage;

				if (target.GetComponent<EnemyBehaviour>().health <= 0)
				{
					enemiesInRange.Remove(target);
					destroyedEnemies++;
				}
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			enemiesInRange.Add(other.gameObject);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			enemiesInRange.Remove(other.gameObject);
		}
	}
}
