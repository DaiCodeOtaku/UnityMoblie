using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiWave : MonoBehaviour {

    public Text T;


   public void Wave(int wave)
    {
        T.text = wave.ToString();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
