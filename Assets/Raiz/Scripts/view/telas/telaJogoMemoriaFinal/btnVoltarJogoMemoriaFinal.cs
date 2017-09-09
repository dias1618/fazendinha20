using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class btnVoltarJogoMemoriaFinal : MonoBehaviour
{

	//O jogo voltara para o menu principal
	public void OnClick(){
		StartCoroutine(delayOnClick(0.7F));

	}

	private IEnumerator delayOnClick(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);

		TelaJogoMemoriaFinal.destroyTela ();
		TelaPrincipal.getTela ();
		ViewCarregamento.direcionadorTelaCarregamento = Parametros.TELA_PRINCIPAL;
		SceneManager.LoadScene ("tela_carregamento");
	}
}

