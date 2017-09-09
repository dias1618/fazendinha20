using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JogoConfiguracao : Entidade {

	//Atributos
	private int cdJogoConfiguracao;
	private double vlVolume;
	private int qtTempoMemoria;
	private int lgAvancarSemEsgotarTempo;
	private int qtTotalExperiencia;

	//Construtores
	public JogoConfiguracao(){
	}

	public JogoConfiguracao(int cdJogoConfiguracao){
		setCdJogoConfiguracao (cdJogoConfiguracao);
	}

	public JogoConfiguracao(int cdJogoConfiguracao,
							double vlVolume,
							int qtTempoMemoria,
							int lgAvancarSemEsgotarTempo,
							int qtTotalExperiencia){
		setCdJogoConfiguracao (cdJogoConfiguracao);
		setVlVolume (vlVolume);
		setQtTempoMemoria (qtTempoMemoria);
		setLgAvancarSemEsgotarTempo (lgAvancarSemEsgotarTempo);
		setQtTotalExperiencia (qtTotalExperiencia);
	}

	//Métodos Acessores
	public void setCdJogoConfiguracao(int cdJogoConfiguracao){
		this.cdJogoConfiguracao = cdJogoConfiguracao;
	}

	public int getCdJogoConfiguracao(){
		return cdJogoConfiguracao;
	}

	public void setVlVolume(double vlVolume){
		this.vlVolume = vlVolume;
	}

	public double getVlVolume(){
		return vlVolume;
	}

	public void setQtTempoMemoria(int qtTempoMemoria){
		this.qtTempoMemoria = qtTempoMemoria;
	}

	public int getQtTempoMemoria(){
		return qtTempoMemoria;
	}

	public void setLgAvancarSemEsgotarTempo(int lgAvancarSemEsgotarTempo){
		this.lgAvancarSemEsgotarTempo = lgAvancarSemEsgotarTempo;
	}

	public int getLgAvancarSemEsgotarTempo(){
		return lgAvancarSemEsgotarTempo;
	}

	public void setQtTotalExperiencia(int qtTotalExperiencia){
		this.qtTotalExperiencia = qtTotalExperiencia;
	}

	public int getQtTotalExperiencia(){
		return qtTotalExperiencia;
	}

	//Métodos de Acesso ao Banco de Dados - CRUD
	override public Result save(){
		int ret = 0;
		if (getCdJogoConfiguracao () == 0) {
			setCdJogoConfiguracao (Dao.getNextCode ("jogo_configuracao", "cd_tipo_item"));
			ret = Dao.insert ("jogo_configuracao", getItensDataBase ());
		}
		else
			ret = Dao.update ("jogo_configuracao", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar salvar configuracao de jogo" : "Sucesso ao salvar configuracao de jogo"));
	}

	override public Result remove(){
		int ret = Dao.delete("jogo_configuracao", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar remover configuracao de jogo" : "Sucesso ao remover configuracao de jogo"));
	}

	override public Result get(){
		object objeto = Dao.get ("jogo_configuracao", getItensDataBase ());

		Dictionary<string, object> arrayObjeto = (Dictionary<string, object>)objeto;

		if (objeto != null) {
			setCdJogoConfiguracao(System.Convert.ToInt32(arrayObjeto["cd_jogo_configuracao"]));
			if(arrayObjeto["cd_jogo_configuracao"] != DBNull.Value)
				setVlVolume(System.Convert.ToDouble(arrayObjeto["vl_volume"]));
			if(arrayObjeto["qt_tempo_memoria"] != DBNull.Value)
				setQtTempoMemoria(System.Convert.ToInt32(arrayObjeto["qt_tempo_memoria"]));
			if(arrayObjeto["lg_avancar_sem_esgotar_tempo"] != DBNull.Value)
				setLgAvancarSemEsgotarTempo(System.Convert.ToInt32(arrayObjeto["lg_avancar_sem_esgotar_tempo"]));
			if(arrayObjeto["qt_total_experiencia"] != DBNull.Value)
				setQtTotalExperiencia(System.Convert.ToInt32(arrayObjeto["qt_total_experiencia"]));
		}

		return new Result ((objeto == null ? -1 : 1), (objeto == null ? "Erro ao tentar buscar configuracao de jogo" : "Sucesso ao buscar configuracao de jogo"));
	}

	//Métodos Auxiliares
	public string ToString(){
		return "{cdJogoConfiguracao=" + getCdJogoConfiguracao () + "," +
				"vlVolume=" + getVlVolume () + "," +
				"qtTempoMemoria=" + getQtTempoMemoria () + "," +
				"lgAvantarSemEsgotarTempo=" + getLgAvancarSemEsgotarTempo () + ","+
				"qtTotalExperiencia=" + getQtTotalExperiencia() + "}";
	}

	override public object clone(){
		return new JogoConfiguracao (getCdJogoConfiguracao (),
									getVlVolume (),
									getQtTempoMemoria (),
									getLgAvancarSemEsgotarTempo (),
									getQtTotalExperiencia());
	}

	override public ArrayList getItensDataBase(){
		ArrayList itensDataBase = new ArrayList ();

		if(getCdJogoConfiguracao () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_jogo_configuracao", getCdJogoConfiguracao ().ToString(), true, false));
		if(getVlVolume () != 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.DOUBLE, "vl_volume", getVlVolume ().ToString(), false, false));
		if(getQtTempoMemoria () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "qt_tempo_memoria", getQtTempoMemoria ().ToString(), false, false));
		if(getLgAvancarSemEsgotarTempo () >= 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "lg_avancar_sem_esgotar_tempo", getLgAvancarSemEsgotarTempo ().ToString(), false, false));
		if(getQtTotalExperiencia () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "qt_total_experiencia", getQtTotalExperiencia ().ToString(), false, false));
		
		return itensDataBase;
	}

}
