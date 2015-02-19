using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {

	float texOffset = 0.0f;
	public float scrollAmount = 0.57f;

	// Use this for initialization
	void Start () {
		scrollAmount = 0.57f;
	}
	
	// Update is called once per frame
	void Update () {
		texOffset += Time.deltaTime * scrollAmount;
		//renderer.material.SetTextureOffset ("checkered", new Vector2(0.0f, texOffset));
		renderer.material.mainTextureOffset = new Vector2(0.0f, -texOffset);
	}
}
