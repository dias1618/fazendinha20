using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogVerificarJogo : MonoBehaviour{

	public void OnClickSim (){
		TelaCenas.getTela ();
		SceneManager.LoadScene ("historia_inicial");
	}

	public void OnClickNao(){
		this.transform.parent.gameObject.SetActive (false);
	}

}

