using UnityEngine;
using System.Collections;

public class AreaDeAtaque : MonoBehaviour
{
	public ComportamentoDeUnidade unidade = null;
	public ArrayList itens = new ArrayList();

	public void Start()
	{
		unidade = gameObject.transform.parent.GetComponent<ComportamentoDeUnidade>();
	}

	public void OnTriggerStay(Collider colisor)
	{
		//Se a unidade nao tiver nenhum inimigo e o colisor for uma unidade inimiga
		if (unidade.inimigo == null && colisor.transform.tag == "Soldado")
		{
			//se a tropa que chegou for de um tipo diferente (aliado != inimigo)
			if (colisor.transform.parent.GetComponent<ComportamentoDeUnidade>().tipoTropa != unidade.tipoTropa)
				unidade.definirInimigoParaUnidade(colisor.transform.parent.gameObject);
		}
	}

	public void OnTriggerEnter(Collider colisor)
	{
		if (colisor.transform.tag == "Item")
			itens.Add(colisor.gameObject);
	}

	public void OnTriggerExit(Collider colisor)
	{
		if (colisor.transform.tag == "Item" && itens.Contains(colisor.gameObject))
			itens.Remove(colisor.gameObject);
	}
}
