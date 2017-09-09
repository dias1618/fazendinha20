using UnityEngine;
using System.Collections;

public class TelaJogoMemoria : Tela{

	private static TelaJogoMemoria telaJogoMemoria;

	private bool lgTutorial;

	protected TelaJogoMemoria(bool lgTutorial) : base(){
		setLgTutorial(lgTutorial);
	}

	public static TelaJogoMemoria getTela(){
		return getTela (false);
	}

	public static TelaJogoMemoria getTela(bool lgTutorial){
		if (telaJogoMemoria == null) {
			telaJogoMemoria = new TelaJogoMemoria (lgTutorial);
		}

		return telaJogoMemoria;
	}

	public static void destroyTela(){
		telaJogoMemoria = null;
	}

	public bool getLgTutorial(){
		return lgTutorial;
	}

	public void setLgTutorial(bool lgTutorial){
		this.lgTutorial = lgTutorial;
	}



}

