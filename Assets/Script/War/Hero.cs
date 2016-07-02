using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hero : MonoBehaviour {

	public float posX, posY;
	public int row;
	public int hp;
	public float attackRadius;
	public int attackDamage;
	public FieldController fieldController;
	ArrayList monsterList;

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
	private Vector2 touchOrigin = -Vector2.one;
    private float coolDown;
    private float lastAttackTime;

	void Start () {
		posX = this.transform.position.x;
		posY = this.transform.position.y;
		hp = 100;
		attackDamage = 20;


        coolDown = 1.0f;
        lastAttackTime = Time.time;
		animator = GetComponent<Animator> ();

		// find field controller
		fieldController = GameObject.FindGameObjectWithTag("FieldController").GetComponent<FieldController> ();
		monsterList = fieldController.monsterList;
	}

	void Update () {
		this.posX = this.transform.position.x;

		// handle input
		if (Input.GetKeyDown ("space")) {
			DoAttack ();
		}
	}

	void DoAttack () {
		if (monsterList.Count > 0) {
			// get closest monster
			Monster minDistanceMonster = null;
			float minDistance = 100f;
			for (int i = 0; i < monsterList.Count; i++) {
				Monster monster = (Monster) monsterList [i];
				if (minDistanceMonster == null) {
					minDistance = Mathf.Abs(this.posX - monster.posX);
					minDistanceMonster = monster;
				} else {
					float distance = Mathf.Abs (this.posX - monster.posX);
					if (distance < minDistance) {
						minDistance = distance;
						minDistanceMonster = monster;
					}
				}
			}

			if (minDistanceMonster != null) {
				minDistanceMonster.CallDoHit (attackDamage, 0.1f);
			}

			animator.SetTrigger ("HeroAttack");
		}
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
