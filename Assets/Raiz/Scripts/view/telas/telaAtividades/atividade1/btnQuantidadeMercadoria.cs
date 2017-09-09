using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class btnQuantidadeMercadoria : MonoBehaviour
{

	public int quantMercadoria;

	public GameObject panelPergunta2;

	public GameObject panelFazendeiro;
	public GameObject panelFecharPergunta;

	public RectTransform rectPanel;

	void Start(){
		rectPanel = Camera.allCameras[1].GetComponent<ViewAtividade1>().canvas.GetComponentInChildren<RectTransform>(); 
	}

	public void OnClickMercadoria () {

		int quantSorteada = Camera.allCameras[1].GetComponent<ViewAtividade1>().qtMercadoria;

		Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.setQtSegundosJogados (Camera.allCameras[1].GetComponent<ViewAtividade1>().contagemProgressiva);
		Result resultado = Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
		}

		RegistroEtapaAtividade registroEtapaAtividade = new RegistroEtapaAtividade (Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.getCdRegistroAtividade (), Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.getCdRegistro (), Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.getCdJogo (), Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.getCdAtividade (), Parametros.CD_ETAPA_ATIVIDADE_RACIOCINIO_DIRETO_1, Camera.allCameras[1].GetComponent<ViewAtividade1>().contagemProgressiva, 0);
		resultado = registroEtapaAtividade.save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
		}


		if(quantMercadoria == quantSorteada){
			Util.hasVisible(panelPergunta2, true);

			panelFecharPergunta = Instantiate(Camera.allCameras[1].GetComponent<ViewAtividade1>().fecharPanelPrimeiraPergunta);
			panelFecharPergunta.transform.SetParent(rectPanel.transform, false);

			PrototipoItem animal = new PrototipoItem (Camera.allCameras[1].GetComponent<ViewAtividade1> ().numMercadoria);
			resultado = animal.get();
			if (resultado.getCode () < 0) {
				Debug.LogError (resultado.getMessage ());
			}

			panelFecharPergunta.GetComponentInChildren<Text>().text = "Existem " + quantMercadoria + " " + animal.getNmPrototipoItem();

			panelFazendeiro = Instantiate(Camera.allCameras[1].GetComponent<ViewAtividade1>().fazendeiroCorreto);
			panelFazendeiro.transform.SetParent(rectPanel.transform, false);

			registroEtapaAtividade.setLgFinalizada(1);
			resultado = registroEtapaAtividade.save ();
			if (resultado.getCode () < 0) {
				Debug.LogError (resultado.getMessage ());
			}

			panelFazendeiro.GetComponent<AudioSource>().volume *= (float) Jogo.getConfiguracao().getVlVolume();
			panelFazendeiro.GetComponent<AudioSource>().Play();

			StartCoroutine(delayChamadaFazendeiro(3));
		}
		else{
			panelFazendeiro = Instantiate(Camera.allCameras[1].GetComponentInChildren<ViewAtividade1>().fazendeiroErrado);
			panelFazendeiro.transform.SetParent(rectPanel.transform, false);

			panelFazendeiro.transform.FindChild("resposta").GetComponentInChildren<Text>().text = "Desafio: Quantos tem?\n\n" + Registro.getRegistro().getQtAtividadeFeita() + " vezes feito\n\n" + Registro.getRegistro().getQtAtividadeCorreta() + " respostas corretas.";

			StartCoroutine(delayOnClickFinal(3));
		}

	}

	private IEnumerator delayChamadaFazendeiro(float waitTime)
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

