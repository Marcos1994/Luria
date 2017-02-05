using UnityEngine;
using System.Collections;

public class CarregarItens : MonoBehaviour
{
	public GameObject[] itens; //as chances de cair um item são de 10%;
	public static Hashtable prefabsItens = new Hashtable();

	void Start()
	{
		prefabsItens = new Hashtable();
		foreach (GameObject item in itens)
			prefabsItens.Add(item.GetComponent<ComportamentoItem>().nome, item);
		itens = null;
	}

	public static Item carregarItem(string nome)
	{
		if (prefabsItens.ContainsKey(nome))
			return ((GameObject)prefabsItens[nome]).GetComponent<ComportamentoItem>().Item();
		else
			return null;
    }

	public static GameObject droparItem(string nome)
	{
		GameObject drop = null;
		string[] listaDeDrops = listaDeDrop(nome);
		int limiteDrop = 0;

		//vou somar as chances de drop de cada item
		foreach (string nomeItem in listaDeDrops)
		{
			if (prefabsItens.ContainsKey(nomeItem))
			{
				limiteDrop += ((GameObject)prefabsItens[nomeItem]).GetComponent<ComportamentoItem>().taxaDeDrop;
			}
        }

		//e sortear um numero entre 1 e a somatoria das chances * 10
		int r = Random.Range(1, limiteDrop * 10);
		if (r > limiteDrop) //se o numero tiver dentro do intervalo, sigo em frente
			return null;

		//vou somar novamente limiteDrop enquanto este for menor ou igual ao numero sorteado
		limiteDrop = 0;
		foreach (string nomeItem in listaDeDrops)
		{
			if (prefabsItens.ContainsKey(nomeItem))
			{
				drop = ((GameObject)prefabsItens[nomeItem]);
                limiteDrop += drop.GetComponent<ComportamentoItem>().taxaDeDrop;
				if (r <= limiteDrop)
					break;
			}
		}

		return drop;
	}

	private static string[] listaDeDrop(string nome)
	{
		string[] lista = null;

		switch (nome)
		{
			case "Artilheiro Diktar":
				lista = new string[2];
				lista[1] = "Carregador de Energia";
				break;
			case "Artilheiro Diktar Deserto":
				lista = new string[3];
				lista[1] = "Carregador de Energia";
				lista[2] = "Mantimentos";
				break;
			case "Automato AXK":
				lista = new string[2];
				lista[1] = "Componente Tecnologico";
				break;
			case "Automato AXK Deserto":
				lista = new string[2];
				lista[1] = "Componente Tecnologico";
				break;
			case "Infantaria Diktar":
				lista = new string[2];
				lista[1] = "Quite Medico";
				break;
			case "Infantaria Diktar Deserto":
				lista = new string[3];
				lista[1] = "Quite Medico";
				lista[2] = "Mantimentos";
				break;
			case "Automato TKD":
				lista = new string[3];
				lista[1] = "Componente Tecnologico";
				lista[2] = "Carregador de Energia";
				break;
			case "Automato TKD Deserto":
				lista = new string[3];
				lista[1] = "Componente Tecnologico";
				lista[2] = "Carregador de Energia";
				break;
			default:
				lista = new string[2];
				lista[1] = "Quite Medico";
				break;
		}

		lista[0] = "Experiencia";
		return lista;
	}
}
