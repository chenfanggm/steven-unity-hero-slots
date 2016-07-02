using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hero : MovingObject {

	public float posX, posY;
	public int row;


	public int wallDamage = 1;
	public int pointsPerFood = 10;
	public int pointsPerSoda = 20;
	public float restartLevelDelay = 1f;
	public Text foodText;
	public AudioClip moveSound1;
	public AudioClip moveSound2;
	public AudioClip eatSound1;
	public AudioClip eatSound2;
	public AudioClip drinkSound1;
	public AudioClip drinkSound2;
	public AudioClip gameOverSound;

	private Animator animator;
	private int hp;
	private Vector2 touchOrigin = -Vector2.one;
    private float coolDown;
    private float lastAttackTime;

	protected override void Start () {

		posX = this.transform.position.x;
		posY = this.transform.position.y;

        coolDown = 1.0f;
        lastAttackTime = Time.time;

		animator = GetComponent<Animator> ();

	
		base.Start ();
	}

	// Update is called once per frame
	void Update () {
		//if (!GameManager.instance.playersTurn)
			//return;

		int horizontal = 0;
		int vertical = 0;

        if (Time.time - lastAttackTime > coolDown)
        {
            lastAttackTime = Time.time;
        }

	}

	protected override void AttemptMove <T> (int xDir, int yDir)
	{
		//hp--;
		foodText.text = "HP: " + hp;

		base.AttemptMove <T> (xDir, yDir);



		CheckIfGameOver ();

	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Exit") {
			Invoke ("Restart", restartLevelDelay);
			enabled = false;
		} else if (other.tag == "Food") {
			hp += pointsPerFood;
			foodText.text = "+" + pointsPerFood + " HP: " + hp;
			other.gameObject.SetActive (false);
		} else if (other.tag == "Soda") {
			hp += pointsPerSoda;
			foodText.text = "+" + pointsPerSoda + " HP: " + hp;
			other.gameObject.SetActive (false);
		}
	}

	protected override void OnCantMove <T> (T component)
	{
		animator.SetTrigger ("playerChop");
	}
		

	public void LoseFood (int loss)
	{
		animator.SetTrigger ("playerHit");
		hp -= loss;
		foodText.text = "-" + loss + " HP: " + hp;
		CheckIfGameOver ();
	}

	private void CheckIfGameOver()
	{

	}
}
