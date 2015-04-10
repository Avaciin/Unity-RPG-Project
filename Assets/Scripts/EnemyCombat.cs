using UnityEngine;
using System.Collections;

public class EnemyCombat : MonoBehaviour {
	
	public float attackRange;
	public float aggroRange;
	
	private float maxDistanceToHomePosition = 0.6f;
	
	public Vector3 homePosition;
	public Vector3 homeRotation;
	
	public CharacterController controller;
	public Transform player;
	
	private PlayerCombat opponent;
	public PlayerLevel playerExp;
	public EnemyStats enemyStats;

	public AnimationClip animationIdle;
	public AnimationClip animationRun;
	public AnimationClip animationAttack;
	public AnimationClip animationDie;
	
	public double impactTime = 0.036;
	public bool impacted;

	public float expGiven;
	
	void Start () {
		opponent = player.GetComponent<PlayerCombat>();
		homePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		homeRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
	}
	
	void Update () {
		if (!isDead()) {
			if (inAggroRange()) {
				if (!inAttackRange()) {
					chase();
				} else {
					animation.CrossFade(animationAttack.name);
					attack();
					
					if (animation[animationAttack.name].time > 0.9 * animation[animationAttack.name].length) {
						impacted = false;
					}
				}
			} else if (!inAggroRange()){
				//Go back to home position
				if ((transform.position - homePosition).magnitude > maxDistanceToHomePosition) {
					animation.CrossFade(animationRun.name);
					transform.LookAt(homePosition);
					controller.SimpleMove(transform.forward * enemyStats.speed);
				} else {
					//Enemy is at home position
					animation.CrossFade(animationIdle.name);
					transform.eulerAngles = homeRotation;
				}
			}
		} else {
			dieMethod();
		}
	}
	
	void attack() {
		if (animation[animationAttack.name].time > animation[animationAttack.name].length * impactTime && !impacted && animation[animationAttack.name].time < 0.9 * animation[animationAttack.name].length) {
			opponent.takeDamage(enemyStats.attack);
			impacted = true;
		}
	}
	
	public bool inAggroRange() {
		if (Vector3.Distance (transform.position, player.position) < aggroRange) {
			return true;
		} else {
			return false;
		}
	}
	
	public bool inAttackRange() {
		if (Vector3.Distance (transform.position, player.position) < attackRange) {
			return true;
		} else {
			return false;
		}
	}
	
	public void takeDamage(double damage) {
		enemyStats.curHealth -= damage;
		
		if (enemyStats.curHealth <= 0) {
			playerExp.GetComponent<PlayerLevel>().exp += expGiven * playerExp.GetComponent<PlayerLevel>().bonusExpModifier;
			enemyStats.curHealth = 0;
		}
	}
	
	public void chase() {
		transform.LookAt(player.position);
		controller.SimpleMove(transform.forward * enemyStats.speed);
		animation.CrossFade(animationRun.name);
	}
	
	public void dieMethod() {
		animation.CrossFade (animationDie.name);
		player.GetComponent<PlayerCombat>().opponent = null;
		
		if (animation[animationDie.name].time > animation[animationDie.name].length * 0.9) {
			gameObject.SetActive(false);
			//Respawn();
		}

		ClickToMove.attack = false;
	}
	
	public bool isDead() {
		if (enemyStats.curHealth <= 0) {
			return true;
		} else {
			return false;
		} 
	}
	
	/*void Respawn() {

	}*/
}
