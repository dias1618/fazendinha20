using UnityEngine;
using System.Collections;

public class Result{

	private int code;
	private string message;
	private ArrayList objects;

	public Result(){
	}

	public Result(int code,
				string message,
				ArrayList objects){
		setCode (code);
		setMessage (message);
		setObjects (objects);
	}

	public Result(int code,
				string message,
				object firstObject){
		setCode (code);
		setMessage (message);
		objects = new ArrayList();
		objects.Add (firstObject);
	}

	public Result(int code,
		string message){
		setCode (code);
		setMessage (message);
		objects = new ArrayList ();
	}

	public Result(int code){
		setCode (code);
	}

	public void setCode(int code){
		this.code = code;
	}

	public int getCode(){
		return code;
	}

	public void setMessage(string message){
		this.message = message;
	}

	public string getMessage(){
		return message;
	}

	public void setObjects(ArrayList objects){
		this.objects = objects;
	}

	public ArrayList getObjects(){
		return objects;
	}

	public Result clone(){
		return new Result (getCode (),
			getMessage (),
			getObjects());
	}

}

