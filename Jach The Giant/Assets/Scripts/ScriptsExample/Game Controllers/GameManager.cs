using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	[HideInInspector]//won't show
	public bool gameStartedFromMainMenu, gameRestartedAfterPlayerDied;

	[HideInInspector]
	public int score, coinScore,lifeScore;

	// Use this for initialization
	void Awake () {
		MakeSingleton ();
	}

	void Start(){
		InitializeVariables ();
	}

	//each time we change the Scene
	void OnLevelWasLoaded(){

		if (Application.loadedLevelName == "Gameplay") {
			/* if game Restarted => meaning that Player Died and you 
				 play again in Gameplay Scene*/
			if(gameRestartedAfterPlayerDied){
				/* Get present score,coinScore and lifeScore */
				GameplayController.instance.SetScore(score);
				GameplayController.instance.SetCoinScore(coinScore);
				GameplayController.instance.SetLifeScore(lifeScore);

				PlayerScore.scoreCount = score;
				PlayerScore.coinCount = coinScore;
				PlayerScore.lifeCount = lifeScore;
			}else if(gameStartedFromMainMenu){
				/*if game Started From Main Menu Scene then reset 
				score,coin = 0 but life give to 2 */
				PlayerScore.scoreCount = 0;
				PlayerScore.coinCount = 0;
				PlayerScore.lifeCount = 2;

				GameplayController.instance.SetScore(0);
				GameplayController.instance.SetCoinScore(0);
				GameplayController.instance.SetLifeScore(2);
			}
		}
	}

	void InitializeVariables(){
		if(!PlayerPrefs.HasKey("Game Initialized")){
		
			GamePreferences.SetEasyDifficultyState(0);
			GamePreferences.SetEasyDifficultyCoinScore(0);
			GamePreferences.SetEasyDifficultyHighScore(0);

			GamePreferences.SetMediumDifficultyState(1);
			GamePreferences.SetMediumDifficultyCoinScore(0);
			GamePreferences.SetMediumDifficultyHighScore(0);

			GamePreferences.SetHardDifficultyState(0);
			GamePreferences.SetHardDifficultyCoinScore(0);
			GamePreferences.SetHardDifficultyHighScore(0);

			GamePreferences.SetMusicState(0);

			PlayerPrefs.SetInt("Game Initialized",123);
		}
	}

	//each Scene in game will take 1 Game Manager
	void MakeSingleton(){
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void CheckGameStatus(int score, int coinScore, int lifeScore){
		/* */
		if (lifeScore < 0) {

			if(GamePreferences.GetEasyDifficultyState() == 1){

				int highScore = GamePreferences.GetEasyDifficultyHighScore();
				int coinHighScore = GamePreferences.GetEasyDifficultyCoinScore();

				if(highScore < score)
					GamePreferences.SetEasyDifficultyHighScore(score);

				if(coinHighScore < coinScore)
					GamePreferences.SetMediumDifficultyCoinScore(coinScore);
			}

			if(GamePreferences.GetMediumDifficultyState() == 1){
				
				int highScore = GamePreferences.GetMediumDifficultyHighScore();
				int coinHighScore = GamePreferences.GetMediumDifficultyCoinScore();
				
				if(highScore < score)
					GamePreferences.SetMediumDifficultyHighScore(score);
				
				if(coinHighScore < coinScore)
					GamePreferences.SetMediumDifficultyCoinScore(coinScore);
			}

			if(GamePreferences.GetHardDifficultyState() == 1){
				
				int highScore = GamePreferences.GetHardDifficultyHighScore();
				int coinHighScore = GamePreferences.GetHardDifficultyCoinScore();
				
				if(highScore < score)
					GamePreferences.SetHardDifficultyHighScore(score);
				
				if(coinHighScore < coinScore)
					GamePreferences.SetHardDifficultyCoinScore(coinScore);
			}
			/* reload level if we have died*/
			gameStartedFromMainMenu = false;
			gameRestartedAfterPlayerDied = false;
			/*if you have no more life, you will tranform Gameover,
			 parse score and coinScore*/
			GameplayController.instance.GameOverShowPanel(score,coinScore);

		} else {
			/*get score,coinScore,lifeScore from function gameRestartedAfterPlayerDied
			 if player still have life*/
			this.score = score;
			this.coinScore = coinScore;
			this.lifeScore = lifeScore;

			/* if player still have a life then Gameplay will restart game and
			 don't restart from Main Menu */
			gameStartedFromMainMenu = false;
			gameRestartedAfterPlayerDied = true;
		
			GameplayController.instance.PlayerDiedRestartTheGame();
		}
	}
}
