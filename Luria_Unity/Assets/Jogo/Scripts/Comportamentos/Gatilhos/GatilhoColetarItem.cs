using UnityEngine;
using System.Collections;

public class GatilhoColetarItem : AcaoEngatilhada
{
	void Start()
	{
		mensagem = "";
	}

	protected override void acaoGatilho()
	{
		GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorDeInventario>().adicionarItem(objetosAlvo[0].GetComponent<ComportamentoItem>().Item());
	}
}
