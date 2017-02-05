using UnityEngine;
using System.Collections;

public class HabiTiroExplosivo : Habilidade
{
	public GameObject prefabHabilidade;

	void Start()
	{
		nome = "Tiro Explosivo";
		energia = 25;
		recargaTotal = 7;
		recarga = 0;
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

	//add debuff de explosao em vez de dano, cd alto, consumo de energia alto
	public override void ativar(GameObject soldado)
	{
		ComportamentoDeSoldado compSoldado = soldado.GetComponent<ComportamentoDeSoldado>();
		Atributos atributos = compSoldado.atributos;
		int danoPrincipal = (int)((atributos.habilidade * 1.25F) * atributos.taxaCritico());
		int danoDireto = atributos.habilidade;
		compSoldado.alvoHabilidade.GetComponent<ComportamentoDeSoldado>().atributos.receberDano(danoPrincipal);
		compSoldado.alvoHabilidade.GetComponent<ComportamentoDeSoldado>().atributos.vida -= danoDireto;
		GameObject explosaoInstancia = Instantiate(prefabHabilidade);
		explosaoInstancia.transform.position = compSoldado.alvoHabilidade.transform.position;
		explosaoInstancia.transform.parent = compSoldado.alvoHabilidade.transform;

		//Custo Habilidade
		compSoldado.atributos.energia -= energia;
		recarga = recargaTotal;
	}
}