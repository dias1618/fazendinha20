using UnityEngine;
using System.Collections;

public class TelaJogoMemoriaFinal : Tela
{

	private static TelaJogoMemoriaFinal telaJogoMemoriaFinal;

	private int qtCartasViradas;
	private int qtTempoSobrando;
	private int qtPontosGanhosCartas;
	private int qtPontosGanhosTempo;

	protected TelaJogoMemoriaFinal() : base(){
	}

	public static TelaJogoMemoriaFinal getTela(){
		if (telaJogoMemoriaFinal == null) {
			telaJogoMemoriaFinal = new TelaJogoMemoriaFinal ();
		}

		return telaJogoMemoriaFinal;
	}

	public static void destroyTela(){
		telaJogoMemoriaFinal = null;
	}

	public int getQtCartasViradas(){
		return qtCartasViradas;
	}

	public void setQtCartasViradas(int qtCartasViradas){
		this.qtCartasViradas = qtCartasViradas;
	}

	public int getQtTempoSobrando(){
		return qtTempoSobrando;
	}

	public void setQtTempoSobrando(int qtTempoSobrando){
		this.qtTempoSobrando = qtTempoSobrando;
	}

	public int getQtPontosGanhosCartas(){
		return qtPontosGanhosCartas;
	}

	public void setQtPontosGanhosCartas(int qtPontosGanhosCartas){
		this.qtPontosGanhosCartas = qtPontosGanhosCartas;
	}

	public int getQtPontosGanhosTempo(){
		return qtPontosGanhosTempo;
	}

	public void setQtPontosGanhosTempo(int qtPontosGanhosTempo){
		this.qtPontosGanhosTempo = qtPontosGanhosTempo;
	}
}

