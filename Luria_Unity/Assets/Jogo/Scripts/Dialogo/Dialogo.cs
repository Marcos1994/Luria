using UnityEngine;
using System.Collections;

public class Dialogo
{
	public Sprite foto { get; set; }
	public string nome { get; set; }
	public string dialogo { get; set; }

	public Dialogo()
	{
	}

	public Dialogo(Sprite foto, string nome, string dialogo)
	{
		this.foto = foto;
		this.nome = nome;
		this.dialogo = dialogo;
    }
}
