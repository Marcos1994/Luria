using UnityEngine;
using System.Collections;

public class Item
{
	public string nome;
	public string descricao;
	public Sprite icone;
	public bool utilizavel;
	public int quantidade;

	public Item(string nome, string descricao, Sprite icone, bool utilizavel, int quantidade)
	{
		this.nome = nome;
		this.descricao = descricao;
		this.icone = icone;
		this.utilizavel = utilizavel;
		this.quantidade = quantidade;
    }
}
