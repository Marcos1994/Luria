using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControladorDeInterface : MonoBehaviour
{
	//variavel responsavel por se comunicar com o controlador do jogo
	public ControladorDeJogo controladorDeJogo = null;
	//prefab para exibir as mensagens
	public Text mensagemPrefab = null;
	//area onde as mensagens aparecerao
	public GameObject areaDeMensagens = null;
	//array dos icones das unidades
	public GameObject[] iconesUnidades = new GameObject[5];
	//array dos status dos soldados
	public StatusDeSoldado[] fotosSoldados = new StatusDeSoldado[5];

	public void atualizarFotosSoldados(GameObject unidadeSelecionada)
	{
		gameObject.GetComponent<Navegacao>().setAlvo(unidadeSelecionada.GetComponent<ComportamentoDeUnidade>().alvoAtual);
		//Percorro todos os filhos dessa unidade
		foreach(Transform soldadoTransform in unidadeSelecionada.transform)
		{//Se algum filho tiver a tag Soldado, digo que deve ser selecionado
			if (soldadoTransform.tag == "Soldado")
                fotosSoldados[soldadoTransform.GetComponent<ComportamentoDeSoldado>().indiceNaUnidade].atualizarSoldado(soldadoTransform.gameObject);
		}
	}
	
	public void subirMensagens()
	{
		int i = 0;
		foreach (Transform mensagem in areaDeMensagens.transform)
			mensagem.position = areaDeMensagens.transform.position + (new Vector3(0, - (i++) * 30, 0));
	}
	
	public void escreverMensagem(string mensagem)
	{
		mensagemPrefab.text = mensagem;
		Text novaMsg = Instantiate(mensagemPrefab);
		novaMsg.transform.SetParent(areaDeMensagens.transform);
		novaMsg.GetComponent<RectTransform>().position = areaDeMensagens.transform.position + (new Vector3(0, - areaDeMensagens.transform.childCount * 30, 0));
		Debug.Log(mensagem);
	}
}
