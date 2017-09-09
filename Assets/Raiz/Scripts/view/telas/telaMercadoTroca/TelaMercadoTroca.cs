using UnityEngine;
using System.Collections;

public class TelaMercadoTroca : Tela
{
	private static TelaMercadoTroca telaMercadoTroca;

	private bool lgPrimeiraEntradaMercado;
	private int cdAnimalJogadorSelecionado;
	private int cdAnimalFazendeiroSelecionado;
	private GameObject prefabTroca;

	protected TelaMercadoTroca(bool lgPrimeiraEntradaMercado) : base(){
		setLgPrimeiraEntradaMercado(lgPrimeiraEntradaMercado);
	}

	public static TelaMercadoTroca getTela(){
		return getTela (false);
	}

	public static TelaMercadoTroca getTela(bool lgPrimeiraEntradaMercado){
		if (telaMercadoTroca == null) {
			telaMercadoTroca = new TelaMercadoTroca (lgPrimeiraEntradaMercado);
		}

		return telaMercadoTroca;
	}

	public static void destroyTela(){
		telaMercadoTroca = null;
	}

	public bool getLgPrimeiraEntradaMercado(){
		return lgPrimeiraEntradaMercado;
	}

	public void setLgPrimeiraEntradaMercado(bool lgPrimeiraEntradaMercado){
		this.lgPrimeiraEntradaMercado = lgPrimeiraEntradaMercado;
	}

	public int getCdAnimalJogadorSelecionado(){
		return cdAnimalJogadorSelecionado;
	}

	public void setCdAnimalJogadorSelecionado(int cdAnimalJogadorSelecionado){
		this.cdAnimalJogadorSelecionado = cdAnimalJogadorSelecionado;
	}

	public int getCdAnimalFazendeiroSelecionado(){
		return cdAnimalFazendeiroSelecionado;
	}

	public void setCdAnimalFazendeiroSelecionado(int cdAnimalFazendeiroSelecionado){
		this.cdAnimalFazendeiroSelecionado = cdAnimalFazendeiroSelecionado;
	}

	public GameObject getPrefabTroca(){
		return prefabTroca;
	}

	public void setPrefabTroca(GameObject prefabTroca){
		this.prefabTroca = prefabTroca;
	}
}

