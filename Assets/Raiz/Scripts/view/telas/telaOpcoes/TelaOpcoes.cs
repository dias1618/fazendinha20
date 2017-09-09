using UnityEngine;
using System.Collections;

public class TelaOpcoes : Tela
{

	private static TelaOpcoes telaOpcoes;

	protected TelaOpcoes() : base(){
		ConexaoController.inicializarConexao ();
	}

	public static TelaOpcoes getTela(){
		if (telaOpcoes == null) {
			telaOpcoes = new TelaOpcoes ();
		}

		return telaOpcoes;
	}

	public static void destroyTela(){
		telaOpcoes = null;
	}
}

