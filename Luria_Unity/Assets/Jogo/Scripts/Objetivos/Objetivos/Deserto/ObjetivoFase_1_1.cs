using UnityEngine;
using System.Collections;

public class ObjetivoFase_1_1 : ControladorDeObjetivos
{
	/* ---Objetivos---
	 * 1-Ob: Coletar 5 jarras d'água
	 * 2-Ob: Coletar 10 raízes de Argo
	*/
	public ObjetivoFase_1_1()
	{
		objetivos = new Objetivo[2];
		objetivos[0] = new Objetivo("Coletar 5 água dos condensadores", "Colete 5 jarras d'água nos condensadores próximos ao acampamento", true, 5);
		objetivos[1] = new Objetivo("Coletar 10 raizes de Argo", "Colete pelo menos 10 raízes de Argo", true, 10);
	}
}

