using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hero : MonoBehaviour {

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

	void Start () {

		posX = this.transform.position.x;
		posY = this.transform.position.y;

        coolDown = 1.0f;
        lastAttackTime = Time.time;

		animator = GetComponent<Animator> ();
	
	}

	void Update () {
		if (Input.GetKeyDown ("space")) {
			DoAttack ();
		}
	}

	void DoAttack () {
		animator.SetTrigger ("HeroAttack");
	}

	void DoSpell () {
		
	}

	void DoHit () {
		animator.SetTrigger ("HeroHit");
	}

	public void CallDoHit (float delayTime) {
		Invoke("DoHit", delayTime); 
	}
}
