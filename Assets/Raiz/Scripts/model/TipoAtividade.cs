using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TipoAtividade : Entidade {

	//Atributos
	private int cdTipoAtividade;
	private string nmTipoAtividade;
	private string txtDescricao;

	//Construtores
	public TipoAtividade(){
	}

	public TipoAtividade(int cdTipoAtividade){
		setCdTipoAtividade (cdTipoAtividade);
	}

	public TipoAtividade(int cdTipoAtividade,
						string nmTipoAtividade,
						string txtDescricao){
		setCdTipoAtividade (cdTipoAtividade);
		setNmTipoAtividade (nmTipoAtividade);
		setTxtDescricao (txtDescricao);
	}

	//Métodos Acessores
	public void setCdTipoAtividade(int cdTipoAtividade){
		this.cdTipoAtividade = cdTipoAtividade;
	}

	public int getCdTipoAtividade(){
		return cdTipoAtividade;
	}

	public void setNmTipoAtividade(string nmTipoAtividade){
		this.nmTipoAtividade = nmTipoAtividade;
	}

	public string getNmTipoAtividade(){
		return nmTipoAtividade;
	}

	public void setTxtDescricao(string txtDescricao){
		this.txtDescricao = txtDescricao;
	}

	public string getTxtDescricao(){
		return txtDescricao;
	}



	//Métodos de Acesso ao Banco de Dados - CRUD
	override public Result save(){
		int ret = 0;
		if (getCdTipoAtividade () == 0) {
			setCdTipoAtividade (Dao.getNextCode ("tipo_atividade", "cd_tipo_atividade"));
			ret = Dao.insert ("tipo_atividade", getItensDataBase ());
		}
		else
			ret = Dao.update ("tipo_atividade", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar salvar tipo de atividade" : "Sucesso ao salvar tipo de atividade"));
	}

	override public Result remove(){
		int ret = Dao.delete("tipo_atividade", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar remover tipo de atividade" : "Sucesso ao remover tipo de atividade"));
	}

	override public Result get(){
		object objeto = Dao.get ("tipo_atividade", getItensDataBase ());

		Dictionary<string, object> arrayObjeto = (Dictionary<string, object>)objeto;

		if (objeto != null) {
			setCdTipoAtividade(System.Convert.ToInt32(arrayObjeto["cd_tipo_atividade"]));
			if(arrayObjeto["nm_tipo_atividade"] != DBNull.Value)
				setNmTipoAtividade((string)arrayObjeto["nm_tipo_atividade"]);
			if(arrayObjeto["txt_descricao"] != DBNull.Value)
				setTxtDescricao((string)arrayObjeto["txt_descricao"]);

		}

		return new Result ((objeto == null ? -1 : 1), (objeto == null ? "Erro ao tentar buscar tipo de atividade" : "Sucesso ao buscar tipo de atividade"));
	}

	//Métodos Auxiliares
	public string ToString(){
		return "{cdTipoAtividade=" + getCdTipoAtividade () + "," +
			"nmTipoAtividade=" + getNmTipoAtividade () + "," +
			"txtDescricao=" + getTxtDescricao ()+ "}";
	}

	override public object clone(){
		return new TipoAtividade (getCdTipoAtividade (),
			getNmTipoAtividade(),
			getTxtDescricao());
	}

	override public ArrayList getItensDataBase(){
		ArrayList itensDataBase = new ArrayList ();

		if(getCdTipoAtividade () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_tipo_atividade", getCdTipoAtividade ().ToString(), false, false));
		if(getNmTipoAtividade() != null && !getNmTipoAtividade ().Equals(""))
			itensDataBase.Add (new ItemDataBase (ItemDataBase.STRING, "nm_tipo_atividade", getNmTipoAtividade ().ToString(), false, false));
		if(getTxtDescricao() != null && !getTxtDescricao ().Equals(""))
			itensDataBase.Add (new ItemDataBase (ItemDataBase.STRING, "txt_descricao", getTxtDescricao ().ToString(), false, false));
		

		return itensDataBase;
	}

}
