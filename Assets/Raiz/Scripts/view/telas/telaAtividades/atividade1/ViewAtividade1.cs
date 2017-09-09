using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ViewAtividade1 : View
{

	public GameObject panelAlgumSobra;
	public GameObject panelPergunta2;
	public GameObject panelSelecione;

	//Campo que marca o tempo ate o jogo acabar
	public Text vlTempo;
	//Variavel que marca os segundos ate o jogo acabar
	public int contagemProgressiva;
	//Variavel usado como contador de segundos (usando o Time.deltaTime)
	private float contagemTempo;

	public Sprite pintinho;
	public Sprite galinha;
	public Sprite sacoMilho;
	public Sprite porco;
	public Sprite ovelha;
	public Sprite cavalo;
	public Sprite vaca;
	public Sprite loteTerra;

	public Sprite pintinhoSelecionado;
	public Sprite galinhaSelecionado;
	public Sprite sacoMilhoSelecionado;
	public Sprite porcoSelecionado;
	public Sprite ovelhaSelecionado;
	public Sprite cavaloSelecionado;
	public Sprite vacaSelecionado;
	public Sprite loteTerraSelecionado;

	public int numMercadoria;
	public int qtMercadoria;

	public GameObject[] conjuntoPanelInicial;

	public GameObject[] conjuntoPanelSelecionar;

	public GameObject fazendeiroCorreto;
	public GameObject fazendeiroCorretoFinal;
	public GameObject fazendeiroErrado;

	public GameObject fecharPanelPrimeiraPergunta;
	public GameObject fecharPanelSegundaPergunta;
	public GameObject fecharPanelTerceiraPergunta;

	public Text quantidadeAtividadeFeita;

	public RegistroAtividade registroAtividade;

	// Use this for initialization
	void Start () {
		base.start (true, Parametros.TUTORIAL_ATIVIDADES);

		Util.hasVisible(panelAlgumSobra, false);
		Util.hasVisible(panelPergunta2, false);
		Util.hasVisible(panelSelecione, false);

		StartCoroutine(iniciarView ());
	}

	private IEnumerator iniciarView(){

		float timeInfinite = 1F;
		while(!liberarStart){
			yield return new WaitForSeconds (timeInfinite);
			timeInfinite = 0.1F;
		}


		numMercadoria = Random.Range(2, 7);
		qtMercadoria  = Random.Range(1, 5);

		Sprite mercadoriaEscolhida = new Sprite ();

		switch(numMercadoria){
		case Parametros.CD_PINTINHO:
			mercadoriaEscolhida = pintinho;
			break;
		case Parametros.CD_GALINHA:
			mercadoriaEscolhida = galinha;
			break;
		case Parametros.CD_SACO_MILHO:
			mercadoriaEscolhida = sacoMilho;
			break;
		case Parametros.CD_PORCO:
			mercadoriaEscolhida = porco;
			break;
		case Parametros.CD_OVELHA:
			mercadoriaEscolhida = ovelha;
			break;
		case Parametros.CD_CAVALO:
			mercadoriaEscolhida = cavalo;
			break;
		case Parametros.CD_VACA:
			mercadoriaEscolhida = vaca;
			break;
		case Parametros.CD_LOTE_TERRA:
			mercadoriaEscolhida = loteTerra;
			break;
		}

		for(int i=0; i < qtMercadoria; i++){
			conjuntoPanelInicial[i].GetComponent<Image>().sprite = mercadoriaEscolhida;
			conjuntoPanelInicial [i].GetComponent<Image> ().color = Util.setColorAlpha (conjuntoPanelInicial [i].GetComponent<Image> ().color, 1);
		}


		registroAtividade = new RegistroAtividade (0, Registro.getRegistro ().getCdRegistro (), Registro.getRegistro ().getCdJogo (), Parametros.CD_ATIVIDADE_RACIOCINIO_DIRETO, 0, 0);
		Result resultado = registroAtividade.save ();
		if (resultado.getCode () < 0) {
			Debug.LogError (resultado.getMessage ());
		}



	}

	// Update is called once per frame
	void Update () {
		base.update ();

		//Recebe o "tempo" ate que de 1 segundo
		contagemTempo += Time.deltaTime;
		//Quando chega em 1 segundo, o contador regressivo decresce, e a contagem de tempo volta a zero
		if(contagemTempo >= 1){
			contagemProgressiva++;
			contagemTempo = 0;
		}

		vlTempo.text = (contagemProgressiva > 9 ? "" : "0") + contagemProgressiva;

	}

}

