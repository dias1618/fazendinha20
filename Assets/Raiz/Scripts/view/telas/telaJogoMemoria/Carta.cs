using UnityEngine;
using System.Collections;

public class Carta : MonoBehaviour{

	public int indice;
	public int par;

	public Carta(int indice,
				 int par){
		setIndice (indice);
		setPar (par);
	}

	public void setIndice(int indice){
		this.indice = indice;
	}

	public int getIndice(){
		return this.indice;
	}

	public void setPar(int par){
		this.par = par;
	}

	public int getPar(){
		return this.par;
	}

}

