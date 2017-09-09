using UnityEngine;
using System.Collections;

public class TelaMundo : Tela
{

	private static TelaMundo telaMundo;

	protected TelaMundo() : base(){
	}

	public static TelaMundo getTela(){
		if (telaMundo == null) {
			telaMundo = new TelaMundo ();
		}

		return telaMundo;
	}

	public static void destroyTela(){
		telaMundo = null;
	}
}

