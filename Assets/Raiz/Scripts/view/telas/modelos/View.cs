using UnityEngine;
using System.Collections;

public class View : MonoBehaviour
{
	public AudioSource prefabMusicaFundo;
	public GameObject canvas;
	public GameObject prefabPanelTutorial;

	private AudioSource musicaFundo;
	private bool contarTempo;

	public bool liberarStart = false;

	// Use this for initialization
	public void start (bool contarTempo, int cdTutorial){
		if (prefabMusicaFundo != null) {
			setMusicaFundo (Instantiate (prefabMusicaFundo));
			getMusicaFundo ().volume *= (float)Jogo.getConfiguracao ().getVlVolume ();
		}
		this.contarTempo = contarTempo;


		RegistroTutorial registroTutorial = Registro.getRegistro ().getRegistroTutorial (cdTutorial);
		if (registroTutorial != null && registroTutorial.getLgExecutado () == 0) {

			RectTransform rectPanel = canvas.GetComponentInChildren<RectTransform> (); 
			GameObject panelTutorial = Instantiate (prefabPanelTutorial);
			panelTutorial.transform.SetParent (rectPanel.transform, false);
			panelTutorial.GetComponentInChildren<btnPularTutorial> ().cdParametroTutorial = cdTutorial;
			panelTutorial.GetComponentInChildren<btnPularTutorial> ().view = this.gameObject;

			registroTutorial.setLgExecutado (1);
			Result resultado = registroTutorial.save ();
			if (resultado.getCode () < 0) {
				Debug.LogError (resultado.getMessage ());
			}
		} 
		else {
			liberarStart = true;
		}
	}
	
	// Update is called once per frame
	public void update (){
		if (contarTempo) {
			Util.guardarSegundosJogados();
		}
	}


	public AudioSource getMusicaFundo(){
		return this.musicaFundo;
	}

	public void setMusicaFundo(AudioSource musicaFundo){
		this.musicaFundo = musicaFundo;
	}

}

