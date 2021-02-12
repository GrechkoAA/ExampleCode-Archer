using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(UIGameOver))]
public class AudioGameOver : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private UIGameOver _uIGameOver;
    [SerializeField] private AudioClip _gameOver;
    [SerializeField] private AudioClip _gameWin;

    private void Play(bool isFinished)
    {
        _audioSource.PlayOneShot(isFinished == true ? _gameWin : _gameOver, _audioSource.volume);
    }

    private void OnEnable()
    {
        _uIGameOver.Enabled += Play;
    }
}