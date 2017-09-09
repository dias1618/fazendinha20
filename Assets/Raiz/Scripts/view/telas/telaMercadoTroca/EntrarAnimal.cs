using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class EntrarAnimal : MonoBehaviour{

	public void OnMouseEnter (){

		string setaStr = "";
		int seletor = this.transform.gameObject.GetComponent<ClicarAnimal> ().seletor;
		if (seletor == 0) {
			setaStr = "SetaAzul";
		} 
		else {
			setaStr = "SetaAzulCima";
		}

		Transform seta = this.transform.Find(setaStr);
		Color color = seta.gameObject.GetComponent<Image> ().color;
		color.a = 1;
		seta.gameObject.GetComponent<Image> ().color = color;
	}

	public void OnMouseExit (){

		string setaStr = "";
		int seletor = this.transform.gameObject.GetComponent<ClicarAnimal> ().seletor;
		if (seletor == 0) {
			setaStr = "SetaAzul";
		} 
		else {
			setaStr = "SetaAzulCima";
		}

		Transform seta = this.transform.Find(setaStr);
		Color color = seta.gameObject.GetComponent<Image> ().color;
		color.a = 0;
		seta.gameObject.GetComponent<Image> ().color = color;
	}
}