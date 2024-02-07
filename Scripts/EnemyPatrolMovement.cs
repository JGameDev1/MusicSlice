using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyPatrolMovement : MonoBehaviour
{   [Range(0, 100)]
    public int DamageValue; public int TopH, ButtomH, LeftMove, RightMove;
    public Rigidbody2D EnemyRb;
    public Vector2 LastPositionRegistred=Vector2.right;
    public float EnemySpeed,OnMoveCronometre,ReinitializeCronometreIn;
    private float MoveCronometre;
    public bool IsLookingAtTheRight,IsLookingAtTheLeft,IsMoving, NoKillEnemy;
    public Animator _Animator;
    public EnemyHealthManager _HealthManager;
    public float LimitsOfMovementX, NegLimitsOfMovementX,LimitsOfMovementY,NegLimitsOfMovementY;
    
    void MovementConf()
    {MoveCronometre-=Time.deltaTime;
    if(MoveCronometre>0){EnemyRb.velocity=LastPositionRegistred*EnemySpeed;IsMoving=true;}
    else{EnemyRb.velocity=Vector2.zero*EnemySpeed;IsMoving=false;}
    if(MoveCronometre<=ReinitializeCronometreIn){MoveCronometre=OnMoveCronometre;int INDEXY=Random.Range(ButtomH,TopH),INDEXX=Random.Range(LeftMove,RightMove);LastPositionRegistred=new Vector2(INDEXX,INDEXY);}
    if(LastPositionRegistred.x==0){LastPositionRegistred.x=-1;}
    if(FindObjectOfType<PlayerController>().gameObject.transform.position.y<=0){LastPositionRegistred=new Vector2(FindObjectOfType<PlayerController>().gameObject.transform.position.x-transform.position.x,LastPositionRegistred.y).normalized;}}

    void DontCrossTheLimits() 
    {if(transform.position.x>= LimitsOfMovementX){transform.position=new Vector3(LimitsOfMovementX, transform.position.y,transform.position.z);}
    if (transform.position.x<= NegLimitsOfMovementX){transform.position = new Vector3(NegLimitsOfMovementX, transform.position.y, transform.position.z);}
    if (transform.position.y>= LimitsOfMovementY){transform.position = new Vector3(transform.position.x, LimitsOfMovementY, transform.position.z); }
    if (transform.position.y<= NegLimitsOfMovementY){transform.position = new Vector3(transform.position.x, NegLimitsOfMovementY, transform.position.z);}}
    
    void UpdateViewOfEnemy()
    {if (LastPositionRegistred.x <= -1) {IsLookingAtTheLeft=true;}else{IsLookingAtTheLeft=false;}
     if (LastPositionRegistred.x >= 1) {IsLookingAtTheRight=true;}else{IsLookingAtTheRight=false;}
     if (LastPositionRegistred.x == 0) {IsLookingAtTheRight=false;IsLookingAtTheLeft=false;}}

    private void OnCollisionEnter2D(Collision2D collision)
{if(collision.gameObject.CompareTag("Player")){collision.gameObject.GetComponent<HealthManager>().CurrentHealth-=DamageValue;}
if(collision.gameObject.CompareTag("PProjectile")){HitsAnimations();}}


    void MoveAnimations(){_Animator.SetBool("IsMoving",IsMoving);_Animator.SetFloat("LastPositionRegistred",LastPositionRegistred.x);}
    void HitsAnimations(){_Animator.SetTrigger("DAMAGERECEIVED");}

    private void OnEnable()
    {MoveCronometre=OnMoveCronometre;}

    private void Start()
    {EnemyRb=GetComponent<Rigidbody2D>();_HealthManager=GetComponent<EnemyHealthManager>();_Animator=GetComponent<Animator>();}

    private void FixedUpdate()
    {if(GameManager._SharedInstanceGameManager.CurrentGamestate==Gamestates.RunningGame){MovementConf();}else if(GameManager._SharedInstanceGameManager.CurrentGamestate==Gamestates.PauseTheGame){IsMoving=false;EnemyRb.velocity=Vector2.zero;}}

    private void Update()
{UpdateViewOfEnemy();DontCrossTheLimits();MoveAnimations();if(NoKillEnemy&&MoveCronometre<=-2){gameObject.SetActive(false);}}
}