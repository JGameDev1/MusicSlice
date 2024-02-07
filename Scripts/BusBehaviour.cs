using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusBehaviour : MonoBehaviour
{Animator _BusAnimator;
 MusicManager _MusicManager;
 void FiestaOn(){_BusAnimator.SetBool("FiestaOn",_MusicManager.MusicOn);}

    void Start()
{_BusAnimator=GetComponent<Animator>();_MusicManager=FindObjectOfType<MusicManager>();}

    void Update()
    {FiestaOn();}
}
