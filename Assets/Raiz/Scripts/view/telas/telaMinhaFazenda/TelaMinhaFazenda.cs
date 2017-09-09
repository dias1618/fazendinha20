using UnityEngine;
using System.Collections;

public class TelaMinhaFazenda : Tela
{

	private static TelaMinhaFazenda telaMinhaFazenda;

	protected TelaMinhaFazenda() : base(){
	}

	public static TelaMinhaFazenda getTela(){
		if (telaMinhaFazenda == null) {
			telaMinhaFazenda = new TelaMinhaFazenda ();
		}

		return telaMinhaFazenda;
	}

	public static void destroyTela(){
		telaMinhaFazenda = null;
	}

}

