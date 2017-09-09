using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class sliderMudancaVolume : MonoBehaviour
{

	public Slider sliderVolume;

	public void OnChangeVolume () {
		Jogo.getConfiguracao().setVlVolume(sliderVolume.value);
		Result resultado = Jogo.getConfiguracao ().save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
		}
	}
}

