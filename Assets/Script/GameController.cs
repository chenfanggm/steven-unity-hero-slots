using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameController : MonoBehaviour {


    public enum GameStatus
    {
        PreGame,
        InGame,
        Win,
        Lose
    };
    private static GameController instance = null;

    public GameStatus status;

    private Text tickText;

    private GameController()
    {
        status = GameStatus.PreGame;
    }

    public static void Init()
    {
        instance= null;

    }
    public static GameController getInstance()
	{
        return instance;
    }

    void Awake()
    {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}
	}

	void Start() {
		status = GameStatus.PreGame;
		StartGame ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Tick()
    {
        for (int i = 3; i > 0; i--)
        {
            tickText.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        tickText.text = "GO!!!!!";
        yield return new WaitForSeconds(0.5f);
        tickText.text = "";
        status = GameStatus.InGame;
    }

    internal void StartGame()
    {
        tickText = GameObject.Find("TickText").GetComponent<Text>();
        StartCoroutine(Tick());
    }
    
    
}
