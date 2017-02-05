using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ListagemFimDeFase : MonoBehaviour
{
	public Objetivo[] objetivos;
	public GameObject areaObjetivos;
	
	void Start ()
	{
		objetivos = ControladorPlayerPrefs.objetivosFase;
		areaObjetivos.GetComponent<AcoesMenu>().listarObjetivos(objetivos);
	}

	public void concluir()
	{
		ControladorPlayerPrefs.concluirMissao();
	}
}
