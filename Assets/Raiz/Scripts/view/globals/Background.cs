using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour
{

	// Use this for initialization
	void Start (){
		ResizeSpriteToScreen();
	}

	//Metodo para redimensionar a imagem de fundo para cobrir a tela inteira
	private void ResizeSpriteToScreen() {
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if (sr == null) return;

		transform.localScale = new Vector3(1,1,1);

		float width = sr.sprite.bounds.size.x;
		float height = sr.sprite.bounds.size.y;

		float worldScreenHeight = (float)(Camera.main.orthographicSize * 2.0F);
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		transform.localScale = new Vector3((worldScreenWidth / width), (worldScreenHeight / height), transform.localScale.z);
	}
}

