using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Enemy : MovingObject {

	Hero hero; 
	Vector3 moveTarget;

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
	
	protected override void Start () {
		posX = this.transform.position.x;
		posY = this.transform.position.y;
		attackCoolDown = 10.0f;
		lastAttackTime = Time.time;
		animator = GetComponent<Animator> ();

		// find hero
		hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero> ();
		moveTarget = new Vector3 (hero.posX, posY, 0);
	}

	void Update() {
		this.transform.DOMove (moveTarget, moveDuration);
	}

	protected override void AttemptMove <T> (int xDir, int yDir)
	{
		if (skipMove) {
			skipMove = false;
			return;
		}

		base.AttemptMove<T> (xDir, yDir);

		skipMove = true;
	}

	public void MoveEnemy()
	{
		int xDir = 0;
		int yDir = 0;

		//if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
		//yDir = target.position.y > transform.position.y ? 1 : -1;
		xDir = -1;
		//else
		//xDir = target.position.x > transform.position.x ? 1 : -1;
		xDir = -1;

		//if (Mathf.RoundToInt(target.position.x) <2  && Mathf.RoundToInt(target.position.y) <2 )
		//hp--;
		//if(hp<=0)Destroy(gameObject);

	}


    public void EnemyAttacked(float y,int minusHp)
    {
        if (Mathf.Abs(transform.position.y - y) < float.Epsilon)
            hp -= minusHp;
        // TODO: Memory leak!
        lock(this)
        {
            if (hp < 0)
            {
                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
        }
    }

	protected override void OnCantMove <T> (T component)
	{

        if (Time.time - lastAttackTime > attackCoolDown)
        {
            lastAttackTime = Time.time;
        }
        else return;

		animator.SetTrigger ("enemyAttack");

	}
}
