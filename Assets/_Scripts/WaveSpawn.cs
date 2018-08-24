using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawn : MonoBehaviour
{
	[SerializeField] GameObject enemy;
	[SerializeField] Transform start;
	[SerializeField] int waveSize;
	[SerializeField] float spawnTimer;
	[SerializeField] Button spawnButton;

	public int DestroyedEnemyCount;
	public int wave = 1;
	public int minHealth = 1;
	public int maxHealth = 2;

	float timer;
	int enemyCounter;
	bool startWave;


	void Update()
	{
		if (enemyCounter < waveSize && startWave)
		{
			timer += Time.deltaTime;
			if (timer >= spawnTimer)
			{
				Instantiate(enemy, start);
				enemyCounter++;
				timer = 0f;
			}
		}
		else if(DestroyedEnemyCount == waveSize)
		{
			enemyCounter = 0;
			DestroyedEnemyCount = 0;
			spawnButton.interactable = true;
			startWave = false;
			wave++;
			waveSize += wave * 2;
		}
			
	}

	public void StartWave()
	{
		startWave = true;
		spawnButton.interactable = false;
	}
}
