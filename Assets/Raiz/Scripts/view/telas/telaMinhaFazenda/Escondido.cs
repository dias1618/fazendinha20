using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Escondido : MonoBehaviour
{

	public int cdItem;

	void Start () {
		RegistroItem registroItem = Registro.getRegistro ().getRegistroItem (cdItem, Parametros.CD_PINTINHO);
		if(registroItem.getLgItem() > 0){
			this.GetComponent<Image>().sprite = Camera.main.GetComponent<ViewMinhaFazenda>().cartas[cdItem-1];
		}
	}
}

