using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class btnRefreshJogoMemoriaFinal : MonoBehaviour
{

	//O jogo voltara para o jogo da memoria
	public void OnClick(){
		StartCoroutine(delayOnClick(0.7F));

	}

	private IEnumerator delayOnClick(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);

		TelaJogoMemoriaFinal.destroyTela ();
		TelaJogoMemoria.getTela ();
		ViewCarregamento.direcionadorTelaCarregamento = Parametros.TELA_JOGO_MEMORIA;
		SceneManager.LoadScene ("tela_carregamento");
	}
}

