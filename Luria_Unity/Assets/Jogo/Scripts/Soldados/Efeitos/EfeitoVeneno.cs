using UnityEngine;
using System.Collections;

public class EfeitoVeneno : Efeito
{
	public EfeitoVeneno() : base()
    {
		nome = "Efeito Veneno";
		cargas = 10;
		poder = 5;
		efeitoPeriodico = true;
	}

	protected override void acaoEfeito()
	{
		if (alvo != null)
			alvo.atributos.vida -= poder;
	}
}
