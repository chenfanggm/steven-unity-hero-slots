using UnityEngine;
using System.Collections;

public class FieldController : MonoBehaviour
{
	// const
	const int FIELD_STATE_IDLE = 0;
	const int FIELD_STATE_START = 1;
	const int FIELD_STATE_STOP = 2;
	const float FIELD_MONSTER_SPAWN_INTERVAL = 4f;

	public int fieldState = 0;
	public float fieldLeft, fieldRight, fieldBottom, fieldTop;

	public GameObject heroPrefab, monsterPrefab;
	public float rowWidth, rowHeight;
	public Vector3 row1, row2, row3;
	float characterOffsetsY = 0.08f;
	Hero hero;
	Vector3 heroSpawnPos;
	ArrayList monsterList = new ArrayList ();
	public ArrayList monsterSpawnPosList = new ArrayList ();

	void Start ()
	{
		Camera camera = Camera.main;
		Vector3 screenBottomLeft = camera.ScreenToWorldPoint (new Vector3 (0, 0, 0));
		Vector3 screenTopRight = camera.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));
		fieldLeft = screenBottomLeft.x;
		fieldRight = screenTopRight.x;
		fieldBottom = 0.1f;
		fieldTop = screenTopRight.y / 2;

		// init rows
		rowWidth = fieldRight - fieldLeft;
		rowHeight = (fieldTop - fieldBottom) / 3;
		row1 = new Vector3 (fieldLeft, fieldBottom + characterOffsetsY, 0);
		row2 = new Vector3 (fieldLeft, row1.y + rowHeight + characterOffsetsY, 0);
		row3 = new Vector3 (fieldLeft, row2.y + rowHeight + characterOffsetsY, 0);

		// init hero
		heroSpawnPos = row2 + new Vector3 (1f, 0, 0);
		GameObject heroObj = (GameObject)Instantiate (heroPrefab, heroSpawnPos, Quaternion.identity);
		hero = heroObj.GetComponent<Hero> ();
		hero.row = 2;

		monsterSpawnPosList.Add (row1 + new Vector3 (rowWidth, 0, 0));
		monsterSpawnPosList.Add (row2 + new Vector3 (rowWidth, 0, 0));
		monsterSpawnPosList.Add (row3 + new Vector3 (rowWidth, 0, 0));

		fieldState = FIELD_STATE_START;

		// spawning monster
		StartCoroutine (
			SpawnMonster (() => {
			})
		);
	}

	void Update ()
	{
		
	}

	IEnumerator SpawnMonster (System.Action callback)
	{
		while (fieldState == FIELD_STATE_START) {
			int oneTimeSpawnCount = Random.Range (1, monsterSpawnPosList.Count);
			ArrayList tempMonsterSpawnPosList = new ArrayList(monsterSpawnPosList);
			for (int i = 0; i < oneTimeSpawnCount; i++) {
				int spawnRowIndex = Random.Range (0, monsterSpawnPosList.Count - 1);
				Vector3 spawnPos = (Vector3) tempMonsterSpawnPosList[spawnRowIndex];
				tempMonsterSpawnPosList.RemoveAt (spawnRowIndex);
				GameObject monsterObj = (GameObject)Instantiate (monsterPrefab, spawnPos, Quaternion.identity);
				Monster monster = monsterObj.GetComponent<Monster> ();
				monsterList.Add (monster);
			}
			yield return new WaitForSeconds (FIELD_MONSTER_SPAWN_INTERVAL);
		}
		callback ();
	}

	void SpawnMonster ()
	{
		
	}

}