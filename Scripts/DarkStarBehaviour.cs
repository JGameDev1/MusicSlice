using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkStarBehaviour : MonoBehaviour
{   public float Starspeed,TorqueSpeed,NegLimitX,PosLimitX,NegLimitY,PosLimitY;
    public NemesisBehaviour _NemesisBehaviour;
    public Rigidbody2D StarBody;
    private TrailRenderer _TrailRenderer;
    public PlayerUI _PlayerUI;
    private float OFFITEMSCRONO=6;
    private void OnEnable()
    {_NemesisBehaviour=FindObjectOfType<NemesisBehaviour>();StarBody=GetComponent<Rigidbody2D>();_TrailRenderer=GetComponent<TrailRenderer>();_PlayerUI=FindObjectOfType<PlayerUI>();
     StarBody.velocity=new Vector2(FindObjectOfType<PlayerController>().gameObject.transform.position.x-transform.position.x,FindObjectOfType<PlayerController>().gameObject.transform.position.y-transform.position.y).normalized*Starspeed;
     StarBody.AddTorque(_NemesisBehaviour.LastPositionRegistred.x*TorqueSpeed*100);_TrailRenderer.enabled=true;}

    private void Start()
    {_NemesisBehaviour=gameObject.GetComponent<NemesisBehaviour>();_NemesisBehaviour=gameObject.GetComponent<NemesisBehaviour>();StarBody=GetComponent<Rigidbody2D>();_TrailRenderer=GetComponent<TrailRenderer>();_PlayerUI=FindObjectOfType<PlayerUI>();}
    private void OnCollisionEnter2D(Collision2D collision)
{if(collision.gameObject.name=="Floor"){_TrailRenderer.enabled=false;gameObject.SetActive(false);}
 if(collision.gameObject.name=="Beer(Clone)"){collision.gameObject.SetActive(false);_TrailRenderer.enabled=false;gameObject.SetActive(false);if(FindObjectOfType<EnemyHealthManager>().CurrentHealth<15){FindObjectOfType<EnemyHealthManager>().CurrentHealth++;}}
 if(collision.gameObject.name=="EnergyDrink(Clone)"){collision.gameObject.SetActive(false);_TrailRenderer.enabled=false;gameObject.SetActive(false);if(FindObjectOfType<EnemyHealthManager>().CurrentHealth<15){FindObjectOfType<EnemyHealthManager>().CurrentHealth++;}}
 if(collision.gameObject.name=="DiscoBall(Clone)"){collision.gameObject.SetActive(false);_TrailRenderer.enabled=false;gameObject.SetActive(false);if(FindObjectOfType<EnemyHealthManager>().CurrentHealth<15){FindObjectOfType<EnemyHealthManager>().CurrentHealth++;}}
 if(collision.gameObject.tag=="Player"&&collision.gameObject.GetComponent<HealthManager>()&&GameManager._SharedInstanceGameManager.PlayerCanvass==enabled){collision.gameObject.GetComponent<HealthManager>().CurrentHealth--;_TrailRenderer.enabled=false;gameObject.SetActive(false);}}
    void DesactivateThis()
    { if (GameManager._SharedInstanceGameManager.CurrentGamestate == Gamestates.RunningGame) { OFFITEMSCRONO -= Time.deltaTime; if (OFFITEMSCRONO <= 0) { gameObject.SetActive(false); OFFITEMSCRONO = 6; } } }

    private void Update()
    {if (transform.position.x<NegLimitX||transform.position.x>PosLimitX){gameObject.SetActive(false);}
    if(transform.position.y<NegLimitY||transform.position.y>PosLimitY){gameObject.SetActive(false);}
    DesactivateThis();}
    }
