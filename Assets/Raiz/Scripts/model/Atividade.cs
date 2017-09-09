using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Atividade : Entidade {

	//Atributos
	private int cdAtividade;
	private string nmAtividade;
	private string txtDescricao;
	private int cdTipoAtividade;

	//Construtores
	public Atividade(){
	}

	public Atividade(int cdAtividade){
		setCdAtividade (cdAtividade);
	}

	public Atividade(int cdAtividade,
					string nmAtividade,
					string txtDescricao,
					int cdTipoAtividade){
		setCdAtividade (cdAtividade);
		setNmAtividade (nmAtividade);
		setTxtDescricao (txtDescricao);
		setCdTipoAtividade(cdTipoAtividade);
	}

	//Métodos Acessores
	public void setCdAtividade(int cdAtividade){
		this.cdAtividade = cdAtividade;
	}

	public int getCdAtividade(){
		return cdAtividade;
	}

	public void setNmAtividade(string nmAtividade){
		this.nmAtividade = nmAtividade;
	}

	public string getNmAtividade(){
		return nmAtividade;
	}

	public void setTxtDescricao(string txtDescricao){
		this.txtDescricao = txtDescricao;
	}

	public string getTxtDescricao(){
		return txtDescricao;
	}

	public void setCdTipoAtividade(int cdTipoAtividade){
		this.cdTipoAtividade = cdTipoAtividade;
	}

	public int getCdTipoAtividade(){
		return cdTipoAtividade;
	}

	//Métodos de Acesso ao Banco de Dados - CRUD

	/// <summary>
	/// Método que salvará o objeto de Atividade no banco de dados
	/// </summary>
	override public Result save(){
		int ret = 0;
		//Caso a chave primária esteja vazia, então fará um insert no objeto, colocando a chave gerada no campo
		//caso contrário, fará um update, atualizando os dados no banco
		if (getCdAtividade () == 0) {
			setCdAtividade (Dao.getNextCode ("atividade", "cd_atividade"));
			ret = Dao.insert ("atividade", getItensDataBase ());
		}
		else
			ret = Dao.update ("atividade", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar salvar atividade" : "Sucesso ao salvar atividade"));
	}

	override public Result remove(){
		int ret = Dao.delete("atividade", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar remover atividade" : "Sucesso ao remover atividade"));
	}

	override public Result get(){
		object objeto = Dao.get ("atividade", getItensDataBase ());

		Dictionary<string, object> arrayObjeto = (Dictionary<string, object>)objeto;

		if (objeto != null) {
			setCdAtividade(System.Convert.ToInt32(arrayObjeto["cd_atividade"]));
			if(arrayObjeto["nm_atividade"] != DBNull.Value)
				setNmAtividade((string)arrayObjeto["nm_atividade"]);
			if(arrayObjeto["txt_descricao"] != DBNull.Value)
				setTxtDescricao((string)arrayObjeto["txt_descricao"]);
			if(arrayObjeto["cd_tipo_atividade"] != DBNull.Value)
				setCdTipoAtividade(System.Convert.ToInt32(arrayObjeto["cd_tipo_atividade"]));
		}

		return new Result ((objeto == null ? -1 : 1), (objeto == null ? "Erro ao tentar buscar atividade" : "Sucesso ao buscar atividade"));
	}

	//Métodos Auxiliares
	public string ToString(){
		return "{cdAtividade=" + getCdAtividade () + "," +
				"nmAtividade=" + getNmAtividade () + "," +
				"txtDescricao=" + getTxtDescricao () + "," +
				"cdTipoAtividade=" + getCdTipoAtividade () + "}";
	}

	override public object clone(){
		return new Atividade (getCdAtividade (),
						getNmAtividade(),
						getTxtDescricao(),
						getCdTipoAtividade ());
	}

	override public ArrayList getItensDataBase(){
		ArrayList itensDataBase = new ArrayList ();

		if(getCdAtividade () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_atividade", getCdAtividade ().ToString(), true, true));
		if(getNmAtividade() != null && !getNmAtividade ().Equals(""))
			itensDataBase.Add (new ItemDataBase (ItemDataBase.STRING, "nm_atividade", getNmAtividade ().ToString(), false, false));
		if(getTxtDescricao() != null && !getTxtDescricao ().Equals(""))
			itensDataBase.Add (new ItemDataBase (ItemDataBase.STRING, "txt_descricao", getTxtDescricao ().ToString(), false, false));
		if(getCdTipoAtividade () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_tipo_atividade", getCdTipoAtividade ().ToString(), false, false));

		return itensDataBase;
	}

}
