using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI healthText;
	[SerializeField] TextMeshProUGUI moneyText;
	[SerializeField] int maxHealth;
	public int currentHealth;
	public int currentMoney;


	void Start()
	{
		currentHealth = maxHealth;
	}

	void Update()
	{
		if (currentHealth < 0)
			currentHealth = 0;

		healthText.text = $"Health: {currentHealth}/{maxHealth}";
		moneyText.text = $"Money: {currentMoney}$";
	}
}
