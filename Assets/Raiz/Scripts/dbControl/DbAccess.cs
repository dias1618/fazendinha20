using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Mono.Data.Sqlite;
using System.Data;
using System.Data.Common;
using System.Globalization;


public class DbAccess
{

	public const string _constr="URI=file:Fazendinha20DB";

	private static SqliteConnection _dbcon;
	private static IDbCommand _dbcm;
	private static IDataReader _dbr;

	public static Result OpenDataBase(){
		_dbcon= new SqliteConnection(_constr);
		_dbcon.Open();
		return new Result (1);
	}

	public static int executeCommand(string sql){
		try{
			OpenDataBase ();
			_dbcm = _dbcon.CreateCommand();
			_dbcm.CommandText = sql;
			_dbr = _dbcm.ExecuteReader ();
			CloseDataBase();
			return 1;
		}
		catch(DbException e){
			Debug.Log (e.Message);
			return -1;
		}
	}

	/// <summary>
	/// Retorna uma lista dos registros que se deseja buscar através do comando SELECT passado
	/// </summary>
	/// <returns>Uma lista de registros do banco de dados.</returns>
	/// <param name="sql">Uma consulta SQL</param>
	public static ArrayList returnQuery(string sql){
		try{
			//Faz a conexão com o banco de dados
			OpenDataBase ();
			//Faz uma consulta ao banco de dados com a sql passada
			_dbcm = _dbcon.CreateCommand();
			_dbcm.CommandText = sql;
			_dbr = _dbcm.ExecuteReader ();
			//Itera sobre os dados trazidos, colocando-os em um Dictionary, 
			//relacionando o nome do campo (na tabela) com a informação
			ArrayList readerList = new ArrayList();
			while(_dbr.Read()){
				Dictionary<string, object> lineList = new Dictionary<string, object>();
				for(int i = 0; i < _dbr.FieldCount; i++){
					if(_dbr.GetDataTypeName(i).Equals("TIMESTAMP")){
						lineList.Add(_dbr.GetName(i), _dbr.GetString(i));
					}
					else
						lineList.Add(_dbr.GetName(i), _dbr.GetValue(i));
				}
				readerList.Add(lineList);
			}
			//Fecha a conexão de banco de dados usada
			CloseDataBase();
			//Retorna as informações buscadas através de um array de dados
			return readerList;
		}
		catch(DbException e){
			Debug.Log (e.Message);
			return null;
		}
	}

	public static Result CloseDataBase(){
		_dbr.Close ();
		_dbr = null;	
		_dbcm.Dispose ();
		_dbcm = null;
		_dbcon.Close ();
		_dbcon = null;

		return new Result (1);
	}
}

