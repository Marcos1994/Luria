using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorDeJogo : MonoBehaviour
{
	public GameObject objetoSelecionado = null;
	public ControladorDeInterface hud = null;
	public DialogoController dialogoController = null;
	public ControladorDeObjetivos objetivos { get; private set; }
	public bool emCinematic = false;

	void Start()
	{
		objetivos = gameObject.GetComponent<ControladorDeObjetivos>();
		ativarCinematic(false);
		hud.GetComponent<Canvas>().enabled = false;
		hud.GetComponent<GerenciadorDeInput>().enabled = false;
		GameObject.Find("TelaDeCarregamento").GetComponent<Carregamento>().enabled = true;
	}

	public void setObjetoSelecionado(GameObject objeto)
	{
		//atribuo o novo objeto selecionado
		objetoSelecionado = objeto;
		//informo para o hud atualizar as fotos dos soldados
		hud.atualizarFotosSoldados(objetoSelecionado);
	}

	public void escreverMensagem(string mensagem)
	{
		hud.GetComponent<ControladorDeInterface>().escreverMensagem(mensagem);
	}

	public void ativar()
	{
		setObjetoSelecionado(GameObject.Find("Unidades").transform.GetChild(0).gameObject);
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CarregarCameraJogo>().enabled = true;
	}

	public void ativarCinematic(bool ativar = true)
	{
		if (dialogoController != null)
		{
			hud.GetComponent<Canvas>().enabled = !ativar;
			hud.GetComponent<GerenciadorDeInput>().enabled = !ativar;

			if (ativar)
			{
				dialogoController.enabled = true;
				dialogoController.ativarDialogo(true);
			}
			else
			{
				dialogoController.ativarDialogo(false);
				dialogoController.enabled = false;
			}

			emCinematic = ativar;

			Time.timeScale = (ativar) ? 0 : 1;
		}
	}

	public void concluirMissao(bool sucesso = true)
	{
		Debug.Log("Finalizar Missão!");

		GameObject[] soldados = GameObject.FindGameObjectsWithTag("Soldado");
		foreach (GameObject soldado in soldados)
		{
			soldado.GetComponent<ComportamentoDeSoldado>().enabled = false;
			soldado.GetComponent<ComportamentoDeSoldado>().animator.SetBool("correndo", false);
			soldado.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        }

		Time.timeScale = 1;
        hud.gameObject.GetComponent<Canvas>().enabled = false;
		hud.gameObject.GetComponent<GerenciadorDeInput>().enabled = false;
		dialogoController.GetComponent<Canvas>().enabled = false;

		if (sucesso)
		{
			PlayerPrefs.SetInt("SucessoMissao", 1);
			hud.escreverMensagem("Missao cumprida!");
			gameObject.GetComponent<ControladorDeInventario>().salvarInventario();
		}
		else
			hud.escreverMensagem("A missao fracassou!");

		StartCoroutine(EncerrarMissao());
    }
	
	IEnumerator EncerrarMissao()
	{
		yield return new WaitForSeconds(3f);
		ControladorPlayerPrefs.objetivosFase = objetivos.objetivos;
		SceneManager.LoadScene("FimDeFase");
	}
}
