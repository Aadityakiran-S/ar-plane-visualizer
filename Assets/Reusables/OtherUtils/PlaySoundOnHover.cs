using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnHover : MonoBehaviour,IHoverable
{
	[SerializeField] AudioSource _audioSource;
	[SerializeField] AudioClip _hoverEnterAudio;
	[SerializeField] AudioClip _hoverExitAudio;


	public void OnHover()
	{
		//throw new System.NotImplementedException();
	}

	public void OnHoverLost()
	{
		if (_audioSource != null) _audioSource.PlayOneShot(_hoverEnterAudio);

	}

	public void OnHoverStart()
	{
		if (_audioSource != null) _audioSource.PlayOneShot(_hoverEnterAudio);
	}

}
