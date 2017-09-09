using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EtapaAtividade : Entidade {

	//Atributos
	private int cdEtapaAtividade;
	private int cdAtividade;
	private string nmEtapaAtividade;
	private int nrOrdem;
	private string txtDescricao;

	//Construtores
	public EtapaAtividade(){
	}

	public EtapaAtividade(int cdEtapaAtividade,
						int cdAtividade){
		setCdEtapaAtividade (cdEtapaAtividade);
		setCdAtividade (cdAtividade);
	}

	public EtapaAtividade(int cdEtapaAtividade,
						int cdAtividade,
						string nmEtapaAtividade,
						int nrOrdem,
						string txtDescricao){
		setCdEtapaAtividade (cdEtapaAtividade);
		setCdAtividade (cdAtividade);
		setNmEtapaAtividade (nmEtapaAtividade);
		setNrOrdem (nrOrdem);
		setTxtDescricao (txtDescricao);
	}

	//Métodos Acessores
	public void setCdEtapaAtividade(int cdEtapaAtividade){
		this.cdEtapaAtividade = cdEtapaAtividade;
	}

	public int getCdEtapaAtividade(){
		return cdEtapaAtividade;
	}

	public void setCdAtividade(int cdAtividade){
		this.cdAtividade = cdAtividade;
	}

	public int getCdAtividade(){
		return cdAtividade;
	}

	public void setNmEtapaAtividade(string nmEtapaAtividade){
		this.nmEtapaAtividade = nmEtapaAtividade;
	}

	public string getNmEtapaAtividade(){
		return nmEtapaAtividade;
	}

	public void setNrOrdem(int nrOrdem){
		this.nrOrdem = nrOrdem;
	}

	public int getNrOrdem(){
		return nrOrdem;
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
		if (getCdEtapaAtividade () == 0) {
			setCdEtapaAtividade (Dao.getNextCode ("etapa_atividade", "cd_etapa_atividade", getItensDataBase()));
			ret = Dao.insert ("etapa_atividade", getItensDataBase ());
		}
		else
			ret = Dao.update ("etapa_atividade", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar salvar etapa de atividade" : "Sucesso ao salvar etapa de atividade"));
	}

	override public Result remove(){
		int ret = Dao.delete("etapa_atividade", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar remover etapa de atividade" : "Sucesso ao remover etapa de atividade"));
	}

	override public Result get(){
		object objeto = Dao.get ("etapa_atividade", getItensDataBase ());

		Dictionary<string, object> arrayObjeto = (Dictionary<string, object>)objeto;

		if (objeto != null) {
			setCdEtapaAtividade(System.Convert.ToInt32(arrayObjeto["cd_etapa_atividade"]));
			setCdAtividade(System.Convert.ToInt32(arrayObjeto["cd_atividade"]));
			if(arrayObjeto["nm_etapa_atividade"] != DBNull.Value)
				setNmEtapaAtividade((string)arrayObjeto["nm_etapa_atividade"]);
			if(arrayObjeto["nr_ordem"] != DBNull.Value)
				setNrOrdem(System.Convert.ToInt32(arrayObjeto["nr_ordem"]));
			if(arrayObjeto["txt_descricao"] != DBNull.Value)
				setTxtDescricao((string)arrayObjeto["txt_descricao"]);
		}

		return new Result ((objeto == null ? -1 : 1), (objeto == null ? "Erro ao tentar buscar etapa de atividade" : "Sucesso ao buscar etapa de atividade"));
	}

	//Métodos Auxiliares
	public string ToString(){
		return "{cdEtapaAtividade=" + getCdEtapaAtividade () + "," +
			"cdAtividade=" + getCdAtividade () + "," +
			"nmEtapaAtividade=" + getNmEtapaAtividade () + "," +
			"nrOrdem=" + getNrOrdem () + "," +
			"txtDescricao=" + getTxtDescricao () + "}";
	}

	override public object clone(){
		return new EtapaAtividade (getCdEtapaAtividade (),
									getCdAtividade(),
									getNmEtapaAtividade(),
									getNrOrdem(),
									getTxtDescricao());
	}

	override public ArrayList getItensDataBase(){
		ArrayList itensDataBase = new ArrayList ();

		if(getCdEtapaAtividade () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_etapa_atividade", getCdEtapaAtividade ().ToString(), true, true));
		if(getCdAtividade () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_atividade", getCdAtividade ().ToString(), true, false));
		if(getNmEtapaAtividade() != null && !getNmEtapaAtividade ().Equals(""))
			itensDataBase.Add (new ItemDataBase (ItemDataBase.STRING, "nm_etapa_atividade", getNmEtapaAtividade ().ToString(), false, false));
		if(getNrOrdem () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "nr_ordem", getNrOrdem ().ToString(), false, false));
		if(getTxtDescricao() != null && !getTxtDescricao ().Equals(""))
			itensDataBase.Add (new ItemDataBase (ItemDataBase.STRING, "txt_descricao", getTxtDescricao ().ToString(), false, false));

		return itensDataBase;
	}

}
