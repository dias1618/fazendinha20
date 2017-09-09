using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AcaoBtnOpcoes : MonoBehaviour
{

	public void OnClick(){
		StartCoroutine(delayOnClick(0.7F));
	}

	private IEnumerator delayOnClick(float waitTime){
		yield return new WaitForSeconds (waitTime);

		TelaInicial.destroyTela ();
		TelaOpcoes.getTela ();
		ViewCarregamento.direcionadorTelaCarregamento = Parametros.TELA_OPCOES;
		SceneManager.LoadScene("tela_carregamento");
	}
}

