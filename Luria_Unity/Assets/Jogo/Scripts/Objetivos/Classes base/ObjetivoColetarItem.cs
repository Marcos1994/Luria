using UnityEngine;
using System.Collections;

public class ObjetivoColetarItem : ComportamentoObjetivo
{
	public string nome;

	public void coletarItem(string nomeItem)
	{
		if (nome == nomeItem)
			concluir();
	}
}