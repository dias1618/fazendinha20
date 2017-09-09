using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class btnPularTutorial : MonoBehaviour
{

	public bool pular;
	public int cdParametroTutorial;

	public GameObject view;
	public GameObject text;

	// Use this for initialization
	public void OnClick(){

		if (text.GetComponent<AutoTextTutorial> ().finishText) {
			text.GetComponent<AutoTextTutorial> ().positionFrase++;

			if (text.GetComponent<AutoTextTutorial> ().positionFrase < MensagensTutorial.arrayTexto [cdParametroTutorial].Length) {
				StartCoroutine (text.GetComponent<AutoTextTutorial> ().Ativar (MensagensTutorial.arrayTexto [cdParametroTutorial] [text.GetComponent<AutoTextTutorial> ().positionFrase]));
			} 
			else {
				view.GetComponent<View> ().liberarStart = true;
				StartCoroutine(delayOnClick(0.7F));
			}
		} 
		else {
			pular = true;
		}

	}

	public IEnumerator delayOnClick(float waitTime){
		yield return new WaitForSeconds (waitTime);
		Util.hasVisible (this.transform.parent.gameObject.transform.parent.gameObject, false);
	}

}

