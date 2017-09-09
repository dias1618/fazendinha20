using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ViewFinalJogo : View
{
	//Primeiro Panel
	public Text txtHorasJogadasPrimeiroPanel;
	public Text txtDesafiosOcultos;
	public Text txtPintinhosCapturados;

	//Segundo Panel
	public Text txtDesafiosRealizados;
	public Text txtHorasJogadasSegundoPanel;
	public Text txtPrimeiraPergunta;
	public Text txtSegundaPergunta;
	public Text txtTerceiraPergunta;
	public Text txtQuartaPergunta;

	//Terceiro Panel
	public Text txtGalinhasTrocadas;
	public Text txtSacosMilhoTrocados;
	public Text txtPorcosTrocados;
	public Text txtOvelhasTrocadas;
	public Text txtCavalosTrocados;
	public Text txtVacasTrocadas;



	// Use this for initialization
	void Start (){
		base.start (false, 0);
		//Primeiro Panel
		txtHorasJogadasPrimeiroPanel.GetComponent<Text> ().text = Util.getHorasJogadas ();
		txtPintinhosCapturados.GetComponent<Text> ().text = Util.getPintinhosCapturados ();
		txtDesafiosOcultos.GetComponent<Text> ().text = Util.getDesafiosOcultos ();
		//Segundo Panel
		txtDesafiosRealizados.GetComponent<Text> ().text = Util.getDesafiosRealizadosCompletos ();
		txtHorasJogadasSegundoPanel.GetComponent<Text> ().text = Util.getDesafiosRealizadosMediaTempo ();
		txtPrimeiraPergunta.GetComponent<Text> ().text = Util.getDesafiosRealizadosPergunta (Parametros.CD_ETAPA_ATIVIDADE_RACIOCINIO_DIRETO_1);
		txtSegundaPergunta.GetComponent<Text> ().text = Util.getDesafiosRealizadosPergunta (Parametros.CD_ETAPA_ATIVIDADE_RACIOCINIO_DIRETO_2);
		txtTerceiraPergunta.GetComponent<Text> ().text = Util.getDesafiosRealizadosPergunta (Parametros.CD_ETAPA_ATIVIDADE_RACIOCINIO_DIRETO_3);
		txtQuartaPergunta.GetComponent<Text> ().text = Util.getDesafiosRealizadosPergunta (Parametros.CD_ETAPA_ATIVIDADE_RACIOCINIO_DIRETO_4);
		//Terceiro Panel
		txtGalinhasTrocadas.GetComponent<Text> ().text = Util.getItensCapturados (Parametros.CD_OBJETIVO_GANHOU_UMA_GALINHA);
		txtSacosMilhoTrocados.GetComponent<Text> ().text = Util.getItensCapturados (Parametros.CD_OBJETIVO_GANHOU_UM_SACO_MILHO);
		txtPorcosTrocados.GetComponent<Text> ().text = Util.getItensCapturados (Parametros.CD_OBJETIVO_GANHOU_UM_PORCO);
		txtOvelhasTrocadas.GetComponent<Text> ().text = Util.getItensCapturados (Parametros.CD_OBJETIVO_GANHOU_UMA_OVELHA);
		txtCavalosTrocados.GetComponent<Text> ().text = Util.getItensCapturados (Parametros.CD_OBJETIVO_GANHOU_UM_CAVALO);
		txtVacasTrocadas.GetComponent<Text> ().text = Util.getItensCapturados (Parametros.CD_OBJETIVO_GANHOU_UMA_VACA);

	}
	
	// Update is called once per frame
	void Update (){
		base.update ();
	}
}

