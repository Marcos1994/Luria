using UnityEngine;
using System.Collections;

public class ComportamentoEscudo : MonoBehaviour
{
	private Atributos atributosPai;
	private float duracao;
	
	void Start ()
	{
		atributosPai = gameObject.GetComponentInParent<ComportamentoDeSoldado>().atributos;
		duracao = 15;
    }
	
	void Update ()
	{
		if (atributosPai.vida <= 0 ||
			atributosPai.escudo <= 0 ||
			(duracao -= Time.deltaTime) <= 0)
		{
			atributosPai.escudo = 0;
			Destroy(gameObject);
		}
	}
}
