using UnityEngine;
using System.Collections;

public abstract class Efeito
{
	public ComportamentoDeSoldado alvo;
	public string nome { get; protected set; }
	public int cargas { get; protected set; }
	public int poder { get; protected set; }
	public bool efeitoPeriodico { get; protected set; }
	protected float espera;

	public Efeito()
	{
		espera = 0;
	}

	public void ativacaoPeriodica()
	{
		if((espera -= Time.deltaTime) <= 0)
		{
			espera = 1;
			ativarEfeito();
		}
	}

	public void ativarEfeito()
	{
		cargas--;
		acaoEfeito();
    }

	protected abstract void acaoEfeito();
}
