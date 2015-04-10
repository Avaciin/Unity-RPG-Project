using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	private float left;
	private float top;
	public float leftModifier;                           
	public float topModifier;

	private Vector3 NPCScreenPosition;
	public float range;

	public ClickToMove player;

	public bool inRange;

	void Start () {
		range = 50.0f;
	}

	void Update () {
		//player = GetComponent<ClickToMove>();
		if (Vector3.Distance(player.transform.position, transform.position) <= range) {
			inRange = true;
		} else {
			inRange = false;
		}

		Vector3 NPCWorldPosition = (transform.position + new Vector3(0.0f, transform.lossyScale.y, 0.0f));
		NPCScreenPosition = Camera.main.WorldToScreenPoint(NPCWorldPosition);
		left = NPCScreenPosition.x + leftModifier;
		top = Screen.height - (NPCScreenPosition.y + topModifier);
	}

	void OnGUI() {
		if (inRange) {
				GUI.Label(new Rect(left, top, 150, 25), gameObject.name.ToString());
		}
	}
}
