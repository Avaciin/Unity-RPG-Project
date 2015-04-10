using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {

	public float speed;
	public CharacterController controller;

	private Vector3 position;
	public Vector3 curPosition;

	public AnimationClip animationIdle;
	public AnimationClip animationRun;
	
	public static bool attack;
	public static bool die;
	public bool running;

	public PlayerCombat player;
	public EnemyCombat enemy;
	public NPC NPC;

	void Start () {
		position = transform.position;
		curPosition = transform.position;
	} 

	void Update () {
		curPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

		if(player.opponent != null) {
			enemy = player.opponent.GetComponent<EnemyCombat>();
		} else {
			enemy = null;
		}

		if (Input.GetMouseButton(0)) {
			locatePosition();
		}

		if (!attack && !die) {
			if (enemy == null) {
				moveToPosition();
			} else {
				moveToTarget();
			}

		} else {}

		getTarget();
	}

	void locatePosition() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 100)) {
			if(hit.collider.tag!="Player") {
				position = hit.point;
			}
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "cube") {
			Debug.Log ("Collision on " + gameObject.name);
		}

	}

	void moveToPosition() {
		if (Vector3.Distance (transform.position, position) > 0.60) {
			Quaternion newRotation = Quaternion.LookRotation (position - transform.position);

			newRotation.x = 0f;
			newRotation.z = 0f;

			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 100);
			controller.SimpleMove (transform.forward * speed);

			animation.CrossFade(animationRun.name);
		} else {
			animation.CrossFade(animationIdle.name);
		}
	}

	void moveToTarget() {
		if (Vector3.Distance (transform.position, enemy.transform.position) > 1.00) {
			Quaternion newRotation = Quaternion.LookRotation (enemy.transform.position - transform.position);
			
			newRotation.x = 0f;
			newRotation.z = 0f;
			
			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 100);
			controller.SimpleMove(transform.forward * speed);
			
			animation.CrossFade(animationRun.name);

			position = curPosition;
		} else {
			animation.CrossFade(animationIdle.name);
		}
	}

	void moveToNPC() {

	}

	void getTarget() {
		if( Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(Physics.Raycast( ray, out hit, 100)) {
				Debug.Log(hit.transform.gameObject.name);

				if (hit.transform.gameObject.tag == "Environment" || hit.transform.gameObject.tag == "Player" || hit.transform.gameObject.tag == "NPC") {
						player.GetComponent<PlayerCombat>().opponent = null;
						attack = false;
				} else {
					player.GetComponent<PlayerCombat>().opponent = hit.transform.gameObject;
				}
			}
		}
	}
}