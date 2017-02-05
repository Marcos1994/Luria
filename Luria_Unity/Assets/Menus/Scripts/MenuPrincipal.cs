using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
	public Transform cameraPrincipal;
	public Transform areaBotoes;
	private bool emAnimacao = false;
	private ArrayList botoes = new ArrayList();

	void Update()
	{
		if (emAnimacao)
		{
			cameraPrincipal.position += new Vector3 (0, 0, Time.deltaTime * 5);
			foreach (Transform botao in botoes)
				botao.transform.position += new Vector3 (Time.deltaTime * 1000, 0);
		}
	}

	public void iniciarJogo()
	{
		StartCoroutine(LoadLevel("IntroducaoPrologo"));
	}

	public void carregarJogo()
	{
		Debug.Log("Não Implementado!");
	}

	public void listarConquistas()
	{
		Debug.Log("Não Implementado!");
	}

	public void creditos()
	{
		Debug.Log("Não Implementado!");
	}

	public void sairDoJogo()
	{
		Application.Quit();
	}

	private IEnumerator LoadLevel(string level)
	{
		emAnimacao = true;
		foreach (Transform botao in areaBotoes)
			botao.GetComponent<Button> ().enabled = false;
		foreach (Transform botao in areaBotoes)
		{
			botoes.Add(botao);
			yield return new WaitForSeconds(0.1F); 
		}
		yield return new WaitForSeconds(1); 
		SceneManager.LoadScene(level);
		emAnimacao = false;
	}
}
