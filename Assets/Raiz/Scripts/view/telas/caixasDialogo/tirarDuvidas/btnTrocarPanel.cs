using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class btnTrocarPanel : MonoBehaviour
{

	public GameObject panel;

	public void OnClick (){
		foreach (Transform child in panel.transform.parent.transform){
			child.gameObject.SetActive (false);
		}

		panel.gameObject.SetActive (true);


		foreach (Transform child in this.transform.parent.transform){
			foreach (Transform childOfChild in child){
				childOfChild.GetComponent<Text> ().color = new Color (0x00, 0x00, 0x00);
			}

			foreach (Transform childOfChild in this.transform){
				childOfChild.GetComponent<Text> ().color = new Color (0xFF, 0xFF, 0xFF);
			}
		}
	}
}

