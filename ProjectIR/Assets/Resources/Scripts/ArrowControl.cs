using UnityEngine;
using System.Collections;

public class ArrowControl : MonoBehaviour {
	// Use this for initialization7




    bool b1;
    bool b2;

   public int arrow;
   float time;
   public float public_time;
	public float translateTime;


	void Start () {
     
        b1 = false;
        b2 = false;
     
	}
	
	// Update is called once per frame
	void Update () {


        if (arrow == 1)
        {
            if (b1 == true)
            {
               
                if (time > 0.0f)
                {
					gameObject.transform.Translate(Time.fixedDeltaTime * -translateTime, 0, 0);
                }
                time = (time - 0.1f);

                if (time < 0.0f)
                {
                    time = 0.0f;
                    b1 = false;
                }
            }


            if (b2 == true)
            {
               
                if (time > 0.0f)
                {
					gameObject.transform.Translate(Time.fixedDeltaTime * translateTime, 0, 0);
                }
                time = (time - 0.1f);

                if (time < 0.0f)
                {
                    time = 0.0f;
                    b2 = false;
                }
            }
        }


        if (arrow == 2)
        {
            if (b1 == true)
            {
              
                if (time > 0.0f)
                {
					gameObject.transform.Translate(Time.fixedDeltaTime * translateTime, 0, 0);
                }
                time = (time - 0.1f);

                if (time < 0.0f)
                {
                    time = 0.0f;
                    b1 = false;
                }
            }


            if (b2 == true)
            {
               
                if (time > 0.0f)
                {
					gameObject.transform.Translate(Time.fixedDeltaTime * -translateTime, 0, 0);
                }
                time = (time - 0.1f);

                if (time < 0.0f)
                {
                    time = 0.0f;
                    b2 = false;
                }
            }
        }


	}




    public void ArrowIn()
    {
        b1 = true;
        time = public_time;



    }



    public void ArrowOut()
    {
        b2 = true;
        time = public_time;
    }





}
