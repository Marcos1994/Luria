using UnityEngine;
using System.Collections;

public class ObjetivoFase_0_0 : ControladorDeObjetivos
{
	/* ---Objetivos---
	 * 1-Ob: Localizar Projeto Logos
	 * 2-Op: Resgatar 5 cientistas
	 * 3-Op: Coletar 5 componentes tecnologicos
	 * 4-Ob: Voltar para a nave
	*/
	public ObjetivoFase_0_0()
	{
		objetivos = new Objetivo[4];
		objetivos[0] = new Objetivo("Localizar Projeto Logos (Com Arom Feahim vivo)", "Localize o núcleo do Projeto Logos com pelo menos o general Arom Feahim vivo", true, 0, true);
		objetivos[1] = new Objetivo("Resgatar 5 cientistas", "Encontre pelo menos 5 cientistas vivos", false, 5);
		objetivos[2] = new Objetivo("Coletar 5 componentes tecnologicos", "Encontre pelo menos 5 componentes tecnologicos", false, 5);
		objetivos[3] = new Objetivo("Voltar para a Sombra", "Voltar para a Sombra e deixar o laboratorio", true, 0, false);
	}
}
