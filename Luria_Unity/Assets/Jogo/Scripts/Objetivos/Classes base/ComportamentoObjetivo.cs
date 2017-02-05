using UnityEngine;
using System.Collections;

public abstract class ComportamentoObjetivo : MonoBehaviour
{
	//Variavel referente ao indice do objetivo no controlador
	public int indice = 0;

	public void concluir(bool atingido = true)
	{
		GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorDeObjetivos>().concluirObjetivo(indice, atingido);
	}
}
