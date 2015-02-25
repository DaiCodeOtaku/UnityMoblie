using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

   // public Text InGameText;
    
    public Text GameOver_score;
    public Text GameOver_Highscore;

    public GameObject New;
    int FinalScore;
    int Highscore;
    int currentScore;
	// Use this for initialization
	void Start () {      
	}
	
	// Update is called once per frame
	void Update () {     
	}


    public void Show()
    {
      //  gameObject.SetActive(true);

        ScoreController S = (ScoreController)GameObject.FindObjectOfType<ScoreController>();
        currentScore = (int)S.score;
      
        Debug.Log(currentScore);
        XML X = (XML)GameObject.FindObjectOfType<XML>();
        Highscore = X.HighScore();
        Debug.Log(Highscore);
     
        if (Highscore < currentScore == true)
        {

            Debug.Log("in");
            X.UpdateScore(currentScore);
            Highscore = currentScore;

         //   NewHS New = (NewHS)GameObject.FindObjectOfType<NewHS>();
            New.SetActive(true);
        }

        string t1;
        string t2;

        t1 = currentScore.ToString();
        GameOver_score.text = t1;

        t2 = Highscore.ToString();
        GameOver_Highscore.text = t2;
    }
}
