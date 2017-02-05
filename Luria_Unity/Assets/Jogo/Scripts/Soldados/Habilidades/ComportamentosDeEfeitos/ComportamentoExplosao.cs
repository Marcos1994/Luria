using UnityEngine;
using System.Collections;

public class ComportamentoExplosao : MonoBehaviour
{
	private float duracao = 2;

	void Update()
	{
		if ((duracao -= Time.deltaTime) < 0)
			destruirEfeito();
    }

	public void destruirEfeito()
	{
		Destroy(gameObject);
	}
}
