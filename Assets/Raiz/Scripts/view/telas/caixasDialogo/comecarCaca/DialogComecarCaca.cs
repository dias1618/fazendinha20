using UnityEngine;
using System.Collections;

public class DialogComecarCaca : MonoBehaviour
{

	public AudioClip clipJogo;

	// Use this for initialization
	public void OnClick (){
		AudioSource audio = this.transform.FindChild("clicarBotao").GetComponent<AudioSource>();
		audio.volume *= (float)Jogo.getConfiguracao().getVlVolume();
		audio.Play();
		StartCoroutine(delayIniciarComecarCaca(0.7F));
	}

	private IEnumerator delayIniciarComecarCaca(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);

		//O botao libera que o jogo comece, buscando a variavel no script da camera principal
		Camera.main.GetComponent<ViewJogoMemoria>().iniciarJogo = true;
		//O Texto do botao e alterado para "Desistir"
		//var child:UI.Text = this.gameObject.GetComponentInChildren.<UI.Text>();
		//child.text = "Desistir";
		this.gameObject.transform.parent.gameObject.SetActive(false);

		Camera.main.GetComponent<AudioSource>().clip = clipJogo;
		Camera.main.GetComponent<AudioSource>().Play();

	}
}

