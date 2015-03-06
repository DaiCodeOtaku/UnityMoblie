using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

   // public Text InGameText;
    
    public Text GameOver_score;
    public Text GameOver_Highscore;
    public Text GameOver_Wave;

    public GameObject New;
    int FinalScore;
    int Highscore;
    int currentScore;
    int Wave;
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

		ObstacleController O = (ObstacleController)GameObject.FindObjectOfType<ObstacleController>();
		Wave = O.waveCounter;
		Debug.Log("wave get");
		Debug.Log(Wave);

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
		string t3;
        t1 = currentScore.ToString();
        GameOver_score.text = t1;

        t2 = Highscore.ToString();
        GameOver_Highscore.text = t2;
		t3 = Wave.ToString();
		GameOver_Wave.text = t3;
    }
}
