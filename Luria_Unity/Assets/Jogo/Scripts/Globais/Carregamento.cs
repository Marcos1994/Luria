using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Carregamento : MonoBehaviour
{
	public ControladorDeJogo controladorDeJogo;
	public AcoesMenu menu;
	public int idMissao;
	public Transform[] posicoesUnidades;

	void Start()
	{
		PlayerPrefs.SetInt("MissaoAtual", idMissao);
		menu.listarObjetivos(controladorDeJogo.gameObject.GetComponent<ControladorDeObjetivos>());

		ControladorPlayerPrefs.carregarUnidades(posicoesUnidades);
		for (int i = 0; i < posicoesUnidades.Length; i++)
			posicoesUnidades[i].GetComponent<ComportamentoDeUnidade>().redefinirLider();
	}
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			ativar();
	}

	public void ativar()
	{
		controladorDeJogo.ativar();
		Destroy(gameObject);
	}
}
