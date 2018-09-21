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
		//getting all the needed references (will be changed)
		targetPoint = GameObject.Find("End").transform;
		waveSpawn = GameObject.Find("Spawner").GetComponent<WaveSpawn>();
		stats = GameObject.Find("Engine").GetComponent<Stats>();
		GetComponent<Renderer>().material.color = Random.ColorHSV();
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(targetPoint.position);
		agent.speed = waveSpawn.speed;
		health = Mathf.FloorToInt(waveSpawn.health);
	}

	void Update()
	{
		//checking if enemy reached it's target point
		Vector3 distance = transform.position - targetPoint.position;
		if (distance.magnitude <= 0.1f)
		{
			waveSpawn.DestroyedEnemyCount++;
			stats.currentHealth--;
			Destroy(gameObject);
		}

		//checking if enemy is destroyed
		if (health <= 0)
		{
			stats.currentMoney += goldDrop;
			waveSpawn.DestroyedEnemyCount++;
			Destroy(gameObject);
		}
	}
}
