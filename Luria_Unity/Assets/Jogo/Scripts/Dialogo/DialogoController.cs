using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogoController : MonoBehaviour
{
	public Camera cameraDialogo;
	public Image campoFoto;
	public Text campoNome;
	public Text campoDialogo;

	//array que contem a posição da câmera em cada cena;
	protected GameObject[] posicoesCamera = new GameObject[1];

	//array que contem as falas no dialogo; o primseiro caractere sempre será o Indice do nome/foto do personagem
	protected ArrayList[] dialogos = new ArrayList[1];

	public int iteradorDialogo = 0;
	public int iteradorCena = 0;

	void Start()
	{
		DialogoFase dialogosFase = gameObject.GetComponent<DialogoFase>();
		posicoesCamera = dialogosFase.posicoesCamera;
		dialogos = dialogosFase.dialogos;
		dialogosFase.enabled = false;
    }

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Break))
			proximoDialogo();
	}

	public void ativarDialogo(bool ativar = true)
	{
		gameObject.GetComponent<Canvas>().enabled = ativar;
		cameraDialogo.enabled = ativar;

		if (GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorDeObjetivos>().missaoConcluida)
		{
			GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorDeJogo>().concluirMissao();
        }
		else if (ativar)
		{
			if (iteradorCena >= dialogos.Length)
			{
				GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorDeJogo>().ativarCinematic(false);
				return;
			}

			cameraDialogo.transform.position = posicoesCamera[iteradorCena].transform.position;
			cameraDialogo.transform.rotation = posicoesCamera[iteradorCena].transform.rotation;
			proximoDialogo();
		}
    }

	public void proximoDialogo()
	{
		if (iteradorCena < dialogos.Length)
		{
			if (iteradorDialogo >= dialogos[iteradorCena].Count)
			{
				iteradorDialogo = 0;
				iteradorCena++;
				GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorDeJogo>().ativarCinematic(false);
				return;
			}
			campoFoto.sprite = ((Dialogo)dialogos[iteradorCena][iteradorDialogo]).foto;
			campoNome.text = ((Dialogo)dialogos[iteradorCena][iteradorDialogo]).nome;
			campoDialogo.text = ((Dialogo)dialogos[iteradorCena][iteradorDialogo]).dialogo;
			iteradorDialogo++;
		}
		else
			GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorDeJogo>().ativarCinematic(false);
	}
}
