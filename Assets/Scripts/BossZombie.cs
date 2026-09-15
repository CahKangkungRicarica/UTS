using UnityEngine;

public class BossZombie : Enemy
{
    [SerializeField] private ZombieConfig config;

    protected override void Start()
    {
        base.Start(); // tetap jalanin Start() milik Enemy
        if (config != null)
        {
            ms = config.kecepatan;
        }
    }

    protected override void Mati()
    {
        Debug.Log("BOSS MATI! Spawn minion dulu...");
        base.Mati(); // tetap jalanin Mati() milik Enemy
    }
}