using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{   [Range(0, 100)]
    public int HealthValue,CurrentHealth,CurrentArmor,Armor,HealthItemValue;
    public bool Player,Enemy,Boss,Invulnerability;
    private PlayerUI _PlayerUI;
    private PlayerController _PlayerController;

    private void Awake()
    {_PlayerUI=FindObjectOfType<PlayerUI>();}

    private void Start()
    {CurrentHealth=HealthValue;
     HealthItemValue=HealthValue-CurrentHealth;}

    private void OnCollisionEnter2D(Collision2D Other)
    {if(Other.gameObject.name == "Beer(Clone)" && Player&&_PlayerUI.Beer.enabled==true&&CurrentHealth<HealthValue){CurrentHealth++;}
    if(Other.gameObject.name == "EnergyDrink(Clone)" && Player && _PlayerUI.EnergyDrink.enabled==true&&CurrentHealth<HealthValue){CurrentHealth++;}
    if(Other.gameObject.name == "DiscoBall(Clone)" && Player && _PlayerUI.DiscoBall.enabled==true){Invulnerability=true;if(CurrentHealth<HealthValue){CurrentHealth++;}}}

private void Update()
{if(CurrentHealth>HealthValue){CurrentHealth=HealthValue;}
if(CurrentHealth<=0&&Player){GetComponent<PlayerController>().IsDeath=true;GameManager._SharedInstanceGameManager.GameOver();enabled=false;}
if(CurrentHealth<=0&&Enemy){gameObject.SetActive(false);}
if(Invulnerability&&_PlayerUI.DiscoBall.enabled==true){CurrentHealth=HealthValue;}if(GetComponent<PlayerController>().Cronometre<=0){Invulnerability=false;}}

private void OnEnable()
{CurrentHealth=HealthItemValue;}
}
