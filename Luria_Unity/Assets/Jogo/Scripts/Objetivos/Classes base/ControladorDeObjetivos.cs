using UnityEngine;
using System.Collections;

public abstract class ControladorDeObjetivos : MonoBehaviour
{
	//os objetivos devem ser postos manualmente, pois estarao distribuidos entre varios objetos.
	public Objetivo[] objetivos;

	public bool missaoConcluida = false;

	public void concluirObjetivo(int indice, bool atingido = true)
	{
		if(indice < objetivos.Length)
		{
			//se o objetivo for composto (como coletar 3 itens ou matar 5 inimigos) so concluo se a quantidade minima tiver sido atingida
			if (objetivos[indice].progressoAtual++ >= objetivos[indice].progressoFinal)
			{
				if (atingido)
				{
					objetivos[indice].estado = 1;
					gameObject.GetComponent<ControladorDeJogo>().escreverMensagem("Objetivo \"" + objetivos[indice].titulo + "\" Concluido!");
					if (objetivos[indice].possuiCinematic)
						gameObject.GetComponent<ControladorDeJogo>().ativarCinematic();
				}
				else
				{
					objetivos[indice].estado = -1;
					gameObject.GetComponent<ControladorDeJogo>().escreverMensagem("Objetivo \"" + objetivos[indice].titulo + "\" Falhou!");
				}
				
				//vejo se todos os objetivos obrigatorios foram concluidos ou se algum falhou
				for (int i = 0; i < objetivos.Length; i++)
				{
					if (objetivos[i].obrigatorio)
					{
						if (objetivos[i].estado < 0)
						{//se o objetivo obrigatorio falhou
							gameObject.GetComponent<ControladorDeJogo>().concluirMissao(false);
							return;
						}
						else if (objetivos[i].estado == 0)
							return; //ainda tem algum objetivo obrigatorio n concluido
					}
				}
				missaoConcluida = true;
				if(!gameObject.GetComponent<ControladorDeJogo>().emCinematic)
					gameObject.GetComponent<ControladorDeJogo>().concluirMissao();
			}
		}
	}
}
