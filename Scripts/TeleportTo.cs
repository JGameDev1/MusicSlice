using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportTo : MonoBehaviour
{public string LvlName;
    private void OnTriggerEnter2D(Collider2D collision)
    {if(collision.gameObject.name=="Player"){SceneManager.LoadScene(LvlName);}}
}