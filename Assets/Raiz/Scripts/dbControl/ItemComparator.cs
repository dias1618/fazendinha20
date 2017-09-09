using UnityEngine;
using System.Collections;

public class ItemComparator : GenericItem{

	//Tipos de Comparação
	public const int EQUALS        = 0;
	public const int DIFFERENT     = 1;
	public const int LESS          = 2;
	public const int GREATER       = 3;
	public const int LESS_EQUAL    = 4;
	public const int GREATER_EQUAL = 5;
	public const int COMPARE_OR    = 6;

	public static string[] comparators  = { "=", "<>", "<", ">", "<=", ">=" };

	private int compare;
	private ArrayList arrayCompare;//Array List de ItensComparator para realizar comparações do tipo OR
	private bool includeKey;//Indica que o campo, mesmo não sendo uma chave, irá compor o JOIN como se fosse uma chave

	public ItemComparator(){
	}

	public ItemComparator(int type,
						string field,
						string value,
						bool primaryKey,
						int compare){
		setType (type);
		setField (field);
		setValue (value);
		setPrimaryKey (primaryKey);
		setCompare (compare);
	}

	public ItemComparator(int type,
						string field,
						string value,
						bool primaryKey,
						int compare,
						ArrayList arrayCompare){
		setType (type);
		setField (field);
		setValue (value);
		setPrimaryKey (primaryKey);
		setCompare (compare);
		setArrayCompare (arrayCompare);
	}

	public ItemComparator(int type,
						string field,
						string value,
						bool primaryKey,
						int compare,
						bool includeKey){
		setType (type);
		setField (field);
		setValue (value);
		setPrimaryKey (primaryKey);
		setCompare (compare);
		setIncludeKey (includeKey);
	}

	public ItemComparator(int type,
						string field,
						string value,
						bool primaryKey,
						int compare,
						ArrayList arrayCompare,
						bool includeKey){
		setType (type);
		setField (field);
		setValue (value);
		setPrimaryKey (primaryKey);
		setCompare (compare);
		setArrayCompare (arrayCompare);
		setIncludeKey (includeKey);
	}

	public void setCompare(int compare){
		this.compare = compare;
	}

	public int getCompare(){
		return compare;
	}

	public void setArrayCompare(ArrayList arrayCompare){
		this.arrayCompare = arrayCompare;
	}

	public ArrayList getArrayCompare(){
		return arrayCompare;
	}

	public void setIncludeKey(bool includeKey){
		this.includeKey = includeKey;
	}

	public bool isIncludeKey(){
		return includeKey;
	}

	public ItemComparator clone(){
		return new ItemComparator (getType (),
			getField (),
			getValue(),
			isPrimaryKey (),
			getCompare(),
			getArrayCompare(),
			isIncludeKey());
	}
}

