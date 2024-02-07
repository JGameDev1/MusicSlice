using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public enum Gamestates {FinishTheLevel,RunningGame,PauseTheGame,GameOver}
public class GameManager : MonoBehaviour
{public static GameManager _SharedInstanceGameManager;
public string PauseKey;
public Canvas PlayerCanvass,GameOverCanvass,PauseCanvass,FinishLevelCanvass;
public Gamestates CurrentGamestate;
public bool MusicOn;
public int Score,ScoreToKeep;
PlayerUI _PlayerUI;

public void RunTheGame(){CurrentGamestate=Gamestates.RunningGame;if(CurrentGamestate==Gamestates.RunningGame){MusicOn=true;PlayerCanvass.enabled=true;GameOverCanvass.enabled=false; FinishLevelCanvass.enabled=false;PauseCanvass.enabled=false;}}
public void PauseTheGame(){CurrentGamestate=Gamestates.PauseTheGame;if(CurrentGamestate==Gamestates.PauseTheGame){MusicOn=false;PlayerCanvass.enabled=false;FinishLevelCanvass.enabled=false;GameOverCanvass.enabled=false;PauseCanvass.enabled=true;}}
public void GameOver(){CurrentGamestate=Gamestates.GameOver;if(CurrentGamestate==Gamestates.GameOver){MusicOn=true;PlayerCanvass.enabled=false;GameOverCanvass.enabled=true;FinishLevelCanvass.enabled=false;PauseCanvass.enabled=false;MusicOn=false;}}
public void FinishTheLevel(){CurrentGamestate=Gamestates.FinishTheLevel;if(CurrentGamestate==Gamestates.FinishTheLevel){MusicOn=false;PlayerCanvass.enabled=true;FinishLevelCanvass.enabled=true;GameOverCanvass.enabled=false;PauseCanvass.enabled=false;}}
public void ChangeGamestates(Gamestates NewGameState)
{if(NewGameState==Gamestates.RunningGame){CurrentGamestate=NewGameState;RunTheGame();}
else if(NewGameState==Gamestates.PauseTheGame){CurrentGamestate=NewGameState;PauseTheGame();}
else if(NewGameState==Gamestates.GameOver){CurrentGamestate=NewGameState;GameOver();}
else if(NewGameState==Gamestates.FinishTheLevel){CurrentGamestate=NewGameState;FinishTheLevel();}}
public void RetryLevel(){if(SceneManager.GetActiveScene().name=="PartyTrain"){SceneManager.LoadScene(SceneManager.GetActiveScene().name);RunTheGame();Score=0;FindObjectOfType<PrefabRespawnManager>().BossMoment=false;}else if(SceneManager.GetActiveScene().name=="LifeClub1"){SceneManager.LoadScene(SceneManager.GetActiveScene().name);RunTheGame();Score=20;FindObjectOfType<PrefabRespawnManager>().BossMoment=false;}else if(SceneManager.GetActiveScene().name=="LifeClub2"){SceneManager.LoadScene(SceneManager.GetActiveScene().name);RunTheGame();Score=40;FindObjectOfType<PrefabRespawnManager>().BossMoment=false;}else if(SceneManager.GetActiveScene().name=="LifeClub3"){SceneManager.LoadScene(SceneManager.GetActiveScene().name);RunTheGame();Score=60;FindObjectOfType<PrefabRespawnManager>().BossMoment=false;}}
public void ButtonGoTo(){if(SceneManager.GetActiveScene().name=="PartyTrain"&&Score>=20&&!MusicOn){RunTheGame();SceneManager.LoadScene("LifeClubDoor");}else if(SceneManager.GetActiveScene().name=="LifeClub1"&&Score>=40&&!MusicOn){RunTheGame();SceneManager.LoadScene("LifeClub2");}else if(SceneManager.GetActiveScene().name=="LifeClub2"&&Score>=60&&!MusicOn){RunTheGame();SceneManager.LoadScene("LifeClub3");}}
public void ButtonBackMenu(){SceneManager.LoadScene("Menu");}
public void EliminateButtonToGo(){if(SceneManager.GetActiveScene().name=="PartyTrain"){RunTheGame();Score=20;}else if(SceneManager.GetActiveScene().name=="LifeClub1"){RunTheGame();Score=40;FindObjectOfType<PrefabRespawnManager>().BossMoment=false;}else if(SceneManager.GetActiveScene().name=="LifeClub2"){RunTheGame();Score=40;FindObjectOfType<PrefabRespawnManager>().BossMoment=false;}else if(SceneManager.GetActiveScene().name=="LifeClub3"){RunTheGame();Score=60;FindObjectOfType<PrefabRespawnManager>().BossMoment=false;}}
public void AdvanceLevel(){if(SceneManager.GetActiveScene().name=="PartyTrain"&&Score>=20){FinishTheLevel();}else if(SceneManager.GetActiveScene().name=="LifeClub1"&&Score>=50&&MusicOn){FinishTheLevel();}else if(SceneManager.GetActiveScene().name=="LifeClub2"&&Score>=70){FinishTheLevel();}}
public void PauseAndContinueTheGame(){if(Input.GetKeyDown(PauseKey)&&CurrentGamestate==Gamestates.RunningGame){PauseTheGame();}else if(Input.GetKeyDown(PauseKey)&&CurrentGamestate==Gamestates.PauseTheGame){RunTheGame();}}
private void Awake(){_SharedInstanceGameManager=this;_PlayerUI=FindObjectOfType<PlayerUI>();PlayerCanvass=GameObject.Find("RunningGame").GetComponent<Canvas>();GameOverCanvass=GameObject.Find("GameOver").GetComponent<Canvas>();PauseCanvass=GameObject.Find("PauseGame").GetComponent<Canvas>();FinishLevelCanvass=GameObject.Find("Win").GetComponent<Canvas>();}

void RecordScore(){if(Score>PlayerPrefs.GetInt("ScoreToRecord",0)){PlayerPrefs.SetInt("ScoreToRecord",Score);ScoreToKeep=PlayerPrefs.GetInt("ScoreToRecord",Score);}}
private void Start(){ChangeGamestates(CurrentGamestate);ScoreToKeep=PlayerPrefs.GetInt("ScoreToRecord",0);}

void Update(){_PlayerUI=FindObjectOfType<PlayerUI>();
PauseAndContinueTheGame();AdvanceLevel();MusicManager.MusicManagerSharedInstance.MusicOn=MusicOn;MusicManager.MusicManagerSharedInstance.Pause=!MusicOn;RecordScore();}
}