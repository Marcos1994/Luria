using UnityEngine;
using System.Collections;

public class HabiSangrar : Habilidade
{
	public GameObject prefabHabilidade;

	void Start()
	{
		nome = "Sangrar";
		energia = 9;
		recargaTotal = 2.5F;
		recarga = 0;
		alvoInimigo = true;
	}

	//Algum soldado que ja n esteja sangrando
	protected override GameObject definirAlvo(ComportamentoDeSoldado soldado)
	{
		int cargas = 100;
		GameObject alvo = null;
		Transform inimigo = soldado.gameObject.GetComponentInParent<ComportamentoDeUnidade>().inimigo.transform;
		foreach (Transform soldadoTransform in inimigo)
			if (soldadoTransform.tag == "Soldado")
			{
				if (!soldadoTransform.GetComponent<ComportamentoDeSoldado>().efeitos.ContainsKey("Efeito Sangrar"))
				{
					alvo = soldadoTransform.gameObject;
					break;
				}
				else
				{
					Efeito efeito = ((Efeito)soldadoTransform.GetComponent<ComportamentoDeSoldado>().efeitos["Efeito Sangrar"]);
                    if (efeito.cargas < cargas)
					{
						cargas = efeito.cargas;
						alvo = soldadoTransform.gameObject;
					}
				}
			}
		return alvo;
	}

	//add debuff em vez de dano, cd baixo e consumo medio de energia
	public override void ativar(GameObject soldado)
	{
		ComportamentoDeSoldado compSoldado = soldado.GetComponent<ComportamentoDeSoldado>();
		compSoldado.alvoHabilidade.GetComponent<ComportamentoDeSoldado>().adicionarEfeito(new EfeitoSangrar());

		GameObject sangriaInstancia = Instantiate(prefabHabilidade);
		sangriaInstancia.transform.position = compSoldado.alvoHabilidade.transform.position;
		sangriaInstancia.transform.parent = compSoldado.alvoHabilidade.transform;

		//Custo Habilidade
		compSoldado.atributos.energia -= energia;
		recarga = recargaTotal;
	}
}