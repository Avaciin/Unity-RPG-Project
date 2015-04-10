using UnityEngine;

public class EnemyHealth : MonoBehaviour 
{

	// FIX THE FONT

	/*public Font font;
	public int fontSize = 8;
	public Vector3 Offset = Vector3.zero; // The offset from the character

	Mob mob = new Mob();

	private float health;
	
	private TextMesh bar;
	
	void Start()
	{
		mob = GetComponent<Mob>();
		// Setup the text mesh
		bar = new GameObject("HealthBar").AddComponent("TextMesh") as TextMesh;
		bar.gameObject.AddComponent("MeshRenderer");
		bar.gameObject.transform.parent = transform;
		bar.transform.localPosition = Vector3.zero + Offset;

		if(font) bar.font = font;
		else bar.font = GUI.skin.font;
		bar.renderer.material = font.material;
		bar.characterSize = 0.25f;
		bar.alignment = TextAlignment.Center;
		bar.anchor = TextAnchor.MiddleCenter;
		bar.fontSize = fontSize;
	}
	
	void Update()
	{
		health = mob.health;
		if(bar.text != "HP:" + health.ToString()) bar.text = "HP: " + health.ToString();
	}*/

	public Font font;
	public Texture2D healthBar;
	public Texture2D healthBarFrame;

	public float maxHealth;
	public float curHealth;

	private int healthBarWidth = 50;
	private int healthBarHeight = 5;
	
	public float left;
	public float top;

	private Vector3 healthBarScreenPosition;

	public PlayerCombat player;
	public EnemyStats target;
	public float healthPercent;

	void Start () {

	}

	void Update () {
		if(player.opponent != null) {		
			target = player.opponent.GetComponent<EnemyStats>();
			healthPercent = (float)target.curHealth / (float)target.maxHealth;

			Vector3 healthBarWorldPosition = (target.transform.position + new Vector3(0.0f, target.transform.lossyScale.y, 0.0f));
			healthBarScreenPosition = Camera.main.WorldToScreenPoint(healthBarWorldPosition);
			left = healthBarScreenPosition.x - (healthBarWidth / 2);
			top = Screen.height - (healthBarScreenPosition.y + (healthBarHeight / 2));
		} else {
			target = null;
			healthPercent = 0;
		}
	}
	
	void OnGUI() {
		if (target != null) {
			GUI.DrawTexture(new Rect(left, top, 50, 5), healthBarFrame);
			GUI.DrawTexture(new Rect(left, top, (50 * healthPercent), 5), healthBar);
		}
	}
}