using UnityEngine;
using System.Collections;

public class QuitCon : MonoBehaviour {

    public void Show()
    {

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }


}
