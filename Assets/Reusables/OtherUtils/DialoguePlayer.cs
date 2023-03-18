using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePlayer : MonoBehaviour
{
    #region References

    [SerializeField] AudioSource _audioSource;
    [SerializeField] bool _playAutomatically;
    [SerializeField] float _delay;
    [SerializeField] float _dialogueDelay;

    List<AudioClip> _dialoguesToPlay;
    Coroutine _audioPlayer;

    #endregion

    #region Unity Functions

    private void OnDisable()
	{
        StopAllCoroutines();
	}

	IEnumerator Start()
    {
        yield return new WaitForSeconds(_delay);

        if(_playAutomatically)
            _audioPlayer = StartCoroutine(PlayDialoguesCoroutine());
    }

    #endregion

    #region Public Functions

    //Function to play a string of dialogues
    public void PlayDialogues(List<AudioClip> dialogues)
	{
        _dialoguesToPlay = dialogues;
        StopAllCoroutines();

        _audioPlayer = StartCoroutine(PlayDialoguesCoroutine());
    }

    //Play just a single dialogue
    public void PlayDialogue_Single(AudioClip dialogue)
    {
        Stop();

        _audioSource.PlayOneShot(dialogue);
    }

    public void Stop()
    {
        StopAllCoroutines();
        _audioSource.Stop();
    }

    #endregion

    #region Private Functions

    private IEnumerator PlayDialoguesCoroutine()
    {
        foreach (var clip in _dialoguesToPlay)
        {
            float delay = clip.length + _dialogueDelay;
            _audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(delay);
        }
    }

    #endregion
}
