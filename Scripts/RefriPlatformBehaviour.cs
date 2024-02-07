using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefriPlatformBehaviour : MonoBehaviour
{
    private CapsuleCollider2D TopCollider;
    public float MyTopHeight;
    private Animator _Animator;
    private GameObject Player;
    void TopSurfaceActivation(){if(Player.transform.position.y>MyTopHeight){TopCollider.enabled=true;}else{TopCollider.enabled=false;}}

    void Start()
    {TopCollider = GetComponent<CapsuleCollider2D>();_Animator=GetComponent<Animator>();Player=GameObject.Find("Player");}

    void LateUpdate()
    {TopSurfaceActivation();}
}
