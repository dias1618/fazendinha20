using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class btnSobra : MonoBehaviour
{

	public int selecionado;

	public GameObject panelFazendeiro;
	public GameObject panelFecharPergunta;

	public RectTransform rectPanel;

	void Start(){
		rectPanel = Camera.allCameras[1].GetComponent<ViewAtividade1>().canvas.GetComponentInChildren<RectTransform>(); 
	}

	public void onClick(){

		Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.setQtSegundosJogados (Camera.allCameras[1].GetComponent<ViewAtividade1>().contagemProgressiva);
		Result resultado = Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
		}

		RegistroEtapaAtividade registroEtapaAtividade = new RegistroEtapaAtividade (Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.getCdRegistroAtividade (), Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.getCdRegistro (), Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.getCdJogo (), Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.getCdAtividade (), Parametros.CD_ETAPA_ATIVIDADE_RACIOCINIO_DIRETO_4, Camera.allCameras[1].GetComponent<ViewAtividade1>().contagemProgressiva, 0);
		resultado = registroEtapaAtividade.save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
		}

		if((selecionado == 0 && TelaAtividade1.getTela().getSobrou() == 0) ||
			(selecionado == 1 && TelaAtividade1.getTela().getSobrou() == 1)){

			panelFazendeiro = Instantiate(Camera.allCameras[1].GetComponentInChildren<ViewAtividade1>().fazendeiroCorretoFinal);
			panelFazendeiro.transform.SetParent(rectPanel.transform, false);


			Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.setLgFinalizada (1);
			resultado = Camera.allCameras[1].GetComponent<ViewAtividade1>().registroAtividade.save ();
			if (resultado.getCode () < 0) {
				Debug.LogError (resultado.getMessage ());
			}

			registroEtapaAtividade.setLgFinalizada(1);
			resultado = registroEtapaAtividade.save ();
			if (resultado.getCode () < 0) {
				Debug.LogError (resultado.getMessage ());
			}

			panelFazendeiro.transform.FindChild("resposta").GetComponentInChildren<Text>().text = "Desafio: Quantos tem?\n\n" + Registro.getRegistro().getQtAtividadeFeita() + " vezes feito\n\n" + Registro.getRegistro().getQtAtividadeCorreta() + " respostas corretas.";

			StartCoroutine(delayOnClickFinal(3));
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

