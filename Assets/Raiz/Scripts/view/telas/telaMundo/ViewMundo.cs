using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewMundo : View
{
	public Button btnAtividade2;
	public Button btnAtividade4;
	public Button btnAtividade6;

	// Use this for initialization
	void Start (){
		base.start (true, Parametros.TUTORIAL_MUNDO);

		Util.hasVisible(btnAtividade2.gameObject, false);
		Util.hasVisible(btnAtividade4.gameObject, false);
		Util.hasVisible(btnAtividade6.gameObject, false);

		StartCoroutine(iniciarView ());

	}

	private IEnumerator iniciarView(){

		float timeInfinite = 1F;
		while(!liberarStart){
			yield return new WaitForSeconds (timeInfinite);
			timeInfinite = 0.1F;
		}

	}
	
	// Update is called once per frame
	void Update (){
		base.update ();
	}
}

