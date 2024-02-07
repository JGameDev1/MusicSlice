using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{public static MusicManager MusicManagerSharedInstance;
private AudioSource MyAudioSource;
public List<AudioClip>SongsToPlay;public GameObject InterrupcionSound;
public bool MusicOn,Pause; public int SongSelected;
public bool Ingles;
public bool UseJoystick;

private void Awake(){if(MusicManager.MusicManagerSharedInstance!=null){Destroy(gameObject);}else{MusicManager.MusicManagerSharedInstance=this;DontDestroyOnLoad(gameObject);}}

void Start(){MusicManagerSharedInstance=this;DontDestroyOnLoad(gameObject);MyAudioSource=GameObject.Find("Music").GetComponent<AudioSource>();MusicOn=true;Pause=false;}

public AudioClip RequestSongs(int Number)
{if(!MyAudioSource.isPlaying&&MusicOn)
{int NumberOfTheSong=Random.Range(0,SongsToPlay.Count);
foreach (var AudioToPlay in SongsToPlay)
{MyAudioSource.PlayOneShot(SongsToPlay[NumberOfTheSong]);SongSelected=NumberOfTheSong;}}return null;}

void RequestInterruption()
{if(!MyAudioSource.isPlaying&&Pause){InterrupcionSound.gameObject.SetActive(true);}}

private void Update()
{if(MusicOn&&!Pause){RequestSongs(SongSelected);InterrupcionSound.gameObject.SetActive(false);MyAudioSource.gameObject.SetActive(true);}
if(Pause&&!MusicOn){MyAudioSource.gameObject.SetActive(false);RequestInterruption();}}

public void ContinuarIdioma(){if(Ingles==true){Ingles=true;}else{Ingles=false;}}
}
