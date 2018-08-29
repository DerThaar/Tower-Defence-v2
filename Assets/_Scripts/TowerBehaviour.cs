using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
	[SerializeField] GameObject bullet;
	public int damage;
	public float detectionRadius;
	public float shootTimer;
	public int numberOfProjectiles = 1;
	public int destroyedEnemies;
	public int upgradeCost;
	public bool upgraded;
	public List<GameObject> enemiesInRange = new List<GameObject>();
	GameObject target;
	float timer;
	Vector3 lookTarget;


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
				lookTarget = target.transform.position;
				lookTarget.y = transform.GetChild(0).transform.position.y;
				transform.GetChild(0).LookAt(lookTarget);
			}
		}
		else
			target = null;
	}

	void Shoot()
	{
		timer += Time.deltaTime;

		if (timer >= shootTimer)
		{
			timer = 0;
			Transform parent = transform.GetChild(0).GetChild(1);
			GameObject firedBullet = Instantiate(bullet, parent.position, Quaternion.identity);
			firedBullet.GetComponent<Bullet>().tower = this;
			target = null;
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
