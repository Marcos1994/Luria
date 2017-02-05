using UnityEngine;
using System.Collections;

public class HabiMirar : Habilidade
{
	public GameObject prefabHabilidade;

	void Start()
	{
		nome = "Mirar";
		energia = 10;
		recargaTotal = 2.5F;
		recarga = 0;
		alvoInimigo = true;
	}

	//alvo que n tenha mira ainda
	protected override GameObject definirAlvo(ComportamentoDeSoldado soldado)
	{
		GameObject alvo = null;
		Transform inimigo = soldado.gameObject.GetComponentInParent<ComportamentoDeUnidade>().inimigo.transform;
		foreach (Transform soldadoTransform in inimigo)
			if (soldadoTransform.tag == "Soldado" &&
				(!soldadoTransform.GetComponent<ComportamentoDeSoldado>().efeitos.ContainsKey("Efeito Mira") ||
				alvo == null))
				alvo = soldadoTransform.gameObject;
		return alvo;
	}

	//coloca debuff de mira no alvo, recarga baixa, custo de mana baixo
	public override void ativar(GameObject soldado)
	{
		recarga = recargaTotal;
		ComportamentoDeSoldado compSoldado = soldado.GetComponent<ComportamentoDeSoldado>();
		compSoldado.alvoHabilidade.GetComponent<ComportamentoDeSoldado>().adicionarEfeito(new EfeitoMira());

		GameObject miraInstancia = Instantiate(prefabHabilidade);
		miraInstancia.transform.position = compSoldado.alvoHabilidade.transform.position;
		miraInstancia.transform.parent = compSoldado.alvoHabilidade.transform;

		//Custo Habilidade
		compSoldado.atributos.energia -= energia;
		recarga = recargaTotal;
	}
}