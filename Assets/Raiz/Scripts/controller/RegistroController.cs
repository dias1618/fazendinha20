using UnityEngine;
using System.Collections;

public class RegistroController{

	public static bool hasRegistroAtivo(){
		return Dao.getAll ("registro").Count > 0;
	}

}

