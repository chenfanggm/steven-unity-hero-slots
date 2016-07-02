using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Monster : MonoBehaviour {

	public float posX, posY;
	public int hp;
	Hero hero; 
	public Vector3 moveTarget;
	public float moveDuration;
	public int attackdamage;
	public float attackCoolDown;
	private float lastAttackTime;

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
		moveDuration = 30f;
		attackCoolDown = 10f;
		lastAttackTime = Time.time;
		animator = GetComponent<Animator> ();

		// start moving
		this.transform.DOMove (moveTarget, moveDuration);
	}

	void Update() {
	}
}
