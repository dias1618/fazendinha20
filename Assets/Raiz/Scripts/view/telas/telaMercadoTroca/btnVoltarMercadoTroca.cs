﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class btnVoltarMercadoTroca : MonoBehaviour
{

	public void OnClick(){
		StartCoroutine(delayOnClick(0.7F));

	}

	private IEnumerator delayOnClick(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);

		TelaMercadoTroca.destroyTela ();
		TelaMundo.getTela ();
		ViewCarregamento.direcionadorTelaCarregamento = Parametros.TELA_MUNDO;
		SceneManager.LoadScene ("tela_carregamento");

	}
}
