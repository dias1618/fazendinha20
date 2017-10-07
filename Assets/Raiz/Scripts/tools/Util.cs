using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Threading;

public class Util{

	//Ferramentas de Programação

	public static string convDateTimeToString(DateTime data){
		string strData = (data.Day < 10 ? "0" + data.Day : "" + data.Day) 
					   + "/" + (data.Month < 10 ? "0" + data.Month : "" + data.Month) 
			      	   + "/" + (data.Year < 10 ? "0" + data.Year : "" + data.Year) + " " 
					   + (data.Hour < 10 ? "0" + data.Hour : "" + data.Hour) 
					   + ":" + (data.Minute < 10 ? "0" + data.Minute : "" + data.Minute) 
					   + ":" + (data.Second < 10 ? "0" + data.Second : "" + data.Second);
		return strData;
	}

	public static string convDateTimeToStringSql(DateTime data){
		string strData = (data.Year < 10 ? "0" + data.Year : "" + data.Year) 
						+ "-" + (data.Month < 10 ? "0" + data.Month : "" + data.Month) 
						+ "-" + (data.Day < 10 ? "0" + data.Day : "" + data.Day) + " " 
						+ (data.Hour < 10 ? "0" + data.Hour : "" + data.Hour) + ":" 
						+ (data.Minute < 10 ? "0" + data.Minute : "" + data.Minute) + ":" 
						+ (data.Second < 10 ? "0" + data.Second : "" + data.Second);
		return strData;
	}

	public static DateTime convStringToDateTime(string strData){
		return Convert.ToDateTime(strData);
	}


	public static void hasVisible(GameObject gameObject, bool visible){
		gameObject.SetActive(visible);
		foreach(Transform child in gameObject.transform){
			Util.hasVisible(child.gameObject, visible);
		}
	}

	public static Color addColorAlpha(Color color, float value){
		color.a = color.a + value;
		return color;
	}

	public static Color setColorAlpha(Color color, float value){
		color.a = value;
		return color;
	}

	public static bool intPertenceAoArray(int[] conjunto, int numero){
		foreach(int i in conjunto){
			if(numero == i)
				return true;
		}

		return false;
	}

	//Ferramentas de Jogo

	private static float contagemTempo;
	private static Thread threadContagemTempo;
	public static void guardarSegundosJogados(){

		contagemTempo += Time.deltaTime;
		//contagemTempo += DateTime.Now.Ticks;

		//Quando chega em 1 segundo, o contador regressivo decresce, e a contagem de tempo volta a zero
		if(contagemTempo >= 1){
			threadContagemTempo = new Thread(adicionarTempo);
			threadContagemTempo.Start();
			contagemTempo = 0;
		}

	}

	private static void adicionarTempo(){
		Registro.getRegistro().setQtSegundosJogo(Registro.getRegistro().getQtSegundosJogo() + 1);
		Result resultado = Registro.getRegistro ().save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
		}
	}

	public static string getHorasJogadas(){
		int qtHorasJogadas = 0;
		int qtMinutosJogados = 0;
		int qtSegundosJogados = Registro.getRegistro().getQtSegundosJogo();

		while(qtSegundosJogados >= 3600){
			qtHorasJogadas++;
			qtSegundosJogados -= 3600;
		}

		while(qtSegundosJogados >= 60){
			qtMinutosJogados++;
			qtSegundosJogados -= 60;
		}

		return (qtHorasJogadas <= 9 ? "0" + qtHorasJogadas.ToString() : qtHorasJogadas.ToString()) + ":" + (qtMinutosJogados <= 9 ? "0" + qtMinutosJogados.ToString() : qtMinutosJogados.ToString())  + ":" + (qtSegundosJogados <= 9 ? "0" + qtSegundosJogados.ToString() : qtSegundosJogados.ToString());
	}

	public static string getPintinhosCapturados(){
		return Registro.getRegistro ().getRegistroQuantidadeItem (Parametros.CD_PINTINHO).getQtItem () +
			(Registro.getRegistro ().getRegistroQuantidadeItem (Parametros.CD_GALINHA).getQtItem () * 2) +
			(Registro.getRegistro ().getRegistroQuantidadeItem (Parametros.CD_SACO_MILHO).getQtItem () * 4) +
			(Registro.getRegistro ().getRegistroQuantidadeItem (Parametros.CD_PORCO).getQtItem () * 8) +
			(Registro.getRegistro ().getRegistroQuantidadeItem (Parametros.CD_OVELHA).getQtItem () * 16) +
			(Registro.getRegistro ().getRegistroQuantidadeItem (Parametros.CD_CAVALO).getQtItem () * 32) +
			(Registro.getRegistro ().getRegistroQuantidadeItem (Parametros.CD_LOTE_TERRA).getQtItem () * 64) + " pintinhos capturados";
	}

	public static string getItensCapturados(int cdObjetivo){
		RegistroObjetivo registroObjetivo = Registro.getRegistro().getRegistroObjetivo (cdObjetivo);
		return registroObjetivo.getQtVezes() + "/" + (registroObjetivo.getQtVezes() + registroObjetivo.getQtVezesErro()) + " trocas realizadas com sucesso";
	}

	public static string getDesafiosRealizados(){
		return Registro.getRegistro ().getQtAtividadeCorreta () + " desafios realizados";
	}

	public static string getDesafiosRealizadosCompletos(){
		return Registro.getRegistro ().getQtAtividadeCorreta () + "/" + Registro.getRegistro ().getQtAtividadeFeita () + " desafios realizados";
	}


	/// <summary>
	/// Busca o tempo médio usado pelo jogador para realizar os desafios
	/// </summary>
	/// <returns>A média de tempo em forma de texto.</returns>
	public static string getDesafiosRealizadosMediaTempo(){

		//Busca todas as atividades feitas pelo jogador
		List<RegistroAtividade> conjuntoRegistroAtividade = Registro.getRegistro ().getAtividades ();

		int qtMinutosJogados = 0;
		int qtSegundosJogados = 0;

		//Soma todos os tempos marcados de cada atividade
		int tempoQtSegundosJogados = 0;
		foreach (RegistroAtividade registroAtividade in conjuntoRegistroAtividade) {
			tempoQtSegundosJogados += registroAtividade.getQtSegundosJogados ();
		}

		//Tira a média de tempo (em segundos) a partir de uma divisão pela quantidade de atividades buscadas
		qtSegundosJogados = tempoQtSegundosJogados / (conjuntoRegistroAtividade.Count > 0 ? conjuntoRegistroAtividade.Count : 1);

		//Contabiliza os minutos a cada 60 segundos
		while(qtSegundosJogados > 60){
			qtMinutosJogados++;
			qtSegundosJogados -= 60;
		}

		return qtMinutosJogados + "m " + qtSegundosJogados + "s (média)";
	}

	public static string getDesafiosRealizadosPergunta(int cdEtapaAtividade){

		List<RegistroEtapaAtividade> conjuntoRegistroEtapaAtividade = Registro.getRegistro ().getAtividadesEtapa (Parametros.CD_ATIVIDADE_RACIOCINIO_DIRETO, cdEtapaAtividade);

		int qtMinutosJogados = 0;
		int qtSegundosJogados = 0;
		int tempoQtSegundosJogados = 0;

		int qtEtapaAtividadeCorreta = 0;

		foreach (RegistroEtapaAtividade registroEtapaAtividade in conjuntoRegistroEtapaAtividade) {
			tempoQtSegundosJogados += registroEtapaAtividade.getQtSegundosJogados ();

			if (registroEtapaAtividade.getLgFinalizada () == 1) {
				qtEtapaAtividadeCorreta++;
			}
		}

		qtSegundosJogados = tempoQtSegundosJogados / (conjuntoRegistroEtapaAtividade.Count > 0 ? conjuntoRegistroEtapaAtividade.Count : 1);

		while(qtSegundosJogados > 60){
			qtMinutosJogados++;
			qtSegundosJogados -= 60;
		}

		return qtEtapaAtividadeCorreta + "/" + conjuntoRegistroEtapaAtividade.Count + "\n" + qtMinutosJogados + "m " + qtSegundosJogados + "s (média)";
	}

	public static string getDesafiosOcultos(){
		return Registro.getRegistro ().getQtObjetivoCumpridos () + " de 16 desafios ocultos conquistados";
	}
}

