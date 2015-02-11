using UnityEngine;
using System.Collections;



public class ObstacleController : MonoBehaviour {

	public GameObject ObstacleTemplate;
	enum spawnPatterns {nullPattern = 0, Basic, Advanced, Painful, Stairs, Butterfly, SpaceInvaders};
	public float patternTimer;
	spawnPatterns previousPattern, currentPattern;
	int patternOffset;
	float spawnHeight, spawnDepth;

							  //time before, //num of blocks, //block offset

	int numBasic = 41;
	float[] basicPattern = {	0.2f, 1, 1.0f,			0.2f, 1, 2.0f,			0.2f, 1, 3.0f, 				
								0.2f, 1, -0.5f,			0.2f, 1, -1.5f,			0.2f, 1, -2.5f, 			
								0.2f, 1, -3.0f,			0.3f, 1, -0.5f,			0.2f, 1, 0.5f,				
								0.2f, 1, 1.5f,			0.2f, 2, 1.5f, -1.5f,	0.2f, 2, 1.5f, -1.5f,	
								0.2f, 1, -1.5f,			0.3f};

	int numAdvanced = 49;
	float[] advancedPattern = {	0.1f, 1, -1.5f,			0.2f, 1, -1.5f,			0.2f, 1, -1.5f,			
								0.2f, 1, 0.0f,			0.2f, 1, 0.0f,			0.2f, 1, 0.0f,			
								0.2f, 1, 1.0f,			0.2f, 1, -2.0f,			0.2f, 1, 0.5f,			
								0.2f, 1, -0.5f,			0.2f, 1, -2.5f,			0.2f, 1, 2.5f,			
								0.2f, 1, 0.0f,			0.2f, 1, -1.0f, 		0.2f, 1, -2.0f,			
								0.1f, 1, 2.0f,			0.0f};											

	int numPainful = 75;
	float[] painfulPattern = {	0.1f, 2, 0.0f, 2.0f,	0.1f, 1, -0.5f, 		0.1f, 1, 0.5f,			
								0.1f, 1, -1.0f, 		0.1f, 1, 1.0f,			0.1f, 1, -1.5f,			
								0.1f, 1, 1.5f,			0.2f, 2, 2.0f, 3.0f,	0.1f, 1, 2.5f,			
								0.1f, 1, -3.0f,			0.2f, 2, -2.5f, 1.5f,	0.2f, 2, -2.0f, 1.0f,	
								0.2f, 2, -1.5f, 0.5f, 	0.1f, 1, 3.0f,			0.1f, 1, 2.5f,			
								0.1f, 1, 2.0f, 			0.1f, 1, 1.5f, 			0.1f, 1, 1.0f,			
								0.1f, 1, -1.0f, 		0.1f, 1, -1.5f, 		0.1f, 1, -2.0f,			
								0.1f, 1, -2.5f, 		0.1f, 1, 3.0f, 			0.0f};					

	int numStairs = 37;
	float[] stairsPattern = {	0.1f, 1, -2.0f,			0.2f, 1, -1.0f,			0.2f, 1, 0.0f,
								0.4f, 1, 2.0f,			0.2f, 1, 1.0f,			0.2f, 1, 0.0f,
								0.2f, 1, -1.0f,			0.4f, 1, -3.0f,			0.2f, 1, -2.0f,
								0.2f, 1, -1.0f,			0.4f, 1, 1.0f,			0.2f, 1, 2.0f,
								0.1f};

	int numButterfly = 49;
	float[] butterflyPattern = {0.1f, 2, 3.0f, -3.0f,	0.2f, 2, 2.5f, -2.5f,	0.2f, 4, 3.0f, -3.0f, 1.5f, -1.5f,
					0.2f, 4, 2.5f, -2.5f, 1.0f, -1.0f,	0.2f, 2, 1.5f, -1.5f,		0.2f, 2, 1.0f,-1.0f,
								0.6f, 1, 0.0f,		0.2f, 3, 0.0f, 1.0f, -1.0f,	0.2f, 2, 1.5f, -1.5f,
								0.2f, 2, 1.0f, -1.0f,	0.2f, 2, 1.5f, -1.5f, 	0.5f};

	int numSpaceInvaders = 57;
	float[] spaceInvadersPattern = {	0.2f, 5, 3.0f, -3.0f, 2.0f, -2.0f, 0.0f, 		0.1f, 2, 2.5f, -2.5f,
								0.5f, 2, 1.0f, -1.0f, 	0.1f, 4, 0.5f, -0.5f, 1.5f, -1.5f,
								0.3f, 1, 2.3f, 			0.1f, 2, 3.0f, 2.0f,	0.2f, 1, -2.5f,
								0.1f, 2, -3.0f, -2.0f, 	0.3f, 1, -0.5f,			0.1f, 2, 0.0f, -1.0f,
								0.3f, 1, 2.5f,			0.1f, 2, 3.0f, 2.0f,	0.3f, 1, 1.0f,
								0.1f, 2, 1.5f, 0.5f,	0.3f};




	// Use this for initialization
	void Start () {
		patternTimer = 2;
		previousPattern = 0;
		patternOffset = 0;
		spawnHeight = 7.0f;
		spawnDepth = -0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		patternTimer -= Time.deltaTime;

		executePattern(currentPattern);

		if (patternTimer < 0) {
			currentPattern = newPattern();
			Debug.Log(currentPattern);
			setPatternTimer(currentPattern);
		}
	}

	void setPatternTimer(spawnPatterns currentPattern){
		switch (currentPattern) {
		case spawnPatterns.nullPattern:
			patternTimer = 0.1f;
			break;
		case spawnPatterns.Basic:
			patternTimer = basicPattern[0];
			patternOffset = 1;
			break;
		case spawnPatterns.Advanced:
			patternTimer = advancedPattern[0];
			patternOffset = 1;
			break;
		case spawnPatterns.Painful:
			patternTimer = painfulPattern[0];
			patternOffset = 1;
			break;
		case spawnPatterns.Stairs:
			patternTimer = stairsPattern[0];
			patternOffset = 1;
			break;
		case spawnPatterns.Butterfly:
			patternTimer = butterflyPattern[0];
			patternOffset = 1;
			break;
		case spawnPatterns.SpaceInvaders:
			patternTimer = spaceInvadersPattern[0];
			patternOffset = 1;
			break;
		}
	}

	spawnPatterns newPattern(){
		spawnPatterns nextPattern = (spawnPatterns)((6 * Random.value) + 1);

		switch(nextPattern){

		case spawnPatterns.nullPattern:
			if(nextPattern == previousPattern){
				previousPattern = spawnPatterns.Basic;
				return spawnPatterns.Basic;
			}
			previousPattern = spawnPatterns.Advanced;
			return spawnPatterns.Advanced;

		case spawnPatterns.Basic:
			if(nextPattern == previousPattern){
				previousPattern = spawnPatterns.Advanced;
				return spawnPatterns.Advanced;
			}
			previousPattern = spawnPatterns.Basic;
			return spawnPatterns.Basic;

		case spawnPatterns.Advanced:
			if(nextPattern == previousPattern){
				previousPattern = spawnPatterns.Basic;
				return spawnPatterns.Basic;
			}
			previousPattern = spawnPatterns.Advanced;
			return spawnPatterns.Advanced;

		case spawnPatterns.Painful:
			if(nextPattern == previousPattern){
				previousPattern = spawnPatterns.Advanced;
				return spawnPatterns.Advanced;
			}
			previousPattern = spawnPatterns.Painful;
			return spawnPatterns.Painful;

		case spawnPatterns.Stairs:
			if(nextPattern == previousPattern){
				return spawnPatterns.Painful;
			}
			previousPattern = spawnPatterns.Stairs;
			return spawnPatterns.Stairs;
		
		case spawnPatterns.Butterfly:
			if(nextPattern == previousPattern){
				previousPattern = spawnPatterns.Stairs;
				return spawnPatterns.Stairs;
			}
			previousPattern = spawnPatterns.Butterfly;
			return spawnPatterns.Butterfly;

		case spawnPatterns.SpaceInvaders:
			if(nextPattern == previousPattern){
				previousPattern = spawnPatterns.Butterfly;
				return spawnPatterns.Butterfly;
			}
			previousPattern = spawnPatterns.SpaceInvaders;
			return spawnPatterns.SpaceInvaders;

		}
		return 0;
	}

	void resetObstacle(){
		ObstacleTemplate.transform.Translate(-ObstacleTemplate.transform.position);
	}

	void spawnObstacle(float x){
		ObstacleTemplate.transform.Translate(new Vector3(x, spawnHeight, spawnDepth));
		GameObject.Instantiate((GameObject)ObstacleTemplate);
		resetObstacle();
	}

	void executePattern(spawnPatterns pattern){
		switch (pattern) {
		case spawnPatterns.nullPattern:
			break;
		case spawnPatterns.Basic:
			if(patternOffset < numBasic){
				if(patternTimer < 0){
					int i = 0;
					for(i = 0; i < basicPattern[patternOffset]; i++){
						spawnObstacle(basicPattern[patternOffset + i + 1]);
					}
					patternOffset += i + 1;
					patternTimer = basicPattern[patternOffset];
					patternOffset++;
				}
			}
			break;
		case spawnPatterns.Advanced:
			if(patternOffset < numAdvanced){
				if(patternTimer < 0){
					int i = 0;
					for(i = 0; i < advancedPattern[patternOffset]; i++){
						spawnObstacle(advancedPattern[patternOffset + i + 1]);
					}
					patternOffset += i + 1;
					patternTimer = advancedPattern[patternOffset];
					patternOffset++;
				}
			}
			break;

		case spawnPatterns.Painful:
			if(patternOffset < numPainful){
				if(patternTimer < 0){
					int i = 0;
					for(i = 0; i < painfulPattern[patternOffset]; i++){
						spawnObstacle(painfulPattern[patternOffset + i + 1]);
					}
					patternOffset += i + 1;
					patternTimer = painfulPattern[patternOffset];
					patternOffset++;
				}
			}
			break;

		case spawnPatterns.Stairs:
			if(patternOffset < numStairs){
				if(patternTimer < 0){
					int i = 0;
					for(i = 0; i < stairsPattern[patternOffset]; i++){
						spawnObstacle(stairsPattern[patternOffset + i + 1]);
					}
					patternOffset += i + 1;
					patternTimer = stairsPattern[patternOffset];
					patternOffset++;
				}
			}
			break;

		case spawnPatterns.Butterfly:
			if(patternOffset < numButterfly){
				if(patternTimer < 0){
					int i = 0;
					for(i = 0; i < butterflyPattern[patternOffset]; i++){
						spawnObstacle(butterflyPattern[patternOffset + i + 1]);
					}
					patternOffset += i + 1;
					patternTimer = butterflyPattern[patternOffset];
					patternOffset++;
				}
			}
			break;

		case spawnPatterns.SpaceInvaders:
			if(patternOffset < numSpaceInvaders){
				if(patternTimer < 0){
					int i = 0;
					for(i = 0; i < spaceInvadersPattern[patternOffset]; i++){
						spawnObstacle(spaceInvadersPattern[patternOffset + i + 1]);
					}
					patternOffset += i + 1;
					patternTimer = spaceInvadersPattern[patternOffset];
					patternOffset++;
				}
			}
			break;

		}
	}

}
















