using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class ViewOpcoes : View
{
	//Valor que marcara a altura do volume no jogo
	public Slider sliderVolume;

	//Valor que marcara o tempo do jogo da memoria
	public Slider sliderTempoMemoria;

	public Toggle toogleAvacarSemEsgotarTrocas;

	// Use this for initialization
	void Start (){
		base.start (false, 0);

		//Valor inicial do volume
		sliderVolume.value = (float) Jogo.getConfiguracao().getVlVolume();

		//Valor inicial do volume
		sliderTempoMemoria.value = (float)Jogo.getConfiguracao().getQtTempoMemoria();

		toogleAvacarSemEsgotarTrocas.isOn = Jogo.getConfiguracao().getLgAvancarSemEsgotarTempo()==1;
	}
	
	// Update is called once per frame
	void Update (){
		base.update ();
	}
}

