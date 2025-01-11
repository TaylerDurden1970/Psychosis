using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewWaveData", menuName = "Wave Data")]
public class WaveData : ScriptableObject
{
    public List<EnemyWaveInfo> Enemies;
    public float spawnInterval;
}

[System.Serializable]
public class EnemyWaveInfo
{
    public EnemyStats EnemyStats;
    public int Count;
}

