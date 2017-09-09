using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Dao : MonoBehaviour
{

	/// <summary>
	/// Insere dados no banco com a informação da tabela e os campos e valores, 
	/// que se encontram no array itensDataBase
	/// </summary>
	/// <param name="nmTabela">Nome da tabela onde serão inseridos os dados</param>
	/// <param name="itensDataBase">Array em que se encontram o nome dos campos na 
	/// tabela do banco com seus respectivos valores</param>
	public static int insert(string nmTabela, ArrayList itensDataBase){
		string campos = "";
		string valores = "";

		//Itera sobre o array de itens para montar as strings que representam os campos e os valores, 
		//seguindo a padronização da linguagem SQL
		foreach(ItemDataBase item in itensDataBase){
			campos += item.getField () + ", ";
			valores += "'" + item.getValue () + "', ";
		}

		//Remove os dois últimos caracteres que serão ', ' do último dado passado
		campos = campos.Remove (campos.Length - 2);
		valores = valores.Remove (valores.Length - 2);

		//Monta a String que será executada via SQL
		string sql = "INSERT INTO " + nmTabela + " (" + campos + ") VALUES ("+valores+")";

		//Faz a execução do comando e retorna o resultado
		return DbAccess.executeCommand (sql);
	}


	public static int update(string nmTabela, ArrayList itensDataBase){
		string campoValor = "";
		string chaves = "";

		foreach(ItemDataBase item in itensDataBase){
			campoValor += item.getField () + " = '" + item.getValue() +"', ";
			if(item.isPrimaryKey())
				chaves += item.getField () + " = '" + item.getValue() +"' AND ";
		}

		campoValor = campoValor.Remove (campoValor.Length - 2);
		chaves = chaves.Remove (chaves.Length - 5);

		string sql = "UPDATE " + nmTabela + " SET " + campoValor + " WHERE "+chaves;

		return DbAccess.executeCommand (sql);
	}

	public static int delete(string nmTabela, ArrayList itensDataBase){
		string chaves = "";

		foreach(ItemDataBase item in itensDataBase){
			if(item.isPrimaryKey())
				chaves += item.getField () + " = '" + item.getValue() +"' AND ";
		}

		chaves = chaves.Remove (chaves.Length - 5);

		string sql = "DELETE FROM " + nmTabela + " WHERE "+chaves;

		return DbAccess.executeCommand (sql);
	}

	public static object get(string nmTabela, ArrayList itensDataBase){
		string chaves = "";

		foreach(ItemDataBase item in itensDataBase){
			if(item.isPrimaryKey())
				chaves += item.getField () + " = '" + item.getValue() +"' AND ";
		}

		if(chaves.Length > 5)
			chaves = chaves.Remove (chaves.Length - 5);

		string sql = "SELECT * FROM " + nmTabela + (chaves.Length > 0 ? " WHERE "+chaves : "");

		ArrayList array = DbAccess.returnQuery (sql);
		if (array.Count >= 1) {
			object objeto = array [0];
			return objeto;
		} 
		else
			return null;
	}

	public static ArrayList getAll(string nmTabela){
		string sql = "SELECT * FROM " + nmTabela;

		return DbAccess.returnQuery (sql);
	}

	public static int deleteAll(string nmTabela){
		string sql = "DELETE FROM " + nmTabela;

		return DbAccess.executeCommand (sql);
	}

	public static ArrayList find(string nmTabela, ItemTable itemTable){
		string camposDeRetorno = "";
		string criterios = "";

		if (itemTable.getCamposRetorno () != null) {
			foreach (string campo in itemTable.getCamposRetorno()) {
				camposDeRetorno += campo + ", ";
			}
			camposDeRetorno = camposDeRetorno.Remove (camposDeRetorno.Length - 2);
		}

		foreach(ItemComparator itemComparator in itemTable.getItensComparator()){
			criterios += itemComparator.getField() + " " + ItemComparator.comparators[itemComparator.getCompare()] + " '" +itemComparator.getValue()+ "' AND ";
		}
		criterios = criterios.Remove (criterios.Length - 5);

		string sql = "SELECT " + (camposDeRetorno.Length == 0 ? "*" : camposDeRetorno) + " FROM " + nmTabela + (criterios.Length != 0 ? " WHERE " + criterios : "");

		return DbAccess.returnQuery (sql);
	}

	public static int getNextCode(string table, string nmCode){
		return getNextCode (table, nmCode, null);
	}

	public static int getNextCode(string table, string nmCode, ArrayList itensDataBase){

		int code = 0;

		if (itensDataBase == null) {
			string sql = "SELECT cod_generator FROM Generator WHERE nm_generator = '" + table + "'";
			ArrayList returnQuery = DbAccess.returnQuery (sql);
			foreach (Dictionary<string, object> line in returnQuery) {
				code = System.Convert.ToInt32 (line ["cod_generator"]);

				code++;

				sql = "UPDATE Generator set cod_generator = '" + code + "' WHERE nm_generator  = '" + table + "'";
				DbAccess.executeCommand (sql);
			}
		}

		if (code == 0) {

			string clausulaWhere = "";
			//Usado para incluir as outras chaves primárias para calcular qual será o próximo valor da chave dependente
			if (itensDataBase != null) {
				foreach (ItemDataBase itemDataBase in itensDataBase) {
					if (itemDataBase.isPrimaryKey() && !itemDataBase.getField ().Equals (nmCode)) {
						clausulaWhere += itemDataBase.getField () + " = '" + itemDataBase.getValue () + "' AND ";
					}
				}

				if (!clausulaWhere.Equals ("")) {
					clausulaWhere = clausulaWhere.Remove ((clausulaWhere.Length - 5));
				}
			}

			string sql = "SELECT MAX("+nmCode+") AS next_code FROM "+table + (!clausulaWhere.Equals("") ? " WHERE " + clausulaWhere : "");
			ArrayList returnQuery = DbAccess.returnQuery (sql);
			foreach (Dictionary<string, object> line in returnQuery) {
				if (!(line ["next_code"]).GetType().ToString().Equals("System.DBNull"))
					code = System.Convert.ToInt32 (line ["next_code"]);
				else
					code = 0;
			}

			code++;

			//Somente usará o generator quando a chave for independente
			if (itensDataBase == null) {
				sql = "INSERT INTO Generator VALUES ('" + table + "', '" + code + "')";
				DbAccess.executeCommand (sql);
			}
		}


		return code;
	}

	public static Result inicializarRegistros(){
		ArrayList registroPrototipoItens = Dao.getAll ("Prototipo_Item");
		if (registroPrototipoItens.Count == 0) {
			//Animais
			Dao.insert("prototipo_item", (new PrototipoItem (Parametros.CD_PINTINHO, "Pintinho", Parametros.CD_TIPO_ITEM_ANIMAL)).getItensDataBase());
			Dao.insert("prototipo_item", (new PrototipoItem (Parametros.CD_GALINHA, "Galinha", Parametros.CD_TIPO_ITEM_ANIMAL)).getItensDataBase());
			Dao.insert("prototipo_item", (new PrototipoItem (Parametros.CD_SACO_MILHO, "Saco de Milho", Parametros.CD_TIPO_ITEM_ANIMAL)).getItensDataBase());
			Dao.insert("prototipo_item", (new PrototipoItem (Parametros.CD_PORCO, "Porco", Parametros.CD_TIPO_ITEM_ANIMAL)).getItensDataBase());
			Dao.insert("prototipo_item", (new PrototipoItem (Parametros.CD_OVELHA, "Ovelha", Parametros.CD_TIPO_ITEM_ANIMAL)).getItensDataBase());
			Dao.insert("prototipo_item", (new PrototipoItem (Parametros.CD_CAVALO, "Cavalo", Parametros.CD_TIPO_ITEM_ANIMAL)).getItensDataBase());
			Dao.insert("prototipo_item", (new PrototipoItem (Parametros.CD_VACA, "Vaca", Parametros.CD_TIPO_ITEM_ANIMAL)).getItensDataBase());
			Dao.insert("prototipo_item", (new PrototipoItem (Parametros.CD_LOTE_TERRA, "Lote de Terra", Parametros.CD_TIPO_ITEM_ANIMAL)).getItensDataBase());
		}

		ArrayList registroTipoItens = Dao.getAll ("Tipo_Item");
		if (registroTipoItens.Count == 0) {
			Dao.insert("tipo_item", (new TipoItem (Parametros.CD_TIPO_ITEM_ANIMAL, "Animal")).getItensDataBase());
		}

		ArrayList registroItens = Dao.getAll ("Item");
		if (registroItens.Count == 0) {
			//Pintinhos
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_COMUM_1, Parametros.CD_PINTINHO, "Pintinho Comum 1")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_COMUM_2, Parametros.CD_PINTINHO, "Pintinho Comum 2")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_COMUM_3, Parametros.CD_PINTINHO, "Pintinho Comum 3")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_COMUM_4, Parametros.CD_PINTINHO, "Pintinho Comum 4")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_COMUM_5, Parametros.CD_PINTINHO, "Pintinho Comum 5")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_COMUM_6, Parametros.CD_PINTINHO, "Pintinho Comum 6")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_COMUM_7, Parametros.CD_PINTINHO, "Pintinho Comum 7")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_COMUM_8, Parametros.CD_PINTINHO, "Pintinho Comum 8")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_COMUM_9, Parametros.CD_PINTINHO, "Pintinho Comum 9")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_COMUM_10, Parametros.CD_PINTINHO, "Pintinho Comum 10")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_COMUM_11, Parametros.CD_PINTINHO, "Pintinho Comum 11")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_COMUM_12, Parametros.CD_PINTINHO, "Pintinho Comum 12")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_COMUM_13, Parametros.CD_PINTINHO, "Pintinho Comum 13")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_COMUM_14, Parametros.CD_PINTINHO, "Pintinho Comum 14")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_COMUM_15, Parametros.CD_PINTINHO, "Pintinho Comum 15")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_COMUM_16, Parametros.CD_PINTINHO, "Pintinho Comum 16")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_COMUM_17, Parametros.CD_PINTINHO, "Pintinho Comum 17")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_COMUM_18, Parametros.CD_PINTINHO, "Pintinho Comum 18")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_AZUL_1, Parametros.CD_PINTINHO, "Pintinho Azul 1")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_AZUL_2, Parametros.CD_PINTINHO, "Pintinho Azul 2")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_AZUL_3, Parametros.CD_PINTINHO, "Pintinho Azul 3")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_AZUL_4, Parametros.CD_PINTINHO, "Pintinho Azul 4")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_AZUL_5, Parametros.CD_PINTINHO, "Pintinho Azul 5")).getItensDataBase());
			Dao.insert("item", (new Item (Parametros.CD_PINTINHO_VERMELHO_1, Parametros.CD_PINTINHO, "Pintinho Vermelho 1")).getItensDataBase());

			Dao.insert("item", (new Item (Parametros.CD_GALINHA_COMUM_1, Parametros.CD_GALINHA, "Galinha Comum 1")).getItensDataBase());

			Dao.insert("item", (new Item (Parametros.CD_SACO_MILHO_COMUM_1, Parametros.CD_SACO_MILHO, "Saco de Milho Comum 1")).getItensDataBase());

			Dao.insert("item", (new Item (Parametros.CD_PORCO_COMUM_1, Parametros.CD_PORCO, "Porco Comum 1")).getItensDataBase());

			Dao.insert("item", (new Item (Parametros.CD_OVELHA_COMUM_1, Parametros.CD_OVELHA, "Ovelha Comum 1")).getItensDataBase());

			Dao.insert("item", (new Item (Parametros.CD_CAVALO_COMUM_1, Parametros.CD_CAVALO, "Cavalo Comum 1")).getItensDataBase());

			Dao.insert("item", (new Item (Parametros.CD_VACA_COMUM_1, Parametros.CD_VACA, "Vaca Comum 1")).getItensDataBase());

			Dao.insert("item", (new Item (Parametros.CD_LOTE_TERRA_COMUM_1, Parametros.CD_LOTE_TERRA, "Lote de Terra Comum 1")).getItensDataBase());


		}

		ArrayList registroTiposAtividade = Dao.getAll ("Tipo_Atividade");
		if (registroTiposAtividade.Count == 0) {
			Dao.insert("tipo_atividade", (new TipoAtividade (Parametros.CD_TIPO_ATIVIDADE_PENSAMENTO_DIRETO, "Pensamento Direto", 
															"Trabalha com as divisões. Onde a troca das mercadorias de menor valor pelas " +
															"de maior valor, dois por um, abrange a noção de divisão dos números " +
															"binários, utilizando as mesmas regras das divisões de números decimais, " +
															"porém de forma mais simples.")).getItensDataBase());

			Dao.insert("tipo_atividade", (new TipoAtividade (Parametros.CD_TIPO_ATIVIDADE_PENSAMENTO_REVERSIVEL, "Pensamento Reversível", 
															"Trabalha com as multiplicações e adições. Onde a troca" +
															"das mercadorias de maior valor pelas de menor valor, dois por um, abrange " +
															"a noção de multiplicação e adição dos números binários, utilizando as mesmas" +
															"regras das multiplicações e adições de números decimais, porém de forma mais simples.")).getItensDataBase());
			
			Dao.insert("tipo_atividade", (new TipoAtividade (Parametros.CD_TIPO_ATIVIDADE_SUBTRACAO, "Subtração", "A subtração é o conceito " +
															"mais fora do comum em relação às outras operações, pois é a retirada de uma parte" +
															"menor de outra maior, fazendo sucessívas trocas para se chegar ao nível de poder" +
															"retirar o que é devido. Assim o sistema de mercadorias se encaixa de uma ótima" +
															"forma para realizar as operações de forma binária e ajudar os alunos a entenderem" +
															"os algoritmos decimais.")).getItensDataBase());
			
		}


		ArrayList registroTipoObjetivos = Dao.getAll ("Tipo_Objetivo");
		if (registroTipoObjetivos.Count == 0) {
			Dao.insert ("tipo_objetivo", (new TipoObjetivo (Parametros.CD_TIPO_OBJETIVO_PEDAGOGICO, Jogo.getJogo ().getCdJogo (), "Pedagógico")).getItensDataBase ());
			Dao.insert ("tipo_objetivo", (new TipoObjetivo (Parametros.CD_TIPO_OBJETIVO_LUDICO, Jogo.getJogo ().getCdJogo (), "Lúdico")).getItensDataBase ());
			Dao.insert ("tipo_objetivo", (new TipoObjetivo (Parametros.CD_TIPO_OBJETIVO_MATEMATICO, Jogo.getJogo ().getCdJogo (), "Matemático")).getItensDataBase ());
		}

		ArrayList registroObjetivos = Dao.getAll ("Objetivo");
		if (registroObjetivos.Count == 0) {
			Dao.insert("objetivo", (new Objetivo(Parametros.CD_OBJETIVO_TROCA_DIRETA, Jogo.getJogo().getCdJogo(), "Troca Direta", Parametros.CD_TIPO_OBJETIVO_PEDAGOGICO, 1, 1, 1)).getItensDataBase());
			Dao.insert("objetivo", (new Objetivo(Parametros.CD_OBJETIVO_TROCA_INVERSA, Jogo.getJogo().getCdJogo(), "Troca Inversa", Parametros.CD_TIPO_OBJETIVO_PEDAGOGICO, 1, 1, 1)).getItensDataBase());
			Dao.insert("objetivo", (new Objetivo(Parametros.CD_OBJETIVO_CAPTUROU_DOIS_PINTINHOS, Jogo.getJogo().getCdJogo(), "Capturou dois pintinhos", Parametros.CD_TIPO_OBJETIVO_LUDICO, 1, 1, 1)).getItensDataBase());
			Dao.insert("objetivo", (new Objetivo(Parametros.CD_OBJETIVO_GANHOU_UMA_GALINHA, Jogo.getJogo().getCdJogo(), "Ganhou uma galinha", Parametros.CD_TIPO_OBJETIVO_MATEMATICO, 1, 1, 1)).getItensDataBase());
			Dao.insert("objetivo", (new Objetivo(Parametros.CD_OBJETIVO_GANHOU_UM_SACO_MILHO, Jogo.getJogo().getCdJogo(), "Ganhou um saco de milho", Parametros.CD_TIPO_OBJETIVO_MATEMATICO, 1, 1, 1)).getItensDataBase());
			Dao.insert("objetivo", (new Objetivo(Parametros.CD_OBJETIVO_GANHOU_UM_PORCO, Jogo.getJogo().getCdJogo(), "Ganhou um porco", Parametros.CD_TIPO_OBJETIVO_MATEMATICO, 1, 1, 1)).getItensDataBase());
			Dao.insert("objetivo", (new Objetivo(Parametros.CD_OBJETIVO_GANHOU_UMA_OVELHA, Jogo.getJogo().getCdJogo(), "Ganhou uma ovelha", Parametros.CD_TIPO_OBJETIVO_MATEMATICO, 1, 1, 1)).getItensDataBase());
			Dao.insert("objetivo", (new Objetivo(Parametros.CD_OBJETIVO_GANHOU_UMA_VACA, Jogo.getJogo().getCdJogo(), "Ganhou uma vaca", Parametros.CD_TIPO_OBJETIVO_MATEMATICO, 1, 1, 1)).getItensDataBase());
			Dao.insert("objetivo", (new Objetivo(Parametros.CD_OBJETIVO_GANHOU_UM_CAVALO, Jogo.getJogo().getCdJogo(), "Ganhou um cavalo", Parametros.CD_TIPO_OBJETIVO_MATEMATICO, 1, 1, 1)).getItensDataBase());
			Dao.insert("objetivo", (new Objetivo(Parametros.CD_OBJETIVO_CONQUISTOU_UM_LOTE_TERRA, Jogo.getJogo().getCdJogo(), "Conquistou um lote de terra", Parametros.CD_TIPO_OBJETIVO_MATEMATICO, 1, 1, 1)).getItensDataBase());
			Dao.insert("objetivo", (new Objetivo(Parametros.CD_OBJETIVO_REALIZOU_TROCA_4_1, Jogo.getJogo().getCdJogo(), "Realizou uma troca 4 por 1", Parametros.CD_TIPO_OBJETIVO_PEDAGOGICO, 1, 1, 1)).getItensDataBase());
			Dao.insert("objetivo", (new Objetivo(Parametros.CD_OBJETIVO_REALIZOU_TROCA_4_2, Jogo.getJogo().getCdJogo(), "Realizou uma troca 4 por 2", Parametros.CD_TIPO_OBJETIVO_PEDAGOGICO, 1, 1, 1)).getItensDataBase());
			Dao.insert("objetivo", (new Objetivo(Parametros.CD_OBJETIVO_REALIZOU_10_SEM_ERRAR, Jogo.getJogo().getCdJogo(), "Realizou 10 trocas sem errar", Parametros.CD_TIPO_OBJETIVO_MATEMATICO, 1, 1, 1)).getItensDataBase());
			Dao.insert("objetivo", (new Objetivo(Parametros.CD_OBJETIVO_REALIZOU_30_SEM_ERRAR, Jogo.getJogo().getCdJogo(), "Realizou 30 trocas sem errar", Parametros.CD_TIPO_OBJETIVO_MATEMATICO, 1, 1, 1)).getItensDataBase());
			Dao.insert("objetivo", (new Objetivo(Parametros.CD_OBJETIVO_REALIZOU_50_SEM_ERRAR, Jogo.getJogo().getCdJogo(), "Realizou 50 trocas sem errar", Parametros.CD_TIPO_OBJETIVO_MATEMATICO, 1, 1, 1)).getItensDataBase());
			Dao.insert("objetivo", (new Objetivo(Parametros.CD_OBJETIVO_CAPTUROU_TODOS_PINTINHOS_NO_TEMPO, Jogo.getJogo().getCdJogo(), "Capturou todos os pintinhos antes do tempo acabar", Parametros.CD_TIPO_OBJETIVO_LUDICO, 1, 1, 1)).getItensDataBase());
		}

		ArrayList registroObjetivoPrincipalSecundario = Dao.getAll ("objetivo_principal_secundario");
		if (registroObjetivoPrincipalSecundario.Count == 0) {
			Dao.insert ("objetivo_principal_secundario", (new ObjetivoPrincipalSecundario (Parametros.CD_OBJETIVO_CONQUISTOU_UM_LOTE_TERRA, Parametros.CD_OBJETIVO_TROCA_DIRETA, Jogo.getJogo ().getCdJogo ())).getItensDataBase ());
			Dao.insert ("objetivo_principal_secundario", (new ObjetivoPrincipalSecundario (Parametros.CD_OBJETIVO_CONQUISTOU_UM_LOTE_TERRA, Parametros.CD_OBJETIVO_TROCA_INVERSA, Jogo.getJogo ().getCdJogo ())).getItensDataBase ());
			Dao.insert ("objetivo_principal_secundario", (new ObjetivoPrincipalSecundario (Parametros.CD_OBJETIVO_CONQUISTOU_UM_LOTE_TERRA, Parametros.CD_OBJETIVO_CAPTUROU_DOIS_PINTINHOS, Jogo.getJogo ().getCdJogo ())).getItensDataBase ());
			Dao.insert ("objetivo_principal_secundario", (new ObjetivoPrincipalSecundario (Parametros.CD_OBJETIVO_CONQUISTOU_UM_LOTE_TERRA, Parametros.CD_OBJETIVO_GANHOU_UMA_GALINHA, Jogo.getJogo ().getCdJogo ())).getItensDataBase ());
			Dao.insert ("objetivo_principal_secundario", (new ObjetivoPrincipalSecundario (Parametros.CD_OBJETIVO_CONQUISTOU_UM_LOTE_TERRA, Parametros.CD_OBJETIVO_GANHOU_UM_SACO_MILHO, Jogo.getJogo ().getCdJogo ())).getItensDataBase ());
			Dao.insert ("objetivo_principal_secundario", (new ObjetivoPrincipalSecundario (Parametros.CD_OBJETIVO_CONQUISTOU_UM_LOTE_TERRA, Parametros.CD_OBJETIVO_GANHOU_UM_PORCO, Jogo.getJogo ().getCdJogo ())).getItensDataBase ());
			Dao.insert ("objetivo_principal_secundario", (new ObjetivoPrincipalSecundario (Parametros.CD_OBJETIVO_CONQUISTOU_UM_LOTE_TERRA, Parametros.CD_OBJETIVO_GANHOU_UMA_OVELHA, Jogo.getJogo ().getCdJogo ())).getItensDataBase ());
			Dao.insert ("objetivo_principal_secundario", (new ObjetivoPrincipalSecundario (Parametros.CD_OBJETIVO_CONQUISTOU_UM_LOTE_TERRA, Parametros.CD_OBJETIVO_GANHOU_UMA_VACA, Jogo.getJogo ().getCdJogo ())).getItensDataBase ());
			Dao.insert ("objetivo_principal_secundario", (new ObjetivoPrincipalSecundario (Parametros.CD_OBJETIVO_CONQUISTOU_UM_LOTE_TERRA, Parametros.CD_OBJETIVO_GANHOU_UM_CAVALO, Jogo.getJogo ().getCdJogo ())).getItensDataBase ());
			Dao.insert ("objetivo_principal_secundario", (new ObjetivoPrincipalSecundario (Parametros.CD_OBJETIVO_CONQUISTOU_UM_LOTE_TERRA, Parametros.CD_OBJETIVO_REALIZOU_TROCA_4_1, Jogo.getJogo ().getCdJogo ())).getItensDataBase ());
			Dao.insert ("objetivo_principal_secundario", (new ObjetivoPrincipalSecundario (Parametros.CD_OBJETIVO_CONQUISTOU_UM_LOTE_TERRA, Parametros.CD_OBJETIVO_REALIZOU_TROCA_4_2, Jogo.getJogo ().getCdJogo ())).getItensDataBase ());
			Dao.insert ("objetivo_principal_secundario", (new ObjetivoPrincipalSecundario (Parametros.CD_OBJETIVO_CONQUISTOU_UM_LOTE_TERRA, Parametros.CD_OBJETIVO_REALIZOU_10_SEM_ERRAR, Jogo.getJogo ().getCdJogo ())).getItensDataBase ());
			Dao.insert ("objetivo_principal_secundario", (new ObjetivoPrincipalSecundario (Parametros.CD_OBJETIVO_CONQUISTOU_UM_LOTE_TERRA, Parametros.CD_OBJETIVO_REALIZOU_30_SEM_ERRAR, Jogo.getJogo ().getCdJogo ())).getItensDataBase ());
			Dao.insert ("objetivo_principal_secundario", (new ObjetivoPrincipalSecundario (Parametros.CD_OBJETIVO_CONQUISTOU_UM_LOTE_TERRA, Parametros.CD_OBJETIVO_REALIZOU_50_SEM_ERRAR, Jogo.getJogo ().getCdJogo ())).getItensDataBase ());
			Dao.insert ("objetivo_principal_secundario", (new ObjetivoPrincipalSecundario (Parametros.CD_OBJETIVO_CONQUISTOU_UM_LOTE_TERRA, Parametros.CD_OBJETIVO_CAPTUROU_TODOS_PINTINHOS_NO_TEMPO, Jogo.getJogo ().getCdJogo ())).getItensDataBase ());
		}

		ArrayList registroAtividades = Dao.getAll ("Atividade");
		if (registroAtividades.Count == 0) {
			Dao.insert ("atividade", (new Atividade (Parametros.CD_ATIVIDADE_RACIOCINIO_DIRETO, "Troca por quem?", "Essa atividade tem como objetivo" +
													" trabalhar o que foi aprendido com as trocas do jogo. Visa estimular tanto o raciocínio" +
													" quanto a memória dos alunos em relação ao que foi aprendido.", Parametros.CD_TIPO_ATIVIDADE_PENSAMENTO_DIRETO)).getItensDataBase ());
		}


		ArrayList registroEtapasAtividade = Dao.getAll ("Etapa_Atividade");
		if (registroEtapasAtividade.Count == 0) {
			Dao.insert ("etapa_atividade", (new EtapaAtividade (Parametros.CD_ETAPA_ATIVIDADE_RACIOCINIO_DIRETO_1, Parametros.CD_ATIVIDADE_RACIOCINIO_DIRETO, "Quantidade de animais", 1, "Verifica a quantidade de animais que aparecem na tela")).getItensDataBase ());
			Dao.insert ("etapa_atividade", (new EtapaAtividade (Parametros.CD_ETAPA_ATIVIDADE_RACIOCINIO_DIRETO_2, Parametros.CD_ATIVIDADE_RACIOCINIO_DIRETO, "Pelo que pode trocar", 2, "Verifica por qual mercadoria, aquela escolhida na primeira etapa, pode ser troca diretamente. Nessa segunda etapa pode ser escolhida apenas a mercadoria sucesso ou antecessora, e apenas caso dê para trocar com a quantidade sorteada.")).getItensDataBase ());
			Dao.insert ("etapa_atividade", (new EtapaAtividade (Parametros.CD_ETAPA_ATIVIDADE_RACIOCINIO_DIRETO_3, Parametros.CD_ATIVIDADE_RACIOCINIO_DIRETO, "Selecione para trocar", 3, "Seleciona a quantidade de mercadorias para troca com a quantidade selecionada, podendo uma delas possuir sobra, caso não venha a ser exata na troca.")).getItensDataBase ());
			Dao.insert ("etapa_atividade", (new EtapaAtividade (Parametros.CD_ETAPA_ATIVIDADE_RACIOCINIO_DIRETO_4, Parametros.CD_ATIVIDADE_RACIOCINIO_DIRETO, "Possui sobras", 4, "Verifica se houve sobras ou não de acordo ao que foi sorteado e o que foi colocado pelo jogador.")).getItensDataBase ());
		}

		ArrayList registroConfiguracaoCartas = Dao.getAll ("Configuracao_Cartas");
		if (registroConfiguracaoCartas.Count == 0) {
			Dao.insert ("configuracao_cartas", (new ConfiguracaoCartas (Parametros.CD_CONFIGURACAO_CARTAS_1, Jogo.getJogo ().getCdJogo (), -6F, 1.5, 1, 1, 12, 6, 2, 2.5, 4, 0)).getItensDataBase ());
			Dao.insert ("configuracao_cartas", (new ConfiguracaoCartas (Parametros.CD_CONFIGURACAO_CARTAS_2, Jogo.getJogo ().getCdJogo (), -4.7F, 1.5, 0.8, 0.8, 18, 6, 3, 2, 2.5, 10)).getItensDataBase ());
			Dao.insert ("configuracao_cartas", (new ConfiguracaoCartas (Parametros.CD_CONFIGURACAO_CARTAS_3, Jogo.getJogo ().getCdJogo (), -6F, 1.5, 0.7, 0.7, 24, 8, 3, 2, 2.5, 20)).getItensDataBase ());
		}

		ArrayList registroTutoriais = Dao.getAll ("Tutorial");
		if (registroTutoriais.Count == 0) {
			Dao.insert ("tutorial", (new Tutorial (Parametros.TUTORIAL_JOGO_MEMORIA, "Tutorial do Jogo da Memória")).getItensDataBase ());
			Dao.insert ("tutorial", (new Tutorial (Parametros.TUTORIAL_MERCADO_TROCA, "Tutorial do Mercado de Trocas")).getItensDataBase ());
			Dao.insert ("tutorial", (new Tutorial (Parametros.TUTORIAL_ATIVIDADES, "Tutorial da Atividade")).getItensDataBase ());
			Dao.insert ("tutorial", (new Tutorial (Parametros.TUTORIAL_MINHA_FAZENDA, "Tutorial da Minha Fazenda")).getItensDataBase ());
			Dao.insert ("tutorial", (new Tutorial (Parametros.TUTORIAL_MUNDO, "Tutorial do Mapa")).getItensDataBase ());

		}

		return new Result (1);
	}
}