using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : Entidade {

	//Atributos
	private int cdItem;
	private int cdPrototipoItem;
	private string nmItem;

	//Construtores
	public Item(){
	}

	public Item(int cdItem,
				int cdPrototipoItem){
		setCdItem (cdItem);
		setCdPrototipoItem (cdPrototipoItem);
	}

	public Item(int cdItem,
				int cdPrototipoItem,
			    string nmItem){
		setCdItem (cdItem);
		setCdPrototipoItem (cdPrototipoItem);
		setNmItem (nmItem);
	}

	//Métodos Acessores
	public void setCdItem(int cdItem){
		this.cdItem = cdItem;
	}

	public int getCdItem(){
		return cdItem;
	}

	public void setCdPrototipoItem(int cdPrototipoItem){
		this.cdPrototipoItem = cdPrototipoItem;
	}

	public int getCdPrototipoItem(){
		return cdPrototipoItem;
	}

	public void setNmItem(string nmItem){
		this.nmItem = nmItem;
	}

	public string getNmItem(){
		return nmItem;
	}


	//Métodos de Acesso ao Banco de Dados - CRUD
	override public Result save(){
		int ret = 0;
		if (getCdItem () == 0) {
			setCdItem (Dao.getNextCode ("item", "cd_item", getItensDataBase()));
			ret = Dao.insert ("item", getItensDataBase ());
		}
		else
			ret = Dao.update ("item", getItensDataBase ());
		
		return new Result (ret, (ret <= 0 ? "Erro ao tentar salvar item" : "Sucesso ao salvar item"));
	}

	override public Result remove(){
		int ret = Dao.delete("item", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar remover item" : "Sucesso ao remover item"));
	}

	override public Result get(){
		object objeto = Dao.get ("item", getItensDataBase ());

		Dictionary<string, object> arrayObjeto = (Dictionary<string, object>)objeto;

		if (objeto != null) {
			setCdItem(System.Convert.ToInt32(arrayObjeto["cd_item"]));
			setCdPrototipoItem(System.Convert.ToInt32(arrayObjeto["cd_prototipo_item"]));
			if(arrayObjeto["nm_item"] != DBNull.Value)
				setNmItem((string)arrayObjeto["nm_item"]);
		}

		return new Result ((objeto == null ? -1 : 1), (objeto == null ? "Erro ao tentar buscar item" : "Sucesso ao buscar item"));
	}

	//Métodos Auxiliares
	public string ToString(){
		return "{cdItem=" + getCdItem () + "," +
				"cdPrototipoItem=" + getCdPrototipoItem() + "," +
				"nmItem=" + getNmItem () + "}";
	}

	override public object clone(){
		return new Item (getCdItem (),
						getCdPrototipoItem(),
						getNmItem ());
	}

	override public ArrayList getItensDataBase(){
		ArrayList itensDataBase = new ArrayList ();

		if(getCdItem () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_item", getCdItem ().ToString(), true, true));
		if(getCdPrototipoItem () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_prototipo_item", getCdPrototipoItem ().ToString(), true, false));
		if(getNmItem () != null && !getNmItem ().Equals(""))
			itensDataBase.Add (new ItemDataBase (ItemDataBase.STRING, "nm_item", getNmItem ().ToString(), false, false));
		
		return itensDataBase;
	}

}
