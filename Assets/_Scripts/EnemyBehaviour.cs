using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
	public int health;
	[SerializeField] int goldDrop;
	Transform targetPoint;
	WaveSpawn waveSpawn;
	Stats stats;



	void Awake()
	{
		targetPoint = GameObject.Find("End").transform;
		waveSpawn = GameObject.Find("Spawner").GetComponent<WaveSpawn>();
		stats = GameObject.Find("Engine").GetComponent<Stats>();
		GetComponent<Renderer>().material.color = Random.ColorHSV();
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(targetPoint.position);

		health = Random.Range(waveSpawn.minHealth, waveSpawn.maxHealth);
		goldDrop = health;
		agent.speed = 1f / (health * 2f) * 5f;
	}

	void LateUpdate()
	{
		Vector3 distance = transform.position - targetPoint.position;

		if (distance.magnitude <= 0.1f)
		{
			waveSpawn.DestroyedEnemyCount++;
			stats.currentHealth--;
			Destroy(gameObject);
		}

		if (health <= 0)
		{
			stats.currentMoney += goldDrop;
			waveSpawn.DestroyedEnemyCount++;
			Destroy(gameObject);
		}
	}
}
