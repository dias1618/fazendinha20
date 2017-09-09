using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AcaoBtnContinuarJogo : MonoBehaviour
{
	//Função de clique no botão
	public void OnClick(){
		StartCoroutine(delayOnClick(0.7F));
	}

	//Função que irá ser chamada com um delay
	private IEnumerator delayOnClick(float waitTime){

		//O código irá ficar em delay exatamente nesse ponto pela quantidade de segundos passada
		yield return new WaitForSeconds (waitTime);

		//Faz a inicialização das classes de dados
		ApresentacaoFazendeiros.inicializarApresentacao ();
		FalasCenas.inicializarFalas ();
		MensagensTutorial.inicializarMensagens ();

		//Atualiza a data de último acesso no jogo
		Registro registro = Registro.getRegistro ();
		registro.setDtUltimoAcesso (System.DateTime.Now);
		Result resultado = registro.save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
		}

		//Destroi o objeto de tela inicial, constroi o da tela principal e chama a tela principal
		TelaInicial.destroyTela ();
		TelaPrincipal.getTela ();
		ViewCarregamento.direcionadorTelaCarregamento = Parametros.TELA_PRINCIPAL;
		SceneManager.LoadScene("tela_carregamento");

	}
}

