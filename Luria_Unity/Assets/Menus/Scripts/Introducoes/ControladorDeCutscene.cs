using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorDeCutscene : MonoBehaviour
{
	public Transform areaCenas;
	public string proximaCena;
	private ArrayList blocos;

	void Start ()
	{
		blocos = new ArrayList();
		ImagemCutscene img;

		foreach(Transform imagem in areaCenas)
		{
			img = imagem.GetComponent<ImagemCutscene>();
			img.desativar();
			if(blocos.Count == img.indiceBloco)
				blocos.Add(new ArrayList());
			((ArrayList)blocos[img.indiceBloco]).Add(img);
		}

		StartCoroutine(ativarBlocos());
	}

	private IEnumerator ativarBlocos()
	{
		yield return new WaitForSeconds(2);

		foreach(ArrayList bloco in blocos)
		{
			foreach(ImagemCutscene imagem in bloco)
			{
				yield return new WaitForSeconds(imagem.esperaCarregamento);
				imagem.ativar();
			}
			yield return new WaitForSeconds(2);
			foreach(ImagemCutscene imagem in bloco)
				imagem.desativar(1);
			yield return new WaitForSeconds(1);
		}

		yield return new WaitForSeconds(2);

		SceneManager.LoadScene(proximaCena);
	}
}
