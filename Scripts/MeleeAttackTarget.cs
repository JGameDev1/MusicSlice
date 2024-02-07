using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyPatrolMovement))]
public class MeleeAttackTarget : MonoBehaviour
{ public string TagToAttack;
    private Rigidbody2D EnemyRb;
    [Range(0, 100)] public float PersuitSpeed, RepulsionForce, OnAttackCronometre, LimitsOfMovementX, NegLimitsOfMovementX, LimitsOfMovementY, NegLimitsOfMovementY; private float AttackCronometre;
    public EnemyPatrolMovement _EnemyPatrolMovement;
    public bool Boss, Flyer, IsLookingAtTheRight, IsLookingAtTheLeft, Persecutioner, IsMoving;
    public Vector2 LastPositionRegistred;
    private BoxCollider2D AreaOfView;
    private Animator _Animator;


    void UpdateViewOfTheEnemy() {
        if (LastPositionRegistred.x < 0) { IsLookingAtTheLeft = true; } else { IsLookingAtTheLeft = false; }
        if (LastPositionRegistred.x > 0) { IsLookingAtTheRight = true; } else { IsLookingAtTheRight = false; }
        if (LastPositionRegistred.x == 0) { IsLookingAtTheRight = false; IsLookingAtTheLeft = false; } }

    private void Start()
    { AreaOfView = GetComponent<BoxCollider2D>(); AreaOfView.isTrigger = true;
        EnemyRb = GetComponent<Rigidbody2D>();
        AttackCronometre = OnAttackCronometre;
        _EnemyPatrolMovement = GetComponent<EnemyPatrolMovement>();
        _Animator=GetComponent<Animator>();

        LimitsOfMovementX =_EnemyPatrolMovement.LimitsOfMovementX;
        NegLimitsOfMovementX =_EnemyPatrolMovement.NegLimitsOfMovementX;
        LimitsOfMovementY =_EnemyPatrolMovement.LimitsOfMovementY;
        NegLimitsOfMovementY =_EnemyPatrolMovement.NegLimitsOfMovementY;
    }

    private void OnTriggerEnter2D(Collider2D Other)
    { if (Other.gameObject.tag == TagToAttack && Flyer)
        { AttackCronometre = OnAttackCronometre; _EnemyPatrolMovement.enabled = false; EnemyRb.velocity = (Other.transform.position - transform.position).normalized * PersuitSpeed; }
        else if (Other.gameObject.tag == TagToAttack && !Flyer)
        { _EnemyPatrolMovement.enabled = false; EnemyRb.velocity = new Vector2(Other.transform.position.x - transform.position.x, transform.position.y).normalized * PersuitSpeed; }
    }

    private void OnTriggerStay2D(Collider2D Other)
    { if (Other.gameObject.tag == TagToAttack && Flyer && Persecutioner) { _EnemyPatrolMovement.enabled = false; IsMoving=true; EnemyRb.velocity = (Other.transform.position - transform.position).normalized * PersuitSpeed; } }

    private void OnTriggerExit2D(Collider2D Other)
    { if (Other.gameObject.tag == TagToAttack) {EnemyRb.velocity = (Other.transform.position - transform.position).normalized * PersuitSpeed; _EnemyPatrolMovement.enabled = true; IsMoving = false; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { if (collision.gameObject.CompareTag(TagToAttack)) { collision.gameObject.GetComponent<Rigidbody2D>().AddForce((transform.position - collision.transform.position).normalized * Time.fixedDeltaTime * RepulsionForce, ForceMode2D.Impulse); } }

    void MoveAnimations(){_Animator.SetBool("IsMoving",IsMoving);_Animator.SetBool("IsLookingAtTheLeft",IsLookingAtTheLeft);
    _Animator.SetBool("IsLookingAtTheRight",IsLookingAtTheRight);_Animator.SetFloat("LastPositionRegistred",LastPositionRegistred.x);}
    void HitsAnimations(){_Animator.SetTrigger("DAMAGERECEIVED");}

    void DontCrossTheLimits()
    {   if (transform.position.x >= LimitsOfMovementX) { transform.position = new Vector3(LimitsOfMovementX, transform.position.y, transform.position.z); }
        if (transform.position.x <= NegLimitsOfMovementX) { transform.position = new Vector3(NegLimitsOfMovementX, transform.position.y, transform.position.z); }
        if (transform.position.y >= LimitsOfMovementY) { transform.position = new Vector3(transform.position.x, LimitsOfMovementY, transform.position.z); }
        if (transform.position.y <= NegLimitsOfMovementY) { transform.position = new Vector3(transform.position.x, NegLimitsOfMovementY, transform.position.z); } }

private void Update(){LastPositionRegistred=EnemyRb.velocity.normalized;UpdateViewOfTheEnemy();DontCrossTheLimits();MoveAnimations();HitsAnimations();}
}
