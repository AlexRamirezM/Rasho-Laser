using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Server : MonoBehaviour {

	public bool useNat = false;
	public int numUsarios = 16;

	public InputField Puerto;
	public InputField IP;
	public InputField NombreUsuario;
	public Toggle Chattoggle;
	public Text Datosdeusuario;
	//Guis
	public GameObject GuiOffline;
	public GameObject GuiChat;
	public GameObject IngameGuiMenu;
	public GameObject BotonIngameGuiMenu;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	public void CrearServidor(){
		if(Network.peerType == NetworkPeerType.Disconnected){
			if(Puerto.text != ""){
				Network.InitializeServer(numUsarios, int.Parse(Puerto.text), useNat);
				foreach(GameObject go in FindObjectsOfType(typeof(GameObject)) as GameObject[]){
					go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
				}
			}else{
				print ("Falta Puerto");
			}
		}

	}
	private void OnServerInitialized(){
		print ("Servidor iniciado correctamente");
//		GuiOffline.SetActive (false);
//		if (NombreUsuario.text != "")
//			Variables.Username = NombreUsuario.text;
//		else
//			Variables.Username = "Servidor";
//
//		GuiChat.SetActive (true);
//		BotonIngameGuiMenu.SetActive (true);
	}


	private void OnConnectedToServer(){
		foreach(GameObject go in FindObjectsOfType(typeof(GameObject)) as GameObject[]){
			go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
		}
		print ("Coneccion Correcta");
//		GuiOffline.SetActive (false);
//		if (NombreUsuario.text != "")
//			Variables.Username = NombreUsuario.text;
//		else
//			Variables.Username = "Usuario: " + Network.time;
//		GuiChat.SetActive (true);
//		BotonIngameGuiMenu.SetActive (true);
		}

	public void Conecctarse(){
		if (Network.peerType == NetworkPeerType.Disconnected)
			Network.Connect (IP.text, int.Parse(Puerto.text));
	}

//	void Update(){
//		GuiChat.SetActive (Chattoggle.isOn);
//		IngameGuiMenu.SetActive (BotonIngameGuiMenu.GetComponent<Toggle>().isOn);
//		Datosdeusuario.text = "<color=red><b>Datos de usuario</b></color> \nIP: " + Network.player.ipAddress + ":" + Network.player.port + "\nIP Global: " + Network.player.externalIP + ":" + Network.player.externalPort+"\nNombre de usuario: "+ Variables.Username;
//	}
	public void Desconectar(){
		if(Network.peerType != NetworkPeerType.Disconnected)
			Network.Disconnect (500);
	}

//	void OnDisconnectedFromServer(NetworkDisconnection info) {
//		GuiOffline.SetActive (true);
//		Variables.Username = "";
//		GuiChat.SetActive (false);
//		BotonIngameGuiMenu.SetActive (false);
//		Chattoggle.isOn = false;
//		BotonIngameGuiMenu.GetComponent<Toggle> ().isOn = false;
//		IngameGuiMenu.SetActive (false);
//
//		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Player")){
//			Destroy(go);
//		}
//
//	}


}
