using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RegistroObjetivo : Entidade {

	//Atributos
	private int cdRegistro;
	private int cdJogo;
	private int cdObjetivo;
	private int qtVezes;
	private int qtVezesErro;

	//Construtores
	public RegistroObjetivo(){
	}

	public RegistroObjetivo(int cdRegistro,
							int cdJogo,
							int cdObjetivo){
		setCdRegistro (cdRegistro);
		setCdJogo (cdJogo);
		setCdObjetivo (cdObjetivo);
	}

	public RegistroObjetivo(int cdRegistro,
							int cdJogo,
							int cdObjetivo,
							int qtVezes,
							int qtVezesErro){
		setCdRegistro (cdRegistro);
		setCdJogo (cdJogo);
		setCdObjetivo (cdObjetivo);
		setQtVezes (qtVezes);
		setQtVezesErro (qtVezesErro);

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


	public void setCdObjetivo(int cdObjetivo){
		this.cdObjetivo = cdObjetivo;
	}

	public int getCdObjetivo(){
		return cdObjetivo;
	}

	public void setQtVezes(int qtVezes){
		this.qtVezes = qtVezes;
	}

	public int getQtVezes(){
		return qtVezes;
	}

	public void setQtVezesErro(int qtVezesErro){
		this.qtVezesErro = qtVezesErro;
	}

	public int getQtVezesErro(){
		return qtVezesErro;
	}

	//Métodos de Acesso ao Banco de Dados - CRUD
	override public Result save(){
		int ret = 0;
		if (!exists()) {
			ret = Dao.insert ("registro_objetivo", getItensDataBase ());
		}
		else
			ret = Dao.update ("registro_objetivo", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar salvar registro objetivo" : "Sucesso ao salvar registro objetivo"));
	}

	override public Result remove(){
		int ret = Dao.delete("registro_objetivo", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar remover registro objetivo" : "Sucesso ao remover registro objetivo"));
	}

	override public Result get(){
		object objeto = Dao.get ("registro_objetivo", getItensDataBase ());

		Dictionary<string, object> arrayObjeto = (Dictionary<string, object>)objeto;

		if (objeto != null) {
			setCdRegistro(System.Convert.ToInt32(arrayObjeto["cd_registro"]));
			setCdJogo(System.Convert.ToInt32(arrayObjeto["cd_jogo"]));
			setCdObjetivo(System.Convert.ToInt32(arrayObjeto["cd_objetivo"]));
			if(arrayObjeto["qt_vezes"] != DBNull.Value)
				setQtVezes(System.Convert.ToInt32(arrayObjeto["qt_vezes"]));
			if(arrayObjeto["qt_vezes_erro"] != DBNull.Value)
				setQtVezesErro(System.Convert.ToInt32(arrayObjeto["qt_vezes_erro"]));
		}

		return new Result ((objeto == null ? -1 : 1), (objeto == null ? "Erro ao tentar buscar registro objetivo" : "Sucesso ao buscar registro objetivo"));
	}

	public bool exists(){
		return  Dao.get ("registro_objetivo", getItensDataBase ()) != null;
	}

	//Métodos Auxiliares
	public string ToString(){
		return "{cdRegistro=" + getCdRegistro () + "," +
			"cdJogo=" + getCdJogo() + "," +
			"cdObjetivo=" + getCdObjetivo() + "," +
			"qtVezes=" + getQtVezes() + "," +
			"qtVezesErro=" + getQtVezesErro() + "}";
	}

	override public object clone(){
		return new RegistroObjetivo (getCdRegistro (),
			getCdJogo(),
			getCdObjetivo(),
			getQtVezes (),
			getQtVezesErro());
	}

	override public ArrayList getItensDataBase(){
		ArrayList itensDataBase = new ArrayList ();

		if(getCdRegistro () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_registro", getCdRegistro ().ToString(), true, false));
		if(getCdJogo () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_jogo", getCdJogo ().ToString(), true, false));
		if(getCdObjetivo () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_objetivo", getCdObjetivo ().ToString(), true, false));
		if(getQtVezes () >= 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "qt_vezes", getQtVezes ().ToString(), false, false));
		if(getQtVezesErro () >= 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "qt_vezes_erro", getQtVezesErro ().ToString(), false, false));
		
		return itensDataBase;
	}

	//Metodos estáticos

	public static bool addObjetivoBD(int cdObjetivo){
		bool primeiraVez = false;

		RegistroObjetivo registroObjetivo = Registro.getRegistro ().getRegistroObjetivo (cdObjetivo);
		if (registroObjetivo.getQtVezes () == 0) {
			primeiraVez = true;
		}
		registroObjetivo.setQtVezes (registroObjetivo.getQtVezes() + 1);
		Result resultado = registroObjetivo.save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
			return false;
		}

		return primeiraVez;
	}

	public static void addObjetivoErroBD(int cdObjetivo){
		
		RegistroObjetivo registroObjetivo = Registro.getRegistro ().getRegistroObjetivo (cdObjetivo);
		registroObjetivo.setQtVezesErro (registroObjetivo.getQtVezesErro() + 1);
		Result resultado = registroObjetivo.save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
		}

	}

	public static ArrayList getAllByRegistro(int cdRegistro){
		ArrayList registrosObjetivo = Dao.getAll("registro_objetivo");
		ArrayList registrosObjetivoFinal = new ArrayList ();
		foreach (Dictionary<string, object> dicRegistroObjetivo in registrosObjetivo) {
			if (System.Convert.ToInt32(dicRegistroObjetivo ["cd_registro"]) == cdRegistro) {
				RegistroObjetivo registroObjetivo = new RegistroObjetivo(cdRegistro, Jogo.getJogo().getCdJogo(), System.Convert.ToInt32(dicRegistroObjetivo["cd_objetivo"]));
				Result resultado = registroObjetivo.get ();
				if (resultado.getCode () < 0) {
					Debug.LogError (resultado.getMessage ());
					return null;
				}
				registrosObjetivoFinal.Add (registroObjetivo);	
			}
		}

		return registrosObjetivoFinal;
	}

}
