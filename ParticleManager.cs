using UnityEngine;
using System.Collections;

// this manager class handles particle effects
public class ParticleManager : MonoBehaviour
{
	public GameObject dropDestroyed;

	public GameObject playerDestroyed;

	// play the clear GamePiece effect
	public void DestroyDrop(float x, float y, float z = 0)
	{
		if (dropDestroyed != null)
		{
			GameObject destroyFX = Instantiate(dropDestroyed, new Vector3(x,y,z), Quaternion.identity) as GameObject;

			ParticlePlayer particlePlayer = destroyFX.GetComponent<ParticlePlayer>();

			if (particlePlayer !=null)
			{
				particlePlayer.Play();
			}
		}
	}

	public void DestroyPlayer(float x, float y, float z = 0)
	{
		if (dropDestroyed != null) {
			GameObject destroyFX = Instantiate (playerDestroyed, new Vector3 (x, y, z), Quaternion.identity) as GameObject;

			ParticlePlayer particlePlayer = destroyFX.GetComponent<ParticlePlayer> ();

			if (particlePlayer != null) {
				particlePlayer.Play ();
			}
		}
	}
}