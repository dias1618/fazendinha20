using UnityEngine;
using System.Collections;


public class TelaCenas : Tela{
	
	private static TelaCenas telaCenas;

	private int nrCenaHistoriaInicial;


	private TelaCenas() : base(){
		setNrCenaHistoriaInicial (-1);
		FalasCenas.inicializarFalas ();
	}

	public static TelaCenas getTela(){
		if (telaCenas == null) {
			telaCenas = new TelaCenas ();
		}

		return telaCenas;
	}

	public static void destroyTela(){
		telaCenas = null;
	}

	//Metódos Acessores
	public int getNrCenaHistoriaInicial(){
		return nrCenaHistoriaInicial;
	}

	public void setNrCenaHistoriaInicial(int nrCenaHistoriaInicial){
		this.nrCenaHistoriaInicial = nrCenaHistoriaInicial;
	}

	public void addNrCenaHistoriaInicial(){
		this.nrCenaHistoriaInicial++;
	}
}

