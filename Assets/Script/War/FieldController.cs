using UnityEngine;
using System.Collections;

public class FieldController : MonoBehaviour
{

	public float fieldLeft, fieldRight, fieldBottom, fieldTop;

	public GameObject heroPrefab, monsterPrefab;
	public float rowWidth, rowHeight;
	public Vector3 row1, row2, row3;
	float characterOffsetsY = 0.08f;
	Vector3 heroPos;
	Vector3[] monsterSpawnPosList;

	void Start() {
		Camera camera = Camera.main;
		Vector3 screenBottomLeft = camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
		Vector3 screenTopRight = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
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
		heroPos = row2 + new Vector3 (1f, 0, 0);
		GameObject heroObj = (GameObject)Instantiate (heroPrefab, heroPos, Quaternion.identity);
		Hero hero = heroObj.GetComponent<Hero>();
		hero.row = 2;

		// init monster
		monsterSpawnPosList = new Vector3[] {
			row1 + new Vector3 (rowWidth - 1f, 0, 0),
			row2 + new Vector3 (rowWidth - 1f, 0, 0),
			row3 + new Vector3 (rowWidth - 1f, 0, 0)
		};
		for (int i = 0; i < monsterSpawnPosList.Length; i++) {
			GameObject monsterObj = (GameObject)Instantiate (monsterPrefab, monsterSpawnPosList[i], Quaternion.identity);
		}

	}

}