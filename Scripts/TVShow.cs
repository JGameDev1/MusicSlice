using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVShow : MonoBehaviour
{private GameObject DJRap,DJCheto;

void DJTOSHOW(){if(MusicManager.MusicManagerSharedInstance.SongSelected>=0&&MusicManager.MusicManagerSharedInstance.SongSelected<7){DJCheto.SetActive(false);DJRap.SetActive(true);}else if(MusicManager.MusicManagerSharedInstance.SongSelected>=7&&MusicManager.MusicManagerSharedInstance.SongSelected<=12){DJCheto.SetActive(true);DJRap.SetActive(false);}}

private void Start()
    {DJRap=GameObject.Find("DJ Rap");DJCheto=GameObject.Find("DJ Cheto");}

private void Update()
{DJTOSHOW();}

}
