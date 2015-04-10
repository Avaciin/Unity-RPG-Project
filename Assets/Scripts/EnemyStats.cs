using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {
	
	public int level;

	public double curHealth;
	public double maxHealth;

	public double attack;
	public double defense;

	public int speed;


	void Start () {
		maxHealth = 100.0f;
		curHealth = maxHealth;
	}

	void Update () {
	
	}
}
