using UnityEngine;
using System.Collections;

public class SelecionarCarta : MonoBehaviour
{

	//Açao iniciada quando o jogador clicar na carta
	void OnMouseDown (){
		//Caso a variavel que esta no script "jogo_memoria" dentro da camera principal estiver ativa, o jogador podera selecionar
		//cartas
		if(Camera.main.GetComponent<ViewJogoMemoria>().liberaCartas && ViewJogoMemoria.telaPausada == 0){
			//Busca do objeto que foi selecionado
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
			if (hit != null && hit.collider != null) {
				//Busca o gameObject do objeto selecionado
				GameObject carta = hit.collider.gameObject;
				//Busca o indice da carta que foi selecionada
				int indice = carta.GetComponent<Carta>().getIndice();
				//Chama o metodo para passar o indice e o objeto que foi selecionado
				Camera.main.GetComponent<ViewJogoMemoria>().recebeCarta(carta, indice);
				//Muda o sprite (a imagem) do objeto selecionado para a carta, ao inves da carta virada
				SpriteRenderer SR = carta.GetComponent<SpriteRenderer>();
				SR.sprite = Camera.main.GetComponent<ViewJogoMemoria>().cartas[indice];

			}
		}
	}

}

