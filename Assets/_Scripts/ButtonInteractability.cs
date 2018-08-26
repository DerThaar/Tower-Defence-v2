using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonInteractability : MonoBehaviour
{
	[SerializeField] Stats stats;
	[SerializeField] BuildTower buildTower;
	[SerializeField] int prefabNumber;
	[SerializeField] string name;


	void Start()
	{
		GetComponentInChildren<TextMeshProUGUI>().text = $"{name} ({buildTower.buildCost[prefabNumber]}$)";
	}

	void Update()
	{
		if (buildTower.buildCost[prefabNumber] <= stats.currentMoney)
			GetComponent<Button>().interactable = true;
		else
			GetComponent<Button>().interactable = false;
	}
}
