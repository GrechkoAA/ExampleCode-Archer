[System.Serializable]
public class TargetList
{
    [UnityEngine.SerializeField] private Health[] _list;

    public Health this[int index] => _list[index];

    public int Count => _list.Length;

    public int MaximumNumberShots { get; set; }
}