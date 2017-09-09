using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AcaoBtnNovoJogo : MonoBehaviour{

	public GameObject prefabPanelVerificarJogo;
	public GameObject canvas;


	public void OnClick (){
		if (RegistroController.hasRegistroAtivo ()) {
			RectTransform panel = canvas.GetComponentInChildren<RectTransform> ();
			GameObject panelVerificarJogo = Instantiate (prefabPanelVerificarJogo);
			panelVerificarJogo.transform.SetParent (panel.transform, false);
			panelVerificarJogo.SetActive (true);
		} 
		else {
			TelaInicial.destroyTela ();
			TelaCenas.getTela ();
			SceneManager.LoadScene ("historia_inicial");
		}
	}

}

