using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using static UnityEngine.RuleTile.TilingRuleOutput;

public class PrefabRespawnManager : MonoBehaviour
{
    public List<GameObject> Prefabs; public List<GameObject> ItemsCreated;public List<GameObject> People; public List<GameObject> Enemies1; public List<GameObject> Enemies1Created; public List<GameObject> SpecialEnemies; public List<GameObject> EspumaDrones; public List<GameObject> PhotoDrones; public List<GameObject> EnemyBosses; public List<GameObject> EnemyBossesCreated; public GameObject[] RespawnPoint;
    public float OnCronometre, NegLimitX, PosLimitX, NegLimitY, PosLimitY, Cronometre, OnNECronometre, NECronometre, OnDronesCronometre, DronesCronometre, OnENCronometre, ENCronometre, OFFITEMSCRONO;
    public bool RespawnOn, RespawnNEOn, RespawnENOn, RespawnDronesOn, HayDrones,BossMoment;

    void InstantiatePrefabs() { GameObject Beer = Instantiate(Prefabs[0]); Beer.SetActive(false); ItemsCreated.Add(Beer); GameObject EnergyDrink = Instantiate(Prefabs[1]); EnergyDrink.SetActive(false); ItemsCreated.Add(EnergyDrink); GameObject DiscoBall = Instantiate(Prefabs[2]); DiscoBall.SetActive(false); ItemsCreated.Add(DiscoBall); }

    void InstantiateNormalEnemies()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject AfroE = Instantiate(Enemies1[0]); AfroE.SetActive(false); Enemies1Created.Add(AfroE); GameObject Bailarin = Instantiate(Enemies1[1]); Bailarin.SetActive(false); Enemies1Created.Add(Bailarin); GameObject BBallet = Instantiate(Enemies1[2]); BBallet.SetActive(false); Enemies1Created.Add(BBallet); GameObject BananaD1 = Instantiate(Enemies1[3]); BananaD1.SetActive(false); Enemies1Created.Add(BananaD1); GameObject BananaD2 = Instantiate(Enemies1[4]); BananaD2.SetActive(false); Enemies1Created.Add(BananaD2);
            GameObject GotticG = Instantiate(Enemies1[5]); GotticG.SetActive(false); Enemies1Created.Add(GotticG); GameObject MoonWalker = Instantiate(Enemies1[6]); MoonWalker.SetActive(false); Enemies1Created.Add(MoonWalker); GameObject PastelG = Instantiate(Enemies1[7]); PastelG.SetActive(false); Enemies1Created.Add(PastelG); GameObject MujerTwerkin = Instantiate(Enemies1[8]); MujerTwerkin.SetActive(false); Enemies1Created.Add(MujerTwerkin);
        }
    }

    void InstantiatePhotoDronesEnemies()
    {   for (int i = 0; i < 2; i++)
        {GameObject PhotoDrone=Instantiate(SpecialEnemies[1]);PhotoDrone.SetActive(false);PhotoDrones.Add(PhotoDrone);}
    }
    void InstantiateEspumaDrones(){for (int i = 0; i < 2; i++){GameObject EspumaDrone=Instantiate(SpecialEnemies[0]);EspumaDrone.SetActive(false);EspumaDrones.Add(EspumaDrone);}}
    void InstantiatePatovicas() {GameObject Pato1=Instantiate(EnemyBosses[0]); Pato1.SetActive(false); EnemyBossesCreated.Add(Pato1); GameObject Pato2 = Instantiate(EnemyBosses[1]); Pato2.SetActive(false); EnemyBossesCreated.Add(Pato2); }
    void InstantiateNemesis() {GameObject Nemesis=Instantiate(EnemyBosses[2]);Nemesis.SetActive(false);EnemyBossesCreated.Add(Nemesis);BossMoment=false;}
    void InstantiateNemesis2() {GameObject Nemesis2=Instantiate(EnemyBosses[3]);Nemesis2.SetActive(false);EnemyBossesCreated.Add(Nemesis2);BossMoment=false;}

    public GameObject RequestRandomNormalEmemies() { int NumberOnList = Random.Range(0,17); if (!Enemies1Created[NumberOnList].activeSelf) { Enemies1Created[NumberOnList].SetActive(true); Enemies1Created[NumberOnList].transform.position = RespawnPoint[Random.Range(0, 2)].transform.position; } return Enemies1Created[NumberOnList]; }
    public GameObject RequestPhotoDronEmemy() { int NumberOnList = Random.Range(0,2);if(!PhotoDrones[NumberOnList].activeSelf) { PhotoDrones[NumberOnList].SetActive(true); PhotoDrones[NumberOnList].transform.position = RespawnPoint[Random.Range(0, 2)].transform.position; } return PhotoDrones[NumberOnList]; }
    public GameObject RequestEspumaDronEmemy() { int NumberOnList = Random.Range(0,2);if(!EspumaDrones[NumberOnList].activeSelf) { EspumaDrones[NumberOnList].SetActive(true); EspumaDrones[NumberOnList].transform.position = RespawnPoint[Random.Range(0, 2)].transform.position; } return EspumaDrones[NumberOnList]; }
    public GameObject RequestRandomBossEmemies() {int NumberOnList=Random.Range(1,2);if(!EnemyBossesCreated[NumberOnList].activeSelf) { EnemyBossesCreated[NumberOnList].SetActive(true);EnemyBossesCreated[NumberOnList].transform.position=RespawnPoint[Random.Range(0,2)].transform.position;} return EnemyBossesCreated[NumberOnList]; }
    public GameObject RequestNemesis() {if(!EnemyBossesCreated[0].activeSelf){EnemyBossesCreated[0].SetActive(true);EnemyBossesCreated[0].transform.position=RespawnPoint[Random.Range(0,2)].transform.position;}return EnemyBossesCreated[0];}
    public GameObject RequestNemesis2() {if(!EnemyBossesCreated[3].activeSelf){EnemyBossesCreated[3].SetActive(true);EnemyBossesCreated[3].transform.position=RespawnPoint[Random.Range(0,2)].transform.position;}return EnemyBossesCreated[3];}
    public GameObject RequestPrefabs() { int Index = Random.Range(0, 3); if (!ItemsCreated[Index].activeSelf) { ItemsCreated[Index].SetActive(true); ItemsCreated[Index].transform.position = new Vector3(Random.Range(NegLimitX, PosLimitX), Random.Range(NegLimitY, PosLimitY), transform.position.z); } return ItemsCreated[Index]; }

    void CronometreFunctionPowerUp() { if (GameManager._SharedInstanceGameManager.CurrentGamestate == Gamestates.RunningGame) { Cronometre -= Time.deltaTime; if (Cronometre <= 0) { RespawnOn = true; if (RespawnOn) { RequestPrefabs(); Cronometre = OnCronometre; RespawnOn = false; } } } }
    void CronometreForNormalEnemiesAparition() { if (GameManager._SharedInstanceGameManager.CurrentGamestate == Gamestates.RunningGame&&!BossMoment) { NECronometre -= Time.deltaTime; if (NECronometre <= 0) { RespawnNEOn = true; if (RespawnNEOn) { RequestRandomNormalEmemies(); NECronometre = OnNECronometre; RespawnNEOn = false; } } } }
    void CronometreForEspecialEnemiesAparition() { if (GameManager._SharedInstanceGameManager.CurrentGamestate == Gamestates.RunningGame&&!BossMoment) { ENCronometre -= Time.deltaTime; if (ENCronometre <= 0) { RespawnENOn = true; if (RespawnENOn) { RequestRandomBossEmemies(); ENCronometre = OnENCronometre; RespawnENOn = false; } } } }
    void CronometreForDronesAparition() { if (GameManager._SharedInstanceGameManager.CurrentGamestate == Gamestates.RunningGame&&HayDrones&&!BossMoment) { DronesCronometre -= Time.deltaTime; if (DronesCronometre <= 0) { RespawnDronesOn = true; if (RespawnDronesOn) { RequestPhotoDronEmemy(); RequestEspumaDronEmemy(); DronesCronometre = OnDronesCronometre; RespawnDronesOn = false;}}}}
    void NemesisCall()
   {if(FindObjectOfType<PlayerUI>().BossAdvice.enabled==true||FindObjectOfType<PlayerUI>().AvisoDeJefe.enabled==true){RequestNemesis();}}
    void Nemesis2Call(){if(FindObjectOfType<PlayerUI>().BossAdvice.enabled==true||FindObjectOfType<PlayerUI>().AvisoDeJefe.enabled==true&&SceneManager.GetActiveScene().name=="LifeClub3"){RequestNemesis2();}}

    void ShowBarTenders(){if(GameManager._SharedInstanceGameManager.CurrentGamestate==Gamestates.RunningGame&&HayDrones)foreach(GameObject P in People){P.SetActive(false);} else { foreach (GameObject P in People){P.SetActive(true);}}}
    private void Start()
    {BossMoment=false;InstantiateNormalEnemies();InstantiateNemesis2();InstantiateEspumaDrones();InstantiatePhotoDronesEnemies();InstantiatePrefabs();InstantiatePatovicas();Cronometre=OnCronometre;NECronometre=OnNECronometre;DronesCronometre=OnDronesCronometre;ENCronometre=OnENCronometre;RespawnOn=false;RespawnDronesOn=false;RespawnENOn=false;RespawnNEOn=false;OFFITEMSCRONO=8;InstantiateNemesis();}

    private void Update()
{NemesisCall();Nemesis2Call();CronometreFunctionPowerUp();if(!BossMoment){CronometreForNormalEnemiesAparition();CronometreForEspecialEnemiesAparition();CronometreForDronesAparition();ShowBarTenders();}}
}