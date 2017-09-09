using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class btnCacarPintinhosMundo : MonoBehaviour
{
	public GameObject canvas;
	public GameObject panelDialog;


	public void OnClick (){
		StartCoroutine (delayOnClick (0.7F));
	}

	private IEnumerator delayOnClick(float waitTime){
		yield return new WaitForSeconds (waitTime);

		if((Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_PINTINHO).getQtItem() > 1 ||
			Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_GALINHA).getQtItem() > 1 ||
			Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_SACO_MILHO).getQtItem() > 1 ||
			Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_PORCO).getQtItem() > 1 ||
			Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_OVELHA).getQtItem() > 1 ||
			Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_CAVALO).getQtItem() > 1 ||
			Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_VACA).getQtItem() > 1) && Jogo.getConfiguracao().getLgAvancarSemEsgotarTempo() == 0){

			RectTransform rectPanel = canvas.GetComponentInChildren<RectTransform>(); 
			GameObject respostaTroca = Instantiate(panelDialog);
			respostaTroca.transform.SetParent(rectPanel.transform, false);

			respostaTroca.GetComponentInChildren<Text>().text = "Necessário trocar todos os seus animais no Mercado de Trocas, antes de continuar caçando pintinhos!";

		}
		else{
			TelaMundo.destroyTela ();
			TelaJogoMemoria.getTela ();
			ViewCarregamento.direcionadorTelaCarregamento = Parametros.TELA_JOGO_MEMORIA;
			SceneManager.LoadScene("tela_carregamento");
		}

	}
}

