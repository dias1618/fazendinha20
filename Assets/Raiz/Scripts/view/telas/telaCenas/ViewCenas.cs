using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ViewCenas : View
{
	public static bool reiniciarDados;

	public Sprite[] backgrounds;
	public GameObject panelBackground;
	public GameObject panelCaixaTexto;
	public bool pular;
	public GameObject btnPularProximo;
	public GameObject panelDigiteNome;
	public Text txtCena;

	public GameObject digitarNome;
	public int posDialogo;


	void Start (){
		base.start (false, 0);
		FalasCenas.inicializarFalas ();
		chamarCena();
	}

	void Update(){
		base.update ();
	}

	public void chamarCena(){
		StartCoroutine(delayChamarCena(1));
	}

	private IEnumerator delayChamarCena(float waitTime){
		inicializarDados ();
		if(digitarNome){
			Util.hasVisible(digitarNome, false);
		}
		else
			digitarNome = null;
		
		panelBackground.GetComponent<Image>().sprite = backgrounds[TelaCenas.getTela().getNrCenaHistoriaInicial()];

		while(panelBackground.GetComponent<Image>().color.a < 1){
			panelBackground.GetComponent<Image> ().color = Util.addColorAlpha (panelBackground.GetComponent<Image> ().color, (!pular ? 0.01F : 1F)); 
			yield return new WaitForSeconds (0.0001F);
		}

		panelCaixaTexto.GetComponent<Image>().color = Util.addColorAlpha (panelBackground.GetComponent<Image> ().color, 1);
		txtCena.GetComponent<Text>().color = Util.addColorAlpha (txtCena.GetComponent<Text>().color, 1);
		btnPularProximo.GetComponent<Image>().color = Util.addColorAlpha (btnPularProximo.GetComponent<Image>().color, 1);
		StartCoroutine(delayControlePassagemTexto ());
	}

	private IEnumerator delayControlePassagemTexto(){
		if(FalasCenas.text.Length > 0){
			StartCoroutine(panelBackground.GetComponentInChildren<AutoTextCenas>().Ativar(FalasCenas.arrayTexto[TelaCenas.getTela().getNrCenaHistoriaInicial()][posDialogo]));
			float timeWaitText = 1F;
			while(!panelBackground.GetComponentInChildren<AutoTextCenas>().finishText && FalasCenas.text.Length > 1){
				yield return new WaitForSeconds (timeWaitText);
				timeWaitText = 0.1F;
			}	
		}

		posDialogo++;

		if(posDialogo < FalasCenas.arrayTexto[TelaCenas.getTela().getNrCenaHistoriaInicial()].Length){
			float timeInfinite = 1F;
			while(!pular && FalasCenas.text.Length > 1){
				yield return new WaitForSeconds (timeInfinite);
				timeInfinite = 0.1F;
			}
			pular = false;

			StartCoroutine(delayControlePassagemTexto());
		}
		else{
			if(TelaCenas.getTela().getNrCenaHistoriaInicial() == Parametros.NR_CENA_VENTANIA_PARTE_FINAL){
				RectTransform rectPanel = panelCaixaTexto.GetComponent<RectTransform>(); 
				digitarNome = Instantiate(panelDigiteNome);
				digitarNome.transform.SetParent(rectPanel.transform, false);
				reiniciarDados = true;
			}

			btnPularProximo.GetComponent<AtivarToolTips> ().selectText = 1;
			btnPularProximo.GetComponent<Image>().sprite = btnPularProximo.GetComponent<btnPularCena>().sprites[1];
			btnPularProximo.GetComponent<btnPularCena>().ativarProximo = true;
		}
	}

	private void inicializarDados(){
		TelaCenas.getTela ().addNrCenaHistoriaInicial ();
		posDialogo = 0;
		btnPularProximo.GetComponent<AtivarToolTips> ().selectText = 0;
		btnPularProximo.GetComponent<btnPularCena>().ativarProximo = false;
		btnPularProximo.GetComponent<Image>().sprite = btnPularProximo.GetComponent<btnPularCena>().sprites[0];
		pular = false;

		panelBackground.GetComponent<Image>().color = Util.setColorAlpha (panelBackground.GetComponent<Image> ().color, 0);
		panelCaixaTexto.GetComponent<Image>().color = Util.setColorAlpha (panelCaixaTexto.GetComponent<Image> ().color, 0);
		txtCena.GetComponent<Text>().color = Util.setColorAlpha (txtCena.GetComponent<Text>().color, 0);
		txtCena.GetComponent<Text>().text = "";
		btnPularProximo.GetComponent<Image>().color = Util.setColorAlpha (btnPularProximo.GetComponent<Image> ().color, 0);
		reiniciarDados = false;
	}

}

