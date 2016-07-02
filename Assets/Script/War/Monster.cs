using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Monster : MonoBehaviour {

	public float posX, posY;
	public int hp;
	Hero hero; 
	public Vector3 moveTarget;
	public float moveDuration;
	public float moveSpeed;
	public float moveDistance;
	public int attackdamage;
	public float attackCoolDown;
	public float lastAttackTime;
	public float timeSinceLastAttack;

    public bool inRange = false;
	private Animator animator;
	private bool skipMove;
	public AudioClip enemyAttck1;
	public AudioClip enemyAttack2;

	void Start () {
		
		posX = this.transform.position.x;
		posY = this.transform.position.y;
		hp = 50;

		// find hero
		hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero> ();
		moveTarget = new Vector3 (hero.posX, posY, 0);
		moveDistance = this.transform.position.x - moveTarget.x;
		moveSpeed = 0.1f;
		moveDuration = moveDistance / moveSpeed;
		attackCoolDown = 2f;
		lastAttackTime = Time.time;
		animator = GetComponent<Animator> ();

		// start moving
		this.transform.DOMove (moveTarget, moveDuration);
	}

	void Update (){
		timeSinceLastAttack = Time.time - lastAttackTime;
		if (timeSinceLastAttack - attackCoolDown > 0) {
			DoAttack ();
			timeSinceLastAttack = 0;
			lastAttackTime = Time.time;
		}
	}

	void DoAttack (){
		animator.SetTrigger ("MonsterAttack");
		hero.CallDoHit (0.3f);
	}
}
