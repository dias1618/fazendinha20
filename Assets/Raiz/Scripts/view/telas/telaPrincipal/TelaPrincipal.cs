using UnityEngine;
using System.Collections;

public class TelaPrincipal : Tela
{

	private static TelaPrincipal telaPrincipal;

	protected TelaPrincipal() : base(){
	}

	public static TelaPrincipal getTela(){
		if (telaPrincipal == null) {
			telaPrincipal = new TelaPrincipal ();
		}

		return telaPrincipal;
	}

	public static void destroyTela(){
		telaPrincipal = null;
	}
}

