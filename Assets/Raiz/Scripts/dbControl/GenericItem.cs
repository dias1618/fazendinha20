using UnityEngine;
using System.Collections;

public class GenericItem{

	public const int INTEGER   = 0;
	public const int STRING    = 1;
	public const int TIMESTAMP = 2;
	public const int BOOLEAN   = 3;
	public const int DOUBLE    = 4;

	private int type;
	private string field;
	private string value;
	private bool primaryKey;

	public GenericItem(){
	}

	public GenericItem(int type,
		string field,
		string value,
		bool primaryKey){
		setType (type);
		setField (field);
		setValue (value);
		setPrimaryKey (primaryKey);
	}

	public void setType(int type){
		this.type = type;
	}

	public int getType(){
		return type;
	}

	public void setField(string field){
		this.field = field;
	}

	public string getField(){
		return field;
	}

	public void setValue(string value){
		this.value = value;
	}

	public string getValue(){
		return value;
	}

	public void setPrimaryKey(bool primaryKey){
		this.primaryKey = primaryKey;
	}

	public bool isPrimaryKey(){
		return primaryKey;
	}

}

