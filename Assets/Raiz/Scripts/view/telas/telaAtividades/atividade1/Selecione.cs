using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Selecione : MonoBehaviour
{

	public bool selecionado;

	public Sprite spriteOriginal;
	public Sprite spriteSelecionado;

	public void OnClick () {

		if(selecionado){
			this.gameObject.GetComponent<Image>().sprite = spriteOriginal;
			selecionado = false;
		}
		else{
			this.gameObject.GetComponent<Image>().sprite = spriteSelecionado;
			selecionado = true;
		}

	}
}

