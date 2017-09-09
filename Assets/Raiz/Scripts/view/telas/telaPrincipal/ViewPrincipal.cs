using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class ViewPrincipal : View
{

	public Text txtBoasVindas;

	// Use this for initialization
	void Start (){
		base.start (true, 0);

		txtBoasVindas.text = "Bem vindo de volta, " + Registro.getRegistro().getNmJogador();
	}
	
	// Update is called once per frame
	void Update (){
		base.update ();
	}
}

