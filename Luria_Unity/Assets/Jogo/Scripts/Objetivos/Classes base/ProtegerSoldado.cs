using UnityEngine;
using System.Collections;

public class ProtegerSoldado : ComportamentoObjetivo
{
	public bool protegido = true;
	public ComportamentoDeSoldado soldado;

	void Update()
	{
		//enquanto a ação engatilhada ainda estiver ativa, o objetivo não terá sido concluído
		if (soldado.atributos.vida <= 0)
		{
			concluir(!protegido);
			this.enabled = false;
		}
	}
}
