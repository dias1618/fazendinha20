using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class btnTabelaTrocas : MonoBehaviour
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

	}

}

