using UnityEngine;
using System.Collections;

public class CameraCinematic : MonoBehaviour
{
	public Transform baseCamera;
	public float smooth = 0.5F;
	public Transform[] alvosCamera;
	private int indiceAlvo = 0;
	private Transform alvoCamera;

	void Start()
	{
		moverPara(indiceAlvo);
	}

	void FixedUpdate()
	{
		SmoothLookAt();
		if(baseCamera != null)
			gameObject.transform.position = baseCamera.position;
	}

	void SmoothLookAt()
	{
		// Create a vector from the camera towards the player.
		Vector3 relPlayerPosition = alvoCamera.position - transform.position;

		// Create a rotation based on the relative position of the player being the forward vector.
		Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);

		// Lerp the camera's rotation between it's current rotation and the rotation that looks at the player.
		transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
	}

	public void moverPara(int i)
	{
		indiceAlvo = i;
		alvoCamera = alvosCamera[indiceAlvo].GetChild(1);
		if(baseCamera != null)
			baseCamera.GetComponent<NavMeshAgent>().destination = alvosCamera[indiceAlvo].GetChild(0).position;
	}

	public void moverParaProximo()
	{
		indiceAlvo++;
		indiceAlvo = indiceAlvo % alvosCamera.Length;
		moverPara(indiceAlvo);
	}
}
