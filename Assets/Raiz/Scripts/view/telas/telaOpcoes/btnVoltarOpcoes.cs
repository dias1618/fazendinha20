using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class btnVoltarOpcoes : MonoBehaviour
{

	public void OnClick(){
		StartCoroutine(delayOnClick(0.7F));
	}

	private IEnumerator delayOnClick(float waitTime){
		yield return new WaitForSeconds (waitTime);

		TelaOpcoes.destroyTela ();
		TelaInicial.getTela ();
		ViewCarregamento.direcionadorTelaCarregamento = Parametros.TELA_INICIAL;
		SceneManager.LoadScene("tela_carregamento");
	}
}

