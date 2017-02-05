using UnityEngine;
using System.Collections;

public class ControladorDeInventario : MonoBehaviour
{
	private Hashtable inventario = new Hashtable();

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
			salvarInventario();
    }

	public void adicionarItem(Item item)
	{
		gameObject.GetComponent<ControladorDeJogo>().escreverMensagem("Item " + item.nome + " coletado");
		if (inventario.ContainsKey(item.nome))
			((Item)inventario[item.nome]).quantidade += item.quantidade;
		else
			inventario.Add(item.nome, item);

		ObjetivoColetarItem[] objetivos = gameObject.GetComponents<ObjetivoColetarItem>();
		foreach (ObjetivoColetarItem obj in objetivos)
			obj.coletarItem(item.nome);
	}

	public void salvarInventario()
	{
		foreach (Item item in inventario.Values)
			ControladorPlayerPrefs.adicionarAoInventario(item);
		ControladorPlayerPrefs.salvarInventario();
    }
}
