using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Registro : Entidade {
	
	/// <summary>
	/// Padrão Singleton: Utilização de uma única instância de 'Registro' em toda a aplicação
	/// </summary>
	private static Registro registro;

	private ArrayList registroQuantidadeItens;
	/// <summary>
	/// Uma lista com os itens atuais do registro
	/// </summary>
	private ArrayList registroItens;
	/// <summary>
	/// Uma lista com os objetivos atuais do registro
	/// </summary>
	private ArrayList registroObjetivos;
	/// <summary>
	/// Uma lista com os tutoriais atuais do registro
	/// </summary>
	private ArrayList registroTutoriais;


	//Atributos
	private int cdRegistro;
	private int cdJogo;
	private DateTime dtRegistro;
	private DateTime dtUltimoAcesso;
	private string nmJogador;
	private int qtNivel;
	private double qtExperiencia;
	private int qtSegundosJogo;
	private int qtTrocasSemErrar;

	//Construtores
	protected Registro(){
		inicializarListas ();
	}

	protected Registro(int cdRegistro,
					int cdJogo){
		setCdRegistro (cdRegistro);
		setCdJogo (cdJogo);
		inicializarListas ();
	}

	protected Registro(int cdRegistro,
					int cdJogo,
					DateTime dtRegistro,
					DateTime dtUltimoAcesso,
					string nmJogador,
					int qtNivel,
					double qtExperiencia,
					int qtSegundosJogo,
					int qtTrocasSemErrar){
		setCdRegistro (cdRegistro);
		setCdJogo (cdJogo);
		setDtRegistro (dtRegistro);
		setDtUltimoAcesso (dtUltimoAcesso);
		setNmJogador (nmJogador);
		setQtNivel (qtNivel);
		setQtExperiencia (qtExperiencia);
		setQtSegundosJogo (qtSegundosJogo);
		setQtTrocasSemErrar (qtTrocasSemErrar);
		inicializarListas ();
	}

	public void inicializarListas(){
		this.registroItens = new ArrayList ();
		this.registroObjetivos = new ArrayList ();
		this.registroQuantidadeItens = new ArrayList ();
		this.registroTutoriais = new ArrayList ();
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

	public void setDtRegistro(DateTime dtRegistro){
		this.dtRegistro = dtRegistro;
	}

	public DateTime getDtRegistro(){
		return dtRegistro;
	}

	public void setDtUltimoAcesso(DateTime dtUltimoAcesso){
		this.dtUltimoAcesso = dtUltimoAcesso;
	}

	public DateTime getDtUltimoAcesso(){
		return dtUltimoAcesso;
	}

	public void setNmJogador(string nmJogador){
		this.nmJogador = nmJogador;
	}

	public string getNmJogador(){
		return nmJogador;
	}

	public void setQtNivel(int qtNivel){
		this.qtNivel = qtNivel;
	}

	public int getQtNivel(){
		return qtNivel;
	}

	public void setQtExperiencia(double qtExperiencia){
		this.qtExperiencia = qtExperiencia;
	}

	public double getQtExperiencia(){
		return qtExperiencia;
	}

	public void setQtSegundosJogo(int qtSegundosJogo){
		this.qtSegundosJogo = qtSegundosJogo;
	}

	public int getQtSegundosJogo(){
		return qtSegundosJogo;
	}

	public void setQtTrocasSemErrar(int qtTrocasSemErrar){
		this.qtTrocasSemErrar = qtTrocasSemErrar;
	}

	public int getQtTrocasSemErrar(){
		return qtTrocasSemErrar;
	}

	//Métodos de Acesso ao Banco de Dados - CRUD
	override public Result save(){
		int ret = 0;
		if (getCdRegistro () == 0) {
			setCdRegistro (Dao.getNextCode ("registro", "cd_registro"));
			ret = Dao.insert ("registro", getItensDataBase ());
		}
		else
			ret = Dao.update ("registro", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar salvar registro" : "Sucesso ao salvar registro"));
	}

	override public Result remove(){
		int ret = Dao.delete("registro", getItensDataBase ());

		return new Result (ret, (ret <= 0 ? "Erro ao tentar remover registro" : "Sucesso ao remover registro"));
	}

	override public Result get(){
		object objeto = Dao.get ("registro", getItensDataBase ());

		Dictionary<string, object> arrayObjeto = (Dictionary<string, object>)objeto;
		if (objeto != null) {
			setCdRegistro(System.Convert.ToInt32(arrayObjeto["cd_registro"]));
			setCdJogo(System.Convert.ToInt32(arrayObjeto["cd_jogo"]));
			if(arrayObjeto["dt_registro"] != DBNull.Value)
				setDtRegistro(Util.convStringToDateTime((string)arrayObjeto["dt_registro"]));
			if(arrayObjeto["dt_ultimo_acesso"] != DBNull.Value)
				setDtUltimoAcesso(Util.convStringToDateTime((string)arrayObjeto["dt_ultimo_acesso"]));
			if(arrayObjeto["nm_jogador"] != DBNull.Value)
				setNmJogador((string)arrayObjeto["nm_jogador"]);
			if(arrayObjeto["qt_nivel"] != DBNull.Value)
				setQtNivel(System.Convert.ToInt32(arrayObjeto["qt_nivel"]));
			if(arrayObjeto["qt_experiencia"] != DBNull.Value)
				setQtExperiencia(System.Convert.ToDouble(arrayObjeto["qt_experiencia"]));
			if (arrayObjeto ["qt_segundos_jogo"] != DBNull.Value) 
				setQtSegundosJogo (System.Convert.ToInt32 (arrayObjeto ["qt_segundos_jogo"]));			
			if(arrayObjeto["qt_trocas_sem_errar"] != DBNull.Value)
				setQtTrocasSemErrar(System.Convert.ToInt32(arrayObjeto["qt_trocas_sem_errar"]));
		}

		return new Result ((objeto == null ? -1 : 1), (objeto == null ? "Erro ao tentar buscar registro" : "Sucesso ao buscar registro"));
	}

	//Métodos Auxiliares

	public string ToString(){
		return "{cdRegistro=" + getCdRegistro () + "," +
				"cdJogo=" + getCdJogo() + "," +
				"dtRegistro=" + getDtRegistro() + "," +
				"dtUltimoAcesso=" + getDtUltimoAcesso() + "," +
				"nmJogador=" + getNmJogador() + "," +
				"qtNivel=" + getQtNivel() + "," +
				"qtExperiencia=" + getQtExperiencia() + "}";
	}

	override public object clone(){
		return new Registro (getCdRegistro (),
							getCdJogo(),
							getDtRegistro(),
							getDtUltimoAcesso(),
							getNmJogador(),
							getQtNivel(),
							getQtExperiencia (),
							getQtSegundosJogo(),
							getQtTrocasSemErrar());
	}

	override public ArrayList getItensDataBase(){
		ArrayList itensDataBase = new ArrayList ();

		if(getCdRegistro () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_registro", getCdRegistro ().ToString(), true, true));
		if(getCdJogo () > 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "cd_jogo", getCdJogo ().ToString(), true, false));
		if(getDtRegistro () != null)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.TIMESTAMP, "dt_registro", Util.convDateTimeToStringSql(getDtRegistro ()), false, false));
		if(getDtUltimoAcesso () != null)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.TIMESTAMP, "dt_ultimo_acesso", Util.convDateTimeToStringSql(getDtUltimoAcesso ()), false, false));
		if(getNmJogador () != null && !getNmJogador ().Equals(""))
			itensDataBase.Add (new ItemDataBase (ItemDataBase.STRING, "nm_jogador", getNmJogador ().ToString(), false, false));
		if(getQtNivel () >= 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "qt_nivel", getQtNivel ().ToString(), false, false));
		if(getQtExperiencia () >= 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.DOUBLE, "qt_experiencia", getQtExperiencia ().ToString(), false, false));
		if(getQtSegundosJogo () >= 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "qt_segundos_jogo", getQtSegundosJogo().ToString(), false, false));
		if(getQtTrocasSemErrar () >= 0)
			itensDataBase.Add (new ItemDataBase (ItemDataBase.INTEGER, "qt_trocas_sem_errar", getQtTrocasSemErrar().ToString(), false, false));
		


		return itensDataBase;
	}

	//Métodos específicos

	/// <summary>
	/// Padrão Singleton: Cria ou busca a instância única de Registro da aplicação
	/// </summary>
	/// <returns>O registro principal a ser trabalhado.</returns>
	public static Registro getRegistro(){
		return getRegistro (null, false);
	}
	public static Registro getRegistro(bool primeiroRegistro){
		return getRegistro (null, primeiroRegistro);
	}
	public static Registro getRegistro(string nmJogador){
		return getRegistro (nmJogador, false);
	}
	public static Registro getRegistro(string nmJogador, bool primeiroRegistro){
		//TODO: Essa parte do código irá receber o código escolhido de jogos salvos, quando o sistema estiver trabalhando dessa forma
		if (registro == null) {
			registro = new Registro ();
			//TODO: Temporário, pois por enquanto o sistema não está trabalhando com mais de um registro por vez
			ArrayList registros = Dao.getAll("registro");
			foreach (Dictionary<string, object> register in registros) {
				registro.setCdRegistro(System.Convert.ToInt32(register["cd_registro"]));
				registro.setCdJogo (Jogo.getJogo ().getCdJogo ());
			}
			Result resultado = registro.get ();
			if (resultado.getCode () < 0 && primeiroRegistro) {
				registro.setCdRegistro (0);
				registro.setCdJogo (Jogo.getJogo ().getCdJogo ());
				registro.setDtRegistro (System.DateTime.Now);
				registro.setDtUltimoAcesso (System.DateTime.Now);
				registro.setQtExperiencia (0);
				registro.setQtNivel (1);
				registro.setQtSegundosJogo (0);
				registro.setQtTrocasSemErrar (0);
				if (nmJogador != null)
					registro.setNmJogador (nmJogador);

				resultado = registro.save ();
				if (resultado.getCode () < 0) {
					Debug.LogError (resultado.getMessage ());
					return null;	
				}

				//Faz a inicialização dos registros itens do registro atual
				registro.addQuantidadeItem (Parametros.CD_PINTINHO);
				registro.addQuantidadeItem (Parametros.CD_GALINHA);
				registro.addQuantidadeItem (Parametros.CD_SACO_MILHO);
				registro.addQuantidadeItem (Parametros.CD_PORCO);
				registro.addQuantidadeItem (Parametros.CD_OVELHA);
				registro.addQuantidadeItem (Parametros.CD_CAVALO);
				registro.addQuantidadeItem (Parametros.CD_VACA);
				registro.addQuantidadeItem (Parametros.CD_LOTE_TERRA);

				registro.addItem (Parametros.CD_PINTINHO_COMUM_1, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_COMUM_2, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_COMUM_3, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_COMUM_4, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_COMUM_5, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_COMUM_6, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_COMUM_7, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_COMUM_8, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_COMUM_9, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_COMUM_10, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_COMUM_11, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_COMUM_12, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_COMUM_13, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_COMUM_14, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_COMUM_15, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_COMUM_16, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_COMUM_17, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_COMUM_18, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_AZUL_1, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_AZUL_2, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_AZUL_3, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_AZUL_4, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_AZUL_5, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_PINTINHO_VERMELHO_1, Parametros.CD_PINTINHO);
				registro.addItem (Parametros.CD_GALINHA_COMUM_1, Parametros.CD_GALINHA);
				registro.addItem (Parametros.CD_SACO_MILHO_COMUM_1, Parametros.CD_SACO_MILHO);
				registro.addItem (Parametros.CD_PORCO_COMUM_1, Parametros.CD_PORCO);
				registro.addItem (Parametros.CD_OVELHA_COMUM_1, Parametros.CD_OVELHA);
				registro.addItem (Parametros.CD_CAVALO_COMUM_1, Parametros.CD_CAVALO);
				registro.addItem (Parametros.CD_VACA_COMUM_1, Parametros.CD_VACA);
				registro.addItem (Parametros.CD_LOTE_TERRA_COMUM_1, Parametros.CD_LOTE_TERRA);

				//Faz a inicialização dos registros objetivo do registro atual
				registro.addObjetivo (Parametros.CD_OBJETIVO_TROCA_DIRETA);
				registro.addObjetivo (Parametros.CD_OBJETIVO_TROCA_INVERSA);
				registro.addObjetivo (Parametros.CD_OBJETIVO_CAPTUROU_DOIS_PINTINHOS);
				registro.addObjetivo (Parametros.CD_OBJETIVO_GANHOU_UMA_GALINHA);
				registro.addObjetivo (Parametros.CD_OBJETIVO_GANHOU_UM_SACO_MILHO);
				registro.addObjetivo (Parametros.CD_OBJETIVO_GANHOU_UM_PORCO);
				registro.addObjetivo (Parametros.CD_OBJETIVO_GANHOU_UMA_OVELHA);
				registro.addObjetivo (Parametros.CD_OBJETIVO_GANHOU_UM_CAVALO);
				registro.addObjetivo (Parametros.CD_OBJETIVO_GANHOU_UMA_VACA);
				registro.addObjetivo (Parametros.CD_OBJETIVO_CONQUISTOU_UM_LOTE_TERRA);
				registro.addObjetivo (Parametros.CD_OBJETIVO_REALIZOU_TROCA_4_1);
				registro.addObjetivo (Parametros.CD_OBJETIVO_REALIZOU_TROCA_4_2);
				registro.addObjetivo (Parametros.CD_OBJETIVO_REALIZOU_10_SEM_ERRAR);
				registro.addObjetivo (Parametros.CD_OBJETIVO_REALIZOU_30_SEM_ERRAR);
				registro.addObjetivo (Parametros.CD_OBJETIVO_REALIZOU_50_SEM_ERRAR);
				registro.addObjetivo (Parametros.CD_OBJETIVO_CAPTUROU_TODOS_PINTINHOS_NO_TEMPO);

				//Faz a inicialização dos registros tutorial do registro atual
				registro.addTutorial (Parametros.TUTORIAL_JOGO_MEMORIA);
				registro.addTutorial (Parametros.TUTORIAL_MERCADO_TROCA);
				registro.addTutorial (Parametros.TUTORIAL_ATIVIDADES);
				registro.addTutorial (Parametros.TUTORIAL_MINHA_FAZENDA);
				registro.addTutorial (Parametros.TUTORIAL_MUNDO);

			} 
			else if (resultado.getCode () < 0 && !primeiroRegistro) {
				
			}
			else {
				if (nmJogador != null) {
					registro.setNmJogador (nmJogador);
					resultado = registro.save();
					if (resultado.getCode () < 0) {
						Debug.LogError (resultado.getMessage ());
					}
				}

				registro.incluirRegistroItem ();
				registro.incluirRegistroObjetivo ();
				registro.incluirRegistroQuantidadeItem ();
				registro.incluirRegistroTutorial ();
			}
		}

		return registro;
	}

	public static void setRegistro(Registro reg){
		registro = reg;
	}

	public void addQuantidadeItem(int cdPrototipoItem){
		bool contemItem = false;

		foreach (RegistroQuantidadeItem item in this.registroQuantidadeItens) {
			if (item.getCdPrototipoItem () == cdPrototipoItem) {
				contemItem = true;
				break;
			}
		}

		if (!contemItem) {
			RegistroQuantidadeItem registroQuantidadeItem = new RegistroQuantidadeItem (registro.getCdRegistro (), Jogo.getJogo ().getCdJogo (), cdPrototipoItem, 0);
			Result result = registroQuantidadeItem.save ();
			if (result.getCode () < 0) {
				Debug.LogError (result.getMessage ());
				return;
			}
			this.registroQuantidadeItens.Add (registroQuantidadeItem);
		}
	}

	public RegistroQuantidadeItem getRegistroQuantidadeItem(int cdPrototipoItem){
		foreach (RegistroQuantidadeItem registroQuantidadeItem in this.registroQuantidadeItens) {
			if (registroQuantidadeItem.getCdPrototipoItem() == cdPrototipoItem) {
				return registroQuantidadeItem;
			}
		}
		return null;
	}

	public void incluirRegistroQuantidadeItem(){
		ArrayList registros = Dao.getAll ("registro_quantidade_item");
		foreach (Dictionary<string, object> register in registros) {
			if (System.Convert.ToInt32 (register ["cd_registro"]) == getCdRegistro ()) {
				RegistroQuantidadeItem registroQuantidadeItem = new RegistroQuantidadeItem (System.Convert.ToInt32 (register ["cd_registro"]), System.Convert.ToInt32 (register ["cd_jogo"]), System.Convert.ToInt32 (register ["cd_prototipo_item"]));
				Result resultado = registroQuantidadeItem.get ();
				if (resultado.getCode () < 0) {
					Debug.LogError (resultado.getMessage ());
				}

				this.registroQuantidadeItens.Add (registroQuantidadeItem);
			}
		}
	}

	/// <summary>
	/// Adiciona um item no array de itens do registro a partir do código do item
	/// </summary>
	/// <param name="cdItem">O código do item.</param>
	/// <param name="cdPrototipoItem"> Código do Prótotipo do Item. </param>
	public void addItem(int cdItem, int cdPrototipoItem){
		bool contemItem = false;

		foreach (RegistroItem item in this.registroItens) {
			if (item.getCdItem () == cdItem && item.getCdPrototipoItem () == cdPrototipoItem) {
				contemItem = true;
				break;
			}
		}

		if (!contemItem) {
			RegistroItem registroItem = new RegistroItem (registro.getCdRegistro (), Jogo.getJogo ().getCdJogo (), cdItem, cdPrototipoItem, 0);
			Result result = registroItem.save ();
			if (result.getCode () < 0) {
				Debug.LogError (result.getMessage ());
				return;
			}
			this.registroItens.Add (registroItem);
		}
	}

	/// <summary>
	/// Busca o RegistroItem a partir do código do item
	/// </summary>
	/// <returns>O Registro Item.</returns>
	/// <param name="cdItem">código do item.</param>
	/// <param name="cdPrototipoItem"> Código do Prótotipo do Item. </param>
	public RegistroItem getRegistroItem(int cdItem, int cdPrototipoItem){
		foreach (RegistroItem registroItem in this.registroItens) {
			if (registroItem.getCdItem () == cdItem && registroItem.getCdPrototipoItem() == cdPrototipoItem) {
				return registroItem;
			}
		}
		return null;
	}

	public void incluirRegistroItem(){
		ArrayList registros = Dao.getAll ("registro_item");
		foreach (Dictionary<string, object> register in registros) {
			if (System.Convert.ToInt32 (register ["cd_registro"]) == getCdRegistro ()) {
				RegistroItem registroItem = new RegistroItem (System.Convert.ToInt32 (register ["cd_registro"]), System.Convert.ToInt32 (register ["cd_jogo"]), System.Convert.ToInt32 (register ["cd_item"]), System.Convert.ToInt32 (register ["cd_prototipo_item"]));
				Result resultado = registroItem.get ();
				if (resultado.getCode () < 0) {
					Debug.LogError (resultado.getMessage ());
				}

				this.registroItens.Add (registroItem);
			}
		}
	}


	public int getQtAtividadeFeita(){
		int qtAtividades = 0;

		ArrayList registrosAtividade = Dao.getAll ("registro_atividade");
		foreach (Dictionary<string, object> regAtividade in registrosAtividade) {
			if (System.Convert.ToInt32 (regAtividade ["cd_registro"]) == getCdRegistro ()) {
				qtAtividades++;
			}
		}

		return qtAtividades;
	}

	/// <summary>
	/// Busca a quantidade de atividades realizadas corretamente, a partir do código da atividade
	/// </summary>
	/// <returns>A quantidade de atividades realizadas corretamente.</returns>
	/// <param name="cdAtividade">O código da atividade.</param>
	public int getQtAtividadeCorreta(){
		int qtAtividades = 0;

		ArrayList registrosAtividade = Dao.getAll ("registro_atividade");
		foreach (Dictionary<string, object> regAtividade in registrosAtividade) {
			if (System.Convert.ToInt32 (regAtividade ["cd_registro"]) == getCdRegistro ()
				&& System.Convert.ToInt32 (regAtividade ["lg_finalizada"]) == 1) {
				qtAtividades++;
			}
		}

		return qtAtividades;
	}

	public List<RegistroAtividade> getAtividades(){

		List<RegistroAtividade> conjunto = new List<RegistroAtividade> ();

		ArrayList registrosAtividade = Dao.getAll ("registro_atividade");
		foreach (Dictionary<string, object> regAtividade in registrosAtividade) {
			if (System.Convert.ToInt32 (regAtividade ["cd_registro"]) == getCdRegistro ()) {

				RegistroAtividade registroAtividade = new RegistroAtividade (System.Convert.ToInt32 (regAtividade ["cd_registro_atividade"]), System.Convert.ToInt32 (regAtividade ["cd_registro"]), System.Convert.ToInt32 (regAtividade ["cd_jogo"]), System.Convert.ToInt32 (regAtividade ["cd_atividade"]));
				Result resultado = registroAtividade.get ();
				if (resultado.getCode () < 0) {
					Debug.LogError (resultado.getMessage ());
				}

				conjunto.Add (registroAtividade);
			}
		}

		return conjunto;
	}

	public List<RegistroAtividade> getAtividadesCorretas(){

		List<RegistroAtividade> conjunto = new List<RegistroAtividade> ();

		ArrayList registrosAtividade = Dao.getAll ("registro_atividade");
		foreach (Dictionary<string, object> regAtividade in registrosAtividade) {
			if (System.Convert.ToInt32 (regAtividade ["cd_registro"]) == getCdRegistro ()
				&& System.Convert.ToInt32 (regAtividade ["lg_finalizada"]) == 1) {

				RegistroAtividade registroAtividade = new RegistroAtividade (System.Convert.ToInt32 (regAtividade ["cd_registro_atividade"]), System.Convert.ToInt32 (regAtividade ["cd_registro"]), System.Convert.ToInt32 (regAtividade ["cd_jogo"]), System.Convert.ToInt32 (regAtividade ["cd_atividade"]));
				Result resultado = registroAtividade.get ();
				if (resultado.getCode () < 0) {
					Debug.LogError (resultado.getMessage ());
				}

				conjunto.Add (registroAtividade);
			}
		}

		return conjunto;
	}

	public int getQtAtividadeCorretaEtapa(int cdAtividade, int cdEtapaAtividade){
		int qtEtapaAtividades = 0;

		ArrayList registrosEtapaAtividade = Dao.getAll ("registro_etapa_atividade");
		foreach (Dictionary<string, object> regEtapaAtividade in registrosEtapaAtividade) {
			if (System.Convert.ToInt32 (regEtapaAtividade ["cd_registro"]) == getCdRegistro ()
				&& System.Convert.ToInt32 (regEtapaAtividade ["cd_atividade"]) == cdAtividade
				&& System.Convert.ToInt32 (regEtapaAtividade ["cd_etapa_atividade"]) == cdEtapaAtividade
				&& System.Convert.ToInt32 (regEtapaAtividade ["lg_finalizada"]) == 1) {
				qtEtapaAtividades++;
			}
		}

		return qtEtapaAtividades;
	}


	public List<RegistroEtapaAtividade> getAtividadesEtapa(int cdAtividade, int cdEtapaAtividade){

		List<RegistroEtapaAtividade> conjunto = new List<RegistroEtapaAtividade> ();

		ArrayList registrosEtapaAtividade = Dao.getAll ("registro_etapa_atividade");
		foreach (Dictionary<string, object> regEtapaAtividade in registrosEtapaAtividade) {
			if (System.Convert.ToInt32 (regEtapaAtividade ["cd_registro"]) == getCdRegistro ()
				&& System.Convert.ToInt32 (regEtapaAtividade ["cd_atividade"]) == cdAtividade
				&& System.Convert.ToInt32 (regEtapaAtividade ["cd_etapa_atividade"]) == cdEtapaAtividade) {

				RegistroEtapaAtividade registroEtapaAtividade = new RegistroEtapaAtividade (System.Convert.ToInt32 (regEtapaAtividade ["cd_registro_atividade"]), System.Convert.ToInt32 (regEtapaAtividade ["cd_registro"]), System.Convert.ToInt32 (regEtapaAtividade ["cd_jogo"]), System.Convert.ToInt32 (regEtapaAtividade ["cd_atividade"]), System.Convert.ToInt32 (regEtapaAtividade ["cd_etapa_atividade"]));
				Result resultado = registroEtapaAtividade.get ();
				if (resultado.getCode () < 0) {
					Debug.LogError (resultado.getMessage ());
				}

				conjunto.Add (registroEtapaAtividade);
			}
		}

		return conjunto;
	}

	public List<RegistroEtapaAtividade> getAtividadesCorretasEtapa(int cdAtividade, int cdEtapaAtividade){

		List<RegistroEtapaAtividade> conjunto = new List<RegistroEtapaAtividade> ();

		ArrayList registrosEtapaAtividade = Dao.getAll ("registro_etapa_atividade");
		foreach (Dictionary<string, object> regEtapaAtividade in registrosEtapaAtividade) {
			if (System.Convert.ToInt32 (regEtapaAtividade ["cd_registro"]) == getCdRegistro ()
				&& System.Convert.ToInt32 (regEtapaAtividade ["cd_atividade"]) == cdAtividade
				&& System.Convert.ToInt32 (regEtapaAtividade ["cd_etapa_atividade"]) == cdEtapaAtividade
				&& System.Convert.ToInt32 (regEtapaAtividade ["lg_finalizada"]) == 1) {

				RegistroEtapaAtividade registroEtapaAtividade = new RegistroEtapaAtividade (System.Convert.ToInt32 (regEtapaAtividade ["cd_registro_atividade"]), System.Convert.ToInt32 (regEtapaAtividade ["cd_registro"]), System.Convert.ToInt32 (regEtapaAtividade ["cd_jogo"]), System.Convert.ToInt32 (regEtapaAtividade ["cd_atividade"]), System.Convert.ToInt32 (regEtapaAtividade ["cd_etapa_atividade"]));
				Result resultado = registroEtapaAtividade.get ();
				if (resultado.getCode () < 0) {
					Debug.LogError (resultado.getMessage ());
				}

				conjunto.Add (registroEtapaAtividade);
			}
		}

		return conjunto;
	}


	/// <summary>
	/// Busca a quantidade de atividades realizadas incorretamente, a partir do código da atividade
	/// </summary>
	/// <returns>A quantidade de atividades realizadas incorretamente.</returns>
	/// <param name="cdAtividade">O código da atividade.</param>
	public int getQtAtividadeIncorreta(){
		int qtAtividades = 0;

		ArrayList registrosAtividade = Dao.getAll ("registro_atividade");
		foreach (Dictionary<string, object> regAtividade in registrosAtividade) {
			if (System.Convert.ToInt32 (regAtividade ["cd_registro"]) == getCdRegistro ()
				&& System.Convert.ToInt32 (regAtividade ["lg_finalizada"]) == 0) {
				qtAtividades++;
			}
		}

		return qtAtividades;
	}


	/// <summary>
	/// Adiciona um Objetivo no array de objetivos do registro a partir do código do objetivo
	/// </summary>
	/// <param name="cdObjetivo">O código do objetivo.</param>
	public void addObjetivo(int cdObjetivo){

		bool contemObjetivo = false;

		foreach (RegistroObjetivo objetivo in this.registroObjetivos) {
			if (objetivo.getCdObjetivo () == cdObjetivo) {
				contemObjetivo = true;
				break;
			}
		}

		if (!contemObjetivo) {
			RegistroObjetivo registroObjetivo = new RegistroObjetivo (registro.getCdRegistro (), Jogo.getJogo ().getCdJogo (), cdObjetivo, 0, 0);
			Result result = registroObjetivo.save ();
			if (result.getCode () < 0) {
				Debug.LogError (result.getMessage ());
				return;
			}
			this.registroObjetivos.Add (registroObjetivo);
		}

	}

	/// <summary>
	/// Busca o RegistroObjetivo a partir do código do objetivo
	/// </summary>
	/// <returns>O Registro Objetivo.</returns>
	/// <param name="cdItem">código do objetivo.</param>
	public RegistroObjetivo getRegistroObjetivo(int cdObjetivo){
		foreach (RegistroObjetivo registroObjetivo in this.registroObjetivos) {
			if (registroObjetivo.getCdObjetivo () == cdObjetivo) {
				return registroObjetivo;
			}
		}
		return null;
	}

	/// <summary>
	/// Busca a quantidade de objetivos realizados que este registro tem, a partir do código do objetivo
	/// </summary>
	/// <returns>Quantas vezes o registro realizou o objetivo.</returns>
	/// <param name="cdItem">O código do Objetivo.</param>
	public int getQtObjetivo(int cdObjetivo){
		foreach (RegistroObjetivo registroObjetivo in this.registroObjetivos) {
			if (registroObjetivo.getCdObjetivo () == cdObjetivo) {
				return registroObjetivo.getQtVezes ();
			}
		}

		return -1;
	}


	public int getQtObjetivoCumpridos(){
		int qtObjetivosCumpridos = 0;
		foreach (RegistroObjetivo registroObjetivo in this.registroObjetivos) {
			if (registroObjetivo.getQtVezes() > 0) {
				qtObjetivosCumpridos++;
			}
		}

		return qtObjetivosCumpridos;
	}

	public void incluirRegistroObjetivo(){
		ArrayList registros = Dao.getAll ("registro_objetivo");
		foreach (Dictionary<string, object> register in registros) {
			if (System.Convert.ToInt32 (register ["cd_registro"]) == getCdRegistro ()) {
				RegistroObjetivo registroObjetivo = new RegistroObjetivo (System.Convert.ToInt32 (register ["cd_registro"]), System.Convert.ToInt32 (register ["cd_jogo"]), System.Convert.ToInt32 (register ["cd_objetivo"]));
				Result resultado = registroObjetivo.get ();
				if (resultado.getCode () < 0) {
					Debug.LogError (resultado.getMessage ());
				}

				this.registroObjetivos.Add (registroObjetivo);
			}
		}
	}


	/// <summary>
	/// Adiciona um Tutorial no array de tutoriais do registro a partir do código do tutorial
	/// </summary>
	/// <param name="cdTutorial">O código do tutorial.</param>
	public void addTutorial(int cdTutorial){

		bool contemTutorial = false;

		foreach (RegistroTutorial tutorial in this.registroTutoriais) {
			if (tutorial.getCdTutorial () == cdTutorial) {
				contemTutorial = true;
				break;
			}
		}

		if (!contemTutorial) {
			RegistroTutorial registroTutorial = new RegistroTutorial (registro.getCdRegistro (), Jogo.getJogo ().getCdJogo (), cdTutorial, 0);
			Result result = registroTutorial.save ();
			if (result.getCode () < 0) {
				Debug.LogError (result.getMessage ());
				return;
			}
			this.registroTutoriais.Add (registroTutorial);
		}

	}

	/// <summary>
	/// Busca o RegistroTutorial a partir do código do tutorial
	/// </summary>
	/// <returns>O Registro Tutorial.</returns>
	/// <param name="cdItem">código do tutorial.</param>
	public RegistroTutorial getRegistroTutorial(int cdTutorial){
		foreach (RegistroTutorial registroTutorial in this.registroTutoriais) {
			if (registroTutorial.getCdTutorial () == cdTutorial) {
				return registroTutorial;
			}
		}
		return null;
	}

	public void incluirRegistroTutorial(){
		ArrayList registros = Dao.getAll ("registro_tutorial");
		foreach (Dictionary<string, object> register in registros) {
			if (System.Convert.ToInt32 (register ["cd_registro"]) == getCdRegistro ()) {
				RegistroTutorial registroTutorial = new RegistroTutorial (System.Convert.ToInt32 (register ["cd_registro"]), System.Convert.ToInt32 (register ["cd_jogo"]), System.Convert.ToInt32 (register ["cd_tutorial"]));
				Result resultado = registroTutorial.get ();
				if (resultado.getCode () < 0) {
					Debug.LogError (resultado.getMessage ());
				}

				this.registroTutoriais.Add (registroTutorial);
			}
		}
	}





	public bool addExperiencia(double qtExperiencia){
		bool passouNivel = false;

		setQtExperiencia (getQtExperiencia () + qtExperiencia);
		if(getQtExperiencia () >= Jogo.getConfiguracao().getQtTotalExperiencia()){

			Registro.getRegistro ().setQtNivel (Registro.getRegistro ().getQtNivel () + 1);
			Registro.getRegistro ().setQtExperiencia (0);

			passouNivel = true;

		}
		else{

			passouNivel = false;
		}

		Result resultado = Registro.getRegistro ().save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
			return false;
		}

		return passouNivel;
	}
}
