using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class btnVoltarMundo : MonoBehaviour
{

	public void OnClick (){
		StartCoroutine (delayOnClick (0.7F));
	}

	private IEnumerator delayOnClick(float waitTime){
		yield return new WaitForSeconds (waitTime);

		TelaMundo.destroyTela ();
		TelaPrincipal.getTela ();
		ViewCarregamento.direcionadorTelaCarregamento = Parametros.TELA_PRINCIPAL;
		SceneManager.LoadScene("tela_carregamento");
	}
}

