using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RegistroQuantidadeItem : Entidade {

	//Atributos
	private int cdRegistro;
	private int cdJogo;
	private int cdPrototipoItem;
	private int qtItem;

	//Construtores
	public RegistroQuantidadeItem(){
	}

	public RegistroQuantidadeItem(int cdRegistro,
		int cdJogo,
		int cdPrototipoItem){
		setCdRegistro (cdRegistro);
		setCdJogo (cdJogo);
		setCdPrototipoItem (cdPrototipoItem);
	}

	public RegistroQuantidadeItem(int cdRegistro,
		int cdJogo,
		int cdPrototipoItem,
		int qtItem){
		setCdRegistro (cdRegistro);
		setCdJogo (cdJogo);
		setCdPrototipoItem (cdPrototipoItem);
		setQtItem (qtItem);

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

	public void setCdPrototipoItem(int cdPrototipoItem){
		this.cdPrototipoItem = cdPrototipoItem;
	}

	public int getCdPrototipoItem(){
		return cdPrototipoItem;
	}

	public void setQtItem(int qtItem){
		this.qtItem = qtItem;
	}

	public int getQtItem(){
		return qtItem;
	}

	//Métodos de Acesso ao Banco de Dados - CRUD
	override public Result save(){
		int ret = 0;
		if (!exists()) {
			ret = Dao.insert ("registro_quantidade_item", getItensDataBase ());
		}
		else
			ret = Dao.update ("registro_quantidade_item", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar salvar registro quantidade item" : "Sucesso ao salvar registro quantidade item"));
	}

	override public Result remove(){
		int ret = Dao.delete("registro_quantidade_item", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar remover registro quantidade item" : "Sucesso ao remover registro quantidade item"));
	}

	override public Result get(){
		object objeto = Dao.get ("registro_quantidade_item", getItensDataBase ());

		Dictionary<string, object> arrayObjeto = (Dictionary<string, object>)objeto;

		if (objeto != null) {
			setCdRegistro(System.Convert.ToInt32(arrayObjeto["cd_registro"]));
			setCdJogo(System.Convert.ToInt32(arrayObjeto["cd_jogo"]));
			setCdPrototipoItem(System.Convert.ToInt32(arrayObjeto["cd_prototipo_item"]));
			if(arrayObjeto["qt_item"] != DBNull.Value)
				setQtItem(System.Convert.ToInt32(arrayObjeto["qt_item"]));
		}

		return new Result ((objeto == null ? -1 : 1), (objeto == null ? "Erro ao tentar buscar registro quantidade item" : "Sucesso ao buscar registro quantidade item"));
	}

	public bool exists(){
		return  Dao.get ("registro_quantidade_item", getItensDataBase ()) != null;
	}

	//Métodos Auxiliares
	public string ToString(){
		return "{cdRegistro=" + getCdRegistro () + "," +
			"cdJogo=" + getCdJogo() + "," +
			"cdPrototipoItem=" + getCdPrototipoItem() + "," +
			"qtItem=" + getQtItem() + "}";
	}

	override public object clone(){
		return new RegistroItem (getCdRegistro (),
			getCdJogo(),
			getCdPrototipoItem(),
			getQtItem ());
	}

	override public ArrayList getItensDataBase(){
		ArrayList itensDataBase = new ArrayList ();

		if(getCdRegistro () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_registro", getCdRegistro ().ToString(), true, false));
		if(getCdJogo () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_jogo", getCdJogo ().ToString(), true, false));
		if(getCdPrototipoItem () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_prototipo_item", getCdPrototipoItem ().ToString(), true, false));
		if(getQtItem () >= 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "qt_item", getQtItem ().ToString(), false, false));

		return itensDataBase;
	}

	//Metodos estáticos
	public static void addPintinhoBD(int qtPintinhos){
		//Adicionado no banco de dados +1 Pintinho do tipo da carta selecionada
		RegistroQuantidadeItem registroQuantidadeItem = Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_PINTINHO);
		registroQuantidadeItem.setQtItem (registroQuantidadeItem.getQtItem() + qtPintinhos);

		Result resultado = registroQuantidadeItem.save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
			return;
		}
	}
}
