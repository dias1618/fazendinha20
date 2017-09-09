using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TipoObjetivo : Entidade {

	//Atributos
	private int cdTipoObjetivo;
	private int cdJogo;
	private string nmTipoObjetivo;

	//Construtores
	public TipoObjetivo(){
	}

	public TipoObjetivo(int cdTipoObjetivo,
						int cdJogo){
		setCdTipoObjetivo (cdTipoObjetivo);
		setCdJogo (cdJogo);
	}

	public TipoObjetivo(int cdTipoObjetivo,
						int cdJogo,
						string nmTipoObjetivo){
		setCdTipoObjetivo (cdTipoObjetivo);
		setCdJogo (cdJogo);
		setNmTipoObjetivo (nmTipoObjetivo);
	}

	//Métodos Acessores
	public void setCdTipoObjetivo(int cdTipoObjetivo){
		this.cdTipoObjetivo = cdTipoObjetivo;
	}

	public int getCdTipoObjetivo(){
		return cdTipoObjetivo;
	}

	public void setCdJogo(int cdJogo){
		this.cdJogo = cdJogo;
	}

	public int getCdJogo(){
		return cdJogo;
	}

	public void setNmTipoObjetivo(string nmTipoObjetivo){
		this.nmTipoObjetivo = nmTipoObjetivo;
	}

	public string getNmTipoObjetivo(){
		return nmTipoObjetivo;
	}



	//Métodos de Acesso ao Banco de Dados - CRUD
	override public Result save(){
		int ret = 0;
		if (getCdTipoObjetivo () == 0) {
			setCdTipoObjetivo (Dao.getNextCode ("tipo_objetivo", "cd_tipo_objetivo"));
			ret = Dao.insert ("tipo_objetivo", getItensDataBase ());
		}
		else
			ret = Dao.update ("tipo_objetivo", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar salvar tipo de objetivo" : "Sucesso ao salvar tipo de objetivo"));
	}

	override public Result remove(){
		int ret = Dao.delete("tipo_objetivo", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar remover tipo de objetivo" : "Sucesso ao remover tipo de objetivo"));
	}

	override public Result get(){
		object objeto = Dao.get ("tipo_objetivo", getItensDataBase ());

		Dictionary<string, object> arrayObjeto = (Dictionary<string, object>)objeto;

		if (objeto != null) {
			setCdTipoObjetivo(System.Convert.ToInt32(arrayObjeto["cd_tipo_objetivo"]));
			setCdJogo(System.Convert.ToInt32(arrayObjeto["cd_jogo"]));
			if(arrayObjeto["nm_tipo_objetivo"] != DBNull.Value)
				setNmTipoObjetivo((string)arrayObjeto["nm_tipo_objetivo"]);
		}

		return new Result ((objeto == null ? -1 : 1), (objeto == null ? "Erro ao tentar buscar tipo de objetivo" : "Sucesso ao buscar tipo de objetivo"));
	}

	//Métodos Auxiliares
	public string ToString(){
		return "{cdTipoObjetivo=" + getCdTipoObjetivo () + "," +
				"cdJogo=" + getCdJogo() + "," +
				"nmTipoItem=" + getNmTipoObjetivo() + "}";
	}

	override public object clone(){
		return new TipoObjetivo (getCdTipoObjetivo (),
								getCdJogo(),
								getNmTipoObjetivo ());
	}

	override public ArrayList getItensDataBase(){
		ArrayList itensDataBase = new ArrayList ();

		if(getCdTipoObjetivo () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_tipo_objetivo", getCdTipoObjetivo ().ToString(), true, true));
		if(getCdJogo () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_jogo", getCdJogo ().ToString(), true, false));
		if(getNmTipoObjetivo () != null && !getNmTipoObjetivo ().Equals(""))
			itensDataBase.Add (new ItemDataBase (ItemDataBase.STRING, "nm_tipo_objetivo", getNmTipoObjetivo ().ToString(), false, false));

		return itensDataBase;
	}

}
