using DG.Tweening;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private UnityEngine.Audio.AudioMixerGroup _mixer;
    [SerializeField] private UnityEngine.UI.Image _music;
    [SerializeField] private UnityEngine.UI.Image _effects;
    [SerializeField] private float _alphaIcon;

    private int _musicVolume;
    private int _effectVolume;
    private int _offAudio = -80;

    private void Start()
    {
        _musicVolume = _saveSystem.MusicVolume == _offAudio? 0 : _offAudio;
        _effectVolume = _saveSystem.EffectVolume == _offAudio ? 0 : _offAudio;

        OnToggleMusic();
        OnToggleEffects();
    }

    public void OnToggleMusic()
    {
        _musicVolume = _musicVolume == 0 ? _offAudio : 0;

        _mixer.audioMixer.SetFloat("MusicVolume", _musicVolume);
        _music.DOFade(_musicVolume == _offAudio ?  _alphaIcon : 1, 0);

        _saveSystem.MusicVolume = _musicVolume;
    }

    public void OnToggleEffects()
    {
        _effectVolume = _effectVolume == 0 ? _offAudio : 0;

        _mixer.audioMixer.SetFloat("EffectsVolume", _effectVolume);
        _effects.DOFade(_effectVolume == _offAudio ? _alphaIcon : 1, 0);

        _saveSystem.EffectVolume = _effectVolume;
       _mixer.audioMixer.SetFloat("UIValume", _effectVolume);
    }
}