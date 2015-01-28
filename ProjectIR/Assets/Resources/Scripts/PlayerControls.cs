using UnityEngine;
using System.Collections;

enum cScheme {moveScale, accel, SOD, moveScaleInv, accelInv, SODInv, tilt, tiltInv};

public class Player
{
	public float speed;
	public float accelSpeed;
	public int scheme;
	public Vector2 direction;

	public Player()
	{
		scheme = (int)cScheme.moveScale;
		speed = 0.0375f;
		direction = Vector2.zero;
		accelSpeed = 0;

	}

	public int GetControlScheme(int scheme)
	{
		int lastScheme = scheme;
		/*if (Otherboolean == true)//janky using tilt control boolean = true
		{
			if (Random.value <= 0.01f) 
			{
				while (lastScheme == scheme) 
				{
					scheme = Random.Range (0, 9);
					
				}
			}	
		} else
		{ */
			if (Random.value <= 0.3f) 
			{
				while (lastScheme == scheme) 
				{
					scheme = Random.Range (0, 11);
					Debug.Log(scheme);
				}
			}	
		//}
		return scheme;
	}
}

public class PlayerControls : MonoBehaviour 
{
	Player player = new Player();
	float timer;
	bool checkSOD;

	// Use this for initialization
	void Start () 
	{
		timer = 0;
		checkSOD = false;

	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;

		if (timer >= 5) 
		{
			player.scheme = player.GetControlScheme (player.scheme);

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
		case cScheme.accelInv:
			if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2)) 
			{					
				if(transform.position.x < 0)
				{
					player.accelSpeed += 0.5f;
				} else
				{
					player.accelSpeed += 0.3f;
				}
			} 
			else if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2)) 
			{	
				if(transform.position.x > 0)
				{
					player.accelSpeed -= 0.5f;
				} else
				{
					player.accelSpeed -= 0.3f;
				}
			}

			if (this.transform.position.x + 0.5 < 3.1  && this.transform.position.x - 0.5 > -3.1) 
			{
				this.transform.Translate (player.accelSpeed * Time.deltaTime, 0, 0);
			}
			break;
		case cScheme.accel:
			if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2)) 
			{	
				if(transform.position.x > 0)
				{
					player.accelSpeed -= 0.4f;
				} else
				{
					player.accelSpeed -= 0.2f;
				}
			} 
			else if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2)) 
			{	
				if(transform.position.x < 0)
				{
					player.accelSpeed += 0.4f;
				} else
				{
					player.accelSpeed += 0.2f;
				}
			}
			if (this.transform.position.x + 0.5 < 3.1  && this.transform.position.x - 0.5 > -3.1) 
			{
				this.transform.Translate (player.accelSpeed * Time.deltaTime, 0, 0);
			}
			break;
		case cScheme.SOD:
			if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2)) 
			{	
				//mousePos.x = Input.mousePosition.x;
				if(transform.position.x > 0)
				{
					if(checkSOD == false)
					{
						player.accelSpeed = -0.01f;
						checkSOD = true;
					}
					else
					{
						player.accelSpeed -= 0.4f;
					}
				} else
				{
					player.accelSpeed -= 0.2f;
					checkSOD = false;
				}
			} 
			else if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2)) 
			{	
				if(transform.position.x < 0)
				{
					if(checkSOD == false)
					{
						player.accelSpeed = 0.0f;
						checkSOD = true;
					}
					else
					{
						player.accelSpeed += 0.4f;
					}
				} else
				{
					player.accelSpeed += 0.2f;
					checkSOD = false;
				}
			}
			if (this.transform.position.x + 0.5 < 3.1  && this.transform.position.x - 0.5 > -3.1) 
			{
				this.transform.Translate (player.accelSpeed * Time.deltaTime, 0, 0);
			}
			break;
		case cScheme.SODInv:
			if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2)) 
			{	
				//mousePos.x = Input.mousePosition.x;
				if(transform.position.x > 0)
				{
					if(checkSOD == false)
					{
						player.accelSpeed = -0.01f;
						checkSOD = true;
					}
					else
					{
						player.accelSpeed -= 0.4f;
					}
				} else
				{
					player.accelSpeed -= 0.2f;
					checkSOD = false;
				}
			} 
			else if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2)) 
			{	
				if(transform.position.x < 0)
				{
					if(checkSOD == false)
					{
						player.accelSpeed = 0.0f;
						checkSOD = true;
					}
					else
					{
						player.accelSpeed += 0.4f;
					}
				} else
				{
					player.accelSpeed += 0.2f;
					checkSOD = false;
				}
			}
			if (this.transform.position.x + 0.5 < 3.1  && this.transform.position.x - 0.5 > -3.1) 
			{
				this.transform.Translate (player.accelSpeed * Time.deltaTime, 0, 0);
			}

			break;


				//Move movement schemes go here
		default:
			break;
		}
		if(Mathf.Abs (transform.position.x) >= 2.6)
		{
			this.transform.position = new Vector3(this.transform.position.x * 0.995f, this.transform.position.y, this.transform.position.z);
			player.accelSpeed = 0;
		}
	}
}
