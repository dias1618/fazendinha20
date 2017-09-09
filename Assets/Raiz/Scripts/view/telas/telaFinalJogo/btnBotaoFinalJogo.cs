using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class btnBotaoFinalJogo : MonoBehaviour{

	public GameObject panel;

	// Use this for initialization
	public void OnClick (){
		StartCoroutine (delayOnClick (0.7F));
	}

	private IEnumerator delayOnClick(float waitTime){

		yield return new WaitForSeconds (waitTime);

		if (panel != null) {
			Util.hasVisible (panel, true);
			Util.hasVisible (this.transform.parent.gameObject, false);
		}

	}

	public void OnClickSaida(){
		StartCoroutine (delayOnClickSaida (0.7F));
	}

	private IEnumerator delayOnClickSaida(float waitTime){
		yield return new WaitForSeconds (waitTime);

		SceneManager.LoadScene("tela_inicial");
	}
}

