using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class btnMinhaFazenda : MonoBehaviour{
	public void OnClick(){
		SceneManager.LoadSceneAsync("tela_minha_fazenda", LoadSceneMode.Additive);
	}
}

