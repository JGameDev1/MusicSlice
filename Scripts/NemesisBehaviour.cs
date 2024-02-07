using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NemesisBehaviour : MonoBehaviour
{   [Range(0, 100)]
    public int DamageValue; public int Ammo,TopH, ButtomH, LeftMove, RightMove;
    public Rigidbody2D EnemyRb;
    public Vector2 LastPositionRegistred=Vector2.right;
    public float EnemySpeed,OnMoveCronometre,ReinitializeCronometreIn;
    private float MoveCronometre;
    public bool IsLookingAtTheRight,IsLookingAtTheLeft,IsMoving,NoKillEnemy,IsDeath;
    public Animator _Animator;
    public EnemyHealthManager _HealthManager;
    public float LimitsOfMovementX,NegLimitsOfMovementX,LimitsOfMovementY,NegLimitsOfMovementY,ShotCronometre;
    public List<GameObject> DarkProjectiles; public GameObject DarkStar;
    private AudioSource _AudioSource;
    public List<AudioClip>FraseDeDerrota;public List<AudioClip>FraseDeEntrada;public List<AudioClip>PhraseOfShame;public List<AudioClip>PhraseOfEnter;


    void MovementConf()
    {if(IsDeath==false){MoveCronometre-=Time.deltaTime;
    if(MoveCronometre>0){EnemyRb.velocity=LastPositionRegistred*EnemySpeed;IsMoving=true;NoKillEnemy=false;}
    else{EnemyRb.velocity=Vector2.zero*EnemySpeed;IsMoving=false;NoKillEnemy=true;}
    if(MoveCronometre<=ReinitializeCronometreIn){MoveCronometre=OnMoveCronometre;int INDEXY=Random.Range(ButtomH,TopH),INDEXX=Random.Range(LeftMove,RightMove);LastPositionRegistred=new Vector2(INDEXX,INDEXY);}
    if(LastPositionRegistred.x==0){LastPositionRegistred.x=-1;}
    if(FindObjectOfType<PlayerController>().gameObject.transform.position.y<=5){LastPositionRegistred=new Vector2(FindObjectOfType<PlayerController>().gameObject.transform.position.x-transform.position.x,FindObjectOfType<PlayerController>().gameObject.transform.position.y-transform.position.y).normalized;}}}

    void DontCrossTheLimits() 
    {if(transform.position.x>= LimitsOfMovementX){transform.position=new Vector3(LimitsOfMovementX, transform.position.y,transform.position.z);}
    if (transform.position.x<= NegLimitsOfMovementX){transform.position = new Vector3(NegLimitsOfMovementX, transform.position.y, transform.position.z);}
    if (transform.position.y>= LimitsOfMovementY){transform.position = new Vector3(transform.position.x, LimitsOfMovementY, transform.position.z); }
    if (transform.position.y<= NegLimitsOfMovementY){transform.position = new Vector3(transform.position.x, NegLimitsOfMovementY, transform.position.z);}}
    
    void UpdateViewOfEnemy()
    {if (LastPositionRegistred.x <= -1) {IsLookingAtTheLeft=true;}else{IsLookingAtTheLeft=false;}
     if (LastPositionRegistred.x >= 1) {IsLookingAtTheRight=true;}else{IsLookingAtTheRight=false;}
     if (LastPositionRegistred.x == 0) {IsLookingAtTheRight=false;IsLookingAtTheLeft=false;}}

     void CreationOfBullets()
    {for (int i=0;i<Ammo;i++)
    {GameObject S=Instantiate(DarkStar);
    S.SetActive(false);
    DarkProjectiles.Add(S);}}

    void DarkStarComprobation(){for(int i=0;i<DarkProjectiles.Count;i++)if(DarkProjectiles[i]==null){GameObject DarkRespuesto=Instantiate(DarkStar);DarkRespuesto.SetActive(false);DarkProjectiles[i]=DarkRespuesto;}}

    public GameObject RequestDarkStar()
    {for(int i=0;i<DarkProjectiles.Count;i++)
    {if(!DarkProjectiles[i].activeSelf)
    {DarkProjectiles[i].SetActive(true);DarkProjectiles[i].transform.position=transform.position+new Vector3(LastPositionRegistred.x,0,0);
    return DarkProjectiles[i];}}
    return DarkProjectiles[0];}

    void ShotStar()
 {if(GameManager._SharedInstanceGameManager.CurrentGamestate==Gamestates.RunningGame&&!IsDeath){ShotCronometre-=Time.deltaTime;if(ShotCronometre<=0){RequestDarkStar();ShotCronometre=Random.Range(0,6);}}}

private void OnCollisionEnter2D(Collision2D collision)
{if(collision.gameObject.CompareTag("Player")){collision.gameObject.GetComponent<HealthManager>().CurrentHealth-=DamageValue;}
if (collision.gameObject.CompareTag("PProjectile")||collision.gameObject.CompareTag("Player")){HitsAnimations();}}

    void MoveAnimations(){_Animator.SetBool("IsMoving",IsMoving);_Animator.SetFloat("LastPositionRegistred",LastPositionRegistred.x);}
    void HitsAnimations(){_Animator.SetTrigger("DAMAGERECEIVED");}
    void DeathAnimation(){if(_HealthManager.CurrentHealth<=0){IsDeath=true;IsMoving=false;EnemyRb.velocity=Vector2.down;_Animator.SetBool("IsDeath",IsDeath);}}

    private void OnEnable()
    {_AudioSource=GetComponent<AudioSource>();MoveCronometre=OnMoveCronometre;FindObjectOfType<PrefabRespawnManager>().BossMoment=true;ShotCronometre=3;_HealthManager.CurrentHealth=15;IsDeath=false;IsMoving=true;
    if(FindObjectOfType<PlayerUI>().Ingles==false){_AudioSource.PlayOneShot(FraseDeEntrada[Random.Range(0,3)]);}else if(FindObjectOfType<PlayerUI>().Ingles){_AudioSource.PlayOneShot(PhraseOfEnter[Random.Range(0,3)]);}}

    private void Start()
    {_AudioSource=GetComponent<AudioSource>();EnemyRb=GetComponent<Rigidbody2D>();_HealthManager=GetComponent<EnemyHealthManager>();_Animator=GetComponent<Animator>();CreationOfBullets();}

    private void FixedUpdate()
    {if(GameManager._SharedInstanceGameManager.CurrentGamestate==Gamestates.RunningGame){MovementConf();}else if(GameManager._SharedInstanceGameManager.CurrentGamestate==Gamestates.PauseTheGame){IsMoving=false;EnemyRb.velocity=Vector2.zero;}}

    private void Update()
{DarkStarComprobation();UpdateViewOfEnemy();DontCrossTheLimits();MoveAnimations();DeathAnimation();ShotStar();
if(FindObjectOfType<PlayerUI>().Ingles==false&&IsDeath&&_HealthManager.DeathCrono==_HealthManager.OnDeathCrono){_AudioSource.PlayOneShot(FraseDeDerrota[Random.Range(0,4)]);FindObjectOfType<PrefabRespawnManager>().BossMoment=false;}else if(FindObjectOfType<PlayerUI>().Ingles&&IsDeath&&_HealthManager.DeathCrono==_HealthManager.OnDeathCrono){_AudioSource.PlayOneShot(PhraseOfShame[Random.Range(0,5)]);FindObjectOfType<PrefabRespawnManager>().BossMoment=false;}}

}
