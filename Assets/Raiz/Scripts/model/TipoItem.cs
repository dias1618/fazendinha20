using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TipoItem : Entidade {

	//Atributos
	private int cdTipoItem;
	private string nmTipoItem;

	//Construtores
	public TipoItem(){
	}

	public TipoItem(int cdTipoItem){
		setCdTipoItem (cdTipoItem);
	}

	public TipoItem(int cdTipoItem,
		string nmTipoItem){
		setCdTipoItem (cdTipoItem);
		setNmTipoItem (nmTipoItem);
	}

	//Métodos Acessores
	public void setCdTipoItem(int cdTipoItem){
		this.cdTipoItem = cdTipoItem;
	}

	public int getCdTipoItem(){
		return cdTipoItem;
	}

	public void setNmTipoItem(string nmTipoItem){
		this.nmTipoItem = nmTipoItem;
	}

	public string getNmTipoItem(){
		return nmTipoItem;
	}



	//Métodos de Acesso ao Banco de Dados - CRUD
	override public Result save(){
		int ret = 0;
		if (getCdTipoItem () == 0) {
			setCdTipoItem (Dao.getNextCode ("tipo_item", "cd_tipo_item"));
			ret = Dao.insert ("tipo_item", getItensDataBase ());
		}
		else
			ret = Dao.update ("tipo_item", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar salvar tipo de item" : "Sucesso ao salvar tipo de item"));
	}

	override public Result remove(){
		int ret = Dao.delete("tipo_item", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar remover tipo de item" : "Sucesso ao remover tipo de item"));
	}

	override public Result get(){
		object objeto = Dao.get ("tipo_item", getItensDataBase ());

		Dictionary<string, object> arrayObjeto = (Dictionary<string, object>)objeto;

		if (objeto != null) {
			setCdTipoItem(System.Convert.ToInt32(arrayObjeto["cd_tipo_item"]));
			if(arrayObjeto["nm_tipo_item"] != DBNull.Value)
				setNmTipoItem((string)arrayObjeto["nm_tipo_item"]);
		}

		return new Result ((objeto == null ? -1 : 1), (objeto == null ? "Erro ao tentar buscar tipo de item" : "Sucesso ao buscar tipo de item"));
	}

	//Métodos Auxiliares
	public string ToString(){
		return "{cdTipoItem=" + getCdTipoItem () + "," +
				"nmTipoItem=" + getNmTipoItem () + "}";
	}

	override public object clone(){
		return new TipoItem (getCdTipoItem (),
						getNmTipoItem ());
	}

	override public ArrayList getItensDataBase(){
		ArrayList itensDataBase = new ArrayList ();

		if(getCdTipoItem () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_tipo_item", getCdTipoItem ().ToString(), true, true));
		if(getNmTipoItem () != null && !getNmTipoItem ().Equals(""))
			itensDataBase.Add (new ItemDataBase (ItemDataBase.STRING, "nm_tipo_item", getNmTipoItem ().ToString(), false, false));
		
		return itensDataBase;
	}

}
