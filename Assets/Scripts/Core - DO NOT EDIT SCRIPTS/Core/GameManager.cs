using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour {

	private static GameManager instance;

	private List<Enemy> activeZombies;
	private List<Enemy> activeSpecials;
	private List<ItemPickup> activeItems;

	private List<Survivor> survivors;

	private Text textDisplay;

	private int score;
	private int counter1;
	private int counter2;

	public float SpawnZombieWave_Speed;
	public float SpawnSpecial_Speed;
	public int MaxZombies;
	public int ZombiesPerWave;
	public int MaxSpecials;

	public Transform[] spawnAreas;
	public GameObject zombiePrefab;
	public GameObject[] specialPrefabs;

	public ItemDropSettings itemDropSettings;

	void Awake()
	{
		if (instance == null) {
			instance = this;
			instance.Setup ();
			DontDestroyOnLoad (gameObject);
		} else {
			DestroyImmediate (gameObject);
		}

	}

	void Setup()
	{
		instance.activeZombies = new List<Enemy> ();
		instance.activeSpecials = new List<Enemy> ();
		instance.activeItems = new List<ItemPickup> ();
	}

	// Use this for initialization
	void Start () {
		textDisplay = GetComponentInChildren<Text> ();
		textDisplay.text = "Score: 0";

		activeZombies = new List<Enemy> ();
		activeSpecials = new List<Enemy> ();
		if (survivors == null) {
			survivors = new List<Survivor> ();
		}
		StartCoroutine (SpawnZombieWave());
		StartCoroutine (SpawnSpecial());

		if (activeZombies.Count < (MaxZombies / ZombiesPerWave)) {
			for (int i = 0; i < ZombiesPerWave; i++) {
				SpawnZombie ();
			}
		}
	}

	void CheckDifficulty(int points)
	{
		counter1 += points;
		counter2 += points;

		if (counter1 >= 100) {
			
			ZombiesPerWave += 1;
			MaxZombies	+= 1;
			counter1 = 0;
		}
		if (counter2 >= 200) {
			SpawnSpecial_Speed -= 1.0f;
			SpawnZombieWave_Speed -= 1.0f;
			MaxSpecials += 1;
			counter2 = 0;
		}

	}


	#region SurvivorCode
	public static void AddSurvivor(Survivor s)
	{
		if (instance.survivors == null) {
			instance.survivors = new List<Survivor> ();
		}

		instance.survivors.Add (s);
	}

	public static void RemoveSurvivor(Survivor s)
	{
		instance.survivors.Remove (s);
	}

	public static List<Survivor> getSurvivorList()
	{
		return instance.survivors;
	}

	public static void AddItem(ItemPickup i)
	{
		instance.activeItems.Add (i);
	}

	public static void RemoveItem(ItemPickup i)
	{
		instance.activeItems.Remove (i);
	}

	public static List<ItemPickup> getItemList()
	{
		return instance.activeItems;
	}

	#endregion

	#region ZombieCode
	// Zombie Code
	public static void AddEnemy(Enemy e, EnemyType type)
	{
		if (EnemyType.ZOMBIE == type) {
			instance.activeZombies.Add (e as Zombie);
		} else {
			instance.activeSpecials.Add (e as SpecialEnemy);
		}

	}

	public static void RemoveEnemy(Enemy e, EnemyType type)
	{
		int points = 0;

		switch (type) {
		case EnemyType.ZOMBIE:
			instance.activeZombies.Remove (e as Zombie);
			instance.score += 10;
			points = 10;
			break;
		case EnemyType.BOOMER:
			instance.activeSpecials.Remove (e as SpecialEnemy);
			instance.score += 50;
			points = 10;
			break;
		case EnemyType.SPITTER:
			instance.activeSpecials.Remove (e as SpecialEnemy);
			instance.score += 50;
			points = 10;
			break;
		case EnemyType.MOOSE:
			instance.activeSpecials.Remove (e as SpecialEnemy);
			instance.score += 100;
			points = 10;
			break;
		default:
			break;
		}
		instance.textDisplay.text = "Score: " + instance.score;

		instance.CheckDifficulty (points);
	}


	public static List<Enemy> getZombieList()
	{
		return instance.activeZombies;
	}
	public static List<Enemy> getSpecialList()
	{
		return instance.activeSpecials;
	}



	IEnumerator SpawnZombieWave()
	{
		yield return new WaitForSeconds (SpawnZombieWave_Speed);

		if (activeZombies.Count < (MaxZombies / ZombiesPerWave)) {
			for (int i = 0; i < ZombiesPerWave; i++) {
				SpawnZombie ();
			}
		}

		StartCoroutine (SpawnZombieWave ());
	}

	void SpawnZombie()
	{
		
		if (activeZombies.Count < MaxZombies) {
			Vector3 pos = getSpawnPosition ();
			Instantiate (zombiePrefab, pos, Quaternion.identity);
		}

	}

	IEnumerator SpawnSpecial()
	{
		yield return new WaitForSeconds (SpawnSpecial_Speed);

		if (activeSpecials.Count < MaxSpecials) {
			Vector3 pos = getSpawnPosition ();
			int i = Random.Range (0, specialPrefabs.Length);

			Instantiate (specialPrefabs[i], pos, Quaternion.identity);
		}
		StartCoroutine (SpawnSpecial ());

	}

	Vector3 getSpawnPosition()
	{
		Vector3 output = Vector3.zero;
		Transform area = spawnAreas [Random.Range (0, spawnAreas.Length)];
		return area.position;
	}
	#endregion

	public static Transform getRandomZombieSpawnLocation()
	{
		int index = Random.Range (0, instance.spawnAreas.Length);

		return instance.spawnAreas [index];
	}

	public static ItemDropSettings getItemSettings()
	{
		return instance.itemDropSettings;
	}
}
