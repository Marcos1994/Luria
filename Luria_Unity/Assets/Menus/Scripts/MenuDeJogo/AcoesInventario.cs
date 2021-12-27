using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AcoesInventario : MonoBehaviour
{
	public bool inventarioCompleto = true;
	public Transform areaItens;
	public Text paginacao;
	public int paginaAtual { get; private set; }
	public int paginaFinal { get; private set; }
	public Hashtable inventario;

	void Start()
	{
		gameObject.transform.localScale = new Vector3(0.75F, 0.75F, 0.75F);

		inventario = (inventarioCompleto) ? ControladorPlayerPrefs.inventario : ControladorPlayerPrefs.inventarioFase;

		if (inventario.Count > 0)
		{
			paginaAtual = 0;
			paginaFinal = (inventario.Count / 5) + 1;
			passarPagina(1);
		}
		else
		{
			for (int i = 0; i < 5; i++)
			{
				areaItens.GetChild(i).Find("Titulo").GetComponent<Text>().text = "";
				areaItens.GetChild(i).Find("Icone").GetComponent<Image>().enabled = false;
			}
		}
	}

	public void passarPagina(int p = 1)
	{
		paginaAtual = Mathf.Clamp(paginaAtual + p, 1, paginaFinal);
		paginacao.text = paginaAtual + "/" + paginaFinal;
		int i = 0;
		int inicio = (paginaAtual - 1) * 5;
		int final = inicio + 5;
		Transform slotItem;
		foreach (Item item in inventario.Values)
		{
			if (i >= inicio)
			{
				if (i >= final)
					break;
				slotItem = areaItens.GetChild(i - inicio);
				slotItem.Find("Titulo").GetComponent<Text>().text = item.nome + " teste x" + item.quantidade;
				slotItem.Find("Icone").GetComponent<Image>().sprite = item.icone;
				slotItem.Find("Icone").GetComponent<Image>().enabled = true;
			}
			i++;
		}

		for (; i < final; i++)
		{
			slotItem = areaItens.GetChild(i - inicio);
			slotItem.Find("Titulo").GetComponent<Text>().text = "";
			slotItem.Find("Icone").GetComponent<Image>().enabled = false;
		}
	}
}
