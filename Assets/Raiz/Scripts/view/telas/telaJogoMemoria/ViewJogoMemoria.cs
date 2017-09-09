using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewJogoMemoria : View{


	//Variavel que recebe as cartas que serao postas no jogo da memoria
	public Sprite[] cartas;
	//Variavel que controla a posicao em X das cartas
	private double posInicialX; 
	//Variavel que controla a posicao em Y das cartas
	private double posInicialY;
	//Variavel que guarda o indice da primeira carta virada, enquanto o jogador nao escolhe a segunda
	private int carta1;
	//Variavel que guarda o objeto da primeira carta virada
	private GameObject objetoCarta1;
	//Variavel que guarda o indice da segunda carta virada, fazendo comparacao com a primeira
	private int carta2;
	//Variavel que guarda o objeto da segunda carta virada
	private GameObject objetoCarta2;

	//Tempo usado para fechar as cartas viradas que nao sao par
	//Esse tempo e usado como uma forma de punicao quando o jogador nao acerta as cartas corretas
	//Ja que nao ha um outro jogador, essa eh a unica forma de se propor um desafio ao jogador
	private float tempoFecharCartas;

	private float tempoDestruirCartas;
	//Variavel que controla se as cartas serao fechadas ou nao (se elas nao formarem um par, quando as duas forem abertas)
	public bool fecharCartas;

	public bool destruirCartas;
	//Variavel que impede que mais cartas sejam abertas ate que o tempo de fechar as duas abertas anteriormente termine
	public bool liberaCartas;

	public GameObject panelCaixa2TutorialMemoria;
	public GameObject panelCaixaTempo;
	//Campo que marca o tempo ate o jogo acabar
	public Text vlTempo;
	//Variavel que marca os segundos ate o jogo acabar
	private int contagemRegressiva;
	//Variavel usado como contador de segundos (usando o Time.deltaTime)
	private float contagemTempo;

	//Variavel que controla o inicio do jogo
	public bool iniciarJogo;

	public Text txtPintinhosGanhos;
	public int qtPintinhosGanhos;

	//Jogador
	public static int pontuacao;

	private int[] cartasPosicoes;

	public GameObject panelComecarCaca;

	public GameObject eventoCarta;

	public static int telaPausada = 0;


	private int chancesSegundaClasse = 0;
	private int chancesTerceiraClasse = 0;

	private ConfiguracaoCartas configuracaoCartas;


	void Start (){
		base.start (!TelaJogoMemoria.getTela ().getLgTutorial(), Parametros.TUTORIAL_JOGO_MEMORIA);

		StartCoroutine(iniciarView ());

	}


	private IEnumerator iniciarView(){

		float timeInfinite = 1F;
		while(!liberarStart){
			yield return new WaitForSeconds (timeInfinite);
			timeInfinite = 0.1F;
		}

		verificarNivel ();

		distribuirCartas ();

		incluirCartasView ();

		inicializaJogoMemoria ();
	}

	private void verificarNivel(){

		configuracaoCartas = new ConfiguracaoCartas ();
		int cdConfiguracaoCartas = 0;

		if(Registro.getRegistro().getQtNivel() < 5){
			cdConfiguracaoCartas = Parametros.CD_CONFIGURACAO_CARTAS_1;
			chancesSegundaClasse = 50;
			chancesTerceiraClasse = 100;
		}
		else if(Registro.getRegistro().getQtNivel() < 10){
			cdConfiguracaoCartas = Parametros.CD_CONFIGURACAO_CARTAS_2;
			chancesSegundaClasse = 20;
			chancesTerceiraClasse = 40;
		}
		else{
			cdConfiguracaoCartas = Parametros.CD_CONFIGURACAO_CARTAS_3;
			chancesSegundaClasse = 10;
			chancesTerceiraClasse = 20;
		}

		configuracaoCartas.setCdConfiguracaoCartas(cdConfiguracaoCartas);
		Result result = configuracaoCartas.get ();
		if(result.getCode() < 0){
			Debug.LogError (result.getMessage ());
			return;
		}

		//Posiçoes em X e Y da primeira carta a ser posicionada na tela
		posInicialX = configuracaoCartas.getVlPosicaoXInicial();//posicaoXInicial
		posInicialY = configuracaoCartas.getVlPosicaoYInicial();//posicaoYInicial


	}


	private void distribuirCartas(){
		cartasPosicoes = new int[configuracaoCartas.getQtCartas()+1];//quantidadeCartas
		for(int j = 1; j < configuracaoCartas.getQtCartas()+1; j++){//quantidadeCartas
			int numeroUtilizado = -1;

			do{
				numeroUtilizado = Random.Range(1, configuracaoCartas.getQtCartas()+1);//quantidadeCartas
			}while(Util.intPertenceAoArray(cartasPosicoes, numeroUtilizado));

			cartasPosicoes[j] = numeroUtilizado;
		}


		incluirCartasEspeciais (chancesSegundaClasse, 37, 46);

		incluirCartasEspeciais (chancesTerceiraClasse, 47, 48);


	}

	private void incluirCartasEspeciais(int chance, int inicioFaixa, int fimFaixa){
		int seletorCarta = Random.Range(1, chance);
		if(seletorCarta == (chance/2)){
			int cartaEscolhida  = Random.Range(inicioFaixa, fimFaixa);

			int parCartaEscolhida = getPar(cartaEscolhida);

			int cartaASerTrocada = Random.Range(1, configuracaoCartas.getQtCartas()+1);
			for(int z = 1; z < configuracaoCartas.getQtCartas()+1; z++){
				if(cartasPosicoes[z] == cartaASerTrocada){
					cartasPosicoes[z] = cartaEscolhida;
					int parCartaASerTrocada = getPar(cartaASerTrocada);
					for(int q = 1; q < configuracaoCartas.getQtCartas()+1; q++){
						if(cartasPosicoes[q] == parCartaASerTrocada){
							cartasPosicoes[q] = parCartaEscolhida;
							break;
						}
					}
					break;
				}
			}

		}
	}

	private void incluirCartasView(){
		int quantCartasLinha=0;

		//Itera sobre as doze cartas, colocando-as organizadas na tela viradas para baixo (ou seja, utilizando
		//uma imagem igual em todas
		for(int i = 1;i<configuracaoCartas.getQtCartas()+1;i++){//quantidadeCartas
			//Seleciona a imagem da carta virada para baixo, buscada pelos Textures importados das imagens
			Sprite carta = cartas[0];

			//Declara uma variavel de objeto para englobar a imagem
			GameObject objetoSprite = new GameObject();
			//Adiciona o componente de SpriteRenderer, necessaria para manipular a imagem
			objetoSprite.AddComponent<SpriteRenderer>();
			//Indica o nome de cada carta a partir de sua posicao
			objetoSprite.name = "carta"+i;
			//Busca o componente de SpriteRenderer que acabou de se criar
			SpriteRenderer SR = objetoSprite.GetComponent<SpriteRenderer>();
			//Diz que o sprite do SpriteRenderer e a carta passada
			SR.sprite = carta;

			//Adiciona um BoxCollider2D, para que a carta possa ser acionada pelo click do mouse
			objetoSprite.AddComponent<BoxCollider2D>();

			//Adiciona o script de selecionar_carta
			objetoSprite.AddComponent<SelecionarCarta>();
			//Adiciona o script de carta
			objetoSprite.AddComponent<Carta>();
			//Adiciona o indice e o indice da carta no jogo
			objetoSprite.GetComponent<Carta>().setIndice(cartasPosicoes[i]);
			//Adiciona o indice do par da carta no jogo
			objetoSprite.GetComponent<Carta>().setPar(getPar(cartasPosicoes[i]));

			//Tamanho das cartas
			objetoSprite.transform.localScale = new Vector3((float)configuracaoCartas.getVlWidth(), (float)configuracaoCartas.getVlHeight(), objetoSprite.transform.localScale.z);

			//Inicializa o objeto com a imagem na tela com todos os componentes adicionados (criando um clone do mesmo)
			Instantiate(SR,new Vector2((float)posInicialX,(float)posInicialY),Quaternion.identity);
			//Destroi o objeto anterior que serviu de base
			Destroy(objetoSprite);
			//Modifica a posicao em X para o proximo elemento
			posInicialX += configuracaoCartas.getVlEspacoEntreCartasX();//espacoEntreCartasX

			quantCartasLinha++;
			//Caso esteja na segunda metade das cartas, o sistema iniciara a posicao de X e fara com que 
			//a posicao de Y esteja mais embaixo, criando assim a segunda fileira
			if(quantCartasLinha == configuracaoCartas.getQtCartasLinha()){//cartasPorLinha
				posInicialX  = configuracaoCartas.getVlPosicaoXInicial();//posicaoXInicial
				posInicialY -= configuracaoCartas.getVlEspacoEntreCartasY();;//posicaoYInicial - espacoEntreCartasY
				quantCartasLinha = 0;
			}
		}
	}

	private void inicializaJogoMemoria(){
		//Inicializa os indices das cartas viradas como -1, ja que 0 tambem e usado como indice
		carta1 = -1;
		carta2 = -1;

		//Nao libera o jogo ate que o jogador aperte o botao de começar
		fecharCartas = false;
		liberaCartas = false;	
		destruirCartas = false;

		//Inicia a contagem inicialmente em 20 segundos
		contagemRegressiva = Jogo.getConfiguracao().getQtTempoMemoria() + configuracaoCartas.getQtAcrescimoTempoBase();

		//Deixa para iniciar o jogo a partir do botao de começar
		iniciarJogo = false;

		pontuacao = 0;

		this.gameObject.GetComponent<AudioSource>().volume *= (float)Jogo.getConfiguracao().getVlVolume();


		RectTransform rectPanel = canvas.GetComponentInChildren<RectTransform>(); 
		GameObject comecarCaca = Instantiate(panelComecarCaca);
		comecarCaca.transform.SetParent(rectPanel.transform, false);

		if(TelaJogoMemoria.getTela().getLgTutorial()){
			Util.hasVisible(panelCaixaTempo, false);
		}
	}

	private int getPar(int indice){
		if (indice % 2 == 0) {
			return indice - 1;
		} 
		else {
			return indice + 1;
		}
	}

	//Metodo usado para receber as cartas escolhidas pelo jogador, as proprias cartas acionaram o metodo
	public void recebeCarta(GameObject carta, int indice){
		//Caso nenhuma carta tenha sido selecionada ainda, ela ira ser posta na variavel "carta1" e "objetoCarta1"
		if(carta1 == -1){
			objetoCarta1 = carta;
			carta1 = indice;
		}
		//Caso a primeira ja tenha sido escolhida, e colocado nas segundas variaveis
		else if(carta2 == -1 && carta1 != carta2){
			objetoCarta2 = carta;
			carta2 = indice;

			//Caso as cartas sejam par
			if(objetoCarta1.GetComponent<Carta>().getPar() == carta2){
				//A pontuacao e acrescida em 2
				pontuacao++;
				pontuacao++;

				if(pontuacao == 2 && TelaJogoMemoria.getTela().getLgTutorial()){
					RectTransform panel = canvas.GetComponentInChildren<RectTransform>(); 
					caixa2TutorialMemoria = Instantiate(panelCaixa2TutorialMemoria);
					caixa2TutorialMemoria.transform.SetParent(panel.transform, false);
					StartCoroutine(delayEsconderMensagemTutorial(2F));
				}
				//Adicionar pintinhos capturados no Hall de pintinhos
				int cdItem = 0;
				if (carta1 % 2 == 0) {
					cdItem = carta1 / 2;
				} 
				else {
					cdItem = (carta1 / 2) + 1;
				}

				//Fora do tutorial os pintinhos serão contabilizados no banco no Jogo Memoria Final
				if (TelaJogoMemoria.getTela ().getLgTutorial ()) {
					//Adicionar a quantidade de pintinhos capturados
					RegistroQuantidadeItem.addPintinhoBD (2);
				}

				RegistroItem.addPintinhoBD (cdItem);

				RegistroObjetivo.addObjetivoBD (Parametros.CD_OBJETIVO_CAPTUROU_DOIS_PINTINHOS);

				if(pontuacao == configuracaoCartas.getQtCartas()){//quantidadeCartas
					RegistroObjetivo.addObjetivoBD (Parametros.CD_OBJETIVO_CAPTUROU_TODOS_PINTINHOS_NO_TEMPO);
					TelaJogoMemoria.destroyTela ();

					TelaJogoMemoriaFinal.getTela().setQtCartasViradas(pontuacao);
					TelaJogoMemoriaFinal.getTela().setQtTempoSobrando(contagemRegressiva);
					TelaJogoMemoriaFinal.getTela().setQtPontosGanhosCartas(pontuacao/2);
					TelaJogoMemoriaFinal.getTela().setQtPontosGanhosTempo((contagemRegressiva * 20)/Jogo.getConfiguracao().getQtTempoMemoria());

					SceneManager.LoadScene("tela_jogo_memoria_final");
				}
				else if(TelaJogoMemoria.getTela().getLgTutorial() && pontuacao == 6){
					TelaJogoMemoria.destroyTela ();
					SceneManager.LoadScene("historia_inicial");
				}
				else{

					//As cartas sao voltadas para seu estado inicial
					carta1 = -1;
					carta2 = -1;
					tempoFecharCartas = 0;
					fecharCartas = false;
					destruirCartas = true;
				}


			}
			//Caso as cartas nao sejam par, e liberado que elas sejam fechadas
			else if(carta1 != carta2)
				fecharCartas = true;
			else
				carta2 = -1;
		}
	}


	private GameObject caixa2TutorialMemoria;
	private IEnumerator delayEsconderMensagemTutorial(float waitTime){
		yield return new WaitForSeconds (waitTime);

		Util.hasVisible(caixa2TutorialMemoria, false);

	}

	void Update (){
		base.update ();

		//Inicia o jogo apenas quando o usuario clicar em "Começar"
		if(iniciarJogo && !TelaJogoMemoria.getTela().getLgTutorial()){
			//Libera as cartas para serem clicadas pelo jogador
			liberaCartas = true;
			//Recebe o "tempo" ate que de 1 segundo
			contagemTempo += Time.deltaTime;
			//Quando chega em 1 segundo, o contador regressivo decresce, e a contagem de tempo volta a zero
			if(contagemTempo >= 1){
				contagemRegressiva--;
				contagemTempo = 0;
			}

			//Quando a contagem regressiva chega a zero, o jogo termina, e muda-se de cena
			if(contagemRegressiva == 0){
				TelaJogoMemoriaFinal.getTela().setQtCartasViradas(pontuacao);
				TelaJogoMemoriaFinal.getTela().setQtTempoSobrando(0);
				TelaJogoMemoriaFinal.getTela().setQtPontosGanhosCartas(pontuacao/2);
				TelaJogoMemoriaFinal.getTela().setQtPontosGanhosTempo(0);
				SceneManager.LoadScene ("tela_jogo_memoria_final");
			}

		}

		if(!TelaJogoMemoria.getTela().getLgTutorial()){
			//Mostra continuamente o tempo regressivo na tela
			vlTempo.text = (contagemRegressiva > 9 ? "" : "0") + contagemRegressiva;
		}
		else{
			liberaCartas = true;
			vlTempo.text = "";
		}

		//Liberado apenas quando for para se fechar as cartas que estiverem abertas (ao nao formarem par)
		if(fecharCartas){
			//Nao e permitido que o jogador abra mais cartas enquanto as duas abertas nao fecharem
			liberaCartas = false;
			//O tempo de fechar cartas e de 1 segundo
			tempoFecharCartas += Time.deltaTime;
			if(tempoFecharCartas >= 1){
				//As cartas recebem novamente a imagem da carta virada
				SpriteRenderer SR = objetoCarta1.GetComponent<SpriteRenderer>();
				SR.sprite = cartas[0];
				SR = objetoCarta2.GetComponent<SpriteRenderer>();
				SR.sprite = cartas[0];
				//As variaveis que controlam as cartas abertas, sao colocadas em seu estado padrao
				carta1 = -1;
				carta2 = -1;
				//O tempo de fechar cartas volta a zero
				tempoFecharCartas = 0;
				//A variavel de fechar cartas eh fechada novamente
				fecharCartas = false;
				//As cartas sao novamente liberadas para serem escolhidas
				liberaCartas = true;
			}
		}

		if(destruirCartas){
			liberaCartas = false;
			tempoDestruirCartas += Time.deltaTime;
			if(tempoDestruirCartas >= 0.5){
				Destroy(objetoCarta1);
				Destroy(objetoCarta2);

				qtPintinhosGanhos += 2;
				txtPintinhosGanhos.text = "" + qtPintinhosGanhos;

				tempoDestruirCartas = 0;
				destruirCartas = false;
				liberaCartas = true;
			}
		}

	}
}

