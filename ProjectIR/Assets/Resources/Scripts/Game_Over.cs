using UnityEngine;
using System.Collections;

public class Game_Over : MonoBehaviour {

    public float public_time;
    public GameObject G;
    float time;
    bool b1;



    void Start()
    {
        b1 = false;
      



       
    }





    void Update()
    {

       

        if (b1 == true)
        {
         
            if (time > 0.0f)
            {
                gameObject.transform.Translate(0, Time.fixedDeltaTime * -2.0f, 0);
            }
            time = (time - 0.1f);

            if (time < 0.0f)
            {
                time = 0.0f;
                b1 = false;

                G.SetActive(true);
                GameOverController game = (GameOverController)GameObject.FindObjectOfType<GameOverController>();
                game.Show();
            }
        }


    }
  public void GameOver()
    {
        time = public_time;
        b1 = true;

    }



}
