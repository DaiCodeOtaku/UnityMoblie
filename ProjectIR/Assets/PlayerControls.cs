using UnityEngine;
using System.Collections;
using System;

public class PlayerControls : MonoBehaviour 
{
	public float speed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{

		Vector3 mousePos = new Vector3 (0, 0, 0);


		if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2)) 
		{	
			mousePos.x = Input.mousePosition.x;
			speed = 0.05f * 0.75f;
			if (this.transform.position.x + 0.25 < 3.1) 
			{
				this.transform.Translate ((mousePos.x - (Screen.width / 2)) * speed * Time.deltaTime, 0, 0);
			}
		} 
		else if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2)) 
		{	
			mousePos.x = Input.mousePosition.x;
			speed = -0.05f * 0.75f;
			if (this.transform.position.x - 0.25 > -3.1) 
			{
			this.transform.Translate ((((Screen.width / 2) - mousePos.x) * speed * Time.deltaTime), 0, 0);
			}
		}
		
	}
}
