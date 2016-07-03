using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class Hero : MonoBehaviour {

	public float posX, posY;
	public int row;
	public int hp;
	public int attackDamage;
	public float spellRadius;
	public float attackCoolDown;
	public float lastAttackTime;
	public float timeSinceLastAttack;
	public FieldController fieldController;
	public ArrayList monsterList;
	Animator animator;

	void Start () {
		posX = this.transform.position.x;
		posY = this.transform.position.y;
		hp = 100;
		attackDamage = 20;
		spellRadius = 10f;
		attackCoolDown = 1.5f;
		lastAttackTime = Time.time;
		animator = GetComponent<Animator> ();

		// find field controller
		fieldController = GameObject.FindGameObjectWithTag("FieldController").GetComponent<FieldController> ();
		monsterList = fieldController.monsterList;
	}

	void Update () {
		// wait until game start
		if (GameController.getInstance().status == GameController.GameStatus.PreGame) {
			return;
		}

		this.posX = this.transform.position.x;

		// auto attacking
		timeSinceLastAttack = Time.time - lastAttackTime;
		if (timeSinceLastAttack - attackCoolDown > 0) {
			DoAttack ();
			timeSinceLastAttack = 0;
			lastAttackTime = Time.time;
		}

		// handle input
		if (Input.GetKeyDown ("space")) {
			DoSpell ();
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

	public void DoSpell () {
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

			// get monster in the attacking radius
			if (minDistanceMonster != null) {
				ArrayList insideRadiusMonsterList = new ArrayList ();
				insideRadiusMonsterList.Add (minDistanceMonster);

				for (int i = 0; i < monsterList.Count; i++) {
					Monster monster = (Monster) monsterList [i];
					float distance = Vector3.Distance (minDistanceMonster.transform.position, monster.transform.position);
					if ( distance < spellRadius) {
						insideRadiusMonsterList.Add (monster);
					}
				}

				for (int i = 0; i < insideRadiusMonsterList.Count; i++) {
					Monster monster = (Monster) insideRadiusMonsterList [i];
					if (monster != null) {
						monster.CallDoHit (attackDamage, 0f);
					}
				}
			}

			this.transform.DOPunchScale (new Vector3(0.8f, 0.8f, 0), 0.8f, 3, 0.8f);
			animator.SetTrigger ("HeroAttack");
		}
	}

	void DoHit () {
		animator.SetTrigger ("HeroHit");
	}

	public void CallDoHit (float delayTime) {
		Invoke("DoHit", delayTime); 
	}
}
