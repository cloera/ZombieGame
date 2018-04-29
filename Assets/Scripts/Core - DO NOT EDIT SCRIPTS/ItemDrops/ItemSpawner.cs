using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemDropSettings
{
	public int lootChance;
	public int minSpawnTime;
	public int maxSpawnTime;
}

public class ItemSpawner : MonoBehaviour {



	public ItemPickup[] pickupList;
	protected GameObject activeItem;


	int dropChance = 50;
	int minSpawnTime = 30;
	int maxSpawnTime = 60;

	// Use this for initialization
	void Start () {
		SetSpawnSettings (GameManager.getItemSettings ());

		StartCoroutine (SpawnItem ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetSpawnSettings(ItemDropSettings settings)
	{
		dropChance = Mathf.Clamp(settings.lootChance, 0, 100);
		minSpawnTime = settings.minSpawnTime;
		maxSpawnTime = settings.maxSpawnTime;
	}

	IEnumerator SpawnItem()
	{
		int randomTime = Random.Range (minSpawnTime, maxSpawnTime);
		yield return new WaitForSeconds (randomTime);

		int luckRoll = Random.Range (0, 100);

		if (luckRoll > 100 - dropChance) {
			int randomPick = Random.Range (0, pickupList.Length);
			ItemPickup pick = pickupList [randomPick];

			Vector3 randomDir = new Vector3 (Random.Range (0.0f, 5.0f), 0,
				                    Random.Range (0.0f, 5.0f));

			Instantiate (pick, transform.position + randomDir, pick.transform.rotation);
		}

		StartCoroutine (SpawnItem());
	}
}
