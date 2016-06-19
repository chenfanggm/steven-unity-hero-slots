using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SlotCell : MonoBehaviour
{
	public enum CellSymbol
	{
		JAR = 0,
		CAT = 1,
		SKULL = 2,
		POT = 3,
		PUMPKIN = 4,
		HAT = 5,
		TREE = 6,
		WITCH = 7,
		DOLL = 8
	}

	public static float STOP_IMPEDENCE_DELAY_TIME = 0.3f;

	[UnityToolbag.SortingLayer]
	public int sortingLayer;

	public float scrollSpeed = 1f;
	public bool isSpin = false;
	public int symbol;

	Renderer rend;
	Vector2 uvOffset;

	void Awake ()
	{
		rend = GetComponent<Renderer> ();
		rend.sortingLayerID = sortingLayer;
	}

	void Update ()
	{
		if (isSpin) {
			Spin ();
		}
	}

	void Spin ()
	{
		float offsetY = uvOffset.y;
		offsetY += Time.deltaTime * scrollSpeed;
		if (offsetY > 0.9f) {
			offsetY -= 0.9f;
		}
		SetUvOffset (offsetY);
	}

	public void StartSpin ()
	{
		isSpin = true;	
	}

	public void StopSpin ()
	{
		if (isSpin == true) {
			float endY = (float)Mathf.Ceil (uvOffset.y * 10) / 10;
			symbol = GetSymbolFromUvOffset (endY);
			DOTween.To (() => uvOffset.y, y => SetUvOffset (y), endY, STOP_IMPEDENCE_DELAY_TIME)
				.OnComplete (() => { isSpin = false; });
		}
	}

	public void HighLight() {
		this.transform.DOPunchScale (new Vector3(0.8f, 0.8f, 0), 0.8f, 3, 0.8f);
	}

	public void SuperHighLight() {
		this.transform.DOPunchScale (new Vector3(0.8f, 0.8f, 0), 0.8f, 3, 0.8f);
	}

	public void SetUvOffset (float offsetY)
	{
		uvOffset = new Vector2 (0f, offsetY);
		rend.material.SetTextureOffset ("_MainTex", uvOffset);
	}

	public void SetSymbol (int symbol)
	{
		this.symbol = symbol;
		SetUvOffset (GetUvOffsetFromSymbol (symbol));
	}

	float GetUvOffsetFromSymbol (int symbol)
	{
		return (float) symbol / 10;
	}

	int GetSymbolFromUvOffset (float uvOffset)
	{
		return Mathf.FloorToInt (uvOffset * 10);
	}
}
