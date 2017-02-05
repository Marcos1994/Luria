using UnityEngine;
using System.Collections;

public class HabilidadeBasica : Habilidade
{
	void Start()
	{
		nome = "Auto ataque";
		energia = 0;
		recarga = 0;
		recargaTotal = 0;
		alvoInimigo = true;
	}

	protected override GameObject definirAlvo(ComportamentoDeSoldado soldado)
	{
		GameObject alvo = null;
		Transform inimigo = soldado.gameObject.GetComponentInParent<ComportamentoDeUnidade>().inimigo.transform;
		if (inimigo.transform.childCount > 1)
			alvo = inimigo.GetChild(Random.Range(1, inimigo.childCount)).gameObject;
		return alvo;
	}

	public override void ativar(GameObject soldado)
	{
		ComportamentoDeSoldado compSoldado = soldado.GetComponent<ComportamentoDeSoldado>();
		Atributos atributos = compSoldado.atributos;
		int dano = (int) ((atributos.atributoBase() + atributos.resistencia/5) * (0.5F + atributos.taxaCritico()));
		compSoldado.alvoHabilidade.GetComponent<ComportamentoDeSoldado>().atributos.receberDano(dano);
	}
}
