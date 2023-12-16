using UnityEngine;
using System.Collections;

public static class GamePreferences {
	//Mode Easy,Medium,Hard
	public static string EasyDifficulty = "EasyDifficulty";
	public static string MediumDifficulty = "MediumDifficulty";
	public static string HardDifficulty = "HardDifficulty";

	public static string EasyDifficultyHighScore = "EasyDifficultyHighScore";
	public static string MediumDifficultyHighScore = "MediumDifficultyHighScore";
	public static string HardDifficultyHighScore = "HardDifficultyHighScore";

	public static string EasyDifficultyCoinScore = "EasyDifficultyCoinScore";
	public static string MediumDifficultyCoinScore = "MediumDifficultyCoinScore";
	public static string HardDifficultyCoinScore = "HardDifficultyCoinScore";

	public static string IsMusicOn = "IsMusicOn";
	 
	/*use int to represent boolean
	  1- true, 0 - false */

	public static int GetMusicState(){
		return PlayerPrefs.GetInt(GamePreferences.IsMusicOn);
	}
	public static void SetMusicState(int state){
		PlayerPrefs.SetInt (GamePreferences.IsMusicOn, state);
	}

	public static int GetEasyDifficultyState(){
		return PlayerPrefs.GetInt (GamePreferences.EasyDifficulty);
	}
	public static void SetEasyDifficultyState(int state){
		PlayerPrefs.SetInt (GamePreferences.EasyDifficulty, state);
	}

	public static int GetMediumDifficultyState(){
		return PlayerPrefs.GetInt (GamePreferences.MediumDifficulty);
	}
	public static void SetMediumDifficultyState(int state){
		PlayerPrefs.SetInt (GamePreferences.MediumDifficulty, state);
	}

	public static int GetHardDifficultyState(){
		return PlayerPrefs.GetInt (GamePreferences.HardDifficulty);
	}
	public static void SetHardDifficultyState(int state){
		PlayerPrefs.SetInt (GamePreferences.HardDifficulty, state);
	}

	public static int GetEasyDifficultyHighScore(){
		return PlayerPrefs.GetInt (GamePreferences.EasyDifficultyHighScore);
	}
	public static void SetEasyDifficultyHighScore(int state){
		PlayerPrefs.SetInt (GamePreferences.EasyDifficultyHighScore, state);
	}

	public static int GetMediumDifficultyHighScore(){
		return PlayerPrefs.GetInt (GamePreferences.MediumDifficultyHighScore);
	}
	public static void SetMediumDifficultyHighScore(int state){
		PlayerPrefs.SetInt (GamePreferences.MediumDifficultyHighScore, state);
	}

	public static int GetHardDifficultyHighScore(){
		return PlayerPrefs.GetInt (GamePreferences.HardDifficultyHighScore);
	}
	public static void SetHardDifficultyHighScore(int state){
		PlayerPrefs.SetInt (GamePreferences.HardDifficultyHighScore, state);
	}

	public static int GetEasyDifficultyCoinScore(){
		return PlayerPrefs.GetInt (GamePreferences.EasyDifficultyCoinScore);
	}
	public static void SetEasyDifficultyCoinScore(int state){
		PlayerPrefs.SetInt (GamePreferences.EasyDifficultyCoinScore, state);
	}

	public static int GetMediumDifficultyCoinScore(){
		return PlayerPrefs.GetInt (GamePreferences.MediumDifficultyCoinScore);
	}
	public static void SetMediumDifficultyCoinScore(int state){
		PlayerPrefs.SetInt (GamePreferences.MediumDifficultyCoinScore, state);
	}

	public static int GetHardDifficultyCoinScore(){
		return PlayerPrefs.GetInt (GamePreferences.HardDifficultyCoinScore);
	}
	public static void SetHardDifficultyCoinScore(int state){
		PlayerPrefs.SetInt (GamePreferences.HardDifficultyCoinScore, state);
	}
}
