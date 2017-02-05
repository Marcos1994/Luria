using UnityEngine;
using System.Collections;

public class HabiCurar : Habilidade
{
	public GameObject prefabHabilidade;

	void Start()
	{
		nome = "Curar";
		energia = 20;
		recargaTotal = 3.5F;
		recarga = 0;
		alvoInimigo = false;
	}

	//alvo que n tenha mira ainda
	protected override GameObject definirAlvo(ComportamentoDeSoldado soldado)
	{
		int cargas = 100;
		GameObject alvo = null;
		Transform aliados = soldado.gameObject.transform.parent;
		foreach (Transform soldadoTransform in aliados)
			if (soldadoTransform.tag == "Soldado")
			{
				if (!soldadoTransform.GetComponent<ComportamentoDeSoldado>().efeitos.ContainsKey("Efeito Cura"))
				{
					alvo = soldadoTransform.gameObject;
					break;
				}
				else
				{
					Efeito efeito = ((Efeito)soldadoTransform.GetComponent<ComportamentoDeSoldado>().efeitos["Efeito Cura"]);
					if (efeito.cargas < cargas)
					{
						cargas = efeito.cargas;
						alvo = soldadoTransform.gameObject;
					}
				}
			}
		return alvo;
	}

	//coloca debuff de mira no alvo, recarga baixa, custo de mana baixo
	public override void ativar(GameObject soldado)
	{
		ComportamentoDeSoldado compSoldado = soldado.GetComponent<ComportamentoDeSoldado>();
		compSoldado.alvoHabilidade.GetComponent<ComportamentoDeSoldado>().adicionarEfeito(new EfeitoCura());

		GameObject habiInstancia = Instantiate(prefabHabilidade);
		habiInstancia.transform.position = compSoldado.alvoHabilidade.transform.position;
		habiInstancia.transform.parent = compSoldado.alvoHabilidade.transform;
		compSoldado.alvoHabilidade = null;

		//Custo Habilidade
		compSoldado.atributos.energia -= energia;
		recarga = recargaTotal;
	}
}
