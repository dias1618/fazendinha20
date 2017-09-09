using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RegistroTutorial : Entidade
{
	//Atributos
	private int cdRegistro;
	private int cdJogo;
	private int cdTutorial;
	private int lgExecutado;

	//Construtores
	public RegistroTutorial(){
	}

	public RegistroTutorial(int cdRegistro,
		int cdJogo,
		int cdTutorial){
		setCdRegistro (cdRegistro);
		setCdJogo (cdJogo);
		setCdTutorial (cdTutorial);
	}

	public RegistroTutorial(int cdRegistro,
		int cdJogo,
		int cdTutorial,
		int lgExecutado){
		setCdRegistro (cdRegistro);
		setCdJogo (cdJogo);
		setCdTutorial (cdTutorial);
		setLgExecutado (lgExecutado);

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


	public void setCdTutorial(int cdTutorial){
		this.cdTutorial = cdTutorial;
	}

	public int getCdTutorial(){
		return cdTutorial;
	}

	public void setLgExecutado(int lgExecutado){
		this.lgExecutado = lgExecutado;
	}

	public int getLgExecutado(){
		return lgExecutado;
	}

	//Métodos de Acesso ao Banco de Dados - CRUD
	override public Result save(){
		int ret = 0;
		if (!exists()) {
			ret = Dao.insert ("registro_tutorial", getItensDataBase ());
		}
		else
			ret = Dao.update ("registro_tutorial", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar salvar registro tutorial" : "Sucesso ao salvar registro tutorial"));
	}

	override public Result remove(){
		int ret = Dao.delete("registro_tutorial", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar remover registro tutorial" : "Sucesso ao remover registro tutorial"));
	}

	override public Result get(){
		object objeto = Dao.get ("registro_tutorial", getItensDataBase ());

		Dictionary<string, object> arrayObjeto = (Dictionary<string, object>)objeto;

		if (objeto != null) {
			setCdRegistro(System.Convert.ToInt32(arrayObjeto["cd_registro"]));
			setCdJogo(System.Convert.ToInt32(arrayObjeto["cd_jogo"]));
			setCdTutorial(System.Convert.ToInt32(arrayObjeto["cd_tutorial"]));
			if(arrayObjeto["lg_executado"] != DBNull.Value)
				setLgExecutado(System.Convert.ToInt32(arrayObjeto["lg_executado"]));
		}

		return new Result ((objeto == null ? -1 : 1), (objeto == null ? "Erro ao tentar buscar registro tutorial" : "Sucesso ao buscar registro tutorial"));
	}

	public bool exists(){
		return  Dao.get ("registro_tutorial", getItensDataBase ()) != null;
	}

	//Métodos Auxiliares
	public string ToString(){
		return "{cdRegistro=" + getCdRegistro () + "," +
			"cdJogo=" + getCdJogo() + "," +
			"cdTutorial=" + getCdTutorial() + "," +
			"lgExecutado=" + getLgExecutado() + "}";
	}

	override public object clone(){
		return new RegistroTutorial (getCdRegistro (),
			getCdJogo(),
			getCdTutorial(),
			getLgExecutado ());
	}

	override public ArrayList getItensDataBase(){
		ArrayList itensDataBase = new ArrayList ();

		if(getCdRegistro () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_registro", getCdRegistro ().ToString(), true, false));
		if(getCdJogo () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_jogo", getCdJogo ().ToString(), true, false));
		if(getCdTutorial () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_tutorial", getCdTutorial ().ToString(), true, false));
		if(getLgExecutado () >= 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "lg_executado", getLgExecutado ().ToString(), false, false));

		return itensDataBase;
	}

}

