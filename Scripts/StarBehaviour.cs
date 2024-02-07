using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBehaviour : MonoBehaviour
{   public float Starspeed,TorqueSpeed,NegLimitX,PosLimitX,NegLimitY,PosLimitY;
    public PlayerController _PlayerController;
    public Rigidbody2D StarBody;
    private TrailRenderer _TrailRenderer;
    public PlayerUI _PlayerUI;
    private float OFFITEMSCRONO=6;
    private void OnEnable()
    {_PlayerController=FindObjectOfType<PlayerController>();StarBody=GetComponent<Rigidbody2D>();_TrailRenderer=GetComponent<TrailRenderer>();_PlayerUI=FindObjectOfType<PlayerUI>();
     StarBody.velocity=new Vector2(_PlayerController.LastMovement.x,_PlayerController.LastMovement.y)*Starspeed;
     StarBody.AddTorque(_PlayerController.LastMovement.x*TorqueSpeed*100);_TrailRenderer.enabled=true;}
    private void OnCollisionEnter2D(Collision2D collision)
{if(collision.gameObject.name=="Floor"){_TrailRenderer.enabled=false;gameObject.SetActive(false);}
if(collision.gameObject.name=="Beer(Clone)"&&GameManager._SharedInstanceGameManager.PlayerCanvass==enabled){_PlayerUI.Beer.enabled=true;_PlayerUI.EnergyDrink.enabled=false;_PlayerUI.DiscoBall.enabled=false;_PlayerController.Cronometre=_PlayerController.OnPowerUpCronometre;collision.gameObject.SetActive(false);_TrailRenderer.enabled=false;gameObject.SetActive(false);if(FindObjectOfType<HealthManager>().CurrentHealth<3){FindObjectOfType<HealthManager>().CurrentHealth++;}}
if(collision.gameObject.name=="EnergyDrink(Clone)"&&GameManager._SharedInstanceGameManager.PlayerCanvass==enabled){_PlayerUI.EnergyDrink.enabled=true;_PlayerUI.DiscoBall.enabled=false;_PlayerUI.Beer.enabled=false;_PlayerController.Cronometre=_PlayerController.OnPowerUpCronometre;collision.gameObject.SetActive(false);_TrailRenderer.enabled=false;gameObject.SetActive(false);if(FindObjectOfType<HealthManager>().CurrentHealth<3){FindObjectOfType<HealthManager>().CurrentHealth++;}}
if(collision.gameObject.name=="DiscoBall(Clone)"&&GameManager._SharedInstanceGameManager.PlayerCanvass==enabled){_PlayerUI.DiscoBall.enabled=true;_PlayerUI.Beer.enabled=false;_PlayerUI.EnergyDrink.enabled=false;_PlayerController.Cronometre=_PlayerController.OnPowerUpCronometre;collision.gameObject.SetActive(false);_TrailRenderer.enabled=false;gameObject.SetActive(false);FindObjectOfType<HealthManager>().Invulnerability=true;}
if(collision.gameObject.tag=="Enemy"&&!collision.gameObject.GetComponent<EnemyPatrolMovement>().NoKillEnemy&&GameManager._SharedInstanceGameManager.PlayerCanvass==enabled||collision.gameObject.tag=="BossEnemy"&&!collision.gameObject.GetComponent<NemesisBehaviour>().NoKillEnemy&&GameManager._SharedInstanceGameManager.PlayerCanvass==enabled||collision.gameObject.tag=="Dron"&&GameManager._SharedInstanceGameManager.PlayerCanvass==enabled){collision.gameObject.GetComponent<EnemyHealthManager>().CurrentHealth--;_TrailRenderer.enabled=false;gameObject.SetActive(false);}
else if(collision.gameObject.tag=="Enemy"&&collision.gameObject.GetComponent<EnemyPatrolMovement>().NoKillEnemy&&GameManager._SharedInstanceGameManager.PlayerCanvass==enabled||collision.gameObject.tag=="BossEnemy"&&collision.gameObject.GetComponent<NemesisBehaviour>().NoKillEnemy&&GameManager._SharedInstanceGameManager.PlayerCanvass==enabled){gameObject.SetActive(false);}}
    void DesactivateThis()
    { if (GameManager._SharedInstanceGameManager.CurrentGamestate == Gamestates.RunningGame) { OFFITEMSCRONO -= Time.deltaTime; if (OFFITEMSCRONO <= 0) { gameObject.SetActive(false);OFFITEMSCRONO=6;}}}

    private void Update()
    {if (transform.position.x<NegLimitX||transform.position.x>PosLimitX){gameObject.SetActive(false);}
    if(transform.position.y<NegLimitY||transform.position.y>PosLimitY){gameObject.SetActive(false);}
    DesactivateThis();}
}
