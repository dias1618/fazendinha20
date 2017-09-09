using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class btnMinhaFazendaMundo : MonoBehaviour
{

	public void OnClick (){
		StartCoroutine (delayOnClick (0.7F));
	}

	private IEnumerator delayOnClick(float waitTime){
		yield return new WaitForSeconds (waitTime);
		TelaMundo.destroyTela ();
		TelaMinhaFazenda.getTela ();
		ViewCarregamento.direcionadorTelaCarregamento = Parametros.TELA_MINHA_FAZENDA;
		SceneManager.LoadScene("tela_carregamento");
	}
}

