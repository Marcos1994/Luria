using UnityEngine;
using System.Collections;

public abstract class DialogoFase : MonoBehaviour
{
	//array que contem a posição da câmera em cada cena;
	public GameObject[] posicoesCamera = new GameObject[1];

	//arrays que contem as fotos dos envolvidos
	public Sprite[] fotos = new Sprite[1];

	//array que contem as falas no dialogo; o primseiro caractere sempre será o Indice do nome/foto do personagem
	public ArrayList[] dialogos = new ArrayList[1];
}
