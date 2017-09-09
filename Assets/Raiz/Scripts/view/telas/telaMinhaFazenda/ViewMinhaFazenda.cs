using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class ViewMinhaFazenda : View
{

	public GameObject panelPintinho;
	public GameObject panelGalinha;
	public GameObject panelSacoMilho;
	public GameObject panelPorco;
	public GameObject panelOvelha;
	public GameObject panelCavalo;
	public GameObject panelVaca;

	//Variavel que recebe as cartas que serao postas no jogo da memoria
	public Sprite[] cartas;

	// Use this for initialization
	void Start (){
		base.start (true, Parametros.TUTORIAL_MINHA_FAZENDA);
		StartCoroutine(iniciarView ());
	}

	private IEnumerator iniciarView(){

		float timeInfinite = 1F;
		while(!liberarStart){
			yield return new WaitForSeconds (timeInfinite);
			timeInfinite = 0.1F;
		}


		if(Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_VACA).getQtItem() > 0){
			panelVaca.GetComponentInChildren<Text>().text = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_VACA).getQtItem();
		}
		else{
			Util.hasVisible (panelVaca, false);
		}

		if(Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_CAVALO).getQtItem() > 0){
			panelCavalo.GetComponentInChildren<Text>().text = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_CAVALO).getQtItem();
		}
		else{
			Util.hasVisible (panelCavalo, false);
		}

		if(Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_OVELHA).getQtItem() > 0){
			panelOvelha.GetComponentInChildren<Text>().text = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_OVELHA).getQtItem();
		}
		else{
			Util.hasVisible (panelOvelha, false);	
		}

		if(Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_PORCO).getQtItem() > 0){
			panelPorco.GetComponentInChildren<Text>().text = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_PORCO).getQtItem();
		}
		else{
			Util.hasVisible (panelPorco, false);
		}

		if(Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_SACO_MILHO).getQtItem() > 0){
			panelSacoMilho.GetComponentInChildren<Text>().text = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_SACO_MILHO).getQtItem();
		}
		else{
			Util.hasVisible (panelSacoMilho, false);
		}

		if(Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_GALINHA).getQtItem() > 0){
			panelGalinha.GetComponentInChildren<Text>().text = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_GALINHA).getQtItem();
		}
		else{
			Util.hasVisible (panelGalinha, false);
		}

		if(Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_PINTINHO).getQtItem() > 0){
			panelPintinho.GetComponentInChildren<Text>().text = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_PINTINHO).getQtItem();
		}
		else{
			Util.hasVisible (panelPintinho, false);
		}

	}

	void Update(){
		base.update ();
	}

}

