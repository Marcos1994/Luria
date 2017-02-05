using UnityEngine;
using System.Collections;

public class GerenciadorDeInput : MonoBehaviour
{
	public GameObject areaMenu = null;	//area do menu
	public GameObject menu = null;		//prefab do menu
	public GameObject menuInst = null;	//instancia do menu
	public ControladorDeInterface controladorDeInterface = null;
	public ControladorDeJogo controladorDeJogo = null;

	void Update ()
	{
		if(Input.anyKeyDown)
		{
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				pausar();
			}
		}
	}

	public void pausar()
	{
		if(Time.timeScale == 1)
		{
			Time.timeScale = 0;
			menuInst = Instantiate(menu);
			menuInst.transform.SetParent(areaMenu.transform);
			menuInst.GetComponent<AcoesMenu>().listarObjetivos(controladorDeJogo.objetivos);
			menuInst.transform.position = areaMenu.transform.position;
		}
		else
		{
			Time.timeScale = 1;
			Destroy(menuInst);
		}
	}
}
