using UnityEngine;
using System.Collections;

enum cScheme {moveScale, tilt, arrows, accel, SOD, moveScaleInv, tiltInv, arrowsInv, accelInv, SODInv};

public class Player
{
	public float speed;
	public int scheme;
	public Vector2 direction;

	public Player()
	{
		scheme = 0;
		speed = 0.0375f;
		direction = Vector2.zero;

	}

	public int GetControlScheme(int scheme)
	{
		int lastScheme = scheme;

		if (Random.value <= 0.01f) 
		{
			while (lastScheme == scheme) 
			{
				scheme = Random.Range (0, 11);

			}
		}	
		return scheme;
	}
}

public class PlayerControls : MonoBehaviour 
{
	Player player = new Player();
	float timer;

	// Use this for initialization
	void Start () 
	{
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;

		if (timer > 5) {
			player.scheme = player.GetControlScheme (player.scheme);
			Debug.Log(player.scheme);
			timer = 0;
		}

		Vector3 mousePos = new Vector3 (0, 0, 0);

		switch ((cScheme)player.scheme)
		{
		case cScheme.moveScale:
				if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2)) 
				{	
					mousePos.x = Input.mousePosition.x;
					
					if (this.transform.position.x + 0.25 < 3.1) 
					{
						this.transform.Translate ((mousePos.x - (Screen.width / 2)) * player.speed * Time.deltaTime, 0, 0);
					}
				} 
				else if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2)) 
				{	
					mousePos.x = Input.mousePosition.x;
					if (this.transform.position.x - 0.25 > -3.1) 
					{
						this.transform.Translate ((((Screen.width / 2) - mousePos.x) * -player.speed * Time.deltaTime), 0, 0);
					}
				}
				break;
		case cScheme.moveScaleInv:
			if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2)) 
			{	
				mousePos.x = Input.mousePosition.x;
				if (this.transform.position.x + 0.25 < 3.1) 
				{
					this.transform.Translate ((mousePos.x - (Screen.width / 2)) * player.speed * Time.deltaTime, 0, 0);
				}
			} 
			else if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2)) 
			{	
				mousePos.x = Input.mousePosition.x;
				if (this.transform.position.x - 0.25 > -3.1) 
				{
					this.transform.Translate ((mousePos.x - (Screen.width / 2)) * player.speed * Time.deltaTime, 0, 0);
				}
			}
			break;
		case cScheme.tilt:
			player.direction.x = Input.acceleration.z;
			if (player.direction.x > 0.1)
			{
				this.transform.Translate (player.speed * Time.deltaTime, 0, 0);
			}
			else if (player.direction.x < -0.1)
			{
				this.transform.Translate (player.speed * Time.deltaTime, 0, 0);
			}
			break;

		case cScheme.tiltInv:
			player.direction.x = Input.acceleration.z;
			if (player.direction.x < -0.1)
			{
				this.transform.Translate (player.speed * Time.deltaTime, 0, 0);
			}
			else if (player.direction.x > 0.1)
			{
				this.transform.Translate (player.speed * Time.deltaTime, 0, 0);
			}
			break; 
		case cScheme.accel:

			break;
		//Move movement schemes go here
		default:
			break;
		

		}
	}
}
