using System;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 100;
    public float ms = 2f;
    protected Transform player;

    [Header("Pengaturan State Machine")]
    [SerializeField] private float jarakDeteksi = 6f;
    [SerializeField] private float jarakSerang = 1.2f;
    [SerializeField] private float jedaSerang = 1f;

    private StateZombie state = StateZombie.IDLE;
    private float waktuSerangTerakhir;

    public static event Action<Enemy> OnZombieMati; 
    [SerializeField] private UnityEvent onZombieMatiVisual;

    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        PeriksaTransisi();
        
        // switch (state)
        // {
        //     case StateZombie.IDLE: PerilakuIdle(); break;
        //     case StateZombie.PATROL: perilakuPatrol(); break;
        //     case StateZombie.CHASE: PerilakuChase(); break;
        //     case StateZombie.ATTACK: perilakuAttack(); break;

        // }
    }

    public float JarakKePlayer()
    {
        if (player == null) return Mathf.Infinity;
        return Vector2.Distance(transform.position, player.position);
    }

    void PeriksaTransisi ()
    {
        float jarak = JarakKePlayer();

        if (jarak <= jarakSerang) 
            state = StateZombie.ATTACK;
        else if (jarak <= jarakDeteksi)
            state = StateZombie.CHASE;
        else 
            state = StateZombie.PATROL;
    }

    public void Kejar()
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            ms * Time.deltaTime
        );
    }

    public virtual void Serang()
    {
        Debug.Log("Enemy menyerang!");
    }

    void PerilakuIdle()
    {

    }

    void perilakuPatrol()
    {
        Debug.Log("Enemy sedang patrol");
    }

    void PerilakuChase()
    {
        Kejar();
        Debug.Log("Enemy sedang mengejar");
    }

    void perilakuAttack()
    {
        if (Time.time >= waktuSerangTerakhir + jedaSerang)
        {
            Serang();
            waktuSerangTerakhir = Time.time;
        }
    }

    public void KenaDamage(int jumlah)
    {
        hp -= jumlah;
        Debug.Log($"{gameObject.name} kena damage {jumlah}, HP sisa: {hp}");

        if (hp <= 0)
        {
            Mati();
        }
    }

    protected virtual void Mati()
    {
        Debug.Log($"{gameObject.name} mati!");
        OnZombieMati?.Invoke(this);
        onZombieMatiVisual?.Invoke();
        Destroy(gameObject);
    }
}
