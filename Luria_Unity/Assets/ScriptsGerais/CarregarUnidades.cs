using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class CarregarUnidades : MonoBehaviour
{
	public GameObject[] soldados;
	public static Hashtable prefabsSoldados = new Hashtable();

	void Start ()
	{
		//Anotações
		Debug.Log("Para que um obstaculo seja interpretado pelo bake do NavMesh, deve ser marcado como Estatico");
		Debug.Log("O Controlador de Objetivos deve ser posto no objeto Mapa (tag = GameController)");
		Debug.Log("Alvos especiais devem ter os scripts ObjetivoAlvoEspecial e GatilhoObjetivo");

		prefabsSoldados = new Hashtable();
		foreach (GameObject soldado in soldados)
			prefabsSoldados.Add(soldado.GetComponent<ComportamentoDeSoldado>().nome, soldado);
		soldados = null;
	}

	public static ComportamentoDeSoldado propriedadesSoldado(string nome)
	{
		if (prefabsSoldados.ContainsKey(nome))
			return ((GameObject) prefabsSoldados[nome]).GetComponent<ComportamentoDeSoldado>();
		return null;
	}

	public static void instanciarSoldado(string nome, Transform posicao)
	{
		if (prefabsSoldados.ContainsKey(nome))
		{
			((GameObject) prefabsSoldados[nome]).transform.position = posicao.position;
			GameObject soldado = Instantiate((GameObject) prefabsSoldados[nome]);
			soldado.transform.parent = posicao;
			soldado.transform.position = posicao.position;
			soldado.transform.rotation = posicao.rotation;
		}
	}

	public static void instanciarUnidade(string[] soldados, Transform posicao)
	{
		//soldados é um array de strings no formato "nome do soldado:indiceNaUnidade"
		string[] soldadoArray;

		//formacao[0] = new Vector3( 0,0, 0);
		//formacao[1] = new Vector3(-2,0,-2);	//Inferior Esquerdo
		//formacao[2] = new Vector3( 2,0,-2);	//Inferior Direito
		//formacao[3] = new Vector3( 2,0, 2);	//Superior Direito
		//formacao[4] = new Vector3(-2,0, 2);	//Superior Esquerdo

		GameObject soldado;
		foreach(string soldadoSerializado in soldados)
		{
			soldadoArray = soldadoSerializado.Split(':');
			if (soldadoArray.Length == 2 && prefabsSoldados.ContainsKey(soldadoArray[0]))
			{
				((GameObject) prefabsSoldados[soldadoArray[0]]).transform.position = posicao.position;
				soldado = Instantiate((GameObject) prefabsSoldados[soldadoArray[0]]);
				soldado.transform.parent = posicao;
				soldado.transform.position = posicao.position;
				soldado.transform.rotation = posicao.rotation;
				soldado.GetComponent<ComportamentoDeSoldado>().indiceNaUnidade = int.Parse(soldadoArray[1]);
				switch(soldadoArray[1])
				{
					case "1":
						soldado.transform.position += new Vector3(-2,0,-2);
						break;
					case "2":
						soldado.transform.position += new Vector3(2,0,-2);
						break;
					case "3":
						soldado.transform.position += new Vector3(2,0,2);
						break;
					case "4":
						soldado.transform.position += new Vector3(-2,0,2);
						break;
				}
			}
		}
	}
}
