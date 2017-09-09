using System.Collections;
using UnityEngine.UI;
using UnityEngine;


public class toggleAvancarSemEsgotarTrocas : MonoBehaviour
{

	public Toggle toggle;

	public void OnChangeAvacarSemEsgotarTrocas () {
		Jogo.getConfiguracao().setLgAvancarSemEsgotarTempo(toggle.isOn?1:0);
		Result resultado = Jogo.getConfiguracao ().save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
		}
	}

}

