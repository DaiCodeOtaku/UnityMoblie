using UnityEngine;
using System.Collections;

public class AndroidButtonHandel : MonoBehaviour {



   public GameObject G;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void Update () {
	
		if(!ObstacleController.GO){

			if(Input.GetKey(KeyCode.Escape))
			{
	            G.SetActive(true);
				Time.timeScale = 0.0f;
	            //QuitCon Quit = (QuitCon)GameObject.FindObjectOfType<QuitCon>();
	           // Quit.Show();
			}
		}

	}
}
