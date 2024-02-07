using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{public float CronoAdvice;
public List<GameObject>HealthRepresentation;
public Image Beer,EnergyDrink,DiscoBall,Yeah,TheEnd,ElFin,BossAdvice,AvisoDeJefe;
public HealthManager _PlayerHealthManager;
public int Index;
public bool Ingles,UseJoystick;public Text MaxScoreText,ScoreText,WinText,PauseText,ContinueText,RestartText,LvlName;
public GameObject ButtonsGroup,JoystickContainer;

private void Start()
{ButtonsGroup=GameObject.Find("Buttons");JoystickContainer=GameObject.Find("Fixed Joystick");
Ingles =MusicManager.MusicManagerSharedInstance.Ingles;UseJoystick=MusicManager.MusicManagerSharedInstance.UseJoystick;
if(UseJoystick){ButtonsGroup.SetActive(false);JoystickContainer.SetActive(true);}else{ButtonsGroup.SetActive(true);JoystickContainer.SetActive(false);}
InitialLife();
_PlayerHealthManager=GameObject.Find("Player").GetComponent<HealthManager>();
Beer.enabled=false;EnergyDrink.enabled=false;DiscoBall.enabled=false;
CronoAdvice=2;
if(SceneManager.GetActiveScene().name=="LifeClub2"&&Ingles){BossAdvice.enabled=true;}else if(SceneManager.GetActiveScene().name=="LifeClub2"&&!Ingles){AvisoDeJefe.enabled=true;}
if(UseJoystick==false){;}else{;}}

void InitialLife(){for(int i=0;i<HealthRepresentation.Count;i++){HealthRepresentation[i].GetComponent<Image>().color=new Color(1,1,1,1);}}

void LifeVisualization()
{if(_PlayerHealthManager.CurrentHealth<3&&_PlayerHealthManager.CurrentHealth==2){HealthRepresentation[2].GetComponent<Image>().color=new Color(1,1,1,0);}else if(_PlayerHealthManager.CurrentHealth>2&&_PlayerHealthManager.CurrentHealth==3){HealthRepresentation[2].GetComponent<Image>().color=new Color(1,1,1,1);}
if(_PlayerHealthManager.CurrentHealth<2&&_PlayerHealthManager.CurrentHealth==1){HealthRepresentation[1].GetComponent<Image>().color=new Color(1,1,1,0);}else if(_PlayerHealthManager.CurrentHealth>1&&_PlayerHealthManager.CurrentHealth==2){HealthRepresentation[1].GetComponent<Image>().color=new Color(1,1,1,1);}
if(_PlayerHealthManager.CurrentHealth<1&&_PlayerHealthManager.CurrentHealth==0){HealthRepresentation[0].GetComponent<Image>().color=new Color(1,1,1,0);}else if(_PlayerHealthManager.CurrentHealth>0&&_PlayerHealthManager.CurrentHealth==1){HealthRepresentation[0].GetComponent<Image>().color=new Color(1,1,1,1);}}

void ShowScreenTexts()
{LvlName.text=SceneManager.GetActiveScene().name;
if(Ingles&&GameManager._SharedInstanceGameManager.CurrentGamestate==Gamestates.RunningGame){ScoreText.text="Score:"+GameManager._SharedInstanceGameManager.Score.ToString();MaxScoreText.text="Max:"+GameManager._SharedInstanceGameManager.ScoreToKeep.ToString();PauseText.text="Pause";
if(GameManager._SharedInstanceGameManager.Score==30*Index){BossAdvice.enabled=true;}}else if(!Ingles&&GameManager._SharedInstanceGameManager.CurrentGamestate==Gamestates.RunningGame){ScoreText.text="Puntos:"+GameManager._SharedInstanceGameManager.Score.ToString();MaxScoreText.text="Max:"+GameManager._SharedInstanceGameManager.ScoreToKeep.ToString();PauseText.text="Pausa";if(GameManager._SharedInstanceGameManager.Score==30*Index){AvisoDeJefe.enabled=true;}}
if(Ingles&&GameManager._SharedInstanceGameManager.CurrentGamestate==Gamestates.PauseTheGame){ContinueText.text="Continue";}else{ContinueText.text="Continuar";}
if(Ingles&&GameManager._SharedInstanceGameManager.CurrentGamestate==Gamestates.GameOver){RestartText.text="Retry";TheEnd.enabled=true;ElFin.enabled=false;}else{RestartText.text="Reintentar";ElFin.enabled=true;TheEnd.enabled=false;}
if(Ingles&&GameManager._SharedInstanceGameManager.CurrentGamestate==Gamestates.FinishTheLevel){WinText.text="Well Done!, use what  you learn";}else if(!Ingles&&GameManager._SharedInstanceGameManager.CurrentGamestate==Gamestates.FinishTheLevel){WinText.text="¡Bien hecho!, usa lo que has aprendido";}}

void UnaAyuda(){if(GameManager._SharedInstanceGameManager.Score==30*Index&&Ingles){CronoAdvice=2;Yeah.enabled=true;_PlayerHealthManager.CurrentHealth+=_PlayerHealthManager.HealthValue-_PlayerHealthManager.CurrentHealth;Index++;}if(Yeah.enabled==true){CronoAdvice-=Time.deltaTime;if(CronoAdvice<=0){Yeah.enabled=false;}}else if(GameManager._SharedInstanceGameManager.Score==30*Index&&!Ingles){CronoAdvice=2;Yeah.enabled=true;_PlayerHealthManager.CurrentHealth+=_PlayerHealthManager.HealthValue-_PlayerHealthManager.CurrentHealth;Index++;}if(Yeah.enabled==true){CronoAdvice-=Time.deltaTime;if(CronoAdvice<=0){Yeah.enabled=false;}}}

private void Update()
{ShowScreenTexts();LifeVisualization();UnaAyuda();
 CronoAdvice-=Time.deltaTime;if(CronoAdvice<=-0.5f){AvisoDeJefe.enabled=false;BossAdvice.enabled=false;}
 if(CronoAdvice<=-1){CronoAdvice=2f;}}
}
