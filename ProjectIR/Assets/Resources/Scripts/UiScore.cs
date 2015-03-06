using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiScore : MonoBehaviour {

    public Text tex;
    string Score;
    public int VarScore;
	// Use this for initialization


	void Start (){   
	
    } 
	
	// Update is called once per frame
	void Update () 
    {
       ScoreController S = (ScoreController)GameObject.FindObjectOfType<ScoreController>();
       VarScore = (int)S.score;

        Score = VarScore.ToString();
        tex.text = Score;
	}

    public void Hide()
    {
       gameObject.SetActive(false);
    }
}
