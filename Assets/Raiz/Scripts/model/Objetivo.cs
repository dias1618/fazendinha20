using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Objetivo : Entidade {

	//Atributos
	private int cdObjetivo;
	private int cdJogo;
	private string nmObjetivo;
	private int cdTipoObjetivo;
	private int lgHabilitado;
	private int stObjetivo;
	private int qtExperienciaGanha;

	//Construtores
	public Objetivo(){
	}

	public Objetivo(int cdObjetivo,
					int cdJogo){
		setCdObjetivo (cdObjetivo);
		setCdJogo (cdJogo);
	}

	public Objetivo(int cdObjetivo,
					int cdJogo,
					string nmObjetivo,
					int cdTipoObjetivo,
					int lgHabilitado,
					int stObjetivo,
					int qtExperienciaGanha){
		setCdObjetivo (cdObjetivo);
		setCdJogo (cdJogo);
		setNmObjetivo (nmObjetivo);
		setCdTipoObjetivo (cdTipoObjetivo);
		setLgHabilitado (lgHabilitado);
		setStObjetivo (stObjetivo);
		setQtExperienciaGanha (qtExperienciaGanha);
	}

	//Métodos Acessores
	public void setCdObjetivo(int cdObjetivo){
		this.cdObjetivo = cdObjetivo;
	}

	public int getCdObjetivo(){
		return cdObjetivo;
	}

	public void setCdJogo(int cdJogo){
		this.cdJogo = cdJogo;
	}

	public int getCdJogo(){
		return cdJogo;
	}

	public void setNmObjetivo(string nmObjetivo){
		this.nmObjetivo = nmObjetivo;
	}

	public string getNmObjetivo(){
		return nmObjetivo;
	}

	public void setCdTipoObjetivo(int cdTipoObjetivo){
		this.cdTipoObjetivo = cdTipoObjetivo;
	}

	public int getCdTipoObjetivo(){
		return cdTipoObjetivo;
	}

	public void setLgHabilitado(int lgHabilitado){
		this.lgHabilitado = lgHabilitado;
	}

	public int getLgHabilitado(){
		return lgHabilitado;
	}

	public void setStObjetivo(int stObjetivo){
		this.stObjetivo = stObjetivo;
	}

	public int getStObjetivo(){
		return stObjetivo;
	}

	public void setQtExperienciaGanha(int qtExperienciaGanha){
		this.qtExperienciaGanha = qtExperienciaGanha;
	}

	public int getQtExperienciaGanha(){
		return qtExperienciaGanha;
	}



	//Métodos de Acesso ao Banco de Dados - CRUD
	override public Result save(){
		int ret = 0;
		if (getCdObjetivo () == 0) {
			setCdObjetivo (Dao.getNextCode ("objetivo", "cd_objetivo", getItensDataBase()));
			ret = Dao.insert ("objetivo", getItensDataBase ());
		}
		else
			ret = Dao.update ("objetivo", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar salvar objetivo" : "Sucesso ao salvar objetivo"));
	}

	override public Result remove(){
		int ret = Dao.delete("objetivo", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar remover objetivo" : "Sucesso ao remover objetivo"));
	}

	override public Result get(){
		object objeto = Dao.get ("objetivo", getItensDataBase ());

		Dictionary<string, object> arrayObjeto = (Dictionary<string, object>)objeto;

		if (objeto != null) {
			setCdObjetivo(System.Convert.ToInt32(arrayObjeto["cd_objetivo"]));
			setCdJogo(System.Convert.ToInt32(arrayObjeto["cd_jogo"]));
			if(arrayObjeto["nm_objetivo"] != DBNull.Value)
				setNmObjetivo((string)arrayObjeto["nm_objetivo"]);
			if(arrayObjeto["cd_tipo_objetivo"] != DBNull.Value)
				setCdTipoObjetivo(System.Convert.ToInt32(arrayObjeto["cd_tipo_objetivo"]));
			if(arrayObjeto["lg_habilitado"] != DBNull.Value)
				setLgHabilitado(System.Convert.ToInt32(arrayObjeto["lg_habilitado"]));
			if(arrayObjeto["st_objetivo"] != DBNull.Value)
				setStObjetivo(System.Convert.ToInt32(arrayObjeto["st_objetivo"]));
			if(arrayObjeto["qt_experiencia_ganha"] != DBNull.Value)
				setQtExperienciaGanha(System.Convert.ToInt32(arrayObjeto["qt_experiencia_ganha"]));
		}

		return new Result ((objeto == null ? -1 : 1), (objeto == null ? "Erro ao tentar buscar objetivo" : "Sucesso ao buscar objetivo"));
	}

	//Métodos Auxiliares
	public string ToString(){
		return "{cdObjetivo=" + getCdObjetivo () + "," +
			"cdJogo=" + getCdJogo () + "," +
			"nmObjetivo=" + getNmObjetivo () + "," +
			"cdTipoObjetivo=" + getCdTipoObjetivo () + "," +
			"lgHabilitado=" + getLgHabilitado () + "," +
			"stObjetivo=" + getStObjetivo () + "," +
			"qtExperienciaGanha=" + getQtExperienciaGanha () + "}";
	}

	override public object clone(){
		return new Objetivo (getCdObjetivo (),
			getCdJogo(),
			getNmObjetivo(),
			getCdTipoObjetivo(),
			getLgHabilitado(),
			getStObjetivo(),
			getQtExperienciaGanha ());
	}

	override public ArrayList getItensDataBase(){
		ArrayList itensDataBase = new ArrayList ();

		if(getCdObjetivo () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_objetivo", getCdObjetivo ().ToString(), true, true));
		if(getCdJogo () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_jogo", getCdJogo ().ToString(), true, false));
		if(getNmObjetivo() != null && !getNmObjetivo ().Equals(""))
			itensDataBase.Add (new ItemDataBase (ItemDataBase.STRING, "nm_objetivo", getNmObjetivo ().ToString(), false, false));
		if(getCdTipoObjetivo () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_tipo_objetivo", getCdTipoObjetivo ().ToString(), false, false));
		if(getLgHabilitado () >= 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "lg_habilitado", getLgHabilitado ().ToString(), false, false));
		if(getStObjetivo () >= 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "st_objetivo", getStObjetivo ().ToString(), false, false));
		if(getQtExperienciaGanha () >= 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "qt_experiencia_ganha", getQtExperienciaGanha ().ToString(), false, false));
		
		return itensDataBase;
	}

}
