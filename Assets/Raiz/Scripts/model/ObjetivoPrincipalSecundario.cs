using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjetivoPrincipalSecundario : Entidade {

	//Atributos
	private int cdObjetivoPrincipal;
	private int cdObjetivoSecundario;
	private int cdJogo;

	//Construtores
	public ObjetivoPrincipalSecundario(){
	}

	public ObjetivoPrincipalSecundario(int cdObjetivoPrincipal,
										int cdObjetivoSecundario,
										int cdJogo){
		setCdObjetivoPrincipal (cdObjetivoPrincipal);
		setCdObjetivoSecundario (cdObjetivoSecundario);
		setCdJogo (cdJogo);
	}

	//Métodos Acessores
	public void setCdObjetivoPrincipal(int cdObjetivoPrincipal){
		this.cdObjetivoPrincipal = cdObjetivoPrincipal;
	}

	public int getCdObjetivoPrincipal(){
		return cdObjetivoPrincipal;
	}

	public void setCdObjetivoSecundario(int cdObjetivoSecundario){
		this.cdObjetivoSecundario = cdObjetivoSecundario;
	}

	public int getCdObjetivoSecundario(){
		return cdObjetivoSecundario;
	}

	public void setCdJogo(int cdJogo){
		this.cdJogo = cdJogo;
	}

	public int getCdJogo(){
		return cdJogo;
	}



	//Métodos de Acesso ao Banco de Dados - CRUD
	override public Result save(){
		int ret = 0;
		if (Dao.get("objetivo_principal_secundario", getItensDataBase()) == null) {
			ret = Dao.insert ("objetivo_principal_secundario", getItensDataBase ());
		}
		else
			ret = Dao.update ("objetivo_principal_secundario", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar salvar objetivo-secundario" : "Sucesso ao salvar objetivo-secundarioo"));
	}

	override public Result remove(){
		int ret = Dao.delete("objetivo_principal_secundario", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar remover objetivo-secundario" : "Sucesso ao remover objetivo-secundario"));
	}

	override public Result get(){
		object objeto = Dao.get ("objetivo_principal_secundario", getItensDataBase ());

		Dictionary<string, object> arrayObjeto = (Dictionary<string, object>)objeto;

		if (objeto != null) {
			setCdObjetivoPrincipal(System.Convert.ToInt32(arrayObjeto["cd_objetivo_principal"]));
			setCdObjetivoSecundario(System.Convert.ToInt32(arrayObjeto["cd_objetivo_secundario"]));
			setCdJogo(System.Convert.ToInt32(arrayObjeto["cd_jogo"]));
		}

		return new Result ((objeto == null ? -1 : 1), (objeto == null ? "Erro ao tentar buscar objetivo-secundario" : "Sucesso ao buscar objetivo-secundario"));
	}

	//Métodos Auxiliares
	public string ToString(){
		return "{cdObjetivoPrincipal=" + getCdObjetivoPrincipal() + "," +
				"cdObjetivoSecundario=" + getCdObjetivoSecundario() + "," +
				"cdJogo=" + getCdJogo() + "}";
	}

	override public object clone(){
		return new ObjetivoPrincipalSecundario (getCdObjetivoPrincipal (),
												getCdObjetivoSecundario(),
												getCdJogo());
	}

	override public ArrayList getItensDataBase(){
		ArrayList itensDataBase = new ArrayList ();

		if(getCdObjetivoPrincipal () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_objetivo_principal", getCdObjetivoPrincipal ().ToString(), true, false));
		if(getCdObjetivoSecundario () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_objetivo_secundario", getCdObjetivoSecundario ().ToString(), true, false));
		if(getCdJogo () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_jogo", getCdJogo ().ToString(), true, false));
		
		return itensDataBase;
	}

}
