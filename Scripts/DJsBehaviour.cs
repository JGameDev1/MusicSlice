using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJsBehaviour : MonoBehaviour
{public PlayerController _PlayerController;
 public Animator _Animator;

    void MusicOnAnimation(){_Animator.SetBool("MusicOn",GameManager._SharedInstanceGameManager.MusicOn);_Animator.SetFloat("PlayerPosition",_PlayerController.gameObject.transform.position.x);}

    void Start()
    {_Animator=GetComponent<Animator>();_PlayerController=FindObjectOfType<PlayerController>();
    }

    void Update()
    {MusicOnAnimation();}
}
