using UnityEngine;
using System.Collections.Generic;

public class MensagensTutorial : MonoBehaviour
{

	public static Dictionary<int, string[]> arrayTexto;
	public static string[] text;

	public static void inicializarMensagens(){
		arrayTexto = new Dictionary<int, string[]>();

		//Jogo Memória
		text = new string[6];
		text[0] = "Aqui é a caçada de pintinhos jovem fazendeiro";
		text[1] = "Para poder jogar você precisa encontrar pintinhos equivalentes clicando nos arbustos";
		text[2] = "A cada par de pintinhos achados, você ganhara dois pontos";
		text[3] = "Nesse primeiro jogo você poderá testar suas habilidades sem preocupação";
		text[4] = "Mas nas próximas caçadas esteja atento ao tempo, e escolha os arbustos rapidamente";
		text[5] = "Boa sorte!";
		arrayTexto.Add (Parametros.TUTORIAL_JOGO_MEMORIA, text);

		//Mercado de Trocas
		text = new string[10];
		text[0] = "Bem vindo ao mercado de trocas pequeno(a) fazendeiro(a)";
		text[1] = "Aqui você encontrará tudo que precisa para fazer suas trocas";
		text[2] = "Para escolher quais animais deseja trocar, basta clicar no animal";
		text[3] = "Todos os que você tem aparecerão logo abaixo";
		text[4] = "Para escolher com qual fazendeiro deseja trocar, basta clicar no fazendeiro";
		text[5] = "Os animais dele aparecerão também abaixo";
		text[6] = "Clique então nos animais que deseja fazer a troca, mandando-os para a Área de Trocas";
		text[7] = "Assim que tiver escolhido seus animais e os animais do fazendeiro, clique no botão de Trocar";
		text[8] = "É bem fácil! Se tiver dúvidas clique no botão de Tirar Dúvidas, ai em cima";
		text[9] = "Agora comece e seja um grande fazendeiro(a)";
		arrayTexto.Add (Parametros.TUTORIAL_MERCADO_TROCA, text);

		//Atividades
		text = new string[5];
		text[0] = "Esta pronto para fazer esse desafio?";
		text[1] = "Toda vez que você passar de nível, será desafiado";
		text[2] = "Veja os animais que aparecem na tela e responda as perguntas corretamente";
		text[3] = "Preste bastante atenção, tenho certeza que saberá responder a todas.";
		text[4] = "Começar!!!";
		arrayTexto.Add(Parametros.TUTORIAL_ATIVIDADES, text);

		//Minha Fazenda
		text = new string[5];
		text[0] = "Aqui é a sua fazenda, porém no momento você está sem um lote de terra";
		text[1] = "Os seus animais não tem um lugarzinho só para eles, conquiste o lote para que eles possam estar mais felizes";
		text[2] = "Aqui você também poderá ver como está indo no jogo, clicando no botão de Estatísticas em cima";
		text[3] = "Você também poderá ver todos os pintinhos que já capturou no botão de Tipos de Pintinho, ai embaixo";
		text[4] = "Complete todos os pintinhos para receber uma grande recompensa!";
		arrayTexto.Add(Parametros.TUTORIAL_MINHA_FAZENDA, text);

		//Mundo
		text = new string[2];
		text[0] = "Aqui é o mapa de todo o Vilarejo.";
		text[1] = "Visite todos os locais e conheça seus segredos";
		arrayTexto.Add(Parametros.TUTORIAL_MUNDO, text);

	}
}

