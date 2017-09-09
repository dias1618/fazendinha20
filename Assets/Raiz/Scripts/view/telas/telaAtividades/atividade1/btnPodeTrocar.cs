using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class btnPodeTrocar : MonoBehaviour
{

	public int cdAnimalEscolhido;

	public GameObject panelPergunta3;

	public GameObject panelFazendeiro;
	public GameObject panelFecharPergunta;

	public RectTransform rectPanel;

	void Start(){
		rectPanel = Camera.allCameras[1].GetComponent<ViewAtividade1>().canvas.GetComponentInChildren<RectTransform>(); 
	}


	public void OnClick(){

		Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.setQtSegundosJogados (Camera.allCameras[1].GetComponent<ViewAtividade1>().contagemProgressiva);
		Result resultado = Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
		}

		RegistroEtapaAtividade registroEtapaAtividade = new RegistroEtapaAtividade (Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.getCdRegistroAtividade (), Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.getCdRegistro (), Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.getCdJogo (), Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.getCdAtividade (), Parametros.CD_ETAPA_ATIVIDADE_RACIOCINIO_DIRETO_2, Camera.allCameras[1].GetComponent<ViewAtividade1>().contagemProgressiva, 0);
		resultado = registroEtapaAtividade.save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
		}

		int numMercadoria = Camera.allCameras[1].GetComponent<ViewAtividade1>().numMercadoria;
		int quantSorteada = Camera.allCameras[1].GetComponent<ViewAtividade1>().qtMercadoria;
		if((cdAnimalEscolhido == (numMercadoria + 1) && quantSorteada > 1) || cdAnimalEscolhido == (numMercadoria - 1)){
			Util.hasVisible(panelPergunta3, true);

			Sprite mercadoriaEscolhida = new Sprite();
			Sprite mercadoriaEscolhidaSelecionada = new Sprite();

			switch(cdAnimalEscolhido){
				case Parametros.CD_PINTINHO:
					mercadoriaEscolhida = Camera.allCameras[1].GetComponent<ViewAtividade1>().pintinho;
					mercadoriaEscolhidaSelecionada = Camera.allCameras[1].GetComponent<ViewAtividade1>().pintinhoSelecionado;
					break;
				case Parametros.CD_GALINHA:
					mercadoriaEscolhida = Camera.allCameras[1].GetComponent<ViewAtividade1>().galinha;
					mercadoriaEscolhidaSelecionada = Camera.allCameras[1].GetComponent<ViewAtividade1>().galinhaSelecionado;
					break;
				case Parametros.CD_SACO_MILHO:
					mercadoriaEscolhida = Camera.allCameras[1].GetComponent<ViewAtividade1>().sacoMilho;
					mercadoriaEscolhidaSelecionada = Camera.allCameras[1].GetComponent<ViewAtividade1>().sacoMilhoSelecionado;
					break;
				case Parametros.CD_PORCO:
					mercadoriaEscolhida = Camera.allCameras[1].GetComponent<ViewAtividade1>().porco;
					mercadoriaEscolhidaSelecionada = Camera.allCameras[1].GetComponent<ViewAtividade1>().porcoSelecionado;
					break;
				case Parametros.CD_OVELHA:
					mercadoriaEscolhida = Camera.allCameras[1].GetComponent<ViewAtividade1>().ovelha;
					mercadoriaEscolhidaSelecionada = Camera.allCameras[1].GetComponent<ViewAtividade1>().ovelhaSelecionado;
					break;
				case Parametros.CD_CAVALO:
					mercadoriaEscolhida = Camera.allCameras[1].GetComponent<ViewAtividade1>().cavalo;
					mercadoriaEscolhidaSelecionada = Camera.allCameras[1].GetComponent<ViewAtividade1>().cavaloSelecionado;
					break;
				case Parametros.CD_VACA:
					mercadoriaEscolhida = Camera.allCameras[1].GetComponent<ViewAtividade1>().vaca;
					mercadoriaEscolhidaSelecionada = Camera.allCameras[1].GetComponent<ViewAtividade1>().vacaSelecionado;
					break;
				case Parametros.CD_LOTE_TERRA:
					mercadoriaEscolhida = Camera.allCameras[1].GetComponent<ViewAtividade1>().loteTerra;
					mercadoriaEscolhidaSelecionada = Camera.allCameras[1].GetComponent<ViewAtividade1>().loteTerraSelecionado;
					break;
			}

			for(int i = 0; i < 10; i++){
				Camera.allCameras[1].GetComponent<ViewAtividade1>().conjuntoPanelSelecionar[i].GetComponent<Image>().sprite = mercadoriaEscolhida;
				Camera.allCameras[1].GetComponent<ViewAtividade1>().conjuntoPanelSelecionar[i].GetComponent<Image>().color = Util.setColorAlpha(Camera.allCameras[1].GetComponent<ViewAtividade1>().conjuntoPanelSelecionar[i].GetComponent<Image>().color, 1);
				Camera.allCameras[1].GetComponent<ViewAtividade1>().conjuntoPanelSelecionar[i].GetComponent<Selecione>().spriteOriginal = mercadoriaEscolhida;
				Camera.allCameras[1].GetComponent<ViewAtividade1>().conjuntoPanelSelecionar[i].GetComponent<Selecione>().spriteSelecionado = mercadoriaEscolhidaSelecionada;
			}

			TelaAtividade1.getTela().setCdAnimalEscolhidoTroca(cdAnimalEscolhido);

			panelFecharPergunta = Instantiate(Camera.allCameras[1].GetComponent<ViewAtividade1>().fecharPanelSegundaPergunta);
			panelFecharPergunta.transform.SetParent(rectPanel.transform, false);

			PrototipoItem animal = new PrototipoItem (cdAnimalEscolhido);
			resultado = animal.get();
			if (resultado.getCode () < 0) {
				Debug.LogError (resultado.getMessage ());
			}

			panelFecharPergunta.GetComponentInChildren<Text>().text = "Pode trocar por " + animal.getNmPrototipoItem();

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
		else{
			panelFazendeiro = Instantiate(Camera.allCameras[1].GetComponentInChildren<ViewAtividade1>().fazendeiroErrado);
			panelFazendeiro.transform.SetParent(rectPanel.transform, false);

			panelFazendeiro.transform.FindChild("resposta").GetComponentInChildren<Text>().text = "Desafio: Quantos tem?\n\n" + Registro.getRegistro().getQtAtividadeFeita() + " vezes feito\n\n" + Registro.getRegistro().getQtAtividadeCorreta() + " respostas corretas.";

			StartCoroutine(delayOnClickFinal(3));

				
		}
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

