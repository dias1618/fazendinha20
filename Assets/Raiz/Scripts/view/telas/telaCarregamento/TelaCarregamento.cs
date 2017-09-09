using UnityEngine;
using System.Collections;

public class TelaCarregamento : Tela{

	private static TelaCarregamento telaCarregamento;

	protected TelaCarregamento() : base(){
		
	}

	public static TelaCarregamento getTela(){
		if (telaCarregamento == null) {
			telaCarregamento = new TelaCarregamento ();
		}

		return telaCarregamento;
	}

}

