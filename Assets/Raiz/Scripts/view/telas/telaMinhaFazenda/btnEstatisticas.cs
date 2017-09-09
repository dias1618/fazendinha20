using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class btnEstatisticas : MonoBehaviour
{

	public GameObject canvas;
	public GameObject panelEstatisticas;

	public void OnClick () {
		RectTransform rectPanel = canvas.GetComponentInChildren<RectTransform>(); 
		GameObject estatistica = Instantiate(panelEstatisticas);
		estatistica.transform.SetParent(rectPanel.transform, false);

		estatistica.transform.FindChild ("txtHorasJogadas").GetComponent<Text> ().text = Util.getHorasJogadas ();
		estatistica.transform.FindChild ("txtPintinhosCapturados").GetComponent<Text> ().text = Util.getPintinhosCapturados ();
		estatistica.transform.FindChild ("txtDesafiosRealizados").GetComponent<Text> ().text = Util.getDesafiosRealizados ();
		estatistica.transform.FindChild ("txtDesafiosOcultos").GetComponent<Text> ().text = Util.getDesafiosOcultos ();
	}

}

