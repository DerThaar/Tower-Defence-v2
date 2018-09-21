using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveSpawn : MonoBehaviour
{
	[SerializeField] GameObject enemy;
	[SerializeField] Transform enemies;
	[SerializeField] int waveSize;
	[SerializeField] float spawnTimer;
	[SerializeField] Button spawnButton;
	[SerializeField] TextMeshProUGUI enemiesOnScreen;
	[SerializeField] TextMeshProUGUI waveText;
	public int DestroyedEnemyCount;
	public int wave = 1;
	public float speed = 3f;
	public float health = 1f;
	float timer;
	int enemyCounter;
	bool startWave;


	void Update()
	{
		//spawn enemies
		if (enemyCounter < waveSize && startWave)
		{
			timer += Time.deltaTime;
			if (timer >= spawnTimer)
			{
				Instantiate(enemy, enemies);
				enemyCounter++;
				timer = 0f;
			}
		}
		//prepare new wave
		else if(DestroyedEnemyCount == waveSize)
		{
			enemyCounter = 0;
			DestroyedEnemyCount = 0;
			spawnButton.interactable = true;
			startWave = false;
			wave++;
			waveSize += wave * 2;
			speed *= 1.02f;
			health *= 1.1f;
			spawnTimer *= 0.9f;
		}

		enemiesOnScreen.text = $"Enemies On Screen: {enemies.childCount}";
		waveText.text = $"Wave: {wave}";
	}

	public void StartWave()
	{
		startWave = true;
		spawnButton.interactable = false;
	}
}
