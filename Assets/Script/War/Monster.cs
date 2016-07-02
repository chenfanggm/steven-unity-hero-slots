using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Monster : MonoBehaviour {

	Hero hero; 
	public Vector3 moveTarget;

	public float moveDuration = 7f;

	public int playerDamage;

    public int hp = 50;

    public bool inRange = false;

	private Animator animator;
	private bool skipMove;
	private float attackCoolDown;
    private float lastAttackTime;
	public AudioClip enemyAttack1;
	public AudioClip enemyAttack2;

	public float posX, posY;
	
	void Start () {
		posX = this.transform.position.x;
		posY = this.transform.position.y;
		attackCoolDown = 10.0f;
		lastAttackTime = Time.time;
		animator = GetComponent<Animator> ();

		// find hero
		hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero> ();
		moveTarget = new Vector3 (hero.posX, posY, 0);
		this.transform.DOMove (moveTarget, moveDuration);
	}

	void Update() {
	}
}
