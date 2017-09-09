using UnityEngine;
using System.Collections;

public class ViewInicial : View
{

	// Use this for initialization
	void Start (){
		base.start (false, 0);
		TelaInicial.getTela ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		base.update ();
	}
}

