using UnityEngine;
using System.Collections;

public class ItemDataBase : GenericItem{
	
	private bool incremental;

	public ItemDataBase(){
	}

	public ItemDataBase(int type,
						string field,
						string value,
						bool primaryKey,
						bool incremental){
		setType (type);
		setField (field);
		setValue (value);
		setPrimaryKey (primaryKey);
		setIncremental (incremental);
	}

	public void setIncremental(bool incremental){
		this.incremental = incremental;
	}

	public bool isIncremental(){
		return incremental;
	}

	public object clone(){
		return new ItemDataBase (getType (),
								getField (),
								getValue(),
								isPrimaryKey (),
								isIncremental());
	}
}

