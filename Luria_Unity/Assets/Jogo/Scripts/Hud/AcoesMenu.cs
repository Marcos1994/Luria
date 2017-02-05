using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AcoesMenu : MonoBehaviour
{
	//substituir por 6 preinstancias de ObjetivoHud
	//area onde os objetivos serao exibidos
	public GameObject areaObjetivos = null; 
	//icones dos objetivos
	public Sprite objAndamento, objSecundario, objConcluido, objFalhado;

	void Start()
	{
		gameObject.transform.localScale = new Vector3(0.75F, 0.75F, 0.75F);
	}

	public void listarObjetivos(ControladorDeObjetivos objetivos)
	{
		listarObjetivos(objetivos.objetivos);
    }

	public void listarObjetivos(Objetivo[] objetivos)
	{
		Transform objPrefab;
		for (int i = 0; i < areaObjetivos.transform.childCount; i++)
		{
			objPrefab = areaObjetivos.transform.GetChild(i);
			if (i < objetivos.Length)
			{
				string titulo = objetivos[i].titulo;
				if (objetivos[i].progressoFinal > 0)
					titulo += " (" + objetivos[i].progressoAtual + "/" + objetivos[i].progressoFinal + ")";
				objPrefab.FindChild("Titulo").GetComponent<Text>().text = titulo;

				if (objetivos[i].estado == 0)
				{
					if (objetivos[i].obrigatorio)
						objPrefab.FindChild("Icone").GetComponent<Image>().sprite = objAndamento;
					else
						objPrefab.FindChild("Icone").GetComponent<Image>().sprite = objSecundario;
				}
				else if (objetivos[i].estado == 1)
					objPrefab.FindChild("Icone").GetComponent<Image>().sprite = objConcluido;
				else
					objPrefab.FindChild("Icone").GetComponent<Image>().sprite = objFalhado;
			}
			else
				Destroy(objPrefab.gameObject);
		}
	}

	public void voltarAoJogo()
	{
		Time.timeScale = 1;
		Destroy(gameObject);
	}

	public void sairDoJogo()
	{
		ControladorPlayerPrefs.concluirMissao();
		//Application.Quit();
	}
}
