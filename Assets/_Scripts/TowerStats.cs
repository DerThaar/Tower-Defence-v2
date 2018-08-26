using UnityEngine;
using TMPro;

public class TowerStats : MonoBehaviour
{
	[SerializeField] GameObject panel;
	[SerializeField] TextMeshProUGUI towerName;
	[SerializeField] TextMeshProUGUI towerDamage;
	[SerializeField] TextMeshProUGUI towerProjectiles;
	[SerializeField] TextMeshProUGUI towerFrequenzy;
	[SerializeField] TextMeshProUGUI towerRadius;
	[SerializeField] TextMeshProUGUI destroyedEnemies;
	GameObject currentTower;


	void Update()
	{
		if (Input.GetButtonUp("Mouse Left"))
		{

			RaycastHit hitInfo;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Tower")))
			{
				currentTower = hitInfo.transform.parent.gameObject;

				if (currentTower.GetComponent<TowerBehaviour>() != null || currentTower.GetComponent<AoETowerBehaviour>() != null)
				{
					panel.SetActive(true);
				}
			}
			else
			{
				panel.SetActive(false);
				currentTower = null;
			}
		}
		else if (Input.GetButtonDown("Mouse Right"))
		{
			panel.SetActive(false);
			currentTower = null;
		}

		if (currentTower != null && currentTower.GetComponent<TowerBehaviour>() != null)
		{
			TowerBehaviour towerBehaviour = currentTower.GetComponent<TowerBehaviour>();
			towerName.text = currentTower.name;
			towerDamage.text = $"Damage: {towerBehaviour.damage}";
			towerProjectiles.text = $"Projectiles: {towerBehaviour.numberOfProjectiles}";
			towerFrequenzy.text = $"Shooting Frequenzy: {towerBehaviour.shootTimer}";
			towerRadius.text = $"Detection Radius: {towerBehaviour.detectionRadius}";
			destroyedEnemies.text = $"Destroyed Enemies: {towerBehaviour.destroyedEnemies}";
		}
		else if (currentTower != null && currentTower.GetComponent<AoETowerBehaviour>() != null)
		{
			AoETowerBehaviour towerBehaviour = currentTower.GetComponent<AoETowerBehaviour>();
			towerName.text = currentTower.name;
			towerDamage.text = $"Damage: {towerBehaviour.damage}";
			towerProjectiles.text = $"Projectiles: {towerBehaviour.numberOfProjectiles}";
			towerFrequenzy.text = $"Shooting Frequenzy: {towerBehaviour.shootTimer}";
			towerRadius.text = $"Detection Radius: {towerBehaviour.detectionRadius}";
			destroyedEnemies.text = $"Destroyed Enemies: {towerBehaviour.destroyedEnemies}";
		}
	}
}
