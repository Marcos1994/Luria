using UnityEngine;
using System.Collections;

public class HabiTiroPreciso : Habilidade
{
	void Start()
	{
		nome = "Tiro de Precisao";
		energia = 20;
		recargaTotal = 5.5F;
		recarga = 0;
		alvoInimigo = true;
	}

	//alvo que tenha debuff de mira
	protected override GameObject definirAlvo(ComportamentoDeSoldado soldado)
	{
		GameObject alvo = null;
		Transform inimigo = soldado.gameObject.GetComponentInParent<ComportamentoDeUnidade>().inimigo.transform;
		foreach (Transform soldadoTransform in inimigo)
			if (soldadoTransform.tag == "Soldado" &&
				(soldadoTransform.GetComponent<ComportamentoDeSoldado>().efeitos.ContainsKey("Efeito Mira") ||
				alvo == null))
					alvo = soldadoTransform.gameObject;
		return alvo;
	}

	//causa 1.5x dano (3X caso o alvo esteja com debuff de mira), recarga alta, custo de mana baixo, remove mira
	public override void ativar(GameObject soldado)
	{
		ComportamentoDeSoldado compSoldado = soldado.GetComponent<ComportamentoDeSoldado>();
		Atributos atributos = compSoldado.atributos;
		int dano = (int)((atributos.atributoBase() + atributos.resistencia / 5) * (0.5F + atributos.taxaCritico()));
		if (compSoldado.alvoHabilidade.GetComponent<ComportamentoDeSoldado>().efeitos.ContainsKey("Efeito Mira"))
		{
			dano = (int)dano * 3;
			compSoldado.alvoHabilidade.GetComponent<ComportamentoDeSoldado>().efeitos.Remove("Efeito Mira");
        }
		else
			dano = (int)(dano * 1.5F);
		compSoldado.alvoHabilidade.GetComponent<ComportamentoDeSoldado>().atributos.receberDano(dano);

		//Custo Habilidade
		compSoldado.atributos.energia -= energia;
		recarga = recargaTotal;
	}
}