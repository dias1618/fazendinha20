using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tutorial : Entidade
{
	//Atributos
	private int cdTutorial;
	private string nmTutorial;

	//Construtores
	public Tutorial(){
	}

	public Tutorial(int cdTutorial){
		setCdTutorial (cdTutorial);
	}

	public Tutorial(int cdTutorial,
		string nmTutorial){
		setCdTutorial (cdTutorial);
		setNmTutorial (nmTutorial);
	}

	//Métodos Acessores
	public void setCdTutorial(int cdTutorial){
		this.cdTutorial = cdTutorial;
	}

	public int getCdTutorial(){
		return cdTutorial;
	}

	public void setNmTutorial(string nmTutorial){
		this.nmTutorial = nmTutorial;
	}

	public string getNmTutorial(){
		return nmTutorial;
	}



	//Métodos de Acesso ao Banco de Dados - CRUD
	override public Result save(){
		int ret = 0;
		if (getCdTutorial () == 0) {
			setCdTutorial (Dao.getNextCode ("tutorial", "cd_tutorial"));
			ret = Dao.insert ("tutorial", getItensDataBase ());
		}
		else
			ret = Dao.update ("tutorial", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar salvar tutorial" : "Sucesso ao salvar tutorial"));
	}

	override public Result remove(){
		int ret = Dao.delete("tutorial", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar remover tutorial" : "Sucesso ao remover tutorial"));
	}

	override public Result get(){
		object objeto = Dao.get ("tutorial", getItensDataBase ());

		Dictionary<string, object> arrayObjeto = (Dictionary<string, object>)objeto;

		if (objeto != null) {
			setCdTutorial(System.Convert.ToInt32(arrayObjeto["cd_tutorial"]));
			if(arrayObjeto["nm_tutorial"] != DBNull.Value)
				setNmTutorial((string)arrayObjeto["nm_tutorial"]);
		}

		return new Result ((objeto == null ? -1 : 1), (objeto == null ? "Erro ao tentar buscar tutorial" : "Sucesso ao buscar tutorial"));
	}

	//Métodos Auxiliares
	public string ToString(){
		return "{cdTipoItem=" + getCdTutorial () + "," +
				"nmTipoItem=" + getNmTutorial () + "}";
	}

	override public object clone(){
		return new TipoItem (getCdTutorial (),
							getNmTutorial ());
	}

	override public ArrayList getItensDataBase(){
		ArrayList itensDataBase = new ArrayList ();

		if(getCdTutorial () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_tutorial", getCdTutorial ().ToString(), true, true));
		if(getNmTutorial () != null && !getNmTutorial ().Equals(""))
			itensDataBase.Add (new ItemDataBase (ItemDataBase.STRING, "nm_tutorial", getNmTutorial ().ToString(), false, false));

		return itensDataBase;
	}

}

