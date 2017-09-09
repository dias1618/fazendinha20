using UnityEngine;
using System.Collections;

public class TelaAtividade1 : Tela
{

	private static TelaAtividade1 telaAtividade1;

	private int cdAnimalEscolhidoTroca;
	private int valorTrocaSorteio;
	private int valorTrocaEscolhido;
	private int sobrou;

	protected TelaAtividade1() : base(){
	}

	public static TelaAtividade1 getTela(){
		if (telaAtividade1 == null) {
			telaAtividade1 = new TelaAtividade1 ();
		}

		return telaAtividade1;
	}

	public static void destroyTela(){
		telaAtividade1 = null;
	}

	public int getCdAnimalEscolhidoTroca(){
		return cdAnimalEscolhidoTroca;
	}

	public void setCdAnimalEscolhidoTroca(int cdAnimalEscolhidoTroca){
		this.cdAnimalEscolhidoTroca = cdAnimalEscolhidoTroca;
	}

	public int getValorTrocaSorteio(){
		return valorTrocaSorteio;
	}

	public void setValorTrocaSorteio(int valorTrocaSorteio){
		this.valorTrocaSorteio = valorTrocaSorteio;
	}

	public int getValorTrocaEscolhido(){
		return valorTrocaEscolhido;
	}

	public void setValorTrocaEscolhido(int valorTrocaEscolhido){
		this.valorTrocaEscolhido = valorTrocaEscolhido;
	}

	public int getSobrou(){
		return sobrou;
	}

	public void setSobrou(int sobrou){
		this.sobrou = sobrou;
	}
}

