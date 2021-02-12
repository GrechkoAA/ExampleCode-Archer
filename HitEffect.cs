using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private GamePointsModel _gamePointsModel;

    private void Play(Vector3 position)
    {
        transform.position = position;
        _particle.Play();
    }

    private void OnEnable()
    {
        _gamePointsModel.ScoreChanged += (generalPoints, points, position) =>
        {
            Play(position);
        };
    }
}