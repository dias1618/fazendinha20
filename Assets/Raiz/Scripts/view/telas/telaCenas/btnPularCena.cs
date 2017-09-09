using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class btnPularCena : MonoBehaviour
{

	public bool ativarProximo;

	public Sprite[] sprites;

	public void OnClick (){
		if(!ativarProximo){
			Camera.main.GetComponent<ViewCenas>().pular = true;
		}
		else{

			if(ViewCenas.reiniciarDados){

				string nmJogador = Camera.main.GetComponent<ViewCenas>().digitarNome.GetComponentInChildren<Text>().text;
				if(nmJogador == null || nmJogador == ""){
					return;
				}

				ViewCenas.reiniciarDados = false;

				Dao.deleteAll ("Registro_Etapa_Atividade");
				Dao.deleteAll ("Registro_Atividade");
				Dao.deleteAll ("Registro_Item");
				Dao.deleteAll ("Registro_Quantidade_Item");
				Dao.deleteAll ("Registro_Objetivo");
				Dao.deleteAll ("Registro_Tutorial");
				Dao.deleteAll ("Registro");

				Registro.setRegistro (null);
				Registro.getRegistro (nmJogador, true);

			}
			if(TelaCenas.getTela().getNrCenaHistoriaInicial() == Parametros.NR_CENA_PIPO_AGINDO_ESTRANHO){
				TelaJogoMemoria.getTela (true);
				ViewCarregamento.direcionadorTelaCarregamento = Parametros.TELA_JOGO_MEMORIA;
				SceneManager.LoadScene("tela_carregamento");
			}
			else if(TelaCenas.getTela().getNrCenaHistoriaInicial() == Parametros.NR_CENA_ENCONTRO_MORADORES_PARTE_FINAL){
				TelaCenas.destroyTela ();
				TelaMercadoTroca.getTela (true);
				ViewCarregamento.direcionadorTelaCarregamento = Parametros.TELA_MERCADO_TROCA;
				SceneManager.LoadScene("tela_carregamento");
			}
			else if(TelaCenas.getTela().getNrCenaHistoriaInicial() == Parametros.NR_CENA_ENCERRAMENTO_CENA_INICIAL){
				TelaCenas.destroyTela ();
				TelaFinalJogo.getTela ();
				SceneManager.LoadScene("tela_final_jogo");
			}
			else
				Camera.main.GetComponent<ViewCenas>().chamarCena();
		}
	}

}

