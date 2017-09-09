using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RegistroItem : Entidade {

	//Atributos
	private int cdRegistro;
	private int cdJogo;
	private int cdItem;
	private int cdPrototipoItem;
	private int lgItem;

	//Construtores
	public RegistroItem(){
	}

	public RegistroItem(int cdRegistro,
						int cdJogo,
						int cdItem,
						int cdPrototipoItem){
		setCdRegistro (cdRegistro);
		setCdJogo (cdJogo);
		setCdItem (cdItem);
		setCdPrototipoItem (cdPrototipoItem);
	}

	public RegistroItem(int cdRegistro,
						int cdJogo,
						int cdItem,
						int cdPrototipoItem,
						int lgItem){
		setCdRegistro (cdRegistro);
		setCdJogo (cdJogo);
		setCdItem (cdItem);
		setCdPrototipoItem (cdPrototipoItem);
		setLgItem (lgItem);

	}

	//Métodos Acessores
	public void setCdRegistro(int cdRegistro){
		this.cdRegistro = cdRegistro;
	}

	public int getCdRegistro(){
		return cdRegistro;
	}

	public void setCdJogo(int cdJogo){
		this.cdJogo = cdJogo;
	}

	public int getCdJogo(){
		return cdJogo;
	}


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

	public void setLgItem(int lgItem){
		this.lgItem = lgItem;
	}

	public int getLgItem(){
		return lgItem;
	}

	//Métodos de Acesso ao Banco de Dados - CRUD
	override public Result save(){
		int ret = 0;
		if (!exists()) {
			ret = Dao.insert ("registro_item", getItensDataBase ());
		}
		else
			ret = Dao.update ("registro_item", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar salvar registro item" : "Sucesso ao salvar registro item"));
	}

	override public Result remove(){
		int ret = Dao.delete("registro_item", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar remover registro item" : "Sucesso ao remover registro item"));
	}

	override public Result get(){
		object objeto = Dao.get ("registro_item", getItensDataBase ());

		Dictionary<string, object> arrayObjeto = (Dictionary<string, object>)objeto;

		if (objeto != null) {
			setCdRegistro(System.Convert.ToInt32(arrayObjeto["cd_registro"]));
			setCdJogo(System.Convert.ToInt32(arrayObjeto["cd_jogo"]));
			setCdItem(System.Convert.ToInt32(arrayObjeto["cd_item"]));
			setCdPrototipoItem(System.Convert.ToInt32(arrayObjeto["cd_prototipo_item"]));
			if(arrayObjeto["lg_item"] != DBNull.Value)
				setLgItem(System.Convert.ToInt32(arrayObjeto["lg_item"]));
		}

		return new Result ((objeto == null ? -1 : 1), (objeto == null ? "Erro ao tentar buscar registro item" : "Sucesso ao buscar registro item"));
	}

	public bool exists(){
		return  Dao.get ("registro_item", getItensDataBase ()) != null;
	}

	//Métodos Auxiliares
	public string ToString(){
		return "{cdRegistro=" + getCdRegistro () + "," +
			"cdJogo=" + getCdJogo() + "," +
			"cdItem=" + getCdItem() + "," +
			"cdPrototipoItem=" + getCdPrototipoItem() + "," +
			"lgItem=" + getLgItem() + "}";
	}

	override public object clone(){
		return new RegistroItem (getCdRegistro (),
			getCdJogo(),
			getCdItem(),
			getCdPrototipoItem(),
			getLgItem ());
	}

	override public ArrayList getItensDataBase(){
		ArrayList itensDataBase = new ArrayList ();

		if(getCdRegistro () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_registro", getCdRegistro ().ToString(), true, false));
		if(getCdJogo () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_jogo", getCdJogo ().ToString(), true, false));
		if(getCdItem () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_item", getCdItem ().ToString(), true, false));
		if(getCdPrototipoItem () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_prototipo_item", getCdPrototipoItem ().ToString(), true, false));
		if(getLgItem () >= 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "lg_item", getLgItem ().ToString(), false, false));

		return itensDataBase;
	}


	//Metodos estáticos

	public static void addPintinhoBD(int carta){
		addPintinhoBD (carta, 1);
	}

	public static void addPintinhoBD(int carta, int qtPintinhos){
		//Adicionado no banco de dados +1 Pintinho do tipo da carta selecionada
		RegistroItem registroItemCarta = Registro.getRegistro().getRegistroItem(carta, Parametros.CD_PINTINHO);
		registroItemCarta.setLgItem (1);
		Result resultado = registroItemCarta.save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
			return;
		}
	}

}
