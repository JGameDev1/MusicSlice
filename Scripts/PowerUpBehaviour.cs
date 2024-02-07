using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    private float OFFITEMSCRONO;

    private void Awake()
    {
        OFFITEMSCRONO=15;
    }

    void DesactivatePowerUps()
    { if (GameManager._SharedInstanceGameManager.CurrentGamestate == Gamestates.RunningGame) { OFFITEMSCRONO -= Time.deltaTime; if (OFFITEMSCRONO <= 0) { gameObject.SetActive(false); OFFITEMSCRONO = 15; } } }

    void Update()
    {DesactivatePowerUps();}
}
