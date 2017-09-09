using UnityEngine;
using System.Collections;

public class MouseControl : MonoBehaviour{

	public AudioSource prefabAudioClick;
	public AudioSource prefabAudioFocus;

	private AudioSource audioClick;
	private AudioSource audioFocus;

	public void Start(){
		if (prefabAudioClick != null) {
			audioClick = Instantiate (prefabAudioClick);
			audioClick.volume *= (float)Jogo.getConfiguracao ().getVlVolume ();
		}
		if (prefabAudioFocus != null) {
			audioFocus = Instantiate (prefabAudioFocus);
			audioFocus.volume *= (float)Jogo.getConfiguracao ().getVlVolume ();
		}
	}

	public void OnMouseClick(){
		if (audioClick != null) {
			audioClick.Play ();
			StartCoroutine (delayOnMouseClick(0.7F));
		}
	}

	private IEnumerator delayOnMouseClick(float waitTime){
		yield return new WaitForSeconds (waitTime);
		audioClick.Stop ();
	}

	public void OnMouseEnter(){
		if (audioFocus != null) {
			audioFocus.Play();
		}
	}

	public void OnMouseExit(){
		if (audioFocus != null) {
			audioFocus.Stop();
		}
	}
}

