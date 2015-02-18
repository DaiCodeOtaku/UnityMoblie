using UnityEngine;
using System.Collections;

public class NEW : MonoBehaviour {



   
    public GameObject G;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        G.transform.localScale = new Vector3((Mathf.Sin(Time.time * 2.2f) / 8) + 1, (Mathf.Sin(Time.time *2.2f) / 8) + 0.5f, 0);
       
      
	}
}
