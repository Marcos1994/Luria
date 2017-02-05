using UnityEngine;
using System.Collections;

public class MenuJogo : MonoBehaviour
{
	private bool inventario = true;
	public Transform inventarioCanvas;
	public CameraCinematic cameraMenu;

	void Start ()
	{
		testarUnidades();

		habilitarInventario();
		listarUnidades();
	}

	void testarUnidades()
	{
		string[] unidades = new string[4];
		unidades[0] = "Arom Feahim:0";
		unidades[1] = "Iana Amara:0";
		unidades[2] = "Alan Hoop:0";
		unidades[3] = "Logos:0";
		ControladorPlayerPrefs.setarUnidadesDisponiveis(unidades);
	}

	void listarUnidades()
	{
		Hashtable unidadesDisponiveis = ControladorPlayerPrefs.listarUnidadesDisponiveis();
		int i = 1;
		foreach(string unidade in unidadesDisponiveis.Keys)
			CarregarUnidades.instanciarSoldado(unidade, cameraMenu.alvosCamera[i++].GetChild(1));
	}

	public void habilitarInventario()
	{
		inventario = !inventario;
		inventarioCanvas.rotation = (inventario) ? new Quaternion(0, 0, 0, 90) : new Quaternion(0, 90, 0, 90);
	}
}
