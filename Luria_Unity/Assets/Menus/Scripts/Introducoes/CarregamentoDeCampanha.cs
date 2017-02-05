using UnityEngine;
using System.Collections;

public class CarregamentoDeCampanha : MonoBehaviour
{
	public string tituloCampanha;
	public string[] unidadesDisponiveis;

	void Start()
	{
		for (int i = 0; i < unidadesDisponiveis.Length; i++)
			unidadesDisponiveis[i] += ":0";

		ControladorPlayerPrefs.definirCampanha(tituloCampanha);
		ControladorPlayerPrefs.setarUnidadesDisponiveis(unidadesDisponiveis);
		ControladorPlayerPrefs.salvarProgresso();
	}
}
