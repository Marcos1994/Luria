using UnityEngine;
using System.Collections;

public class ObjetivoAlvoEspecial : ComportamentoObjetivo
{
	private AcaoEngatilhada acaoEngatilhada;

	void Start ()
	{
		acaoEngatilhada = gameObject.GetComponent<AcaoEngatilhada>();
	}
	
	void Update ()
	{
		//enquanto a ação engatilhada ainda estiver ativa, o objetivo não terá sido concluído
		if (!acaoEngatilhada.enabled)
		{
			concluir();
			this.enabled = false;
		}
	}
}
