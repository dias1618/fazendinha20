using UnityEngine;
using System.Collections;

public class TelaFinalJogo : Tela
{

	private static TelaFinalJogo telaFinalJogo;

	private TelaFinalJogo() : base(){
		
	}

	public static TelaFinalJogo getTela(){
		if (telaFinalJogo == null) {
			telaFinalJogo = new TelaFinalJogo ();
		}

		return telaFinalJogo;
	}

	public static void destroyTela(){
		telaFinalJogo = null;
	}

}

