using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{  [Range(0, 100)]
    public int HealthValue,CurrentHealth,ScoreValue;
    public bool NormalEnemy;
    public float DeathCrono,OnDeathCrono;

    private void Start()
    {CurrentHealth=HealthValue;
    OnDeathCrono=5f;
    DeathCrono=OnDeathCrono;}

    private void OnCollisionEnter2D(Collision2D Other)
    {if(Other.gameObject.tag=="PowerUps"){CurrentHealth+=1;Other.gameObject.SetActive(false);}}

    private void OnEnable()
    {if(!NormalEnemy){HealthValue+=2;}
    CurrentHealth=HealthValue;
    DeathCrono=OnDeathCrono;}

    private void Update()
{if(NormalEnemy){if(CurrentHealth>=HealthValue){CurrentHealth=HealthValue;}if(CurrentHealth<=0){GameManager._SharedInstanceGameManager.Score+=ScoreValue;gameObject.SetActive(false);}}else{if(CurrentHealth<=0&&!NormalEnemy){DeathCrono-=Time.deltaTime;if(DeathCrono<=0){GameManager._SharedInstanceGameManager.Score+=ScoreValue;gameObject.SetActive(false);}}}}
}