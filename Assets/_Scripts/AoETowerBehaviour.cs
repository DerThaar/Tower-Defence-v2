using System.Collections.Generic;
using UnityEngine;

public class AoETowerBehaviour : MonoBehaviour
{
	[SerializeField] LayerMask layer;
	public int damage;
	public float detectionRadius;
	public float shootTimer;
	public int numberOfProjectiles;
	public int destroyedEnemies;
	GameObject[] targets = new GameObject[8];
	Quaternion[] shootQuaternions;
	Vector3[] shootDirections;
	List<GameObject> enemiesInRange = new List<GameObject>();
	float timer;


	private void Start()
	{
		GetComponent<SphereCollider>().radius = detectionRadius;
		shootQuaternions = new Quaternion[numberOfProjectiles];
		shootDirections = new Vector3[numberOfProjectiles];
		int shootAngle = 360 / numberOfProjectiles;

		for (int i = 0; i < numberOfProjectiles; i++)
		{
			shootQuaternions[i] = Quaternion.AngleAxis(shootAngle, Vector3.up);
			shootDirections[i] = shootQuaternions[i] * Vector3.forward;
			shootAngle += 360 / numberOfProjectiles;
		}
	}

	void Update()
	{
		if (enemiesInRange.Count > 0)
			Shoot();
	}

	void Shoot()
	{
		timer += Time.deltaTime;

		if (timer >= shootTimer)
		{
			timer = 0;
			for (int i = 0; i < shootDirections.Length; i++)
			{
				RaycastHit hit;
				Ray ray = new Ray(transform.position, shootDirections[i]);
				if (Physics.Raycast(ray, out hit, detectionRadius, layer))
				{
					targets[i] = hit.transform.gameObject;

					if (targets[i].GetComponent<EnemyBehaviour>().health > 0)
					{
						targets[i].GetComponent<EnemyBehaviour>().health -= damage;

						if (targets[i].GetComponent<EnemyBehaviour>().health <= 0)
						{
							enemiesInRange.Remove(targets[i]);
							destroyedEnemies++;
						}
					}
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
