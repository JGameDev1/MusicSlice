using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DronesMovementBehaviour : MonoBehaviour
{Rigidbody2D DronRB;
public float Speed;
public Vector2 LastPositionRegistred;
public bool IsMoving,EspumaDron;
private Animator _Animator;
private Rigidbody2D EnemyRb;
private EnemyHealthManager _HealthManager;

private void OnCollisionEnter2D(Collision2D collision)
{if(collision.gameObject.CompareTag("Player")){IsMoving=false;PhotoAnimations();}
if(collision.gameObject.CompareTag("PProjectile")){HitsAnimations();}}

void MoveAnimations(){_Animator.SetBool("IsMoving",IsMoving);_Animator.SetFloat("LastPositionRegistred",LastPositionRegistred.x);}
void HitsAnimations(){_Animator.SetTrigger("DAMAGERECEIVED");}
void PhotoAnimations(){_Animator.SetTrigger("PHOTOATTACK");}

private void OnEnable(){DronRB=GetComponent<Rigidbody2D>();EnemyRb=GetComponent<Rigidbody2D>();_HealthManager=GetComponent<EnemyHealthManager>();_Animator=GetComponent<Animator>();}

private void Update(){MoveAnimations();}

private void FixedUpdate()
{if(transform.position.y>=20&&EspumaDron){transform.position=new Vector2(transform.position.x,20);}else if(transform.position.y<=0&&EspumaDron){transform.position=new Vector2(transform.position.x,10f);}
if(transform.position.y>=20){transform.position=new Vector2(transform.position.x,20);}else if(transform.position.y<=0){transform.position=new Vector2(transform.position.x,3f);}
if(transform.position.x<=-45f){DronRB.velocity=Vector2.right*Speed;LastPositionRegistred=Vector2.right;IsMoving=true;}else if(transform.position.x>=50f){DronRB.velocity=Vector2.left*Speed;LastPositionRegistred=Vector2.left;IsMoving=true;}}
}