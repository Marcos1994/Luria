using UnityEngine;
using System.Collections;

public class GatilhoObjetivo : AcaoEngatilhada
{
	void Start()
	{
		mensagem = "";
		gameObject.GetComponent<AcoesJogadorAlvoEspecial>().carregamento = 1;
	}
	protected override void acaoGatilho()
	{
	}
}

