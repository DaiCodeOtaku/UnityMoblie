using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour {



    public List<Sprite> SpriteList;
    public Image image;
    int pointer;
	// Use this for initialization
	void Start () {
        pointer = 0;
	}




    public void left()
    {
        if (pointer > 0)
        {
            image.sprite = SpriteList[(pointer - 1)];
            pointer -= 1;
        }
    }


   public void Right()
    {
      //  Debug.Log(SpriteList.Count);

        if (pointer < SpriteList.Count -1)
        {
            image.sprite = SpriteList[(pointer + 1)];
            pointer += 1;
        }
    }


 

	// Update is called once per frame
	void Update () {  

        
	}
}
