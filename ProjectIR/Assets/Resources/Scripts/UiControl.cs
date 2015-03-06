using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UiControl : MonoBehaviour {


    public Text tex;
    void Start()
    {
       // GameOver();
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameOver();

        }
  
    }
    


    //call this to enable and disable the inverse image 
   public void Inverse(bool show)
    {
        InverseControl Inverse1 = (InverseControl)GameObject.FindObjectOfType<InverseControl>();
        if (show == true)
        {
            Inverse1.InverseShow();

        }

        if(show == false)
        {
            Inverse1.InverseHide();
        }
    }

  //call this to set the Score texts need to be set once per frame for couninese score updateing
   public void ScoreSet(int score)
    {
        UiScore uiscore = (UiScore)GameObject.FindObjectOfType<UiScore>();
        uiscore.VarScore = score;
    }



  //call this to scroll in the game over image
   public void GameOver()
    {

        UiScore uiscore = (UiScore)GameObject.FindObjectOfType<UiScore>();
        uiscore.Hide();

        UiWave uiwave = (UiWave)GameObject.FindObjectOfType<UiWave>();
        uiwave.Hide();

        ControlText TextControl = (ControlText)GameObject.FindObjectOfType<ControlText>();
        TextControl.Hide();


        Inverse(false);
        Game_Over game = (Game_Over)GameObject.FindObjectOfType<Game_Over>();
        game.GameOver();
        ArrowScroll(0);

       
    }

    // ArrowScroll with scroll the arrrow in and out of the screen, take in a short that need to be ehter 1 or 0;
   public void ArrowScroll(short direction)
    {
            ArrowControl[] AC = GameObject.FindObjectsOfType<ArrowControl>();
            if (direction == 1)
            {
                AC[0].ArrowIn();
                AC[1].ArrowIn();
            }

            if (direction == 0)
            {
                AC[0].ArrowOut();
                AC[1].ArrowOut();
            }
        
    }



   public void Quit()
   {
       Application.Quit();
   }

    public void LoadMenu()
    {
		ObstacleController.GO = false;
        Application.LoadLevel("TitleSplash");
    }


   public void LoadGame()
   {
		ObstacleController.GO = false;
        Application.LoadLevel("Main");
    }

   public void Resume()
   {

       QuitCon Quit = (QuitCon)GameObject.FindObjectOfType<QuitCon>();
       Quit.Hide();
		Time.timeScale = 1.0f;
   }




   public void Controls(string control)
   {
       ControlText TextControl = (ControlText)GameObject.FindObjectOfType<ControlText>();
       TextControl.changeText(control);
   }


}
