using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class Ad : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Advertisement.UnityDeveloperInternalTestMode = true;
        Advertisement.debugLevel = Advertisement.DebugLevel.NONE;
        Advertisement.Initialize("131626899");
        //131626899
	}

}
