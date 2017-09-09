using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class ViewMercadoTroca : View{
	
	//Painel onde ficar os animais do jogador
	public GameObject panelJogador;
	//Modelo da figura do pintinho, que sera instanciado via codigo quando necessario
	public GameObject prefabPintinho;

	public Text quantPintinhosText;
	public Text quantGalinhasText;
	public Text quantSacosMilhoText;
	public Text quantPorcosText;
	public Text quantOvelhasText;
	public Text quantCavalosText;
	public Text quantVacasText;

	public GameObject panelComputadorFazendeiros;

	public GameObject slotDonaGertrudes;
	public GameObject slotSeuJoaquim;
	public GameObject slotSeuZe;
	public GameObject slotVelhoChico;
	public GameObject slotMiguel;
	public GameObject slotDonaMaria;
	public GameObject slotSeuToninho;

	public GameObject prefabClone;


	public GameObject panelAnimaisComputador;
	public GameObject panelAnimaisComputadorTroca;

	public GameObject prefabDonaGertrudes;

	public Slider sliderNivel;
	public Text txtNivel;



	void Start (){
		base.start (true, Parametros.TUTORIAL_MERCADO_TROCA);

		StartCoroutine(iniciarView ());
	}

	private IEnumerator iniciarView(){

		float timeInfinite = 1F;
		while(!liberarStart){
			yield return new WaitForSeconds (timeInfinite);
			timeInfinite = 0.1F;
		}

		atualizarQuantidadeAnimais ();

		iniciarSlotsFazendeiros ();

		atualizarNivel ();

		if(TelaMercadoTroca.getTela().getLgPrimeiraEntradaMercado()){
			ApresentacaoFazendeiros.inicializarApresentacao ();
			chamarFazendeiro(prefabDonaGertrudes);
		}

	}

	private void atualizarQuantidadeAnimais(){
		quantPintinhosText.text  = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_PINTINHO).getQtItem();
		quantGalinhasText.text 	 = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_GALINHA).getQtItem();
		quantSacosMilhoText.text = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_SACO_MILHO).getQtItem();
		quantPorcosText.text 	 = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_PORCO).getQtItem();
		quantOvelhasText.text 	 = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_OVELHA).getQtItem();
		quantCavalosText.text 	 = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_CAVALO).getQtItem();
		quantVacasText.text 	 = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_VACA).getQtItem();
	}

	private void iniciarSlotsFazendeiros(){
		slotDonaGertrudes.SetActive(false);
		slotSeuJoaquim.SetActive(false);
		slotSeuZe.SetActive(false);
		slotVelhoChico.SetActive(false);
		slotMiguel.SetActive(false);
		slotDonaMaria.SetActive(false);
		slotSeuToninho.SetActive(false);
	}

	private void atualizarNivel(){
		txtNivel.text = "NIVEL " + Registro.getRegistro().getQtNivel();
		sliderNivel.value = (float)Registro.getRegistro().getQtExperiencia();		
	}

	private GameObject apresentacaoFazendeiro;
	private void chamarFazendeiro(GameObject panelFazendeiro){
		RectTransform rectPanel = canvas.GetComponentInChildren<RectTransform>(); 
		apresentacaoFazendeiro = Instantiate(panelFazendeiro);
		apresentacaoFazendeiro.transform.SetParent(rectPanel.transform, false);
	}

	// Update is called once per frame
	void Update (){
		base.update ();
		atualizarFazendeiros ();
	}

	private void atualizarFazendeiros(){
		if(Registro.getRegistro().getRegistroObjetivo(Parametros.CD_OBJETIVO_GANHOU_UMA_VACA).getQtVezes() > 0){
			slotDonaGertrudes.SetActive(true);
			slotSeuJoaquim.SetActive(true);
			slotSeuZe.SetActive(true);
			slotVelhoChico.SetActive(true);
			slotMiguel.SetActive(true);
			slotDonaMaria.SetActive(true);
			slotSeuToninho.SetActive(true);
		}
		else if(Registro.getRegistro().getRegistroObjetivo(Parametros.CD_OBJETIVO_GANHOU_UM_CAVALO).getQtVezes() > 0){
			slotDonaGertrudes.SetActive(true);
			slotSeuJoaquim.SetActive(true);
			slotSeuZe.SetActive(true);
			slotVelhoChico.SetActive(true);
			slotMiguel.SetActive(true);
			slotDonaMaria.SetActive(true);
		}
		else if(Registro.getRegistro().getRegistroObjetivo(Parametros.CD_OBJETIVO_GANHOU_UMA_OVELHA).getQtVezes() > 0){
			slotDonaGertrudes.SetActive(true);
			slotSeuJoaquim.SetActive(true);
			slotSeuZe.SetActive(true);
			slotVelhoChico.SetActive(true);
			slotMiguel.SetActive(true);
		}
		else if(Registro.getRegistro().getRegistroObjetivo(Parametros.CD_OBJETIVO_GANHOU_UM_PORCO).getQtVezes() > 0){
			slotDonaGertrudes.SetActive(true);
			slotSeuJoaquim.SetActive(true);
			slotSeuZe.SetActive(true);
			slotVelhoChico.SetActive(true);
		}
		else if(Registro.getRegistro().getRegistroObjetivo(Parametros.CD_OBJETIVO_GANHOU_UM_SACO_MILHO).getQtVezes() > 0){
			slotDonaGertrudes.SetActive(true);
			slotSeuJoaquim.SetActive(true);
			slotSeuZe.SetActive(true);
		}
		else if(Registro.getRegistro().getRegistroObjetivo(Parametros.CD_OBJETIVO_GANHOU_UMA_GALINHA).getQtVezes() > 0){
			slotDonaGertrudes.SetActive(true);
			slotSeuJoaquim.SetActive(true);
		}
		else{
			slotDonaGertrudes.SetActive(true);
		}
	}
}

