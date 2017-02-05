using UnityEngine;
using System.Collections;

public class HabiAtordoar : Habilidade
{
	void Start()
	{
		nome = "Atordoar";
		energia = 18;
		recarga = 0;
		recargaTotal = 5.5F;
		alvoInimigo = true;
	}

	//soldado que esteja mais perto de atacar
	protected override GameObject definirAlvo(ComportamentoDeSoldado soldado)
	{
		GameObject alvo = null;
		float proximoAtaque = 10;
		Transform inimigo = soldado.gameObject.GetComponentInParent<ComportamentoDeUnidade>().inimigo.transform;
		foreach (Transform soldadoTransform in inimigo)
			if (soldadoTransform.tag == "Soldado")
			{
				ComportamentoDeSoldado soldadoAlvo = soldadoTransform.GetComponent<ComportamentoDeSoldado>();
				if (soldadoAlvo.esperaProximoAtaque < proximoAtaque)
				{
					proximoAtaque = soldadoAlvo.esperaProximoAtaque;
                    alvo = soldadoTransform.gameObject;
				}
			}
		return alvo;
	}

	//atordoa o alvo, recarga alta, custo de mana medio
	public override void ativar(GameObject soldado)
	{
		ComportamentoDeSoldado compSoldado = soldado.GetComponent<ComportamentoDeSoldado>();
		compSoldado.esperaProximoAtaque = 4;
		compSoldado.alvoHabilidade.GetComponent<ComportamentoDeSoldado>().atributos.vida -= 3;

		//Custo Habilidade
		compSoldado.atributos.energia -= energia;
		recarga = recargaTotal;
	}
}