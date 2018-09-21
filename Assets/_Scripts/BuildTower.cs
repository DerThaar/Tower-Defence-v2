using UnityEngine;

public class BuildTower : MonoBehaviour
{
	[SerializeField] GameObject[] prefab;
	[SerializeField] GameObject[] building;
	public int[] buildCost;
	public GameObject currentPreviewBuilding;
	GameObject previewBuilding;
	bool build;
	Vector3 position;
	GameObject newBuilding;
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
			Physics.Raycast(ray, out hitInfo, 100, LayerMask.GetMask("Border"));
			currentPreviewBuilding.transform.position = hitInfo.point;

			if (Input.GetButtonDown("Mouse Left") && hitInfo.collider && currentBuildCost <= stats.currentMoney)
			{
				GameObject instantiatedBuilding = Instantiate(newBuilding, hitInfo.point, newBuilding.transform.rotation);
				instantiatedBuilding.name = newBuilding.name;
				stats.currentMoney -= currentBuildCost;
			}
		}
	}

	void Instantiate()
	{
		build = true;
		Vector3 instantiatePosition = Input.mousePosition;
		currentPreviewBuilding = Instantiate(previewBuilding, instantiatePosition, Quaternion.identity);
	}

	public void BuildTower1()
	{
		previewBuilding = prefab[0];
		newBuilding = building[0];
		currentBuildCost = buildCost[0];
		Instantiate();
	}

	public void BuildAoETower1()
	{
		previewBuilding = prefab[1];
		newBuilding = building[1];
		currentBuildCost = buildCost[1];
		Instantiate();
	}
}
