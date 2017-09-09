using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class btnMercadoTrocaMundo : MonoBehaviour
{

	public void OnClick (){
		StartCoroutine (delayOnClick (0.7F));
	}

	private IEnumerator delayOnClick(float waitTime){
		yield return new WaitForSeconds (waitTime);
		TelaMundo.destroyTela ();
		TelaMercadoTroca.getTela ();
		ViewCarregamento.direcionadorTelaCarregamento = Parametros.TELA_MERCADO_TROCA;
		SceneManager.LoadScene("tela_carregamento");
	}
}

