using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerStats : MonoBehaviour
{
	[SerializeField] GameObject panel;
	[SerializeField] TextMeshProUGUI towerName;
	[SerializeField] TextMeshProUGUI towerDamage;
	[SerializeField] TextMeshProUGUI towerProjectiles;
	[SerializeField] TextMeshProUGUI towerFrequenzy;
	[SerializeField] TextMeshProUGUI towerRadius;
	[SerializeField] TextMeshProUGUI destroyedEnemies;
	[SerializeField] TextMeshProUGUI upgradeText;
	[SerializeField] Stats stats;
	[SerializeField] BuildTower buildTower;
	[SerializeField] Button upgradeButton;
	GameObject currentTower;	


	void Update()
	{
		//get the current tower
		if (Input.GetButtonUp("Mouse Left") && buildTower.currentPreviewBuilding == null)
		{

			RaycastHit hitInfo;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hitInfo, 100, LayerMask.GetMask("Tower")))
			{
				currentTower = hitInfo.transform.parent.gameObject;

				if (currentTower.GetComponent<TowerBehaviour>() != null || currentTower.GetComponent<AoETowerBehaviour>() != null)
				{
					panel.SetActive(true);
				}
			}			
		}
		else if (Input.GetButtonUp("Mouse Right"))
		{
			panel.SetActive(false);
			currentTower = null;
		}

		//check if current tower is a single shot tower (will possible be changed)
		if (currentTower != null && currentTower.GetComponent<TowerBehaviour>() != null)
		{
			TowerBehaviour towerBehaviour = currentTower.GetComponent<TowerBehaviour>();
			towerName.text = currentTower.name;
			towerDamage.text = $"Damage: {towerBehaviour.damage}";
			towerProjectiles.text = $"Projectiles: {towerBehaviour.numberOfProjectiles}";
			towerFrequenzy.text = $"Shooting Frequenzy: {towerBehaviour.shootTimer}";
			towerRadius.text = $"Detection Radius: {towerBehaviour.detectionRadius}";
			destroyedEnemies.text = $"Destroyed Enemies: {towerBehaviour.destroyedEnemies}";
			upgradeText.text = $"Upgrade ({towerBehaviour.upgradeCost}$)";

			//check if allready upgraded
			if (stats.currentMoney >= towerBehaviour.upgradeCost && !towerBehaviour.upgraded)
				upgradeButton.interactable = true;
			else
				upgradeButton.interactable = false;
		}
		//check if current tower is an AoE tower (will possible be changed)
		else if (currentTower != null && currentTower.GetComponent<AoETowerBehaviour>() != null)
		{
			AoETowerBehaviour towerBehaviour = currentTower.GetComponent<AoETowerBehaviour>();
			towerName.text = currentTower.name;
			towerDamage.text = $"Damage: {towerBehaviour.damage}";
			towerProjectiles.text = $"Projectiles: {towerBehaviour.numberOfProjectiles}";
			towerFrequenzy.text = $"Shooting Frequenzy: {towerBehaviour.shootTimer}";
			towerRadius.text = $"Detection Radius: {towerBehaviour.detectionRadius}";
			destroyedEnemies.text = $"Destroyed Enemies: {towerBehaviour.destroyedEnemies}";
			upgradeText.text = $"Upgrade ({towerBehaviour.upgradeCost}$)";

			//check if allready upgraded
			if (stats.currentMoney >= towerBehaviour.upgradeCost && !towerBehaviour.upgraded)
				upgradeButton.interactable = true;
			else
				upgradeButton.interactable = false;
		}
	}

	public void UpgradeTower()
	{
		//check if current tower is a single shot tower (will possible be changed) and apply the upgrade
		if (currentTower != null && currentTower.GetComponent<TowerBehaviour>() != null)
		{
			TowerBehaviour towerBehaviour = currentTower.GetComponent<TowerBehaviour>();
			if (stats.currentMoney >= towerBehaviour.upgradeCost)
			{
				stats.currentMoney -= towerBehaviour.upgradeCost;
				towerBehaviour.damage = 2;
				towerBehaviour.shootTimer = 0.25f;
				currentTower.GetComponent<Renderer>().material.color = Color.blue;
				towerBehaviour.upgraded = true;
			}

		}
		//check if current tower is an AoE tower (will possible be changed) and apply the upgrade
		else if (currentTower != null && currentTower.GetComponent<AoETowerBehaviour>() != null)
		{
			AoETowerBehaviour towerBehaviour = currentTower.GetComponent<AoETowerBehaviour>();
			if (stats.currentMoney >= towerBehaviour.upgradeCost)
			{
				stats.currentMoney -= towerBehaviour.upgradeCost;
				towerBehaviour.damage = 2;
				towerBehaviour.numberOfProjectiles = 8;				
				towerBehaviour.shootTimer = 0.5f;
				currentTower.transform.GetChild(2).gameObject.SetActive(false);
				currentTower.transform.GetChild(3).gameObject.SetActive(true);
				currentTower.transform.GetChild(1).GetComponent<Renderer>().material.color = Color.blue;
				towerBehaviour.Start();
				towerBehaviour.upgraded = true;
			}
		}
	}
}
