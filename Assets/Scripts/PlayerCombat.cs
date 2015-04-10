using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour {

	public GameObject opponent;

	public AnimationClip animationAttack;
	public AnimationClip animationDie;

	public double maxHealth;
	public double curHealth;
	public double damage;

	public Vector3 currentPosition;

	private double impactLength;

	public double impactTime;
	public bool impacted;

	public float range;

	bool started;
	bool ended;

	void Start () {
		curHealth = maxHealth;
		impactLength = (animation[animationAttack.name].length * impactTime);
	}

	void Update () {
		currentPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

		if (opponent != null && inRange()) {
			animation.Play(animationAttack.name);
			ClickToMove.attack = true;

			if (opponent != null) {
				transform.LookAt(opponent.transform.position);
			}
		} else {
			transform.position = currentPosition;
			impacted = false;
		}

		if (animation[animationAttack.name].time > 0.9 * animation [animationAttack.name].length) {
			impacted = false;
		}

		impact();
		enemyDie();
	}

	void impact() {
		if (opponent != null && animation.IsPlaying(animationAttack.name) && !impacted) {
			if ((animation[animationAttack.name].time > impactLength && animation[animationAttack.name].time < 0.9 * animation[animationAttack.name].length)) {
				opponent.GetComponent<EnemyCombat>().takeDamage(damage);
				impacted = true;
			}
		}
	}

	public void takeDamage(double damage) {
		curHealth -= damage;

		if (curHealth < 0) {
			curHealth = 0;
		}
	}

	bool inRange() {
		if (Vector3.Distance(opponent.transform.position, transform.position) <= range) {
			return true;
		} else {
			return false;
		}
	}

	public bool isDead() {
		if (curHealth == 0) {
			return true;
		} else {
			return false;
		}
	}

	void enemyDie() {
		if (isDead() && !ended) {
			if (!started) {
				ClickToMove.die = true;
				animation.CrossFade(animationDie.name);
				started = true;
			}

			if (started && !animation.IsPlaying(animationDie.name)) {
				Debug.Log ("You have died");
				curHealth = 100;

				ended = true;
				started = false;
				ClickToMove.die = false;
			}
		}
	}
}
