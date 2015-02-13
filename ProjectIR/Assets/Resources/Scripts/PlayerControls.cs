using UnityEngine;
using System.Collections;

enum cScheme {moveScale, arrows, accel, SOD, moveScaleInv, arrowsInv, accelInv, SODInv, tiltInv, tilt};
// moveScale = Player moves at varying speed depending on how extreme their button presses are either side of middle
// arrows = Two arrows appear on screen for the player to click to move at a contstant speed in the chosen direction
// accel = Accelerated movement, the longer the player holds, the faster they move. Let go and they slow down
// SOD = Stop on Dime, if the player changes direction, their speed becomes 0 and they immediately start moving in that direction
// tilt = Gyroscope controls, tilt phone to move in a direction
// suffix -Inv = Inverted control scheme, the player moves in the opposide direction to their input.

public class Player
{
	public float speed; // Constant speed
	public float accelSpeed; // Accelerated speed, cumulative
	public int scheme;  // What control schele is being used
	public float maxSpeed; // Highest positive speed the player can go to in accel scheme
	public float minSpeed; // Lowest etc. 
	public float telegraphTime; // Grace period before the control scheme changes
	public bool teleCheck; // Checks if the grace period is active
	public bool startGetScheme; // Used to tell the control scheme function to get a new control scheme 


	public Player()
	{
		scheme = (int)cScheme.moveScale;
		speed = 0.0375f;
		accelSpeed = 0; 
		maxSpeed = 10;
		minSpeed = -10;
		telegraphTime = 0.0f;
	}

	public int GetControlScheme(int scheme, ref float accelSpeed, ref float telegraphTime, ref bool getScheme)
	{
		int lastScheme = scheme;

			if (Random.value <= 0.85f) 
			{
				if (startGetScheme)
				{
					while (scheme == lastScheme) 
					{
						// if (UseTilt()) // Player stored info from Options, checks if tilt controls are enabled
						//{
							scheme = Random.Range (0, 11);
							accelSpeed = 0;
						/*}
						else
						{
							scheme = Random.Range (0, 9);
							accelSpeed = 0;
						}*/

						if (scheme != lastScheme)
						{
							telegraphTime = 0;
						getScheme = false;

						}
					}
				}
			// DisplayScheme(); A function from UI that displays what control scheme is being used
			// if (scheme >= 4 && scheme <= 9) {ShowInvert();} // shows the "INVERT" flashing UI bit
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
	float arrowTop = (Screen.height / 8) + 0.25f;
	float arrowBottom = (Screen.height / 20);
	bool activateTimer = true;


	// Use this for initialization
	void Start () 
	{
		timer = 0;
		checkSOD = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float normalisedSpeed = player.speed * Time.deltaTime;
		timer += Time.deltaTime;

		if (timer >= 5 && activateTimer) 
		{
			player.scheme = player.GetControlScheme (player.scheme, ref player.accelSpeed, ref player.telegraphTime, ref player.startGetScheme );
			activateTimer = true;
			timer = 0;
			player.teleCheck = true;
		}

		if (player.teleCheck) 
		{
			player.telegraphTime += Time.deltaTime;
			if(player.telegraphTime >= 3)
			{
				player.startGetScheme = true;
				player.teleCheck = false;
				activateTimer = true;
			}
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
						this.transform.Translate ((mousePos.x - (Screen.width / 2)) * normalisedSpeed, 0, 0);
					} 
					else if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2)) 
					{	
						mousePos.x = Input.mousePosition.x;
						this.transform.Translate ((((Screen.width / 2) - mousePos.x) * -normalisedSpeed), 0, 0);
					}
					break;
			case cScheme.moveScaleInv:

				if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2)) 
				{	
					mousePos.x = Input.mousePosition.x;
					if (this.transform.position.x + 0.25 < 3.1) 
					{
						this.transform.Translate ((mousePos.x - (Screen.width / 2)) * normalisedSpeed, 0, 0);
					}
				} 
				else if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2)) 
				{	
					mousePos.x = Input.mousePosition.x;
					if (this.transform.position.x - 0.25 > -3.1) 
					{
						this.transform.Translate ((mousePos.x - (Screen.width / 2)) * normalisedSpeed, 0, 0);
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
						player.accelSpeed += 0.05f;
					} else
					{
						player.accelSpeed += 0.025f;
					}
				} 
				else if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2)) 
				{	
					if(transform.position.x > 0)
					{
						player.accelSpeed -= 0.05f;
					} else
					{
						player.accelSpeed -= 0.025f;
					}
				}
				else 
				{
					player.accelSpeed *= 0.99f;
				}
				this.transform.Translate (player.accelSpeed * Time.deltaTime, 0, 0);
				break;
			case cScheme.accel:
				if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2)) 
				{	
					if(transform.position.x > 0)
					{
						player.accelSpeed -= 0.05f;
					} else
					{
						player.accelSpeed -= 0.025f;
					}
				} 
				else if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2)) 
				{	
					if(transform.position.x < 0)
					{
						player.accelSpeed += 0.05f;
					} else
					{
						player.accelSpeed += 0.025f;
					}
				}
				else 
				{
					player.accelSpeed *= 0.99f;
				}
				this.transform.Translate (player.accelSpeed * Time.deltaTime, 0, 0);
				break;
			case cScheme.SOD:
				if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2)) 
				{	
					if(transform.position.x > 0)
					{
						if(checkSOD == false)
						{
							player.accelSpeed = 0.0f;
							checkSOD = true;
						}
						else
						{
							player.accelSpeed -= 0.05f;
						}
					} else
					{
						player.accelSpeed -= 0.025f;
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
							player.accelSpeed += 0.05f;
						}
					} else
					{
						player.accelSpeed += 0.025f;
						checkSOD = false;
					}
				}
				this.transform.Translate (player.accelSpeed * Time.deltaTime, 0, 0);
				break;
			case cScheme.SODInv:
				if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2)) 
				{	
					if(transform.position.x > 0)
					{
						if(checkSOD == false)
						{
							player.accelSpeed = 0.00f;
							checkSOD = true;
						}
						else
						{
							player.accelSpeed -= 0.05f;
						}
					} else
					{
						player.accelSpeed -= 0.025f;
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
							player.accelSpeed += 0.5f;
						}
					} else
					{
						player.accelSpeed += 0.025f;
						checkSOD = false;
					}
				}
				this.transform.Translate (player.accelSpeed * Time.deltaTime, 0, 0);

				break;
			case cScheme.arrows:
				if (Input.GetMouseButton (0) && (Input.mousePosition.x > ((Screen.width / 4) + (Screen.width / 4))) && ((Input.mousePosition.y < arrowTop) && (Input.mousePosition.y > arrowBottom))) 
				{	
					this.transform.Translate (normalisedSpeed * 150.0f, 0, 0);
				} 
				else if (Input.GetMouseButton (0) && (Input.mousePosition.x < (Screen.width / 4)) && ((Input.mousePosition.y < arrowTop) && (Input.mousePosition.y > arrowBottom))) 
				{	
					this.transform.Translate (-normalisedSpeed * 150.0f, 0, 0);
				}
				break;
			case cScheme.arrowsInv:
				if (Input.GetMouseButton (0) && (Input.mousePosition.x > ((Screen.width / 4) + (Screen.width / 4))) && ((Input.mousePosition.y < arrowTop) && (Input.mousePosition.y > arrowBottom))) 
				{	
					this.transform.Translate (normalisedSpeed * 150.0f, 0, 0);
				} 
				else if (Input.GetMouseButton (0) && (Input.mousePosition.x < (Screen.width / 4)) && ((Input.mousePosition.y < arrowTop) && (Input.mousePosition.y > arrowBottom))) 
				{	
					this.transform.Translate (normalisedSpeed * 150.0f, 0, 0);
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

		if (player.accelSpeed >= player.maxSpeed) 
		{
			player.accelSpeed = player.maxSpeed - 1;
		}
		if (player.accelSpeed <= player.minSpeed) 
		{
			player.accelSpeed = player.minSpeed + 1;
		}
		Debug.Log (player.scheme);
	}
}


