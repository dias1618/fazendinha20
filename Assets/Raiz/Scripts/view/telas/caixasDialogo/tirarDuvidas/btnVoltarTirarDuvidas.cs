using UnityEngine;
using System.Collections;

public class btnVoltarTirarDuvidas : MonoBehaviour
{

	// Use this for initialization
	public void OnClick (){
		StartCoroutine (delayOnClick (0.7F));
	}

	private IEnumerator delayOnClick(float waitTime){
		yield return new WaitForSeconds (waitTime);
		this.gameObject.transform.parent.gameObject.SetActive(false);
	}
}

