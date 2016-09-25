using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Monster : MonoBehaviour {

	public float posX, posY;
	public int hp;
	public bool isMoving = false;
	public Vector3 moveTarget;
	public float moveDuration;
	public float moveSpeed;
	public float moveDistance;
	public int attackdamage;
	public float attackCoolDown;
	public float lastAttackTime;
	public float timeSinceLastAttack;

	// other
	public FieldController fieldController;
	Hero hero; 

    public bool inRange = false;
	private Animator animator;
	private bool skipMove;
	public AudioClip enemyAttck1;
	public AudioClip enemyAttack2;

	void Start () {
		
		posX = this.transform.position.x;
		posY = this.transform.position.y;
		hp = 50;

		// find field controller
		fieldController = GameObject.FindGameObjectWithTag("FieldController").GetComponent<FieldController> ();

		// find hero
		hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero> ();
		moveTarget = new Vector3 (hero.posX, posY, 0);
		moveDistance = this.transform.position.x - moveTarget.x;
		moveSpeed = 0.2f;
		moveDuration = moveDistance / moveSpeed;
		attackCoolDown = 2f;
		lastAttackTime = Time.time;
		animator = GetComponent<Animator> ();
	}

	void Update (){
		// wait until game start
		if (GameController.getInstance().status == GameController.GameStatus.PreGame) {
			return;
		}

		// start moving
		if (!isMoving) {
			this.transform.DOMove (moveTarget, moveDuration);
			isMoving = true;
		}

		this.posX = this.transform.position.x;
		// auto attacking
		timeSinceLastAttack = Time.time - lastAttackTime;
		if (timeSinceLastAttack - attackCoolDown > 0) {
			DoAttack ();
			timeSinceLastAttack = 0;
			lastAttackTime = Time.time;
		}
	}

	void DoAttack (){
		animator.SetTrigger ("MonsterAttack");
		if (hero != null) {
			hero.CallDoHit (0.3f);
		}
	}

	IEnumerator DoHit (int damage, float delayTime) {
		this.hp -= damage;
		yield return new WaitForSeconds (delayTime);
		//animator.SetTrigger ("MonsterHit");
	}

	public void CallDoHit (int damage, float delayTime) {
		StartCoroutine (
			DoHit (damage, delayTime)
		);
	}
}
