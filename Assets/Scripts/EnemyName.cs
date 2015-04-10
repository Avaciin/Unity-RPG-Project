using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GUIText))]
public class EnemyName : MonoBehaviour {
	
	public Transform target;  // Object that this label should follow
	public Vector3 offset;    // Units in world space to offset; 1 unit above object by default
	public bool clampToScreen = false;  // If true, label will be visible even if object is off screen
	public float clampBorderSize = 0.05f;  // How much viewport space to leave at the borders when a label is being clamped
	public bool useMainCamera = true;   // Use the camera tagged MainCamera
	public Camera cameraToUse ;   // Only use this if useMainCamera is false
	Camera cam ;
	Transform thisTransform;
	Transform camTransform;

	public PlayerCombat player;
	public EnemyHealth enemyHealth;

	public float enemyHeight;
	
	void Start () {
		thisTransform = transform;
		if (useMainCamera)
			cam = Camera.main;
		else
			cam = cameraToUse;
		camTransform = cam.transform;
	}

	void Update() {
		if(player.opponent != null) {	
			enemyHealth = GetComponent<EnemyHealth>();
			target = player.opponent.GetComponent<EnemyCombat>().transform;

			enemyHeight = target.collider.bounds.size.y + .45f;
			offset = new Vector3(0, enemyHeight, 0);

			guiText.text = target.name.ToString ();

			if (clampToScreen)
			{
				Vector3 relativePosition = camTransform.InverseTransformPoint(target.position);
				relativePosition.z =  Mathf.Max(relativePosition.z, 1.0f);
				thisTransform.position = cam.WorldToViewportPoint(camTransform.TransformPoint(relativePosition + offset));
				thisTransform.position = new Vector3(Mathf.Clamp(thisTransform.position.x, clampBorderSize, 1.0f - clampBorderSize),
				                                     Mathf.Clamp(thisTransform.position.y, clampBorderSize, 1.0f - clampBorderSize),
				                                     thisTransform.position.z);
				
			}
			else
			{
				thisTransform.position = cam.WorldToViewportPoint(target.position + offset);
			}
		} else {
			target = null;
			guiText.text = null;
		}
	}
}