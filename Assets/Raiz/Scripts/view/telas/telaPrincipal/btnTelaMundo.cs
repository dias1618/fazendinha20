using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class btnTelaMundo : MonoBehaviour
{

	public void OnClick (){
		StartCoroutine (delayOnClick (0.7F));
	}

	private IEnumerator delayOnClick(float waitTime){
		yield return new WaitForSeconds (waitTime);
		TelaPrincipal.destroyTela ();
		TelaMundo.getTela ();
		ViewCarregamento.direcionadorTelaCarregamento = Parametros.TELA_MUNDO;
		SceneManager.LoadScene("tela_carregamento");
	}
}

