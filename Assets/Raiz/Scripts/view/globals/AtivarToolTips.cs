using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class AtivarToolTips : MonoBehaviour
{


	public GameObject panelToolTip;

	public int selectText = -1;

	public string[] text;


	public void Ativar () {

		if (selectText >= 0) {
			panelToolTip.GetComponentInChildren<Text> ().text = text [selectText];
		}

		panelToolTip.SetActive(true);
	}

	public void Desativar () {
		panelToolTip.SetActive(false);
	}

}

