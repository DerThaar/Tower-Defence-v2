﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : MonoBehaviour
{
	[SerializeField] GameObject[] prefab;
	[SerializeField] GameObject[] building;
	[SerializeField] int[] buildCost;
	bool build;
	Vector3 position;
	GameObject previewBuilding;
	GameObject newBuilding;
	GameObject currentPreviewBuilding;
	int currentBuildCost;
	Stats stats;

	void Awake()
	{
		stats = GetComponent<Stats>();
	}

	void Update()
	{
		if (Input.GetButtonDown("Mouse Right") && previewBuilding != null)
		{
			Destroy(currentPreviewBuilding);
			build = false;
		}

		if (build)
		{
			RaycastHit hitInfo;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Border"));
			currentPreviewBuilding.transform.position = hitInfo.point;

			if (Input.GetButtonUp("Mouse Left") && hitInfo.collider && currentBuildCost <= stats.currentMoney)
			{
				Instantiate(newBuilding, hitInfo.point, newBuilding.transform.rotation);
				stats.currentMoney -= currentBuildCost;
			}

		}
	}

	void Instantiate()
	{
		build = true;
		if (currentPreviewBuilding == null)
			currentPreviewBuilding = Instantiate(previewBuilding);
	}

	public void BuildTower1()
	{
		previewBuilding = prefab[0];
		newBuilding = building[0];
		currentBuildCost = buildCost[0];
		Instantiate();
	}

	public void BuildTower2()
	{
		previewBuilding = prefab[1];
		Instantiate();
	}

	public void BuildTower3()
	{
		previewBuilding = prefab[2];
		Instantiate();
	}
}