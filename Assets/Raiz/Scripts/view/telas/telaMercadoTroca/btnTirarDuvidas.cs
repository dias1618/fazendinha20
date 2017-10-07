using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class btnTirarDuvidas : MonoBehaviour
{

	public GameObject panelTirarDuvidas;

	public GameObject canvas;

	public void OnClick(){
		AudioSource audio = this.transform.FindChild("clicarBotao").GetComponent<AudioSource>();
		audio.volume *= (float)Jogo.getConfiguracao().getVlVolume();
		audio.Play();
		StartCoroutine(delayOnClick(0.7F));

	}

	private IEnumerator delayOnClick(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
		AudioSource audio = this.transform.FindChild("clicarBotao").GetComponent<AudioSource>();
		audio.volume /= (float)Jogo.getConfiguracao().getVlVolume();
		audio.Stop();

		RectTransform rectPanel = canvas.GetComponentInChildren<RectTransform>(); 
		GameObject panelTirarDuvidasAtivo = Instantiate(panelTirarDuvidas);
		panelTirarDuvidasAtivo.transform.SetParent(rectPanel.transform, false);

	}

}

