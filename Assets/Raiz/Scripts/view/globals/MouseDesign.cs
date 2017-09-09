using UnityEngine;
using System.Collections;

public class MouseDesign : MonoBehaviour{

	public Texture2D cursor;
	private int cursorSizeX = 60;
	private int cursorSizeY = 120;

	void Start(){
		Cursor.visible = false;
	}

	void OnGUI (){
		GUI.DrawTexture (new Rect(Event.current.mousePosition.x - cursorSizeX / 2, Event.current.mousePosition.y - cursorSizeY / 2, cursorSizeX, cursorSizeY), cursor);
	}

}

