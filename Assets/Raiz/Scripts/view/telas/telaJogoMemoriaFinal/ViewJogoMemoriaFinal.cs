using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewJogoMemoriaFinal : View
{

	public Text vlCartasViradas;
	public Text vlTempoFaltando;

	public Text txtNivel;
	public Slider sliderPontuacao;

	public Button btnRefresh;

	public bool passouNivel = false;

	public GameObject panelSubiuNivel;
	private GameObject subiuNivel;

	void Start () {
		base.start (true, 0);

		if(TelaJogoMemoriaFinal.getTela().getQtCartasViradas() > 0 && Jogo.getConfiguracao().getLgAvancarSemEsgotarTempo()==0){
			btnRefresh.gameObject.active = false;
		}

		//Coloca o texto com a pontuacao ganha (fazendo tratamento para menores de 10
		vlCartasViradas.text = (TelaJogoMemoriaFinal.getTela().getQtCartasViradas() < 10 ? "0" : "") + TelaJogoMemoriaFinal.getTela().getQtCartasViradas();

		vlTempoFaltando.text = (TelaJogoMemoriaFinal.getTela().getQtTempoSobrando() < 10 ? "0" : "") + TelaJogoMemoriaFinal.getTela().getQtTempoSobrando();

		//Adicionar a quantidade de pintinhos capturados
		RegistroQuantidadeItem.addPintinhoBD (TelaJogoMemoriaFinal.getTela().getQtCartasViradas());


		txtNivel.text = "NIVEL " + Registro.getRegistro().getQtNivel();
		sliderPontuacao.value = (float)Registro.getRegistro ().getQtExperiencia ();	

		StartCoroutine(FillSlider(1.0f, (TelaJogoMemoriaFinal.getTela().getQtPontosGanhosCartas() + TelaJogoMemoriaFinal.getTela().getQtPontosGanhosTempo())));
	}

	private IEnumerator FillSlider(float timeToWait, float fillQuantity){

		yield return new WaitForSeconds(timeToWait);

		if(fillQuantity > 0){
			Registro.getRegistro().setQtExperiencia(Registro.getRegistro().getQtExperiencia() + 0.05f);
			if(Registro.getRegistro().getQtExperiencia() >= Jogo.getConfiguracao().getQtTotalExperiencia()){
				Registro.getRegistro ().setQtNivel (Registro.getRegistro ().getQtNivel () + 1);
				Registro.getRegistro ().setQtExperiencia (0);
				txtNivel.text = "NIVEL " + Registro.getRegistro ().getQtNivel ();
				sliderPontuacao.value = (float)Registro.getRegistro().getQtExperiencia();
				passouNivel = true;
			}
			else{
				sliderPontuacao.value = (float)Registro.getRegistro().getQtExperiencia();
			}

			float tempoDeEspera = 0.01f;

			StartCoroutine(FillSlider(tempoDeEspera, (fillQuantity - 0.05f)));
		}
		else{
			Result resultado = Registro.getRegistro ().save ();
			if(resultado.getCode() < 0){
				Debug.LogError(resultado.getMessage());
			}

			if(passouNivel){

				RectTransform rectPanel = canvas.GetComponentInChildren<RectTransform>(); 
				subiuNivel = Instantiate(panelSubiuNivel);
				subiuNivel.transform.SetParent(rectPanel.transform, false);
				StartCoroutine(delayPassouNivel(2.5F));

			}
		}
	}

	private IEnumerator delayPassouNivel(float waitTime ){

		yield return new WaitForSeconds (waitTime);

		subiuNivel.SetActive(false);
		subiuNivel.GetComponent<AudioSource>().Stop();

		TelaAtividade1.getTela ();
		SceneManager.LoadSceneAsync("tela_atividade1", LoadSceneMode.Additive);
	}

	void Update(){
		base.update ();
	}
}

