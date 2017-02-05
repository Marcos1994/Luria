using UnityEngine;
using System.Collections;

public class EfeitoCura : Efeito
{
	public EfeitoCura() : base()
    {
		nome = "Efeito Cura";
		cargas = 5;
		poder = 8;
		efeitoPeriodico = true;
	}

	protected override void acaoEfeito()
	{
		if (alvo != null)
			alvo.atributos.receberCura(poder);
	}
}
