using UnityEngine;
using System.Collections;

public class DestruirMensagem : MonoBehaviour
{
	public float duracao = 3;

	void Update ()
	{
		if((duracao -= Time.deltaTime) <= 0)
		{
			gameObject.transform.parent.parent.GetComponent<ControladorDeInterface>().subirMensagens();
			Destroy(gameObject);
		}
	}
}
