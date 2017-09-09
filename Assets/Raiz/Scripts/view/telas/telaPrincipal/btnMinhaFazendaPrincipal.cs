using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class btnMinhaFazendaPrincipal : MonoBehaviour
{
	public void OnClick (){
		StartCoroutine (delayOnClick (0.7F));
	}

	private IEnumerator delayOnClick(float waitTime){
		yield return new WaitForSeconds (waitTime);
		SceneManager.LoadScene("tela_minha_fazenda", LoadSceneMode.Additive);
	}
}

