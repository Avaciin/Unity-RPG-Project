using UnityEngine;
using System.Collections;

public class PlayerLevel : MonoBehaviour {

	public int level;

	public float exp;
	public float maxExp;
	public float bonusExpModifier;

	public PlayerStats stats;

	void Start () {
		exp = 0;
		maxExp = 100;
	}

	void Update () {
		if (exp >= maxExp) {
			LevelUp();
		}
	}

	void LevelUp() {
		level += 1;
		exp = 0;
		maxExp *= 1.888f;
		maxExp = Mathf.Round(maxExp);
		StatUpdate();
	}

	void StatUpdate() {
		stats = GetComponent<PlayerStats>();
		stats.maxHealth *= 1.044;
		stats.maxHealth = (int)stats.maxHealth;
		stats.curHealth = stats.maxHealth;
	}
}
