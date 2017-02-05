using UnityEngine;
using System.Collections;

public class GatilhoPortao : AcaoEngatilhada
{
	protected override void acaoGatilho()
	{
		foreach(GameObject objeto in objetosAlvo)
			objeto.GetComponent<AbrirPortao>().abrivel = true;
	}
}
