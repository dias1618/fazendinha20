using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConfiguracaoCartas : Entidade {

	//Atributos
	private int cdConfiguracaoCartas;
	private int cdJogoConfiguracao;
	private double vlPosicaoXInicial;
	private double vlPosicaoYInicial;
	private double vlWidth;
	private double vlHeight;
	private int qtCartas;
	private int qtCartasPorLinha;
	private int qtCartasPorColuna;
	private double vlEspacoEntreCartasX;
	private double vlEspacoEntreCartasY;
	private int qtAcrescimoTempoBase;

	//Construtores
	public ConfiguracaoCartas(){
	}

	public ConfiguracaoCartas(int cdConfiguracaoCartas,
								int cdJogoConfiguracao){
		setCdConfiguracaoCartas (cdConfiguracaoCartas);
		setCdJogoConfiguracao (cdJogoConfiguracao);
	}

	public ConfiguracaoCartas(int cdConfiguracaoCartas,
							int cdJogoConfiguracao,
							double vlPosicaoXInicial,
							double vlPosicaoYInicial,
							double vlWidth,
							double vlHeight,
							int qtCartas,
							int qtCartasPorLinha,
							int qtCartasPorColuna,
							double vlEspacoEntreCartasX,
							double vlEspacoEntreCartasY,
							int qtAcrescimoTempoBase){
		setCdConfiguracaoCartas (cdConfiguracaoCartas);
		setCdJogoConfiguracao (cdJogoConfiguracao);
		setVlPosicaoXInicial (vlPosicaoXInicial);
		setVlPosicaoYInicial (vlPosicaoYInicial);
		setVlWidth (vlWidth);
		setVlHeight (vlHeight);
		setQtCartas (qtCartas);
		setQtCartasLinha (qtCartasPorLinha);
		setQtCartasColuna (qtCartasPorColuna);
		setVlEspacoEntreCartasX (vlEspacoEntreCartasX);
		setVlEspacoEntreCartasY (vlEspacoEntreCartasY);
		setQtAcrescimoTempoBase (qtAcrescimoTempoBase);
	}

	//Métodos Acessores
	public void setCdConfiguracaoCartas(int cdConfiguracaoCartas){
		this.cdConfiguracaoCartas = cdConfiguracaoCartas;
	}

	public int getCdConfiguracaoCartas(){
		return cdConfiguracaoCartas;
	}

	public void setCdJogoConfiguracao(int cdJogoConfiguracao){
		this.cdJogoConfiguracao = cdJogoConfiguracao;
	}

	public int getCdJogoConfiguracao(){
		return cdJogoConfiguracao;
	}

	public void setVlPosicaoXInicial(double vlPosicaoXInicial){
		this.vlPosicaoXInicial = vlPosicaoXInicial;
	}

	public double getVlPosicaoXInicial(){
		return vlPosicaoXInicial;
	}

	public void setVlPosicaoYInicial(double vlPosicaoYInicial){
		this.vlPosicaoYInicial = vlPosicaoYInicial;
	}

	public double getVlPosicaoYInicial(){
		return vlPosicaoYInicial;
	}

	public void setVlWidth(double vlWidth){
		this.vlWidth = vlWidth;
	}

	public double getVlWidth(){
		return vlWidth;
	}

	public void setVlHeight(double vlHeight){
		this.vlHeight = vlHeight;
	}

	public double getVlHeight(){
		return vlHeight;
	}

	public void setQtCartas(int qtCartas){
		this.qtCartas = qtCartas;
	}

	public int getQtCartas(){
		return qtCartas;
	}

	public void setQtCartasLinha(int qtCartasPorLinha){
		this.qtCartasPorLinha = qtCartasPorLinha;
	}

	public int getQtCartasLinha(){
		return qtCartasPorLinha;
	}

	public void setQtCartasColuna(int qtCartasPorColuna){
		this.qtCartasPorColuna = qtCartasPorColuna;
	}

	public int getQtCartasColuna(){
		return qtCartasPorColuna;
	}

	public void setVlEspacoEntreCartasX(double vlEspacoEntreCartasX){
		this.vlEspacoEntreCartasX = vlEspacoEntreCartasX;
	}

	public double getVlEspacoEntreCartasX(){
		return vlEspacoEntreCartasX;
	}

	public void setVlEspacoEntreCartasY(double vlEspacoEntreCartasY){
		this.vlEspacoEntreCartasY = vlEspacoEntreCartasY;
	}

	public double getVlEspacoEntreCartasY(){
		return vlEspacoEntreCartasY;
	}

	public void setQtAcrescimoTempoBase(int qtAcrescimoTempoBase){
		this.qtAcrescimoTempoBase = qtAcrescimoTempoBase;
	}

	public int getQtAcrescimoTempoBase(){
		return qtAcrescimoTempoBase;
	}
	

	//Métodos de Acesso ao Banco de Dados - CRUD
	override public Result save(){
		int ret = 0;
		if (getCdConfiguracaoCartas () == 0) {
			setCdConfiguracaoCartas (Dao.getNextCode ("configuracao_cartas", "cd_etapa_atividade", getItensDataBase()));
			ret = Dao.insert ("configuracao_cartas", getItensDataBase ());
		}
		else
			ret = Dao.update ("configuracao_cartas", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar salvar configuração de cartas" : "Sucesso ao salvar configuração de cartas"));
	}

	override public Result remove(){
		int ret = Dao.delete("configuracao_cartas", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar remover configuração de cartas" : "Sucesso ao remover configuração de cartas"));
	}

	override public Result get(){
		object objeto = Dao.get ("configuracao_cartas", getItensDataBase ());

		Dictionary<string, object> arrayObjeto = (Dictionary<string, object>)objeto;

		if (objeto != null) {
			setCdConfiguracaoCartas(System.Convert.ToInt32(arrayObjeto["cd_configuracao_cartas"]));
			setCdJogoConfiguracao(System.Convert.ToInt32(arrayObjeto["cd_jogo_configuracao"]));
			if(arrayObjeto["vl_posicao_x_inicial"] != DBNull.Value)
				setVlPosicaoXInicial(System.Convert.ToDouble(arrayObjeto["vl_posicao_x_inicial"]));
			if(arrayObjeto["vl_posicao_y_inicial"] != DBNull.Value)
				setVlPosicaoYInicial(System.Convert.ToDouble(arrayObjeto["vl_posicao_y_inicial"]));
			if(arrayObjeto["vl_width"] != DBNull.Value)
				setVlWidth(System.Convert.ToDouble(arrayObjeto["vl_width"]));
			if(arrayObjeto["vl_height"] != DBNull.Value)
				setVlHeight(System.Convert.ToDouble(arrayObjeto["vl_height"]));
			if(arrayObjeto["qt_cartas"] != DBNull.Value)
				setQtCartas(System.Convert.ToInt32(arrayObjeto["qt_cartas"]));
			if(arrayObjeto["qt_cartas_linha"] != DBNull.Value)
				setQtCartasLinha(System.Convert.ToInt32(arrayObjeto["qt_cartas_linha"]));
			if(arrayObjeto["qt_cartas_coluna"] != DBNull.Value)
				setQtCartasColuna(System.Convert.ToInt32(arrayObjeto["qt_cartas_coluna"]));
			if(arrayObjeto["vl_espaco_entre_cartas_x"] != DBNull.Value)
				setVlEspacoEntreCartasX(System.Convert.ToDouble(arrayObjeto["vl_espaco_entre_cartas_x"]));
			if(arrayObjeto["vl_espaco_entre_cartas_y"] != DBNull.Value)
				setVlEspacoEntreCartasY(System.Convert.ToDouble(arrayObjeto["vl_espaco_entre_cartas_y"]));
			if(arrayObjeto["qt_acrescimo_tempo_base"] != DBNull.Value)
				setQtAcrescimoTempoBase(System.Convert.ToInt32(arrayObjeto["qt_acrescimo_tempo_base"]));
		}

		return new Result ((objeto == null ? -1 : 1), (objeto == null ? "Erro ao tentar buscar configuração de cartas" : "Sucesso ao buscar configuração de cartas"));
	}

	//Métodos Auxiliares
	public string ToString(){
		return "{cdConfiguracaoCartas=" + getCdConfiguracaoCartas () + "," +
			"cdJogoConfiguracao=" + getCdJogoConfiguracao () + "," +
			"vlPosicaoXInicial=" + getVlPosicaoXInicial () + "," +
			"vlPosicaoYInicial=" + getVlPosicaoYInicial () + "," +
			"vlWidth=" + getVlWidth () + "," +
			"vlHeight=" + getVlHeight () + "," +
			"qtCartas=" + getQtCartas () + "," +
			"qtCartasPorLinha=" + getQtCartasLinha () + "," +
			"qtCartasPorColuna=" + getQtCartasColuna () + "," +
			"vlEspacoEntreCartasX=" + getVlEspacoEntreCartasX () + "," +
			"vlEspacoEntreCartasY=" + getVlEspacoEntreCartasY () + "," +
			"qtAcrescimoTempoBase=" + getQtAcrescimoTempoBase () + "}";
	}

	override public object clone(){
		return new ConfiguracaoCartas (getCdConfiguracaoCartas (),
										getCdJogoConfiguracao(),
										getVlPosicaoXInicial(),
										getVlPosicaoYInicial(),
										getVlWidth(),
										getVlHeight(),
										getQtCartas(),
										getQtCartasLinha(),
										getQtCartasColuna(),
										getVlEspacoEntreCartasX(),
										getVlEspacoEntreCartasY(),
										getQtAcrescimoTempoBase());
	}

	override public ArrayList getItensDataBase(){
		ArrayList itensDataBase = new ArrayList ();

		if(getCdConfiguracaoCartas () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_configuracao_cartas", getCdConfiguracaoCartas ().ToString(), true, true));
		if(getCdJogoConfiguracao () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_jogo_configuracao", getCdJogoConfiguracao ().ToString(), true, false));
		if(getVlPosicaoXInicial () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.DOUBLE, "vl_posicao_x_inicial", getVlPosicaoXInicial ().ToString(), false, false));
		if(getVlPosicaoYInicial () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.DOUBLE, "vl_posicao_y_inicial", getVlPosicaoYInicial ().ToString(), false, false));
		if(getVlWidth () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.DOUBLE, "vl_width", getVlWidth ().ToString(), false, false));
		if(getVlHeight () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.DOUBLE, "vl_height", getVlHeight ().ToString(), false, false));
		if(getQtCartas () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "qt_cartas", getQtCartas ().ToString(), false, false));
		if(getQtCartasLinha () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "qt_cartas_linha", getQtCartasLinha ().ToString(), false, false));
		if(getQtCartasColuna () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "qt_cartas_coluna", getQtCartasColuna ().ToString(), false, false));
		if(getVlEspacoEntreCartasX () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.DOUBLE, "vl_espaco_entre_cartas_x", getVlEspacoEntreCartasX ().ToString(), false, false));
		if(getVlEspacoEntreCartasY () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.DOUBLE, "vl_espaco_entre_cartas_y", getVlEspacoEntreCartasY ().ToString(), false, false));
		if(getQtAcrescimoTempoBase () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "qt_acrescimo_tempo_base", getQtAcrescimoTempoBase ().ToString(), false, false));

		return itensDataBase;
	}

}
