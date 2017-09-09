using UnityEngine;
using System.Collections;

public class btnTiposPintinhos : MonoBehaviour
{

	public GameObject canvas;
	public GameObject panelTiposPintinhos;

	public void OnClick () {
		RectTransform rectPanel = canvas.GetComponentInChildren<RectTransform>(); 
		GameObject tiposPintinho = Instantiate(panelTiposPintinhos);
		tiposPintinho.transform.SetParent(rectPanel.transform, false);
	}
}

