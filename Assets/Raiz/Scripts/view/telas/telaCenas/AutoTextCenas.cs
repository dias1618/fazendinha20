using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class AutoTextCenas : MonoBehaviour
{

	public int currentPosition = 0;
	public float Delay;  // 10 characters per sec.
	public string Text = "";
	public string[] additionalLines ;
	public bool finishText;

	public IEnumerator Ativar(string aText){
		this.gameObject.GetComponent<Text>().text = "";

		AudioSource audio = this.gameObject.GetComponent<AudioSource>();
		audio.volume *= (float)Jogo.getConfiguracao().getVlVolume();
		audio.Play();

		finishText = false;
		Text = aText;
		currentPosition = 0;
		Delay = 0.05F;
		foreach ( string S in additionalLines )
			Text += "\n" + S;
		while (true){
			if(!Camera.main.GetComponent<ViewCenas>().pular){
				if (currentPosition < Text.Length)
					this.gameObject.GetComponent<Text>().text += Text[currentPosition++];
				else
					break;	

				yield return new WaitForSeconds (Delay);
			}
			else{
				this.gameObject.GetComponent<Text>().text = Text;
				if(Camera.main.GetComponent<ViewCenas>().posDialogo < (FalasCenas.text.Length))
					Camera.main.GetComponent<ViewCenas>().pular = false;
				break;
			}

		}

		if(this.gameObject.GetComponent<Text>().text.Length == Text.Length){
			finishText = true;
		}

		audio.volume /= (float)Jogo.getConfiguracao().getVlVolume();
		audio.Stop();
	}
}

