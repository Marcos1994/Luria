using UnityEngine;
using System.Collections;

public class ComportamentoEfeito : MonoBehaviour
{
	public string nomeEfeito;
	private ComportamentoDeSoldado soldado;

	void Start ()
	{
		soldado = gameObject.GetComponentInParent<ComportamentoDeSoldado>();
	}
	
	void Update ()
	{
		if (soldado.atributos.vida <= 0 ||
			!soldado.efeitos.ContainsKey(nomeEfeito))
			Destroy(gameObject);
	}
}
