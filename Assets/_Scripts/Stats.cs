using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI healthText;
	[SerializeField] TextMeshProUGUI moneyText;
	[SerializeField] int maxHealth = 5;
	public int currentHealth;
	public int currentMoney;


	void Awake()
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
