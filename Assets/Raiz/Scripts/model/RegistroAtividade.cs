using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RegistroAtividade : Entidade {

	//Atributos
	private int cdRegistroAtividade;
	private int cdRegistro;
	private int cdJogo;
	private int cdAtividade;
	private int qtSegundosJogados;
	private int lgFinalizada;

	//Construtores
	public RegistroAtividade(){
	}

	public RegistroAtividade(int cdRegistroAtividade,
							int cdRegistro,
							int cdJogo,
							int cdAtividade){
		setCdRegistroAtividade (cdRegistroAtividade);
		setCdRegistro (cdRegistro);
		setCdJogo (cdJogo);
		setCdAtividade (cdAtividade);
	}

	public RegistroAtividade(int cdRegistroAtividade,
							int cdRegistro,
							int cdJogo,
							int cdAtividade,
							int qtSegundosJogados,
							int lgFinalizada){
		setCdRegistroAtividade (cdRegistroAtividade);
		setCdRegistro (cdRegistro);
		setCdJogo (cdJogo);
		setCdAtividade (cdAtividade);
		setQtSegundosJogados (qtSegundosJogados);
		setLgFinalizada (lgFinalizada);

	}

	//Métodos Acessores
	public void setCdRegistroAtividade(int cdRegistroAtividade){
		this.cdRegistroAtividade = cdRegistroAtividade;
	}

	public int getCdRegistroAtividade(){
		return cdRegistroAtividade;
	}

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

	public void setCdAtividade(int cdAtividade){
		this.cdAtividade = cdAtividade;
	}

	public int getCdAtividade(){
		return cdAtividade;
	}

	public void setQtSegundosJogados(int qtSegundosJogados){
		this.qtSegundosJogados = qtSegundosJogados;
	}

	public int getQtSegundosJogados(){
		return qtSegundosJogados;
	}

	public void setLgFinalizada(int lgFinalizada){
		this.lgFinalizada = lgFinalizada;
	}

	public int getLgFinalizada(){
		return lgFinalizada;
	}

	//Métodos de Acesso ao Banco de Dados - CRUD
	override public Result save(){
		int ret = 0;
		if (getCdRegistroAtividade() == 0) {
			setCdRegistroAtividade (Dao.getNextCode ("registro_atividade", "cd_registro_atividade", getItensDataBase()));
			ret = Dao.insert ("registro_atividade", getItensDataBase ());
		}
		else
			ret = Dao.update ("registro_atividade", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar salvar registro atividade" : "Sucesso ao salvar registro atividade"));
	}

	override public Result remove(){
		int ret = Dao.delete("registro_atividade", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar remover registro atividade" : "Sucesso ao remover registro atividade"));
	}

	override public Result get(){
		object objeto = Dao.get ("registro_atividade", getItensDataBase ());

		Dictionary<string, object> arrayObjeto = (Dictionary<string, object>)objeto;

		if (objeto != null) {
			setCdRegistroAtividade(System.Convert.ToInt32(arrayObjeto["cd_registro_atividade"]));
			setCdRegistro(System.Convert.ToInt32(arrayObjeto["cd_registro"]));
			setCdJogo(System.Convert.ToInt32(arrayObjeto["cd_jogo"]));
			setCdAtividade(System.Convert.ToInt32(arrayObjeto["cd_atividade"]));
			if(arrayObjeto["qt_segundos_jogados"] != DBNull.Value)
				setQtSegundosJogados(System.Convert.ToInt32(arrayObjeto["qt_segundos_jogados"]));
			if(arrayObjeto["lg_finalizada"] != DBNull.Value)
				setLgFinalizada(System.Convert.ToInt32(arrayObjeto["lg_finalizada"]));
		}

		return new Result ((objeto == null ? -1 : 1), (objeto == null ? "Erro ao tentar buscar registro atividade" : "Sucesso ao buscar registro atividade"));
	}

	//Métodos Auxiliares
	public string ToString(){
		return "{cdRegistroAtividade=" + getCdRegistroAtividade () + "," +
			"cdRegistro=" + getCdRegistro () + "," +
			"cdJogo=" + getCdJogo() + "," +
			"cdAtividade=" + getCdAtividade() + "," +
			"qtSegundosJogados=" + getQtSegundosJogados() + ","+
			"lgFinalizada="+getLgFinalizada()+"}";
	}

	override public object clone(){
		return new RegistroAtividade (getCdRegistroAtividade (),
								getCdRegistro (),
								getCdJogo(),
								getCdAtividade(),
								getQtSegundosJogados (),
								getLgFinalizada());
	}

	override public ArrayList getItensDataBase(){
		ArrayList itensDataBase = new ArrayList ();

		if(getCdRegistroAtividade () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_registro_atividade", getCdRegistroAtividade ().ToString(), true, true));
		if(getCdRegistro () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_registro", getCdRegistro ().ToString(), true, false));
		if(getCdJogo () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_jogo", getCdJogo ().ToString(), true, false));
		if(getCdAtividade () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_atividade", getCdAtividade ().ToString(), true, false));
		if(getQtSegundosJogados () >= 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "qt_segundos_jogados", getQtSegundosJogados ().ToString(), false, false));
		if(getLgFinalizada() >= 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "lg_finalizada", getLgFinalizada ().ToString(), false, false));

		return itensDataBase;
	}

}
