public class ListTargetsLevel : UnityEngine.MonoBehaviour
{
    [UnityEngine.SerializeField] private TargetList[] _listGoalsLevel;

    public TargetList this[int index] => _listGoalsLevel[index];

    public int Count { get => _listGoalsLevel.Length; }
}