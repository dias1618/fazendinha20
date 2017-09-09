using UnityEngine;
using System.Collections;

public class ItemTable : MonoBehaviour
{

	private string nmTable;
	private bool leftOuter;
	private ArrayList itensComparator;
	private ArrayList camposRetorno;
	private ArrayList itensTabela;

	public ItemTable(){
	}

	public ItemTable(string nmTable,
		bool leftOuter,
		ArrayList itensComparator,
		ArrayList camposRetorno,
		ArrayList itensTabela){
		setNmTable (nmTable);
		setLeftOuter (leftOuter);
		setItensComparator (itensComparator);
	}

	public void setNmTable(string nmTable){
		this.nmTable = nmTable;
	}

	public string getNmTable(){
		return nmTable;
	}

	public void setLeftOuter(bool leftOuter){
		this.leftOuter = leftOuter;
	}

	public bool isLeftOuter(){
		return leftOuter;
	}

	public void setItensComparator(ArrayList itensComparator){
		this.itensComparator = itensComparator;
	}

	public ArrayList getItensComparator(){
		return itensComparator;
	}

	public void setCamposRetorno(ArrayList camposRetorno){
		this.camposRetorno = camposRetorno;
	}

	public ArrayList getCamposRetorno(){
		return camposRetorno;
	}

	public void setItensTabela(ArrayList itensTabela){
		this.itensTabela = itensTabela;
	}

	public ArrayList getItensTabela(){
		return itensTabela;
	}

	public ItemTable clone(){
		return new ItemTable (getNmTable (),
			isLeftOuter (),
			getItensComparator(),
			getCamposRetorno(),
			getItensTabela());
	}
}

