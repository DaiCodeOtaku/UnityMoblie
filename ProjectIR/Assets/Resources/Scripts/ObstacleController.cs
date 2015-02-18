using UnityEngine;
using System.Collections;



public class ObstacleController : MonoBehaviour {

	public GameObject ObstacleTemplate;
	enum spawnPatterns {nullPattern = 0, Basic, Advanced, Painful, Stairs, Butterfly, 
		SpaceInvaders, Pillar, TargetLocked, Platforms, Spears};
	public float patternTimer;
	public static bool GO = false;
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

	int numPillar = 61;
	float[] pillarPattern = {	0.1f, 1, -1.5f,		0.1f, 1, 1.0f,		0.1f, 1, -2.0f,		0.1f, 1, 1.5f,
		0.1f, 1, -1.5f,			0.1f, 1, 1.0f,		0.1f, 1, -2.0f,		0.2f, 1, -1.5f,		0.2f, 2, -2.0f, -0.5f,
		0.2f, 2, -1.5f, 0.0f,	0.2f, 2, -2.0f, -0.5f,		0.2f, 1, -1.5f,		0.2f, 2, -2.0f, 1.5f,
		0.2f, 2, -1.5f, 2.0f,	0.2f, 2, -2.0f, 1.5f,		0.2f, 1, -1.5f,		0.2f, 1, -2.0f,		0.2f, 1, -1.5f, 0.1f};

	int numTargetLocked = 59;
	float[] targetLockedPattern = {	0.2f, 2, -1.5f, 1.5f,	0.2f, 2, 1.0f, -1.0f,	0.2f, 2, 0.5f, -0.5f, 	0.2f, 1, 0.0f,
		0.5f, 2, 3.0f, -3.0f,	0.1f, 2, 2.5f, -2.5f,	0.1f, 2, 2.0f, -2.0f,	0.1f, 2, 1.5f, -1.5f,	0.1f, 2, 2.0f, -2.0f,
		0.1f, 2, 2.5f, -2.5f,	0.1f, 2, 3.0f, -3.0f,	0.5f, 1, 0.0f,			0.2f, 2, 0.5f, -0.5f,	0.2f, 2, 1.0f, -1.0f,
		0.2f, 2, 1.5f, -1.5f,	0.3f};

	int numPlatforms = 56;
	float[] platformsPattern = {	0.5f, 9, 3.0f, 0.5f, 0.0f, -0.5f, -1.0f, -1.5f, -2.0f, -2.5f, -3.0f,
									0.5f, 9, 3.0f, 2.5f, 0.0f, -0.5f, -1.0f, -1.5f, -2.0f, -2.5f, -3.0f,
									0.5f, 9, 3.0f, 2.5f, 2.0f, -0.5f, -1.0f, -1.5f, -2.0f, -2.5f, -3.0f,
									0.5f, 9, 3.0f, 0.5f, 0.0f, -0.5f, -1.0f, -1.5f, -2.0f, -2.5f, -3.0f,
									0.5f, 9, 3.0f, 2.5f, 2.0f, 1.5f, -1.0f, -1.5f, -2.0f, -2.5f, -3.0f, 	0.5f};

	int numSpears = 289;
	float[] spearsPattern = {		0.1f, 1, -2.5f,		0.1f, 1, -2.5f, 	0.1f, 1, -2.5f, 	0.1f, 2, 1.5f, -2.5f,
		0.1f, 2, 1.5f, -2.5f,		0.1f, 2, 1.5f, -2.5f,		0.1f, 2, 1.5f, -2.5f, 		0.1f, 2, 1.5f, -2.5f,
		0.1f, 1, 1.5f,		0.1f, 1, 1.5f,		0.1f, 2, 1.5f, -1.5f, 		0.1f, 1, -1.5f, 	0.1f, 1, -1.5f,
		0.1f, 1, -1.5f,		0.1f, 1, -1.5f,		0.1f, 1, -1.5f,		0.1f, 1, -1.5f,		0.1f, 1, -1.5f,		0.1f, 1, 1.0f,
		0.1f, 1, 1.0f, 		0.1f, 1, 1.0f,		0.1f, 1, 1.0f,		0.1f, 1, 1.0f,		0.1f, 1, 1.0f,		0.1f, 1, 1.0f,
		0.1f, 1, 1.0f,		0.3f, 1, -0.5f, 		0.1f, 1, -0.5f,		0.1f, 1, -0.5f, 	0.1f, 1, -0.5f,		0.1f, 1, -0.5f,
		0.1f, 1, -0.5f, 	0.1f, 2, -0.5f, 2.5f,		0.1f, 2, -0.5f, 2.5f,		0.1f, 1, 2.5f,			0.1f, 1, 2.5f,
		0.1f, 1, 2.5f,		0.1f, 2, 2.5f, -2.5f, 		0.1f, 2, 2.5f, -2.5f,		0.1f, 2, 2.5f, -2.5f,	0.1f, 1, -2.5f,
		0.1f, 1, -2.5f,		0.1f, 1, -2.5f,		0.1f, 1, -2.5f,		0.1f, 1, -2.5f,		0.4f, 1, 1.5f, 		0.1f, 2, 1.5f, -1.0f,
		0.1f, 2, 1.5f, -1.0f,		0.1f, 2, 1.5f, -1.0f,		0.1f, 2, 1.5f, -1.0f,		0.1f, 2, 1.5f, -1.0f,
		0.1f, 2, 1.5f, -1.0f,		0.1f, 2, 1.5f, -1.0f,		0.1f, 1, -1.0f,		0.3f, 1, 0.5f,			0.1f, 1, 0.5f,
		0.1f, 1, 0.5f,		0.1f, 2, 0.5f, -2.5f,		0.1f, 2, 0.5f, -2.5f,		0.1f, 2, 0.5f, -2.5f,	0.1f, 2, 0.5f, -2.5f,
		0.1f, 2, 0.5f, -2.5f,		0.1f, 1,  -2.5f,		0.1f, 1,  -2.5f,		0.1f, 1,  -2.5f,		0.4f, 2, 2.0f, -1.0f,
		0.1f, 2, 2.0f, -1.0f,		0.1f, 2, 2.0f, -1.0f,		0.1f, 2, 2.0f, -1.0f,		0.1f, 2, 2.0f, -1.0f,		0.1f, 2, 2.0f, -1.0f,
		0.1f, 2, 2.0f, -1.0f,		0.1f, 2, 2.0f, -1.0f,		0.4f, 1, -2.0f,		0.1f, 1, -2.0f,			0.1f, 1, -2.0f,
		0.1f, 2, -2.0f, 0.0f,		0.1f, 2, -2.0f, 0.0f,		0.1f, 2, -2.0f, 0.0f,		0.1f, 2, -2.0f, 0.0f,
		0.1f, 2, -2.0f, 0.0f,		0.1f, 1, 0.0f,		0.1f, 1, 0.0f,		0.1f, 1, 0.0f,		0.3f};

	// Use this for initialization
	void Start () {
		patternTimer = 2;
		previousPattern = 0;
		patternOffset = 0;
		spawnHeight = 7.0f;
		spawnDepth = -0.5f;
		//GO = false;
	}
	
	// Update is called once per frame
	void Update () {
		patternTimer -= Time.deltaTime;

		executePattern(currentPattern);

		if (patternTimer < 0) {
			currentPattern = newPattern();
			Debug.Log(currentPattern);
		}
	}

	spawnPatterns setPatternTimer(spawnPatterns CurrentPattern){
		switch (CurrentPattern) {
		case spawnPatterns.nullPattern:
			break;
		case spawnPatterns.Basic:
			patternTimer = basicPattern[0];
			break;
		case spawnPatterns.Advanced:
			patternTimer = advancedPattern[0];
			break;
		case spawnPatterns.Painful:
			patternTimer = painfulPattern[0];
			break;
		case spawnPatterns.Stairs:
			patternTimer = stairsPattern[0];
			break;
		case spawnPatterns.Butterfly:
			patternTimer = butterflyPattern[0];
			break;
		case spawnPatterns.SpaceInvaders:
			patternTimer = spaceInvadersPattern[0];
			break;
		case spawnPatterns.Pillar:
			patternTimer = pillarPattern[0];
			break;
		case spawnPatterns.TargetLocked:
			patternTimer = targetLockedPattern[0];
			break;
		case spawnPatterns.Platforms:
			patternTimer = platformsPattern[0];
			break;
		case spawnPatterns.Spears:
			patternTimer = spearsPattern[0];
			break;
		}
		patternOffset = 1;
		return CurrentPattern;
	}

	spawnPatterns newPattern(){
		spawnPatterns nextPattern = (spawnPatterns)((10 * Random.value) + 1);

		switch(nextPattern){

		case spawnPatterns.nullPattern:
			return setPatternTimer(setUpPattern(spawnPatterns.TargetLocked, spawnPatterns.Pillar));

		case spawnPatterns.Basic:
			return setPatternTimer(setUpPattern(spawnPatterns.Basic, spawnPatterns.Advanced));

		case spawnPatterns.Advanced:
			return setPatternTimer(setUpPattern(spawnPatterns.Advanced, spawnPatterns.Basic));

		case spawnPatterns.Painful:
			return setPatternTimer(setUpPattern(spawnPatterns.Painful, spawnPatterns.Advanced));

		case spawnPatterns.Stairs:
			return setPatternTimer(setUpPattern(spawnPatterns.Stairs, spawnPatterns.Painful));
		
		case spawnPatterns.Butterfly:
			return setPatternTimer(setUpPattern(spawnPatterns.Butterfly, spawnPatterns.Stairs));

		case spawnPatterns.SpaceInvaders:
			return setPatternTimer(setUpPattern(spawnPatterns.SpaceInvaders, spawnPatterns.Butterfly));

		case spawnPatterns.Pillar:
			return setPatternTimer(setUpPattern(spawnPatterns.Pillar, spawnPatterns.SpaceInvaders));

		case spawnPatterns.TargetLocked:
			return setPatternTimer(setUpPattern(spawnPatterns.TargetLocked, spawnPatterns.Pillar));

		case spawnPatterns.Platforms:
			return setPatternTimer(setUpPattern(spawnPatterns.Platforms, spawnPatterns.TargetLocked));

		case spawnPatterns.Spears:
			return setPatternTimer(setUpPattern(spawnPatterns.Spears, spawnPatterns.Platforms));

		}
		return 0;
	}

	spawnPatterns setUpPattern(spawnPatterns set, spawnPatterns backUp){
		if(set == previousPattern){
			previousPattern = backUp;
			return backUp;
		}
		previousPattern = set;
		return set;
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
			executePattern(basicPattern, numBasic);
			break;
		case spawnPatterns.Advanced:
			executePattern(advancedPattern, numAdvanced);
			break;

		case spawnPatterns.Painful:
			executePattern(painfulPattern, numPainful);
			break;

		case spawnPatterns.Stairs:
			executePattern(stairsPattern, numStairs);
			break;

		case spawnPatterns.Butterfly:
			executePattern(butterflyPattern, numButterfly);
			break;

		case spawnPatterns.SpaceInvaders:
			executePattern(spaceInvadersPattern, numSpaceInvaders);
			break;

		case spawnPatterns.Pillar:
			executePattern(pillarPattern, numPillar);
			break;

		case spawnPatterns.TargetLocked:
			executePattern(targetLockedPattern, numTargetLocked);
			break;

		case spawnPatterns.Platforms:
			executePattern(platformsPattern, numPlatforms);
			break;

		case spawnPatterns.Spears:
			executePattern(spearsPattern, numSpears);
			break;

		}
	}

	void executePattern(float[] Pattern, int NumPattern){
		if(patternOffset < NumPattern){
			if(patternTimer < 0){
				int i = 0;
				for(i = 0; i < Pattern[patternOffset]; i++){
					spawnObstacle(Pattern[patternOffset + i + 1]);
				}
				patternOffset += i + 1;
				patternTimer = Pattern[patternOffset];
				patternOffset++;
			}
		}
	}

}
















