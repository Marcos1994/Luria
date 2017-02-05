using UnityEngine;
using System.Collections;

public class HabiGerarEscudos : Habilidade
{
	public GameObject prefabHabilidade;

	void Start()
	{
		nome = "Gerar Escudos";
		energia = 30;
		recargaTotal = 10;
		recarga = 0;
		alvoInimigo = false;
	}

	//o proprio soldado
	protected override GameObject definirAlvo(ComportamentoDeSoldado soldado)
	{
		return soldado.gameObject;
	}

	//coloca efeito de escudo em alvos aliados (que n ele), cd alto, energia medio
	public override void ativar(GameObject soldado)
	{
		foreach (Transform soldadoTransform in soldado.transform.parent)
		{
			if (soldadoTransform.tag == "Soldado" &&
				soldadoTransform != soldado.transform)
			{
				GameObject escudoIntancia = Instantiate(prefabHabilidade);
				escudoIntancia.transform.position = soldadoTransform.position;
                escudoIntancia.transform.parent = soldadoTransform;
				soldadoTransform.GetComponent<ComportamentoDeSoldado>().atributos.escudo = 35;
            }
		}

		//Custo Habilidade
		soldado.GetComponent<ComportamentoDeSoldado>().atributos.energia -= energia;
		recarga = recargaTotal;
	}
}