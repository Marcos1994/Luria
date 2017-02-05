using UnityEngine;
using System.Collections;

public class HabiProvocacao : Habilidade
{
	void Start()
	{
		nome = "Provocar";
		energia = 9;
		recargaTotal = 4;
		recarga = 0;
		alvoInimigo = true;
	}

	//soldado que esteja mais perto de atacar
	protected override GameObject definirAlvo(ComportamentoDeSoldado soldado)
	{
		GameObject alvo = null;
		float atributoBase = 0;
		Transform inimigo = soldado.gameObject.GetComponentInParent<ComportamentoDeUnidade>().inimigo.transform;
		foreach (Transform soldadoTransform in inimigo)
			if (soldadoTransform.tag == "Soldado")
			{
				Atributos atributosAlvo = soldadoTransform.GetComponent<ComportamentoDeSoldado>().atributos;
				if (atributosAlvo.atributoBase() > atributoBase)
				{
					atributoBase = atributosAlvo.atributoBase();
					alvo = soldadoTransform.gameObject;
				}
			}
		return alvo;
	}

	//altera o alvo do inimigo para o soldado que utilizou a habilidade, recarga media, custo de mana baixo
	public override void ativar(GameObject soldado)
	{
		ComportamentoDeSoldado compSoldado = soldado.GetComponent<ComportamentoDeSoldado>();
		compSoldado.alvoHabilidade.GetComponent<ComportamentoDeSoldado>().alvoHabilidade = soldado;
		compSoldado.alvoHabilidade.GetComponent<ComportamentoDeSoldado>().atributos.vida -= 3;

		//Custo Habilidade
		compSoldado.atributos.energia -= energia;
		recarga = recargaTotal;
	}
}