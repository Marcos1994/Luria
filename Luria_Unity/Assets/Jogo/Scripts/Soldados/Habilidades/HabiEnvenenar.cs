using UnityEngine;
using System.Collections;

public class HabiEnvenenar : Habilidade
{
	public GameObject prefabHabilidade;

	void Start()
	{
		nome = "Envenenar";
		energia = 35;
		recargaTotal = 3.5F;
		recarga = 0;
		alvoInimigo = true;
	}

	//alvo que n tenha mira ainda
	protected override GameObject definirAlvo(ComportamentoDeSoldado soldado)
	{
		int cargas = 100;
		GameObject alvo = null;
		Transform inimigo = soldado.gameObject.GetComponentInParent<ComportamentoDeUnidade>().inimigo.transform;
		foreach (Transform soldadoTransform in inimigo)
			if (soldadoTransform.tag == "Soldado")
			{
				if (!soldadoTransform.GetComponent<ComportamentoDeSoldado>().efeitos.ContainsKey("Efeito Veneno"))
				{
					alvo = soldadoTransform.gameObject;
					break;
				}
				else
				{
					Efeito efeito = ((Efeito)soldadoTransform.GetComponent<ComportamentoDeSoldado>().efeitos["Efeito Veneno"]);
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
		compSoldado.alvoHabilidade.GetComponent<ComportamentoDeSoldado>().adicionarEfeito(new EfeitoVeneno());

		GameObject habiInstancia = Instantiate(prefabHabilidade);
		habiInstancia.transform.position = compSoldado.alvoHabilidade.transform.position;
		habiInstancia.transform.parent = compSoldado.alvoHabilidade.transform;

		//Custo Habilidade
		compSoldado.atributos.energia -= energia;
		recarga = recargaTotal;
	}
}