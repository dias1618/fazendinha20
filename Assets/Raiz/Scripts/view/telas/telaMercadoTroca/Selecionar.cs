using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class Selecionar : MonoBehaviour{

	//Modelo da figura do animal, que sera instanciado via codigo quando necessario
	public GameObject prefabAnimal;

	public GameObject panel;
	public GameObject panelTroca;
	public GameObject panelArea;

	public Sprite background;

	void Update (){
		RectTransform objectRectTransform = gameObject.GetComponent<RectTransform> ();                // This section gets the RectTransform information from this object. Height and width are stored in variables. The borders of the object are also defined
		float width = objectRectTransform.rect.width;
		float height = objectRectTransform.rect.height;
		float rightOuterBorder = (width * .5f);
		float leftOuterBorder = (width * -.5f);
		float topOuterBorder = (height * .5f);
		float bottomOuterBorder = (height * -.5f);
		// The following line determines if the cursor is on the object
		if(Input.mousePosition.x <= (transform.position.x + rightOuterBorder) && Input.mousePosition.x >= (transform.position.x + leftOuterBorder) && Input.mousePosition.y <= (transform.position.y + topOuterBorder) && Input.mousePosition.y >= (transform.position.y + bottomOuterBorder))
		{
			PerformRaycast ();                                // Calls the function to perform a raycast
		}
	}

	void PerformRaycast ()                // This function performs a raycast to detect if this object is in front of other objects if any overlap at the cursor's position when the mouse button is initially pressed
	{
		PointerEventData cursor = new PointerEventData(EventSystem.current);                            // This section prepares a list for all objects hit with the raycast
		cursor.position = Input.mousePosition;
		List<RaycastResult> objectsHit = new List<RaycastResult> ();
		EventSystem.current.RaycastAll(cursor, objectsHit);
		//int count = objectsHit.Count;
		int x = 0;


		if(objectsHit[x].gameObject == this.gameObject)                                         
		{    
			if(Input.GetMouseButtonUp(0)){
				if(panelArea != null){
					panelArea.GetComponent<Image>().sprite = background;
					Color temp = panelArea.GetComponent<Image>().color;
					temp.a = 0.6f;
					panelArea.GetComponent<Image>().color = temp;
				}

				if (this.gameObject.tag == "DonaGertrudes"){

					foreach (Transform child in panelTroca.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					TelaMercadoTroca.getTela().setCdAnimalFazendeiroSelecionado(Parametros.CD_GALINHA);
					foreach (Transform child in panel.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
						GameObject prefabGalinhaClone = Instantiate(prefabAnimal);
						prefabGalinhaClone.GetComponent<ClicarAnimal> ().panelTroca = panelTroca;
						prefabGalinhaClone.transform.parent = child;
					}
				}

				if (this.gameObject.tag == "SeuJoaquim"){

					foreach (Transform child in panelTroca.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					TelaMercadoTroca.getTela().setCdAnimalFazendeiroSelecionado(Parametros.CD_SACO_MILHO);

					foreach (Transform child in panel.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
						GameObject prefabSacoMilhoClone = Instantiate(prefabAnimal);
						prefabSacoMilhoClone.GetComponent<ClicarAnimal> ().panelTroca = panelTroca;
						prefabSacoMilhoClone.transform.parent = child;
					}

				}

				if (this.gameObject.tag == "SeuZe"){

					foreach (Transform child in panelTroca.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					TelaMercadoTroca.getTela().setCdAnimalFazendeiroSelecionado(Parametros.CD_PORCO);

					foreach (Transform child in panel.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
						GameObject prefabPorcoClone = Instantiate(prefabAnimal);
						prefabPorcoClone.GetComponent<ClicarAnimal> ().panelTroca = panelTroca;
						prefabPorcoClone.transform.parent = child;
					}

				}

				if (this.gameObject.tag == "VelhoChico"){

					foreach (Transform child in panelTroca.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					TelaMercadoTroca.getTela().setCdAnimalFazendeiroSelecionado(Parametros.CD_OVELHA);

					foreach (Transform child in panel.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
						GameObject prefabOvelhaClone = Instantiate(prefabAnimal);
						prefabOvelhaClone.GetComponent<ClicarAnimal> ().panelTroca = panelTroca;
						prefabOvelhaClone.transform.parent = child;
					}

				}

				if (this.gameObject.tag == "Miguel"){

					foreach (Transform child in panelTroca.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					TelaMercadoTroca.getTela().setCdAnimalFazendeiroSelecionado(Parametros.CD_CAVALO);

					foreach (Transform child in panel.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
						GameObject prefabCavaloClone = Instantiate(prefabAnimal);
						prefabCavaloClone.GetComponent<ClicarAnimal> ().panelTroca = panelTroca;
						prefabCavaloClone.transform.parent = child;
					}

				}

				if (this.gameObject.tag == "DonaMaria"){

					foreach (Transform child in panelTroca.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					TelaMercadoTroca.getTela().setCdAnimalFazendeiroSelecionado(Parametros.CD_VACA);

					foreach (Transform child in panel.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
						GameObject prefabVacaClone = Instantiate(prefabAnimal);
						prefabVacaClone.GetComponent<ClicarAnimal> ().panelTroca = panelTroca;
						prefabVacaClone.transform.parent = child;
					}

				}

				if (this.gameObject.tag == "SeuToninho"){

					foreach (Transform child in panelTroca.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					TelaMercadoTroca.getTela().setCdAnimalFazendeiroSelecionado(Parametros.CD_LOTE_TERRA);

					foreach (Transform child in panel.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
						GameObject prefabLoteTerraoClone = Instantiate(prefabAnimal);
						prefabLoteTerraoClone.GetComponent<ClicarAnimal> ().panelTroca = panelTroca;
						prefabLoteTerraoClone.transform.parent = child;
					}

				}


				if (this.gameObject.tag == "Pintinho"){

					foreach (Transform child in panelTroca.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					foreach (Transform child in panel.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					int qtPintinhos = Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_PINTINHO).getQtItem();
					TelaMercadoTroca.getTela().setCdAnimalJogadorSelecionado(Parametros.CD_PINTINHO);
					TelaMercadoTroca.getTela().setPrefabTroca(prefabAnimal);

					if(qtPintinhos > 0){
						foreach (Transform child in panel.transform){
							GameObject prefabPintinhoClone = Instantiate(prefabAnimal);		
							prefabPintinhoClone.GetComponent<ClicarAnimal> ().panelTroca = panelTroca;
							prefabPintinhoClone.transform.parent = child;

							qtPintinhos--;
							if(qtPintinhos == 0){
								break;
							}
						}
					}

				}

				if (this.gameObject.tag == "Galinha"){

					foreach (Transform child in panelTroca.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					foreach (Transform child in panel.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					int qtGalinhas = Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_GALINHA).getQtItem();
					TelaMercadoTroca.getTela().setCdAnimalJogadorSelecionado(Parametros.CD_GALINHA);
					TelaMercadoTroca.getTela().setPrefabTroca(prefabAnimal);

					if(qtGalinhas > 0){
						foreach (Transform child in panel.transform){
							GameObject prefabGalinhaClone = Instantiate(prefabAnimal);
							prefabGalinhaClone.GetComponent<ClicarAnimal> ().panelTroca = panelTroca;
							prefabGalinhaClone.transform.parent = child;

							qtGalinhas--;
							if(qtGalinhas == 0){
								break;
							}
						}
					}

				}

				if (this.gameObject.tag == "SacoMilho"){

					foreach (Transform child in panelTroca.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					foreach (Transform child in panel.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					int qtSacosMilho = Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_SACO_MILHO).getQtItem();
					TelaMercadoTroca.getTela().setCdAnimalJogadorSelecionado(Parametros.CD_SACO_MILHO);
					TelaMercadoTroca.getTela().setPrefabTroca(prefabAnimal);

					if(qtSacosMilho > 0){
						foreach (Transform child in panel.transform){
							GameObject prefabSacoMilhoClone = Instantiate(prefabAnimal);
							prefabSacoMilhoClone.GetComponent<ClicarAnimal> ().panelTroca = panelTroca;
							prefabSacoMilhoClone.transform.parent = child;

							qtSacosMilho--;
							if(qtSacosMilho == 0){
								break;
							}
						}
					}

				}

				if (this.gameObject.tag == "Porco"){

					foreach (Transform child in panelTroca.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					foreach (Transform child in panel.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					int qtPorcos = Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_PORCO).getQtItem();
					TelaMercadoTroca.getTela().setCdAnimalJogadorSelecionado(Parametros.CD_PORCO);
					TelaMercadoTroca.getTela().setPrefabTroca(prefabAnimal);

					if(qtPorcos > 0){
						foreach (Transform child in panel.transform){
							GameObject prefabPorcoClone = Instantiate(prefabAnimal);
							prefabPorcoClone.GetComponent<ClicarAnimal> ().panelTroca = panelTroca;
							prefabPorcoClone.transform.parent = child;

							qtPorcos--;
							if(qtPorcos == 0){
								break;
							}
						}
					}
				}

				if (this.gameObject.tag == "Ovelha"){

					foreach (Transform child in panelTroca.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					foreach (Transform child in panel.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					int qtOvelhas = Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_OVELHA).getQtItem();
					TelaMercadoTroca.getTela().setCdAnimalJogadorSelecionado(Parametros.CD_OVELHA);
					TelaMercadoTroca.getTela().setPrefabTroca(prefabAnimal);

					if(qtOvelhas > 0){
						foreach (Transform child in panel.transform){
							GameObject prefabOvelhaClone = Instantiate(prefabAnimal);
							prefabOvelhaClone.GetComponent<ClicarAnimal> ().panelTroca = panelTroca;
							prefabOvelhaClone.transform.parent = child;

							qtOvelhas--;
							if(qtOvelhas == 0){
								break;
							}
						}
					}
				}

				if (this.gameObject.tag == "Cavalo"){

					foreach (Transform child in panelTroca.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					foreach (Transform child in panel.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					int qtCavalos = Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_CAVALO).getQtItem();
					TelaMercadoTroca.getTela().setCdAnimalJogadorSelecionado(Parametros.CD_CAVALO);
					TelaMercadoTroca.getTela().setPrefabTroca(prefabAnimal);

					if(qtCavalos > 0){
						foreach (Transform child in panel.transform){
							GameObject prefabCavaloClone = Instantiate(prefabAnimal);
							prefabCavaloClone.GetComponent<ClicarAnimal> ().panelTroca = panelTroca;
							prefabCavaloClone.transform.parent = child;

							qtCavalos--;
							if(qtCavalos == 0){
								break;
							}
						}
					}
				}

				if (this.gameObject.tag == "Vaca"){

					foreach (Transform child in panelTroca.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					foreach (Transform child in panel.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					int qtVacas = Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_VACA).getQtItem();
					TelaMercadoTroca.getTela().setCdAnimalJogadorSelecionado(Parametros.CD_VACA);
					TelaMercadoTroca.getTela().setPrefabTroca(prefabAnimal);

					if(qtVacas > 0){
						foreach (Transform child in panel.transform){
							GameObject prefabVacaClone = Instantiate(prefabAnimal);
							prefabVacaClone.GetComponent<ClicarAnimal> ().panelTroca = panelTroca;
							prefabVacaClone.transform.parent = child;

							qtVacas--;
							if(qtVacas == 0){
								break;
							}
						}
					}
				}


				if (this.gameObject.tag == "LoteTerra"){

					foreach (Transform child in panelTroca.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					foreach (Transform child in panel.transform){
						foreach (Transform childOfChild in child){
							Destroy (childOfChild.gameObject);
						}
					}

					int qtLotesTerra = Registro.getRegistro().getRegistroQuantidadeItem(Parametros.CD_LOTE_TERRA).getQtItem();
					TelaMercadoTroca.getTela().setCdAnimalJogadorSelecionado(Parametros.CD_LOTE_TERRA);
					TelaMercadoTroca.getTela().setPrefabTroca(prefabAnimal);

					if(qtLotesTerra > 0){
						foreach (Transform child in panel.transform){
							GameObject prefabLoteTerraClone = Instantiate(prefabAnimal);
							prefabLoteTerraClone.GetComponent<ClicarAnimal> ().panelTroca = panelTroca;
							prefabLoteTerraClone.transform.parent = child;

							qtLotesTerra--;
							if(qtLotesTerra == 0){
								break;
							}
						}
					}
				}
			}
		}
	}
}
