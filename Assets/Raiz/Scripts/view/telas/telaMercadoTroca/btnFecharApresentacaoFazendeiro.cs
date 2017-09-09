using UnityEngine;
using System.Collections;

public class btnFecharApresentacaoFazendeiro : MonoBehaviour
{

	public void OnClick (){
		StartCoroutine (delayOnClick (0.7F));
	}

	private IEnumerator delayOnClick(float waitTime){
		yield return new WaitForSeconds (waitTime);

		Util.hasVisible(this.gameObject.transform.parent.parent.gameObject, false);
	}

}

