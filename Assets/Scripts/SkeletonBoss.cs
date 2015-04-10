using UnityEngine;
using System.Collections;

public class SkeletonBoss : MonoBehaviour {

	private float maxDistanceToExit = 5.6f;
	public GameObject skeletonBoss;
	public Transform exit;
	public Transform lightShaft;
	public int questComplete = 1;
	public Vector3 exitPoint;
	public ClickToMove player;

	void Start () {

	}

	void Update () {

		exitPoint = new Vector3(695.6766f, 0.08f, 261.2647f);

		if (questComplete == 1) {
			if (skeletonBoss.activeSelf == false) {
				Debug.Log("Skeleton Boss is dead...");
				questComplete = 2;
				ShowExit();
			}
		}
	}

	void ShowExit() {
		if (questComplete == 2) {
			Instantiate(exit, new Vector3(695.172f, 0.2f, 260.8134f), new Quaternion(0,0,0,0));
			Instantiate (lightShaft, new Vector3(695.6766f, 3.009073f, 261.2647f), new Quaternion(0,0,0,0));
			exit.localScale = new Vector3(.35f,.35f,.35f);
			lightShaft.transform.localScale = new Vector3(.35f, .35f, .35f);
			questComplete = 3;
		}
	}
}
