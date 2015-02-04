using UnityEngine;
using System.Collections;

enum cScheme {moveScale, arrows, accel, SOD, moveScaleInv, arrowsInv, accelInv, SODInv, tilt, tiltInv};

public class Player
{
	public float speed;
	public float accelSpeed;
	public int scheme;
	public Vector3 direction;


	public Player()
	{
		scheme = (int)cScheme.moveScale;
		speed = 0.0375f;
		direction = Vector3.zero;
		accelSpeed = 0;
	}

	public int GetControlScheme(int scheme, float accelSpeed)
	{
		int lastScheme = scheme;
			if (Random.value <= 0.3f) 
			{
				while (lastScheme == scheme) 
				{
					scheme = Random.Range (0, 11);
					accelSpeed = 0;
				}
				Debug.Log(scheme);
			}
		return scheme;
	}
}

public class PlayerControls : MonoBehaviour 
{
	Player player = new Player();
	float timer;
	bool checkSOD;
	float moveThreshold = 0.2f;

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
			player.scheme = player.GetControlScheme (player.scheme, player.accelSpeed);

			timer = 0;
		}

		Vector3 mousePos = new Vector3 (0, 0, 0);

		if ((this.transform.position.x + 0.25 < 3.1) && (this.transform.position.x - 0.25 > -3.1))
		{

			switch ((cScheme)player.scheme)
			{
			case cScheme.moveScale:
					if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2)) 
					{	
						mousePos.x = Input.mousePosition.x;
						this.transform.Translate ((mousePos.x - (Screen.width / 2)) * player.speed * Time.deltaTime, 0, 0);
					} 
					else if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2)) 
					{	
						mousePos.x = Input.mousePosition.x;
						this.transform.Translate ((((Screen.width / 2) - mousePos.x) * -player.speed * Time.deltaTime), 0, 0);
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
				if (Input.acceleration.y > moveThreshold)
				{
				transform.Translate(Input.acceleration.x * player.speed,0,0);
				}
				else if (Input.acceleration.y < -moveThreshold)
				{
				transform.Translate(Input.acceleration.x * player.speed,0,0);
				} 
				break;

			case cScheme.tiltInv:
			if (Input.acceleration.y < -moveThreshold)
				{
				transform.Translate(-Input.acceleration.x * 0.5f,0,0);
				}
				else if (Input.acceleration.y > moveThreshold)
				{
				transform.Translate(-Input.acceleration.x * 0.5f,0,0);
				}
				break;
			case cScheme.accelInv:
				if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2)) 
				{					
					if(transform.position.x < 0)
					{
						player.accelSpeed += 0.1f;
					} else
					{
						player.accelSpeed += 0.03f;
					}
				} 
				else if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2)) 
				{	
					if(transform.position.x > 0)
					{
						player.accelSpeed -= 0.1f;
					} else
					{
						player.accelSpeed -= 0.03f;
					}
				}
				Debug.Log(player.accelSpeed);
				this.transform.Translate (player.accelSpeed * Time.deltaTime, 0, 0);
				break;
			case cScheme.accel:
				if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2)) 
				{	
					if(transform.position.x > 0)
					{
						player.accelSpeed -= 0.1f;
					} else
					{
						player.accelSpeed -= 0.03f;
					}
				} 
				else if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2)) 
				{	
					if(transform.position.x < 0)
					{
						player.accelSpeed += 0.1f;
					} else
					{
						player.accelSpeed += 0.03f;
					}
				}
				
				Debug.Log(player.accelSpeed);
				this.transform.Translate (player.accelSpeed * Time.deltaTime, 0, 0);
				break;
			case cScheme.SOD:
				if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2)) 
				{	
					if(transform.position.x > 0)
					{
						if(checkSOD == false)
						{
							player.accelSpeed = -0.01f;
							checkSOD = true;
						}
						else
						{
							player.accelSpeed -= 0.1f;
						}
					} else
					{
						player.accelSpeed -= 0.03f;
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
							player.accelSpeed += 0.1f;
						}
					} else
					{
						player.accelSpeed += 0.03f;
						checkSOD = false;
					}
				}
				Debug.Log(player.accelSpeed);
				this.transform.Translate (player.accelSpeed * Time.deltaTime, 0, 0);
				break;
			case cScheme.SODInv:
				if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2)) 
				{	
					if(transform.position.x > 0)
					{
						if(checkSOD == false)
						{
							player.accelSpeed = -0.01f;
							checkSOD = true;
						}
						else
						{
							player.accelSpeed -= 0.1f;
						}
					} else
					{
						player.accelSpeed -= 0.03f;
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
							player.accelSpeed += 0.1f;
						}
					} else
					{
						player.accelSpeed += 0.03f;
						checkSOD = false;
					}
				}
				
				Debug.Log(player.accelSpeed);
				this.transform.Translate (player.accelSpeed * Time.deltaTime, 0, 0);

				break;
			case cScheme.arrows:
				if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2) - 1) 
				{	
					mousePos.x = Input.mousePosition.x;
					this.transform.Translate (player.speed * Time.deltaTime, 0, 0);
				} 
				else if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2) + 1) 
				{	
					mousePos.x = Input.mousePosition.x;
					this.transform.Translate ((-player.speed * Time.deltaTime), 0, 0);
				}
				break;
			case cScheme.arrowsInv:
				if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2) - 1) 
				{	
					mousePos.x = Input.mousePosition.x;
					if (this.transform.position.x + 0.25 < 3.1) 
					{
						this.transform.Translate (player.speed * Time.deltaTime, 0, 0);
					}
				} 
				else if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2) + 1) 
				{	
					mousePos.x = Input.mousePosition.x;
					if (this.transform.position.x - 0.25 > -3.1) 
					{
						this.transform.Translate (player.speed * Time.deltaTime, 0, 0);
					}
				}
				break;

					//Move movement schemes go here
			default:
				break;
			}
		}
		else
		{
			player.accelSpeed = 0;
			if (this.transform.position.x < (Screen.width/2))
			{
				this.transform.Translate ((this.transform.position.x * -0.005f),0,0);
			}
			else
			{
				this.transform.Translate ((this.transform.position.x * -0.005f),0,0);
			}
		}
		;
	}
}


