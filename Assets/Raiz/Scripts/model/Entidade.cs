using UnityEngine;
using System.Collections;

public abstract class Entidade{
	abstract public Result save();
	abstract public Result remove();
	abstract public Result get();
	abstract public object clone ();
	abstract public ArrayList getItensDataBase ();
}

