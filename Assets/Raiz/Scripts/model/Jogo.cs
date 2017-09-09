using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Jogo : Entidade {

	/// <summary>
	/// Padrão Singleton: Utilização de uma única instância de 'Jogo' em toda a aplicação
	/// </summary>
	private static Jogo jogo;

	private static JogoConfiguracao jogoConfiguracao;


	//Atributos
	private int cdJogo;
	private DateTime dtCriacao;
	private int lgAtivo;

	//Construtores
	protected Jogo(){
	}

	protected Jogo(int cdJogo){
		setCdJogo (cdJogo);
	}

	protected Jogo(int cdJogo,
				DateTime dtCriacao,
				int lgAtivo){
		setCdJogo (cdJogo);
		setDtCriacao (dtCriacao);
		setLgAtivo (lgAtivo);
	}

	//Métodos Acessores
	public void setCdJogo(int cdJogo){
		this.cdJogo = cdJogo;
	}

	public int getCdJogo(){
		return cdJogo;
	}

	public void setDtCriacao(DateTime dtCriacao){
		this.dtCriacao = dtCriacao;
	}

	public DateTime getDtCriacao(){
		return dtCriacao;
	}

	public void setLgAtivo(int lgAtivo){
		this.lgAtivo = lgAtivo;
	}

	public int getLgAtivo(){
		return lgAtivo;
	}



	//Métodos de Acesso ao Banco de Dados - CRUD
	override public Result save(){
		int ret = 0;
		if (getCdJogo () == 0) {
			setCdJogo (Dao.getNextCode ("jogo", "cd_jogo"));
			ret = Dao.insert ("jogo", getItensDataBase ());
		}
		else
			ret = Dao.update ("jogo", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar salvar jogo" : "Sucesso ao salvar jogo"));
	}

	override public Result remove(){
		int ret = Dao.delete("jogo", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar remover jogo" : "Sucesso ao remover jogo"));
	}

	override public Result get(){
		object objeto = Dao.get ("jogo", getItensDataBase ());

		Dictionary<string, object> arrayObjeto = (Dictionary<string, object>)objeto;

		if (objeto != null) {
			setCdJogo(System.Convert.ToInt32(arrayObjeto["cd_jogo"]));
			if(arrayObjeto["dt_criacao"] != null)
				setDtCriacao(Util.convStringToDateTime((string)arrayObjeto["dt_criacao"]));
			if(arrayObjeto["lg_ativo"] != null)
				setLgAtivo(System.Convert.ToInt32(arrayObjeto["lg_ativo"]));
		}

		return new Result ((objeto == null ? -1 : 1), (objeto == null ? "Erro ao tentar buscar jogo" : "Sucesso ao buscar jogo"));
	}

	//Métodos Auxiliares
	public string ToString(){
		return "{cdJogo=" + getCdJogo() + "," +
				"dtCriacao=" + getDtCriacao() + "," +
				"lgAtivo=" + getLgAtivo() + "}";
	}

	override public object clone(){
		return new Jogo (getCdJogo (),
						getDtCriacao(),
						getLgAtivo ());
	}

	override public ArrayList getItensDataBase(){
		ArrayList itensDataBase = new ArrayList ();

		if(getCdJogo () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_jogo", getCdJogo ().ToString(), true, true));
		if(getDtCriacao () != null)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.TIMESTAMP, "dt_criacao", Util.convDateTimeToStringSql(getDtCriacao ()), false, false));
		if(getLgAtivo () >= 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "lg_ativo", getLgAtivo ().ToString(), false, false));

		return itensDataBase;
	}

	//Métodos específicos

	/// <summary>
	/// Padrão Singleton: Cria ou busca a instância única de Jogo da aplicação
	/// </summary>
	/// <returns>A instância do Jogo.</returns>
	public static Jogo getJogo(){
		if (jogo == null) {
			jogo = new Jogo ();

			Result resultado = jogo.getAtivo ();
			if (resultado.getCode () < 0) {
				Debug.LogError (resultado.getMessage ());
				return null;	
			}
		}

		return jogo;
	}

	public static JogoConfiguracao getConfiguracao(){
		if(jogoConfiguracao == null){
			jogoConfiguracao = new JogoConfiguracao (Jogo.getJogo ().getCdJogo ());
			Result resultado = jogoConfiguracao.get ();
			if (resultado.getCode () < 0) {
				
				jogoConfiguracao.setVlVolume (0.5F);
				jogoConfiguracao.setQtTempoMemoria (20);
				jogoConfiguracao.setLgAvancarSemEsgotarTempo (0);
				jogoConfiguracao.setQtTotalExperiencia (10);

				int ret = Dao.insert ("jogo_configuracao", jogoConfiguracao.getItensDataBase ());

				if (ret <= 0) {
					Debug.LogError (resultado.getMessage());
					return null;
				}
			}
		}
		return jogoConfiguracao;
	}

	/// <summary>
	/// Busca o Jogo que está ativo atualmente
	/// </summary>
	/// <returns>Um result indicando se o jogo ativo foi buscado com sucesso ou não.</returns>
	public Result getAtivo(){

		ItemComparator itemComparator = new ItemComparator (ItemComparator.INTEGER, "lg_ativo", "1", false, ItemComparator.EQUALS);
		ArrayList itensComparator = new ArrayList ();
		itensComparator.Add (itemComparator);
		ItemTable itemTable = new ItemTable ("jogo", false, itensComparator, null, null);

		object line = Dao.find ("jogo", itemTable);

		ArrayList lineList = (ArrayList)line;

		if (line != null && lineList.Count > 0) {
			//Faz a busca da primeira linha do resultado, pois é considerado que apenas um jogo estará ativo por vez, logo não haverá mais de 1 registro
			Dictionary<string, object> objeto = (Dictionary<string, object>)lineList [0];

			setCdJogo (System.Convert.ToInt32 (objeto ["cd_jogo"]));
			if(objeto ["dt_criacao"] != DBNull.Value)
				setDtCriacao (Util.convStringToDateTime((string)objeto ["dt_criacao"]));
			if(objeto ["lg_ativo"] != DBNull.Value)
				setLgAtivo (System.Convert.ToInt32 (objeto ["lg_ativo"]));
		} 
		else {

			setCdJogo (0);
			setDtCriacao (System.DateTime.Now);
			setLgAtivo (1);

			Result resultado = save ();
			if (resultado.getCode () < 0) {
				Debug.LogError (resultado.getMessage ());
				return resultado;
			}

		}

		return new Result ((line == null ? -1 : 1), (line == null ? "Erro ao tentar buscar jogo" : "Sucesso ao buscar jogo"));
	}

}
