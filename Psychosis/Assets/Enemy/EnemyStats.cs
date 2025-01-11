using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName ="Enemy/Enemy stats")]
public class EnemyStats : ScriptableObject
{
    public string Name;
    public int Health;
    public int Damage;
    public float Speed;
    public GameObject Prefab;
}
