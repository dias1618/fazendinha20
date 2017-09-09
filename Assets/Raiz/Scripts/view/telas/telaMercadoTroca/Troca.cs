using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Troca : MonoBehaviour{

	//Painel onde ficarao os animais do jogador para serem trocados
	public GameObject panelTrocaJogador;
	//Painel onde ficarao os animais do computador para serem trocados
	public GameObject panelTrocaComputador;

	//Painel onde ficarao os animais do jogador
	public GameObject panelJogador;
	//Painel onde ficarao os animais do computador
	public GameObject panelComputador;



	//Registro buscado do banco de dados para saber a quantidade de pintinhos do jogador
	private ArrayList registroQuantJogador;
	//Quantidade de pintinhos do jogador
	private int qtAnimaisJogadorTotal;

	//Registro buscado do banco de dados para saber a quantidade de galinhas do jogador
	private ArrayList registroQuantComputador;
	//Quantidade de galinhas do jogador
	private int qtAnimaisComputadorTotal;

	//Modelo da figura do pintinho, que sera instanciado via codigo quando necessario
	public GameObject prefabPintinho;
	//Modelo da figura da galinha, que sera instanciado via codigo quando necessario
	public GameObject prefabGalinha;
	//Modelo da figura do saco de milho, que sera instanciado via codigo quando necessario
	public GameObject prefabSacoMilho;
	//Modelo da figura do porco, que sera instanciado via codigo quando necessario
	public GameObject prefabPorco;
	//Modelo da figura da ovelha, que sera instanciado via codigo quando necessario
	public GameObject prefabOvelha;
	//Modelo da figura do cavalo, que sera instanciado via codigo quando necessario
	public GameObject prefabCavalo;
	//Modelo da figura da vaca, que sera instanciado via codigo quando necessario
	public GameObject prefabVaca;
	//Modelo da figura do lote de terra, que sera instanciado via codigo quando necessario
	public GameObject prefabLoteTerra;

	public Text quantPintinhosText;
	public Text quantGalinhasText;
	public Text quantSacosMilhoText;
	public Text quantPorcosText;
	public Text quantOvelhasText;
	public Text quantCavalosText;
	public Text quantVacasText;


	public GameObject panelDialog;
	public GameObject canvas;

	public Slider sliderNivel;
	public Text txtNivel;

	public GameObject panelSubiuNivel;

	public ArrayList registroQuantVezes;

	public GameObject prefabDonaGertrudes;
	public GameObject prefabSeuJoaquim;
	public GameObject prefabSeuZe;
	public GameObject prefabVelhoChico;
	public GameObject prefabMiguel;
	public GameObject prefabDonaMaria;
	public GameObject prefabSeuToninho;

	public GameObject prefabObjetivoCumprido;

	public GameObject panelLoteTerraConquistado;

	private int novoNivel;
	private string novosObjetivos;
	private string txtFalaFazendeiro;
	private GameObject prefabFazendeiro;
	private GameObject loteTerraConquistado;

	public GameObject prefabConquistasMensagem;

	public void OnClick(){
		AudioSource audio = this.transform.FindChild("clicarBotao").GetComponent<AudioSource>();
		audio.volume *= (float)Jogo.getConfiguracao().getVlVolume();
		audio.Play();
		StartCoroutine(delayTroca(0.5F));
	}

	private int somadorTroca = 0;
	private IEnumerator delayTroca(float waitTime){
		yield return new WaitForSeconds (waitTime);

		limparMensagemConquistas ();

		AudioSource audio = this.transform.FindChild("clicarBotao").GetComponent<AudioSource>();
		audio.volume /= (float)Jogo.getConfiguracao().getVlVolume();
		audio.Stop();

		RectTransform rectPanel = canvas.GetComponentInChildren<RectTransform>(); 
		GameObject respostaTroca = Instantiate(panelDialog);
		respostaTroca.transform.SetParent(rectPanel.transform, false);

		//Busca a quantidade de pintinhos que foram colocados no painel de troca do jogador
		int quantAnimaisJogador = 0;
		foreach(Transform child in panelTrocaJogador.transform){
			quantAnimaisJogador += child.childCount;
		}

		//Busca a quantidade de galinhas que foram colocadas no painel de troca do computador
		int quantAnimaisComputador = 0;
		foreach(Transform child  in panelTrocaComputador.transform){
			quantAnimaisComputador += child.childCount;
		}

		//Caso o jogador nao tenha colocado qualquer animal nos paineis de troca
		if(quantAnimaisJogador == 0 && quantAnimaisComputador == 0){
			respostaTroca.SetActive(true);
			respostaTroca.GetComponentInChildren<Text>().text = "Nao ha animais para serem trocados";
			Registro.getRegistro ().setQtTrocasSemErrar (0);
			Result resultado = Registro.getRegistro ().save ();
			if (resultado.getCode () < 0) {
				Debug.LogError (resultado.getMessage ());
			}
		}
		else{

			int valorAnimaisJogador = (int) Mathf.Pow(2, TelaMercadoTroca.getTela().getCdAnimalJogadorSelecionado()) * quantAnimaisJogador;
			int valorAnimaisComputador = (int) Mathf.Pow(2, TelaMercadoTroca.getTela().getCdAnimalFazendeiroSelecionado()) * quantAnimaisComputador;

			//Caso o jogador tenha colocado a proporçao correta entre animais para se trocar
			if(valorAnimaisJogador == valorAnimaisComputador && 
				valorAnimaisJogador != 0 && 
				valorAnimaisComputador != 0){

				somadorTroca = 0;

				if(TelaMercadoTroca.getTela().getCdAnimalJogadorSelecionado() < TelaMercadoTroca.getTela().getCdAnimalFazendeiroSelecionado()){
					bool primeiraVez = RegistroObjetivo.addObjetivoBD (Parametros.CD_OBJETIVO_TROCA_DIRETA);

					if(primeiraVez){
						Objetivo objetivo = new Objetivo(Parametros.CD_OBJETIVO_TROCA_DIRETA, Jogo.getJogo().getCdJogo());
						Result resultado = objetivo.get ();
						if (resultado.getCode () < 0) {
							Debug.LogError (resultado.getMessage ());
						}

						somadorTroca += objetivo.getQtExperienciaGanha ();
						chamarObjetivoCumprido("Troca Direta");
					}	
				}
				else{
					bool primeiraVez = RegistroObjetivo.addObjetivoBD (Parametros.CD_OBJETIVO_TROCA_INVERSA);

					if(primeiraVez){
						Objetivo objetivo = new Objetivo (Parametros.CD_OBJETIVO_TROCA_INVERSA, Jogo.getJogo().getCdJogo());
						Result resultado = objetivo.get ();
						if (resultado.getCode () < 0) {
							Debug.LogError (resultado.getMessage ());
						}

						somadorTroca += objetivo.getQtExperienciaGanha ();
						chamarObjetivoCumprido("Troca Inversa");
					}	
						
				}

				Registro.getRegistro ().setQtTrocasSemErrar (Registro.getRegistro ().getQtTrocasSemErrar() + 1);
				Result result = Registro.getRegistro ().save ();
				if (result.getCode () < 0) {
					Debug.LogError (result.getMessage ());
				}


				if(Registro.getRegistro ().getQtTrocasSemErrar() == 10){

					bool primeiraVez = RegistroObjetivo.addObjetivoBD (Parametros.CD_OBJETIVO_REALIZOU_10_SEM_ERRAR);

					if (primeiraVez) {
						chamarObjetivoCumprido("10 trocas sem errar");	
					}
				}
				else if(Registro.getRegistro ().getQtTrocasSemErrar() == 30){
					bool primeiraVez = RegistroObjetivo.addObjetivoBD (Parametros.CD_OBJETIVO_REALIZOU_30_SEM_ERRAR);

					if (primeiraVez) {
						chamarObjetivoCumprido("30 trocas sem errar");	
					}
				}
				else if(Registro.getRegistro ().getQtTrocasSemErrar() == 50){
					bool primeiraVez = RegistroObjetivo.addObjetivoBD (Parametros.CD_OBJETIVO_REALIZOU_50_SEM_ERRAR);

					if (primeiraVez) {
						chamarObjetivoCumprido("50 trocas sem errar");	
					}	

					Registro.getRegistro ().setQtTrocasSemErrar (0);
					result = Registro.getRegistro ().save ();
					if (result.getCode () < 0) {
						Debug.LogError (result.getMessage ());
					}
				}

				respostaTroca.SetActive(false);

				chamarNovoFazendeiro (Parametros.CD_GALINHA, Parametros.CD_OBJETIVO_GANHOU_UMA_GALINHA, prefabSeuJoaquim);
				chamarNovoFazendeiro (Parametros.CD_SACO_MILHO, Parametros.CD_OBJETIVO_GANHOU_UM_SACO_MILHO, prefabSeuZe);
				chamarNovoFazendeiro (Parametros.CD_PORCO, Parametros.CD_OBJETIVO_GANHOU_UM_PORCO, prefabVelhoChico);
				chamarNovoFazendeiro (Parametros.CD_OVELHA, Parametros.CD_OBJETIVO_GANHOU_UMA_OVELHA, prefabMiguel);
				chamarNovoFazendeiro (Parametros.CD_CAVALO, Parametros.CD_OBJETIVO_GANHOU_UM_CAVALO, prefabDonaMaria);
				chamarNovoFazendeiro (Parametros.CD_VACA, Parametros.CD_OBJETIVO_GANHOU_UMA_VACA, prefabSeuToninho);

				if((quantAnimaisJogador == 4 && quantAnimaisComputador == 1) || (quantAnimaisComputador == 4 && quantAnimaisJogador == 1)){
					bool primeiraVez = RegistroObjetivo.addObjetivoBD (Parametros.CD_OBJETIVO_REALIZOU_TROCA_4_1);

					if(primeiraVez){
						Objetivo objetivo = new Objetivo (Parametros.CD_OBJETIVO_REALIZOU_TROCA_4_1, Jogo.getJogo().getCdJogo());
						Result resultado = objetivo.get ();
						if (resultado.getCode () < 0) {
							Debug.LogError (resultado.getMessage ());
						}
						somadorTroca += objetivo.getQtExperienciaGanha ();
						chamarObjetivoCumprido("Troca 4 por 1");
					}	
				}

				if((quantAnimaisJogador == 4 && quantAnimaisComputador == 2) || (quantAnimaisComputador == 4 && quantAnimaisJogador == 2)){
					bool primeiraVez = RegistroObjetivo.addObjetivoBD (Parametros.CD_OBJETIVO_REALIZOU_TROCA_4_2);

					if(primeiraVez){
						Objetivo objetivo = new Objetivo (Parametros.CD_OBJETIVO_REALIZOU_TROCA_4_2, Jogo.getJogo().getCdJogo());
						Result resultado = objetivo.get ();
						if (resultado.getCode () < 0) {
							Debug.LogError (resultado.getMessage ());
						}
						somadorTroca += objetivo.getQtExperienciaGanha ();
						chamarObjetivoCumprido("Troca 4 por 2");
					}	
				}

				bool passouNivel = Registro.getRegistro ().addExperiencia ((double)somadorTroca);
				if (passouNivel) {
					txtNivel.text = "NIVEL " + Registro.getRegistro().getQtNivel();
					if (TelaMercadoTroca.getTela().getCdAnimalFazendeiroSelecionado() != Parametros.CD_LOTE_TERRA)
						chamadaSubiuNivel (Registro.getRegistro().getQtNivel());
				} 

				sliderNivel.value = (float)Registro.getRegistro().getQtExperiencia();

				if(Registro.getRegistro().getQtTrocasSemErrar() == 1){
					respostaTroca.SetActive(true);
					respostaTroca.gameObject.GetComponentInChildren<Text>().text = "Parabens. Voce acertou a troca!";
					respostaTroca.gameObject.transform.localScale = new Vector3(1F, 0.7F, 0F);
				}


				//Faz uma busca para saber a quantidade de animais do tipo que ele trocou, para diminuir o total
				RegistroQuantidadeItem registroQuantidadeItem = Registro.getRegistro().getRegistroQuantidadeItem(TelaMercadoTroca.getTela().getCdAnimalJogadorSelecionado());
				registroQuantidadeItem.setQtItem(registroQuantidadeItem.getQtItem() - quantAnimaisJogador);
				result = registroQuantidadeItem.save();
				if (result.getCode () < 0) {
					Debug.LogError (result.getMessage ());
				}

				//Faz uma busca para saber a quantidade de animais do tipo que ele ganhou na troca, para aumentar o total
				registroQuantidadeItem = Registro.getRegistro().getRegistroQuantidadeItem(TelaMercadoTroca.getTela().getCdAnimalFazendeiroSelecionado());
				registroQuantidadeItem.setQtItem(registroQuantidadeItem.getQtItem() + quantAnimaisComputador);
				result = registroQuantidadeItem.save();
				if (result.getCode () < 0) {
					Debug.LogError (result.getMessage ());
				}

				quantPintinhosText.text  = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_PINTINHO).getQtItem();
				quantGalinhasText.text 	 = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_GALINHA).getQtItem();
				quantSacosMilhoText.text = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_SACO_MILHO).getQtItem();
				quantPorcosText.text 	 = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_PORCO).getQtItem();
				quantOvelhasText.text 	 = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_OVELHA).getQtItem();
				quantCavalosText.text 	 = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_CAVALO).getQtItem();
				quantVacasText.text 	 = "" + Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_VACA).getQtItem();

				//Limpa os slots do painel de troca do jogador
				foreach(Transform child  in panelJogador.transform){
					foreach(Transform childSlot in child.transform){
						Destroy(childSlot.gameObject);
					}
				}

				int qtAnimal = Registro.getRegistro().getRegistroQuantidadeItem(TelaMercadoTroca.getTela().getCdAnimalJogadorSelecionado()).getQtItem();
				if(qtAnimal > 0){
					foreach (Transform child in panelJogador.transform){
						GameObject prefabClone = Instantiate(TelaMercadoTroca.getTela().getPrefabTroca());
						ClicarAnimal clicarAnimal1 = (ClicarAnimal) prefabClone.GetComponent("ClicarAnimal");
						clicarAnimal1.panelTroca = panelTrocaJogador;	
						prefabClone.transform.parent = child;

						qtAnimal--;
						if(qtAnimal == 0){
							break;
						}
					}
				}


				//Limpa os slots do painel de troca do jogador
				foreach(Transform child in panelTrocaJogador.transform){
					foreach(Transform childSlot in child.transform){
						Destroy(childSlot.gameObject);
					}
				}

				//Limpa os slots do painel de troca do computador
				foreach(Transform childComputador in panelTrocaComputador.transform){
					foreach(Transform childSlotComputador in childComputador.transform){
						Destroy(childSlotComputador.gameObject);
					}
				}

				//Lota novamente os slots do computador
				foreach(Transform child in panelComputador.transform){
					if(child.childCount == 0){
						GameObject prefabAnimalComputadorCloneIncluir = new GameObject(); 
						if(TelaMercadoTroca.getTela().getCdAnimalFazendeiroSelecionado() == Parametros.CD_GALINHA)
							prefabAnimalComputadorCloneIncluir = Instantiate(prefabGalinha);
						if(TelaMercadoTroca.getTela().getCdAnimalFazendeiroSelecionado() == Parametros.CD_SACO_MILHO)
							prefabAnimalComputadorCloneIncluir = Instantiate(prefabSacoMilho);
						if(TelaMercadoTroca.getTela().getCdAnimalFazendeiroSelecionado() == Parametros.CD_PORCO)
							prefabAnimalComputadorCloneIncluir = Instantiate(prefabPorco);		
						if(TelaMercadoTroca.getTela().getCdAnimalFazendeiroSelecionado() == Parametros.CD_OVELHA)
							prefabAnimalComputadorCloneIncluir = Instantiate(prefabOvelha);
						if(TelaMercadoTroca.getTela().getCdAnimalFazendeiroSelecionado() == Parametros.CD_CAVALO)
							prefabAnimalComputadorCloneIncluir = Instantiate(prefabCavalo);
						if(TelaMercadoTroca.getTela().getCdAnimalFazendeiroSelecionado() == Parametros.CD_VACA)
							prefabAnimalComputadorCloneIncluir = Instantiate(prefabVaca);
						if(TelaMercadoTroca.getTela().getCdAnimalFazendeiroSelecionado() == Parametros.CD_LOTE_TERRA)
							prefabAnimalComputadorCloneIncluir = Instantiate(prefabLoteTerra);		
						ClicarAnimal clicarAnimal2 = (ClicarAnimal) prefabAnimalComputadorCloneIncluir.GetComponent("ClicarAnimal");
						clicarAnimal2.panelTroca = panelTrocaComputador;		
						prefabAnimalComputadorCloneIncluir.transform.parent = child;
					}
				}

				if (TelaMercadoTroca.getTela ().getCdAnimalFazendeiroSelecionado () == Parametros.CD_LOTE_TERRA) {
					chamarLoteTerraConquistado ();
				}
				else if (hasChamarMensagemConquista ()) {
					chamarMensagemConquistas ();	
				}

			}
			//Caso o jogador erre, o sistema apenas indica uma mensagem de erro
			//VERIFICAR O QUE SE PODE FAZER NESSE CASO PARA AJUDAR O JOGADOR
			else{
				respostaTroca.SetActive(true);
				respostaTroca.GetComponentInChildren<Text>().text = "Infelizmente a troca nao esta feita corretamente. Reveja!";
				respostaTroca.gameObject.transform.localScale = new Vector3(1F, 0.7F, 0F);
				Registro.getRegistro().setQtTrocasSemErrar(0);

				int cdObjetivo = 0;
				switch(TelaMercadoTroca.getTela().getCdAnimalJogadorSelecionado()){

					case Parametros.CD_GALINHA:
						cdObjetivo = Parametros.CD_OBJETIVO_GANHOU_UMA_GALINHA;
						break;

					case Parametros.CD_SACO_MILHO:
						cdObjetivo = Parametros.CD_OBJETIVO_GANHOU_UM_SACO_MILHO;
						break;

					case Parametros.CD_PORCO:
						cdObjetivo = Parametros.CD_OBJETIVO_GANHOU_UM_PORCO;
						break;

					case Parametros.CD_OVELHA:
						cdObjetivo = Parametros.CD_OBJETIVO_GANHOU_UMA_OVELHA;
						break;

					case Parametros.CD_CAVALO:
						cdObjetivo = Parametros.CD_OBJETIVO_GANHOU_UM_CAVALO;
						break;

					case Parametros.CD_VACA:
						cdObjetivo = Parametros.CD_OBJETIVO_GANHOU_UMA_VACA;
						break;

				}

				RegistroObjetivo.addObjetivoErroBD (cdObjetivo);

			}
		}
	}

	private void chamarNovoFazendeiro(int cdAnimal, int cdObjetivo, GameObject prefabFazendeiro){
		if(TelaMercadoTroca.getTela().getCdAnimalFazendeiroSelecionado() == cdAnimal){
			if(Registro.getRegistro().getRegistroObjetivo(cdObjetivo).getQtVezes() == 0 && cdAnimal != Parametros.CD_LOTE_TERRA){
				chamarFazendeiro(prefabFazendeiro, ApresentacaoFazendeiros.arrayTexto[(cdAnimal-1)].ToString());
			}

			bool primeiraVez = RegistroObjetivo.addObjetivoBD (cdObjetivo);
			if (primeiraVez) {
				Objetivo objetivo = new Objetivo (cdObjetivo, Jogo.getJogo ().getCdJogo ());
				Result resultado = objetivo.get ();
				if (resultado.getCode () < 0) {
					Debug.LogError (resultado.getMessage ());
					return;
				}

				somadorTroca += objetivo.getQtExperienciaGanha ();
			}
		}


	}


	private void limparMensagemConquistas(){
		novoNivel = 0;
		novosObjetivos = null;
		txtFalaFazendeiro = null;
		prefabFazendeiro = null;
	}

	private bool hasChamarMensagemConquista(){
		return novoNivel > 0 || novosObjetivos != null || prefabFazendeiro != null;
	}

	private void chamarMensagemConquistas(){

		RectTransform rectPanel = canvas.GetComponentInChildren<RectTransform>(); 
		GameObject panelConquistasMensagem = Instantiate(prefabConquistasMensagem);
		panelConquistasMensagem.transform.SetParent(rectPanel.transform, false);

		GameObject txtNivel = panelConquistasMensagem.transform.FindChild ("PanelArmazenaNivel").gameObject.transform.FindChild ("txtNivel").gameObject;
		GameObject textNovoNivel = panelConquistasMensagem.transform.FindChild ("PanelArmazenaNivel").gameObject.transform.FindChild ("textNovo").gameObject;
		txtNivel.GetComponent<Text> ().text = (Registro.getRegistro ().getQtNivel () < 10 ? "0" + Registro.getRegistro ().getQtNivel () : "" + Registro.getRegistro ().getQtNivel ());
		if (novoNivel > 0) {
			textNovoNivel.SetActive (true);
		} 


		GameObject txtObjetivo = panelConquistasMensagem.transform.FindChild ("PanelArmazenaObjetivo").gameObject.transform.FindChild ("txtObjetivo").gameObject;
		GameObject textNovoObjetivo = panelConquistasMensagem.transform.FindChild ("PanelArmazenaObjetivo").gameObject.transform.FindChild ("textNovo").gameObject;
		if (novosObjetivos != null) {
			novosObjetivos = novosObjetivos.Remove (novosObjetivos.Length - 2);
			txtObjetivo.GetComponent<Text> ().text = novosObjetivos;
			textNovoObjetivo.SetActive (true);
		} 
		else {
			txtObjetivo.GetComponent<Text> ().text = "Nenhum";
		}

		GameObject panelFazendeiro = panelConquistasMensagem.transform.FindChild ("PanelArmazenaFazendeiro").gameObject.transform.FindChild ("panelFazendeiro").gameObject;
		GameObject figuraPanelFazendeiro = panelFazendeiro.transform.FindChild ("figuraPanelFazendeiro").gameObject;
		GameObject textNovoFazendeiro = panelConquistasMensagem.transform.FindChild ("PanelArmazenaFazendeiro").gameObject.transform.FindChild ("textNovo").gameObject;
		if (prefabFazendeiro != null) {
			
			figuraPanelFazendeiro = Instantiate (prefabFazendeiro);
			figuraPanelFazendeiro.transform.SetParent (panelFazendeiro.transform, false);

			GameObject txtFalaFazendeiro = panelConquistasMensagem.transform.FindChild ("PanelArmazenaFazendeiro").transform.FindChild ("txtFalaFazendeiro").gameObject;
			txtFalaFazendeiro.GetComponent<Text> ().text = this.txtFalaFazendeiro;

			textNovoFazendeiro.SetActive (true);
		} 
		else {
			figuraPanelFazendeiro = Instantiate (prefabSeuToninho);
			figuraPanelFazendeiro.transform.SetParent (panelFazendeiro.transform, false);

			GameObject txtFalaFazendeiro = panelConquistasMensagem.transform.FindChild ("PanelArmazenaFazendeiro").transform.FindChild ("txtFalaFazendeiro").gameObject;
			txtFalaFazendeiro.GetComponent<Text> ().text = "Não há um novo fazendeiro(a)";
		}

	}

	private void chamarObjetivoCumprido(string txtObjetivo){
		if (novosObjetivos == null) {
			novosObjetivos = txtObjetivo + ", ";
		} 
		else {
			novosObjetivos += txtObjetivo + ", ";
		}
	}

	private void chamadaSubiuNivel(int qtNivel){
		novoNivel = qtNivel;

		StartCoroutine(delayChamadaSubiuNivel(2.5F));
	}

	private IEnumerator delayChamadaSubiuNivel(float waitTime){

		yield return new WaitForSeconds (waitTime);

		TelaAtividade1.getTela ();
		SceneManager.LoadSceneAsync("tela_atividade1", LoadSceneMode.Additive);
	}

	private void chamarFazendeiro(GameObject prefabFazendeiro, string txtFalaFazendeiro){
		this.prefabFazendeiro = prefabFazendeiro;
		this.txtFalaFazendeiro = txtFalaFazendeiro;
	}


	private void chamarLoteTerraConquistado(){
		RectTransform rectPanel = canvas.GetComponentInChildren<RectTransform>(); 
		loteTerraConquistado = Instantiate(panelLoteTerraConquistado);
		loteTerraConquistado.transform.SetParent(rectPanel.transform, false);
		StartCoroutine(delayChamarLoteConquistado(4F));
	}

	private IEnumerator delayChamarLoteConquistado(float waitTime){

		yield return new WaitForSeconds (waitTime);

		TelaCenas.getTela().setNrCenaHistoriaInicial(Parametros.NR_CENA_ENCONTRO_MORADORES_PARTE_FINAL);
		SceneManager.LoadScene("historia_inicial");
	}

}

