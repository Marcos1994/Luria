using UnityEngine;
using System.Collections;

public class EfeitoMira : Efeito
{
	public EfeitoMira() : base()
    {
		nome = "Efeito Mira";
		cargas = 1;
		poder = 0;
		efeitoPeriodico = false;
	}

	protected override void acaoEfeito()
	{
		//não tem ação direta
	}
}
