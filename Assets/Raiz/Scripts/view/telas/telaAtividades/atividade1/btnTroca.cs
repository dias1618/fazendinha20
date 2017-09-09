using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class btnTroca : MonoBehaviour
{


	public GameObject panelPergunta2;

	public GameObject panelSobra;

	public GameObject panelButtonsSelecionar;

	public GameObject panelFazendeiro;
	public GameObject panelFecharPergunta;

	public RectTransform rectPanel;

	private RegistroEtapaAtividade registroEtapaAtividade;

	void Start(){
		rectPanel = Camera.allCameras[1].GetComponent<ViewAtividade1>().canvas.GetComponentInChildren<RectTransform>(); 
	}

	public void OnClick () {

		int numMercadoria = Camera.allCameras[1].GetComponent<ViewAtividade1>().numMercadoria;
		int quantSorteada = Camera.allCameras[1].GetComponent<ViewAtividade1>().qtMercadoria;

		int cdAnimalEscolhido = TelaAtividade1.getTela().getCdAnimalEscolhidoTroca();

		int quantAnimaisSelecionados = 0;
		foreach(Transform child in panelButtonsSelecionar.transform){
			if(child.gameObject.GetComponent<Selecione>().selecionado)
				quantAnimaisSelecionados++;
		}

		Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.setQtSegundosJogados (Camera.allCameras[1].GetComponent<ViewAtividade1>().contagemProgressiva);
		Result resultado = Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
		}

		registroEtapaAtividade = new RegistroEtapaAtividade (Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.getCdRegistroAtividade (), Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.getCdRegistro (), Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.getCdJogo (), Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.getCdAtividade (), Parametros.CD_ETAPA_ATIVIDADE_RACIOCINIO_DIRETO_3, Camera.allCameras[1].GetComponent<ViewAtividade1>().contagemProgressiva, 0);
		resultado = registroEtapaAtividade.save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
		}

		int valorAnimaisJogador = (int)(Mathf.Pow(2, numMercadoria) * quantSorteada);
		int valorAnimaisComputador = (int)(Mathf.Pow(2, cdAnimalEscolhido) * quantAnimaisSelecionados);

		//Caso o jogador tenha colocado a proporçao correta entre animais para se trocar
		if(valorAnimaisJogador == valorAnimaisComputador && 
			valorAnimaisJogador != 0 && 
			valorAnimaisComputador != 0){
			TelaAtividade1.getTela().setValorTrocaSorteio(valorAnimaisJogador);
			TelaAtividade1.getTela().setValorTrocaEscolhido(valorAnimaisComputador);
			TelaAtividade1.getTela().setSobrou(0);
			Util.hasVisible(panelSobra, true);

			acertou(cdAnimalEscolhido, quantAnimaisSelecionados);
		}
		else{
			valorAnimaisJogador = (int) (Mathf.Pow(2, numMercadoria) * (quantSorteada-1));
			if(valorAnimaisJogador == valorAnimaisComputador && 
				valorAnimaisJogador != 0 && 
				valorAnimaisComputador != 0 &&
				numMercadoria < cdAnimalEscolhido){
				TelaAtividade1.getTela().setValorTrocaSorteio(valorAnimaisJogador);
				TelaAtividade1.getTela().setValorTrocaEscolhido(valorAnimaisComputador);
				TelaAtividade1.getTela().setSobrou(1);
				Util.hasVisible(panelSobra, true);

				acertou(cdAnimalEscolhido, quantAnimaisSelecionados);
			}
			else{
				panelFazendeiro = Instantiate(Camera.allCameras[1].GetComponentInChildren<ViewAtividade1>().fazendeiroErrado);
				panelFazendeiro.transform.SetParent(rectPanel.transform, false);

				panelFazendeiro.transform.FindChild("resposta").GetComponentInChildren<Text>().text = "Desafio: Quantos tem?\n\n" + Registro.getRegistro().getQtAtividadeFeita() + " vezes feito\n\n" + Registro.getRegistro().getQtAtividadeCorreta() + " respostas corretas.";

				StartCoroutine(delayOnClickFinal(3));
			}
		}

	}


	private void acertou(int cdAnimalEscolhido, int quantAnimaisSelecionados){
		panelFecharPergunta = Instantiate(Camera.allCameras[1].GetComponent<ViewAtividade1>().fecharPanelTerceiraPergunta);
		panelFecharPergunta.transform.SetParent(rectPanel.transform, false);

		PrototipoItem animal = new PrototipoItem (cdAnimalEscolhido);
		Result resultado = animal.get();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
		}

		panelFecharPergunta.GetComponentInChildren<Text>().text = "Trocado por " + quantAnimaisSelecionados + " " + animal.getNmPrototipoItem();


		panelFazendeiro = Instantiate(Camera.allCameras[1].GetComponent<ViewAtividade1>().fazendeiroCorreto);
		panelFazendeiro.transform.SetParent(rectPanel.transform, false);


		registroEtapaAtividade.setLgFinalizada(1);
		resultado = registroEtapaAtividade.save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
		}

		panelFazendeiro.GetComponent<AudioSource>().volume *= (float) Jogo.getConfiguracao().getVlVolume();
		panelFazendeiro.GetComponent<AudioSource>().Play();


		StartCoroutine(delayOnClick(3));
	}

	private IEnumerator delayOnClick(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);

		panelFazendeiro.GetComponentInChildren<AudioSource>().Stop();
		panelFazendeiro.SetActive(false);


	}

	private IEnumerator delayOnClickFinal(float waitTime){
		yield return new WaitForSeconds (waitTime);

		TelaAtividade1.destroyTela();
		SceneManager.UnloadSceneAsync ("tela_atividade1");
	}



}

