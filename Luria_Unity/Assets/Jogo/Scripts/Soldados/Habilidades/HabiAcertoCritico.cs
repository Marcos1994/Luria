using UnityEngine;
using System.Collections;

public class HabiAcertoCritico : Habilidade
{
	void Start()
	{
		nome = "Acerto Critico";
		energia = 15;
		recargaTotal = 5.5F;
		recarga = 0;
		alvoInimigo = true;
	}

	//alvo com maior defesa
	protected override GameObject definirAlvo(ComportamentoDeSoldado soldado)
	{
		GameObject alvo = null;
		int armadura = 0;
		Transform inimigo = soldado.gameObject.GetComponentInParent<ComportamentoDeUnidade>().inimigo.transform;
		foreach (Transform soldadoTransform in inimigo)
			if (soldadoTransform.tag == "Soldado" &&
				soldadoTransform.GetComponent<ComportamentoDeSoldado>().atributos.armadura > armadura)
			{
				alvo = soldadoTransform.gameObject;
				armadura = alvo.GetComponent<ComportamentoDeSoldado>().atributos.armadura;
			}
			
		return alvo;
	}

	//dano 1.5x, com cd medio e consumo alto de energia
	public override void ativar(GameObject soldado)
	{
		ComportamentoDeSoldado compSoldado = soldado.GetComponent<ComportamentoDeSoldado>();
		Atributos atributos = compSoldado.atributos;
		int dano = (int)((atributos.atributoBase() + atributos.resistencia / 5) * (0.5F + atributos.taxaCritico()) * 1.5F);
        compSoldado.alvoHabilidade.GetComponent<ComportamentoDeSoldado>().atributos.receberDano(dano);

		//Custo Habilidade
		compSoldado.atributos.energia -= energia;
		recarga = recargaTotal;
	}
}