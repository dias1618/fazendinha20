using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class btnSairPrincipal : MonoBehaviour
{

	public void OnClick (){
		StartCoroutine (delayOnClick (0.7F));
	}

	private IEnumerator delayOnClick(float waitTime){
		yield return new WaitForSeconds (waitTime);
		TelaPrincipal.destroyTela ();
		TelaInicial.getTela ();
		ViewCarregamento.direcionadorTelaCarregamento = Parametros.TELA_INICIAL;
		SceneManager.LoadScene("tela_carregamento");
	}
}

