using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewCarregamento : View
{

	public GameObject panelPrincipal;

	public Sprite background1;
	public Sprite background2;
	public Sprite background3;
	public Sprite background4;
	public Sprite background5;

	public static int direcionadorTelaCarregamento;

	// Use this for initialization
	void Start (){
		base.start (false, 0);

		TelaCarregamento.getTela ();
		selecionarBackground ();
		direcionar ();
	}

	void Update(){
		base.update ();
	}

	private void selecionarBackground(){
		int numeroUtilizado = Random.Range(1, 5);

		switch(numeroUtilizado){
			case 1:
				panelPrincipal.GetComponent<Image>().sprite = background1;
				break;

			case 2:
				panelPrincipal.GetComponent<Image>().sprite = background2;
				break;

			case 3:
				panelPrincipal.GetComponent<Image>().sprite = background3;
				break;

			case 4:
				panelPrincipal.GetComponent<Image>().sprite = background4;
				break;

			case 5:
				panelPrincipal.GetComponent<Image>().sprite = background5;
				break;
		}
	}

	private void direcionar(){
		switch(ViewCarregamento.direcionadorTelaCarregamento){
			case Parametros.TELA_JOGO_MEMORIA:
				SceneManager.LoadScene ("tela_jogo_memoria");
				break;

			case Parametros.TELA_MERCADO_TROCA:
				SceneManager.LoadScene ("tela_mercado_trocas");
				break;

			case Parametros.TELA_ATIVIDADES:
				SceneManager.LoadScene ("tela_atividade1");
				break;

			case Parametros.TELA_PRINCIPAL:
				SceneManager.LoadScene ("tela_principal");
				break;

			case Parametros.TELA_INICIAL:
				SceneManager.LoadScene ("tela_inicial");
				break;

			case Parametros.TELA_CENA_POSTERIOR_TUTORIAL_MEMORIA:
				SceneManager.LoadScene ("historia_inicial");
				break;

			case Parametros.TELA_MUNDO:
				SceneManager.LoadScene ("tela_mundo");
				break;

			case Parametros.TELA_OPCOES:
				SceneManager.LoadScene ("tela_opcoes");
				break;

			case Parametros.TELA_MINHA_FAZENDA:
				SceneManager.LoadScene ("tela_minha_fazenda");
				break;
		}
	}
	

}

