using System.Collections.Generic;
using UnityEngine;

public class AoETowerBehaviour : MonoBehaviour
{
	[SerializeField] LayerMask layer;
	[SerializeField] GameObject bullet;
	public int damage;
	public float detectionRadius;
	public float shootTimer;
	public int numberOfProjectiles;
	public int destroyedEnemies;
	public int upgradeCost;
	public bool upgraded;
	GameObject[] targets = new GameObject[8];
	Quaternion[] shootQuaternions;
	Vector3[] shootDirections;
	public List<GameObject> enemiesInRange = new List<GameObject>();
	float timer;


	public void Start()
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
		{
			if (enemiesInRange[0] == null)
				enemiesInRange.Remove(enemiesInRange[0]);

			Shoot();
		}
	}

	void Shoot()
	{
		timer += Time.deltaTime;

		if (timer >= shootTimer)
		{
			timer = 0;
			for (int i = 0; i < shootDirections.Length; i++)
			{
				if (upgraded)
				{
					Transform parent = transform.GetChild(3).GetChild(i);
					GameObject firedBullet = Instantiate(bullet, parent.position, Quaternion.identity);
					firedBullet.GetComponent<AoEBullet>().tower = this;
					firedBullet.GetComponent<AoEBullet>().parent = parent;
				}
				else
				{
					Transform parent = transform.GetChild(2).GetChild(i);
					GameObject firedBullet = Instantiate(bullet, parent.position, Quaternion.identity);
					firedBullet.GetComponent<AoEBullet>().tower = this;
					firedBullet.GetComponent<AoEBullet>().parent = parent;
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
