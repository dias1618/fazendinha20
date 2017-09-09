using UnityEngine;
using System.Collections;

public class TelaInicial : Tela{

	private static TelaInicial telaInicial;

	protected TelaInicial() : base(){
		ConexaoController.inicializarConexao ();
		Jogo.getJogo ();
		Jogo.getConfiguracao ();
	}

	public static TelaInicial getTela(){
		if (telaInicial == null) {
			telaInicial = new TelaInicial ();
		}

		return telaInicial;
	}

	public static void destroyTela(){
		telaInicial = null;
	}

}

