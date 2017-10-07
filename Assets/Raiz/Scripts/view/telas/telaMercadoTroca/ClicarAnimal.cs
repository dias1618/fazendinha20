using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class ClicarAnimal : MonoBehaviour{

	public Transform panelArea;
	public GameObject panelTroca;
	public int seletor = 0;

	public void OnClickMouse (){
		if (seletor == 0) {
			foreach (Transform child in panelTroca.transform) {
				int i = 0;
				foreach (Transform childOfChild in child) {
					i++;
				}
				if (i == 0) {
					panelArea = this.transform.parent;
					this.transform.parent = child;	
					this.gameObject.GetComponent<EntrarAnimal> ().OnMouseExit ();
					seletor = 1;
					break;
				}
			}


		}
		else if (seletor == 1) {
			this.transform.parent = panelArea;		
			this.gameObject.GetComponent<EntrarAnimal> ().OnMouseExit ();
			seletor = 0;
		}
	}
}

