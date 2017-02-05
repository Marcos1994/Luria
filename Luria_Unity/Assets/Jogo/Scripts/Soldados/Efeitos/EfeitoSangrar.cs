using UnityEngine;
using System.Collections;

public class EfeitoSangrar : Efeito
{
	public EfeitoSangrar() : base()
    {
		nome = "Efeito Sangrar";
		cargas = 6;
		poder = 6;
		efeitoPeriodico = true;
    }

	protected override void acaoEfeito()
	{
		if(alvo != null)
			alvo.atributos.vida -= poder;
	}
}