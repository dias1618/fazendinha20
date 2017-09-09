using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class sliderMudancaTempoMemoria : MonoBehaviour
{

	public Slider sliderTempoMemoria;
	public Text txtTempoMemoriaQuantidade;

	public void OnChangeTempoMemoria () {
		Jogo.getConfiguracao().setQtTempoMemoria((int)sliderTempoMemoria.value);
		Result resultado = Jogo.getConfiguracao ().save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
		}

		txtTempoMemoriaQuantidade.text = "" + Jogo.getConfiguracao().getQtTempoMemoria() + " segundos";
	}
}

