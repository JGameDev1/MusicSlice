using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuCanvassController : MonoBehaviour
{public Canvas Principal,Instrucciones,Creditos,ControlSelector,CanvassToHide;
public Button BotonJugar,BotonInstrucciones,BotonIdioma,BotonLanguaje,BotonCreditos,BotonDonar,BotonRegresar1,BotonRegresar2,BottonParaJoystick,BottonParaBotones;
public Text TextoJugar,TextoInstrucciones,TextoIdioma,TextoCreditos,GloboDeDialogoPrincipal,GloboDeInstrucciones,PizarraB,PizarraE,PizarraC;
public bool Ingles;
public string LvlName;

void Start()
{Principal.enabled=true;Instrucciones.enabled=false;Creditos.enabled=false;BotonLanguaje.gameObject.SetActive(false);}

public void BotonJugarFunction(){SceneManager.LoadScene(LvlName);}
public void BotonInstruccionesFunction(){Principal.enabled=false;Instrucciones.enabled=true;Creditos.enabled=false;}
public void EnIngles(){if(Ingles==false){Ingles=true;}if(Ingles==true){TextoJugar.text="Play";TextoInstrucciones.text="Instructions";TextoCreditos.text="Credits";GloboDeDialogoPrincipal.text="Welcome to LIFE CLUB, enjoy the trip to the best party that you ever have in your life.\r\n¡AND DEFEND YOUR SITE!";GloboDeDialogoPrincipal.fontSize=40;GloboDeInstrucciones.text="Do not leave them to hit you, move pressing the buttons and defeat dancers using your stars meanwhile you dance. Take items that may can help you... or not.";GloboDeInstrucciones.fontSize=34;PizarraB.text="+Velocity\r\nInvulnerability";PizarraC.text="-Velocity";PizarraE.text="+Velocity";BotonLanguaje.gameObject.SetActive(true);BotonIdioma.gameObject.SetActive(false);}}
public void EnEspañol(){if(Ingles==true){Ingles=false;}if(Ingles==false){TextoJugar.text="Jugar";TextoIdioma.text="Idioma";TextoInstrucciones.text="Instructiones";TextoCreditos.text="Creditos";GloboDeDialogoPrincipal.text="Bienvenido a LIFE CLUB, disfruta del viaje a la mejor fiesta de tu vida.\r\n¡DEFIENDE TU LUGAR!";GloboDeDialogoPrincipal.fontSize=40;GloboDeInstrucciones.text="No dejes que te golpeen, muevete pulsando los botones y vence bailarines arrojando estrellas mientras bailas. Recoge items que podrían ayudarte... o no.";GloboDeInstrucciones.fontSize=37;PizarraB.text="+Velocidad\r\nInvulnerabilidad";PizarraC.text="-Velocidad";PizarraE.text="+Velocidad";BotonIdioma.gameObject.SetActive(true);BotonLanguaje.gameObject.SetActive(false);}}
public void QuitarApp(){Application.Quit();}
public void BotonCreditosFunction(){Principal.enabled=false;Instrucciones.enabled=false;Creditos.enabled=true;}
public void AccesToURL(string Site){Application.OpenURL(Site);}
public void BotonRegresarFunction(){Principal.enabled=true;Instrucciones.enabled=false;Creditos.enabled=false;}
public void SeleccionarJoystick(){FindObjectOfType<MusicManager>().UseJoystick=true;ControlSelector.enabled=false;}
public void SeleccionarBotones(){FindObjectOfType<MusicManager>().UseJoystick=false;ControlSelector.enabled=false;}
public void HideCanvass(){CanvassToHide.enabled=false;}

public void DesactivaElBoton(GameObject BotonASacar){BotonASacar.SetActive(false);}
}
