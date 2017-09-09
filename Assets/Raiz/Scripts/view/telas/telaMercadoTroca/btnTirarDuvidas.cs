using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class btnTirarDuvidas : MonoBehaviour
{

	public GameObject panelTabelaTroca;

	public Sprite pintinhoColorido;
	public Sprite galinhaColorido;
	public Sprite sacoMilhoColorido;
	public Sprite porcoColorido;
	public Sprite ovelhaColorido;
	public Sprite cavaloColorido;
	public Sprite vacaColorido;
	public Sprite loteTerraColorido;

	public ArrayList databaseData = new ArrayList();

	public GameObject canvas;

	public void OnClick(){
		AudioSource audio = this.transform.FindChild("clicarBotao").GetComponent<AudioSource>();
		audio.volume *= (float)Jogo.getConfiguracao().getVlVolume();
		audio.Play();
		StartCoroutine(delayOnClick(0.7F));

	}

	private IEnumerator delayOnClick(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
		AudioSource audio = this.transform.FindChild("clicarBotao").GetComponent<AudioSource>();
		audio.volume /= (float)Jogo.getConfiguracao().getVlVolume();
		audio.Stop();

		RectTransform rectPanel = canvas.GetComponentInChildren<RectTransform>(); 
		GameObject panelTirarDuvidasAtivo = Instantiate(panelTabelaTroca);
		panelTirarDuvidasAtivo.transform.SetParent(rectPanel.transform, false);


		GameObject panelObjetivos = panelTirarDuvidasAtivo.transform.FindChild("panelObjetivos").gameObject;

		GameObject txtObjetivoTrocaDireta  = panelObjetivos.transform.FindChild("panelTxtObjetivoTrocaDireta").gameObject;
		GameObject txtObjetivoTrocaInversa = panelObjetivos.transform.FindChild("panelTxtObjetivoTrocaInversa").gameObject;
		GameObject txtObjetivoTroca4por1   = panelObjetivos.transform.FindChild("panelTxtObjetivoTroca4por1").gameObject;
		GameObject txtObjetivoTroca4por2   = panelObjetivos.transform.FindChild("panelTxtObjetivoTroca4por2").gameObject;
		GameObject txtObjetivo10SemErrar   = panelObjetivos.transform.FindChild("panelTxtObjetivo10SemErrar").gameObject;
		GameObject txtObjetivo30SemErrar   = panelObjetivos.transform.FindChild("panelTxtObjetivo30SemErrar").gameObject;
		GameObject txtObjetivo50SemErrar   = panelObjetivos.transform.FindChild("panelTxtObjetivo50SemErrar").gameObject;
		GameObject txtObjetivoCapturouAntesDoTempo = panelObjetivos.transform.FindChild("panelTxtObjetivoCapturouAntesDoTempo").gameObject;

		txtObjetivoTrocaDireta.SetActive(false);
		txtObjetivoTrocaInversa.SetActive(false);
		txtObjetivoTroca4por1.SetActive(false);
		txtObjetivoTroca4por2.SetActive(false);
		txtObjetivo10SemErrar.SetActive(false);
		txtObjetivo30SemErrar.SetActive(false);
		txtObjetivo50SemErrar.SetActive(false);
		txtObjetivoCapturouAntesDoTempo.SetActive(false);

		//Busca o codigo, o nome do jogador da tabela Registro, no banco de dados
		ArrayList registrosObjetivo = RegistroObjetivo.getAllByRegistro(Registro.getRegistro().getCdRegistro());
		int quantidadeObjetivosAlcancados = 0;
		foreach (RegistroObjetivo registroObjetivo in registrosObjetivo) {

			if (registroObjetivo.getQtVezes () > 0) {
				switch (registroObjetivo.getCdObjetivo ()) {
					case Parametros.CD_OBJETIVO_TROCA_DIRETA:
						txtObjetivoTrocaDireta.SetActive (true);
						quantidadeObjetivosAlcancados++;
						break;
					case Parametros.CD_OBJETIVO_TROCA_INVERSA:
						txtObjetivoTrocaInversa.SetActive (true);
						quantidadeObjetivosAlcancados++;
						break;
					case Parametros.CD_OBJETIVO_REALIZOU_TROCA_4_1:
						txtObjetivoTroca4por1.SetActive (true);
						quantidadeObjetivosAlcancados++;
						break;
					case Parametros.CD_OBJETIVO_REALIZOU_TROCA_4_2:
						txtObjetivoTroca4por2.SetActive (true);
						quantidadeObjetivosAlcancados++;
						break;
					case Parametros.CD_OBJETIVO_REALIZOU_10_SEM_ERRAR:
						txtObjetivo10SemErrar.SetActive (true);
						quantidadeObjetivosAlcancados++;
						break;

					case Parametros.CD_OBJETIVO_REALIZOU_30_SEM_ERRAR:
						txtObjetivo30SemErrar.SetActive (true);
						quantidadeObjetivosAlcancados++;
						break;

					case Parametros.CD_OBJETIVO_REALIZOU_50_SEM_ERRAR:
						txtObjetivo50SemErrar.SetActive (true);
						quantidadeObjetivosAlcancados++;
						break;

					case Parametros.CD_OBJETIVO_CAPTUROU_TODOS_PINTINHOS_NO_TEMPO:
						txtObjetivoCapturouAntesDoTempo.SetActive (true);
						quantidadeObjetivosAlcancados++;
						break;
				}
			}
		}

		GameObject panelPrincipal = panelTirarDuvidasAtivo.transform.FindChild("panelTabelaTrocas").gameObject;

		if(Registro.getRegistro().getRegistroObjetivo(Parametros.CD_OBJETIVO_GANHOU_UMA_GALINHA).getQtVezes() > 0){
			panelPrincipal.transform.FindChild("pintinho1").GetComponent<Image>().sprite = pintinhoColorido;
			panelPrincipal.transform.FindChild("pintinho2").GetComponent<Image>().sprite = pintinhoColorido;
			panelPrincipal.transform.FindChild("galinha3").GetComponent<Image>().sprite = galinhaColorido;
		}

		if(Registro.getRegistro().getRegistroObjetivo(Parametros.CD_OBJETIVO_GANHOU_UM_SACO_MILHO).getQtVezes() > 0){
			panelPrincipal.transform.FindChild("galinha4").GetComponent<Image>().sprite = galinhaColorido;
			panelPrincipal.transform.FindChild("galinha5").GetComponent<Image>().sprite = galinhaColorido;
			panelPrincipal.transform.FindChild("sacoMilho6").GetComponent<Image>().sprite = sacoMilhoColorido;
		}

		if(Registro.getRegistro().getRegistroObjetivo(Parametros.CD_OBJETIVO_GANHOU_UM_PORCO).getQtVezes() > 0){
			panelPrincipal.transform.FindChild("sacoMilho7").GetComponent<Image>().sprite = sacoMilhoColorido;
			panelPrincipal.transform.FindChild("sacoMilho8").GetComponent<Image>().sprite = sacoMilhoColorido;
			panelPrincipal.transform.FindChild("porco9").GetComponent<Image>().sprite = porcoColorido;
		}

		if(Registro.getRegistro().getRegistroObjetivo(Parametros.CD_OBJETIVO_GANHOU_UMA_OVELHA).getQtVezes() > 0){
			panelPrincipal.transform.FindChild("porco10").GetComponent<Image>().sprite = porcoColorido;
			panelPrincipal.transform.FindChild("porco11").GetComponent<Image>().sprite = porcoColorido;
			panelPrincipal.transform.FindChild("ovelha12").GetComponent<Image>().sprite = ovelhaColorido;
		}

		if(Registro.getRegistro().getRegistroObjetivo(Parametros.CD_OBJETIVO_GANHOU_UM_CAVALO).getQtVezes() > 0){
			panelPrincipal.transform.FindChild("ovelha13").GetComponent<Image>().sprite = ovelhaColorido;
			panelPrincipal.transform.FindChild("ovelha14").GetComponent<Image>().sprite = ovelhaColorido;
			panelPrincipal.transform.FindChild("cavalo15").GetComponent<Image>().sprite = cavaloColorido;
		}

		if(Registro.getRegistro().getRegistroObjetivo(Parametros.CD_OBJETIVO_GANHOU_UMA_VACA).getQtVezes() > 0){
			panelPrincipal.transform.FindChild("cavalo16").GetComponent<Image>().sprite = cavaloColorido;
			panelPrincipal.transform.FindChild("cavalo17").GetComponent<Image>().sprite = cavaloColorido;
			panelPrincipal.transform.FindChild("vaca18").GetComponent<Image>().sprite = vacaColorido;
		}

		if(Registro.getRegistro().getRegistroObjetivo(Parametros.CD_OBJETIVO_CONQUISTOU_UM_LOTE_TERRA).getQtVezes() > 0){
			panelPrincipal.transform.FindChild("vaca19").GetComponent<Image>().sprite = vacaColorido;
			panelPrincipal.transform.FindChild("vaca20").GetComponent<Image>().sprite = vacaColorido;
			panelPrincipal.transform.FindChild("loteTerra21").GetComponent<Image>().sprite = loteTerraColorido;
		}


		GameObject txtObjetivoQuantidade = panelObjetivos.transform.FindChild("txtOutrosObjetivosQuantidade").gameObject;

		txtObjetivoQuantidade.GetComponent<Text>().text = "("+quantidadeObjetivosAlcancados+" de 8)";

	}

}

