using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
	// const
	const int SLOT_STATE_IDLE = 0;
	const int SLOT_STATE_SPIN = 1;
	const int SLOT_STATE_STOP = 2;
	public const float LINE_STOP_INTERVAL = 0.1f;

	// ui
	public Button spinBtn;

	// property
	public GameObject cellPrefab;
	public int cellSpinState = 0;
	public Vector3 firstCellPos = new Vector3 (-2f, -2.5f, 0f);
	public Vector3 secondCellPos = new Vector3 (-0.6f, -2.5f, 0f);
	public Vector3 thirdCellPos = new Vector3 (0.8f, -2.5f, 0f);
	Vector3[] cellPos;
	SlotCell[] cells;

	// other
	Hero hero;

	void Start ()
	{		
		cellPos = new Vector3[] { firstCellPos, secondCellPos, thirdCellPos };
		cells = new SlotCell[cellPos.Length];
		for (int i = 0; i < cellPos.Length; i++) {
			GameObject cell = (GameObject)Instantiate (cellPrefab, cellPos [i], Quaternion.identity);
			cells [i] = cell.GetComponent<SlotCell> ();
			cells [i].SetSymbol (Random.Range (0, 9));
		}

		// find hero
		hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero> ();
	}

	void Update ()
	{
		if (cellSpinState == SLOT_STATE_SPIN) {
			for (int i = 0; i < cells.Length; i++) {
				SlotCell cell = cells [i];
				if (cell.isSpin == false) {
					cells [i].StartSpin ();
				} 
			}
		} else if (cellSpinState == SLOT_STATE_STOP) {
			cellSpinState = SLOT_STATE_IDLE;
			if (cells [0].isSpin == true) {
				StartCoroutine (
					StopLines (() => {
						HighlightSlotResults (GetSlotResults ());
					}));
			}
		}
	}

	public void SwitchSpin ()
	{
		if (cellSpinState == SLOT_STATE_IDLE) {
			cellSpinState = SLOT_STATE_SPIN;
		} else if (cellSpinState == SLOT_STATE_SPIN) {
			cellSpinState = SLOT_STATE_STOP;
		}
	}

	public void StopSpin ()
	{
		cellSpinState = SLOT_STATE_STOP;
	}

	IEnumerator StopLines (System.Action callback)
	{
		for (int i = 0; i < cells.Length; i++) {
			SlotCell cell = cells [i];
			if (cell.isSpin == true) {
				cell.StopSpin ();
				yield return new WaitForSeconds (LINE_STOP_INTERVAL);
			}
		}
		yield return new WaitForSeconds (LINE_STOP_INTERVAL + SlotCell.STOP_IMPEDENCE_DELAY_TIME);
		callback ();
	}

	Hashtable GetSlotResults ()
	{
		Hashtable results = new Hashtable ();
		for (int i = 0; i < cells.Length; i++) {
			int symbol = cells [i].symbol;
			if (results.ContainsKey (symbol)) {
				results [symbol] = (int)results [symbol] + 1;
			} else {
				results [symbol] = 1;
			}
		}
		return results;
	}

	void HighlightSlotResults (Hashtable slotResults)
	{
		foreach (DictionaryEntry result in slotResults) {
			if (result.Value.Equals (2)) {
				for (int i = 0; i < cells.Length; i++) {
					if (cells [i].symbol == (int)result.Key) {
						cells [i].HighLight ();
					}
				}
				// trigger hero spell
				hero.DoSpell ();
			} else if (result.Value.Equals (3)) {
				for (int i = 0; i < cells.Length; i++) {
					if (cells [i].symbol == (int)result.Key) {
						cells [i].SuperHighLight ();
					}
				}
			} 
		}
	}
}