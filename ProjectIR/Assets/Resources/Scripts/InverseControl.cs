using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class InverseControl : MonoBehaviour {


   public Image img;
   Color C1;
	// Use this for initialization
	void Start () {
     
	}
	
	// Update is called once per frame
	void Update () 
    {
            float f;
            C1 = img.color;
            f = Mathf.Abs(Mathf.Sin(Time.time * 6));
            C1.a = f;
            C1.b = (Mathf.Sin(Time.time * 3));
            C1.g = (Mathf.Sin(Time.time * 4));
            //C1.r = (Mathf.Sin(Time.time * 1)+0.5f);
            img.color = C1;
        
	}



     public void InverseShow()
    {
        img.enabled = true;
      
    }

     public void InverseHide()
     {
         img.enabled = false;
     }
}
