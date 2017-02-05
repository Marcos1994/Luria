using UnityEngine;
using System.Collections;

public class CarregarCameraJogo : MonoBehaviour
{
	public GameObject hud;
	private float carregando = 1.5F;
	
	void Start ()
	{
		gameObject.transform.position -= new Vector3(0, carregando * 2, 0);
    }
	
	void Update ()
	{
		if (carregando > 0)
		{
			float t = Time.deltaTime;
			carregando = Mathf.Clamp(carregando - t, 0, 10);
			if (carregando == 0)
			{
				hud.GetComponent<Canvas>().enabled = true;
				hud.GetComponent<GerenciadorDeInput>().enabled = true;
				this.enabled = false;
			}
			else
				gameObject.transform.position += new Vector3(0, t * 2.5F, 0);
		}
	}
}
