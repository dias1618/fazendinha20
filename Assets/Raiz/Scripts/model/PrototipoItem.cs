using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PrototipoItem : Entidade {

	//Atributos
	private int cdPrototipoItem;
	private string nmPrototipoItem;
	private int cdTipoItem;

	//Construtores
	public PrototipoItem(){
	}

	public PrototipoItem(int cdPrototipoItem){
		setCdPrototipoItem (cdPrototipoItem);
	}

	public PrototipoItem(int cdPrototipoItem,
		string nmPrototipoItem,
		int cdTipoItem){
		setCdPrototipoItem (cdPrototipoItem);
		setNmPrototipoItem (nmPrototipoItem);
		setCdTipoItem (cdTipoItem);
	}

	//Métodos Acessores
	public void setCdPrototipoItem(int cdPrototipoItem){
		this.cdPrototipoItem = cdPrototipoItem;
	}

	public int getCdPrototipoItem(){
		return cdPrototipoItem;
	}

	public void setNmPrototipoItem(string nmPrototipoItem){
		this.nmPrototipoItem = nmPrototipoItem;
	}

	public string getNmPrototipoItem(){
		return nmPrototipoItem;
	}

	public void setCdTipoItem(int cdTipoItem){
		this.cdTipoItem = cdTipoItem;
	}

	public int getCdTipoItem(){
		return cdTipoItem;
	}


	//Métodos de Acesso ao Banco de Dados - CRUD
	override public Result save(){
		int ret = 0;
		if (getCdPrototipoItem () == 0) {
			setCdPrototipoItem (Dao.getNextCode ("prototipo_item", "cd_prototipo_item"));
			ret = Dao.insert ("prototipo_item", getItensDataBase ());
		}
		else
			ret = Dao.update ("prototipo_item", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar salvar prototipo item" : "Sucesso ao salvar prototipo item"));
	}

	override public Result remove(){
		int ret = Dao.delete("prototipo_item", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar remover prototipo item" : "Sucesso ao remover prototipo item"));
	}

	override public Result get(){
		object objeto = Dao.get ("prototipo_item", getItensDataBase ());

		Dictionary<string, object> arrayObjeto = (Dictionary<string, object>)objeto;

		if (objeto != null) {
			setCdPrototipoItem(System.Convert.ToInt32(arrayObjeto["cd_prototipo_item"]));
			if(arrayObjeto["nm_prototipo_item"] != DBNull.Value)
				setNmPrototipoItem((string)arrayObjeto["nm_prototipo_item"]);
			if(arrayObjeto["cd_tipo_item"] != DBNull.Value)
				setCdTipoItem(System.Convert.ToInt32(arrayObjeto["cd_tipo_item"]));
		}

		return new Result ((objeto == null ? -1 : 1), (objeto == null ? "Erro ao tentar buscar prototipo item" : "Sucesso ao buscar prototipo item"));
	}

	//Métodos Auxiliares
	public string ToString(){
		return "{cdPrototipoItem=" + getCdPrototipoItem () + "," +
			"nmPrototipoItem=" + getNmPrototipoItem () + "," +
			"cdTipoItem=" + getCdTipoItem () + "}";
	}

	override public object clone(){
		return new PrototipoItem (getCdPrototipoItem (),
									getNmPrototipoItem (),
									getCdTipoItem ());
	}

	override public ArrayList getItensDataBase(){
		ArrayList itensDataBase = new ArrayList ();

		if(getCdPrototipoItem () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_prototipo_item", getCdPrototipoItem ().ToString(), true, true));
		if(getNmPrototipoItem () != null && !getNmPrototipoItem ().Equals(""))
			itensDataBase.Add (new ItemDataBase (ItemDataBase.STRING, "nm_prototipo_item", getNmPrototipoItem ().ToString(), false, false));
		if(getCdTipoItem () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_tipo_item", getCdTipoItem ().ToString(), false, false));
		
		return itensDataBase;
	}

}
