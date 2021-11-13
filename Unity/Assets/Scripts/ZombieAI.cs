using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum ZombieStates//animatorde verdiğimiz numaraları karıştırmamak için
{
    Idle = 0,
    Walk = 1,
    Dead = -1,
    Attack = 2
}
public class ZombieAI : MonoBehaviour
{
    public int damage = 10;
    [SerializeField] float range = 1.5f;
    Animator animator;
    NavMeshAgent agent;
    ZombieStates zombiState;
    GameObject playerObject;
    PlayerHealth playerHealth;
    ZombiHealth zombiHealth;
    Vector3 vec;
    void Start()
    {
        zombiState = ZombieStates.Idle;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerObject = GameObject.FindWithTag("Player");
        playerHealth = playerObject.GetComponent<PlayerHealth>();
        zombiHealth = GetComponent<ZombiHealth>();
    }
    void Update()
    {
        if(zombiHealth.GetHeal() <= 0)
        {
            zombiState = ZombieStates.Dead;
        }

        switch (zombiState)
        {
            case ZombieStates.Dead:
                KillZombi();
                break;
            case ZombieStates.Attack:
                AttackZombi();
                break;
            case ZombieStates.Walk:
                SearchForTarget();
                break;
            case ZombieStates.Idle:
                SearchForTarget();
                break;
        }
        //StartCoroutine(Zaman());
        //StartCoroutine(Attack());
    }

    /*IEnumerator Attack()
    {
        RaycastHit hit;
        vec = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        if (Physics.Raycast(vec, transform.forward, out hit, range))
        {
            if (hit.transform.tag == "Player")
            {
                hit.transform.GetComponent<PlayerHealth>().Damage(damage);
            }
        }
        yield return new WaitForSeconds(0.5f);
    }*/

    /*IEnumerator Zaman()
    {
        switch (zombiState)
        {
            case ZombieStates.Dead:
                KillZombi();
                break;
            case ZombieStates.Attack:
                AttackZombi();
                break;
            case ZombieStates.Walk:
                SearchForTarget();
                break;
            case ZombieStates.Idle:
                SearchForTarget();
                break;
        }
        yield return new WaitForSeconds(0.5f);
    }*/

    private void AttackZombi()
    {
        agent.isStopped = true;
        SetState(ZombieStates.Attack);
    }
    void MakeAttack()//attack animasyonuna gidip event oluşturarak yaptık.
    {
        if (ZombiSaldirmali_mi() == 1)
        {
            playerHealth.DeductHealth(damage);
        }        
        SearchForTarget();
    }
    int ZombiSaldirmali_mi()
    {
        float distance = Vector3.Distance(transform.position, playerObject.transform.position);
        if (distance > range)
        {
            return 0;
        }
        return 1;
    }
    private void SearchForTarget()
    {
        float distance = Vector3.Distance(transform.position, playerObject.transform.position);//iki vectör arası uzaklık
        if (distance < range)
        {
            AttackZombi();
        }
        else if (distance < 1000)
        {
            MoveToPlayer();
        }
        else
        {
            SetState(ZombieStates.Idle);
            agent.isStopped = true;//yürümeden durmaya direk geçmesi için agent ı durduruyoruz. yoksa karakteri gördüğün son konuma kadar gider.
        }
    }

    private void SetState(ZombieStates state)
    {
        zombiState = state;
        animator.SetInteger("State", (int)state);//animatör içindeki int state değişkenini değiştiriyoruz ki diğer animasyona geçsin.
    }

    private void MoveToPlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(playerObject.transform.position);// karakterin son gördüğü son pozisyonuna kadar kovala demek
        SetState(ZombieStates.Walk);
    }

    private void KillZombi()
    {
        agent.isStopped = true;
        SetState(ZombieStates.Dead);
        Destroy(gameObject, 5);//objeyi 5 saniye sonra yok et.
    }
}