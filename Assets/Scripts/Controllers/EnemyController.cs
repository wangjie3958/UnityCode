using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;
    //玩家接近到攻击范围的事件回调
    public event Action EnterDetectionRange;
    public event Action ExitDetectionRange;
    public event Action OnBackCallback;

    private bool isDetectionRange = false;
    [HideInInspector]
    public   Transform target;
    NavMeshAgent agent;
    NavMeshObstacle obstacle;
    CharacterCombat combat;
    CharacterStats stats;
    //敌人的原来位置
    Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        combat = GetComponent<CharacterCombat>();
        stats = GetComponent<CharacterStats>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.isDead)
        {
            return;
        }
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            //第一次进入侦测范围回调
            if (!isDetectionRange)
            {
                EnterDetectionRange?.Invoke();
                isDetectionRange = true;
                agent.SetDestination(target.position);
            }

           if (distance <= agent.stoppingDistance)
                {
                    //Attack target
                    CharacterStats targetStats = target.GetComponent<CharacterStats>();
                    if (targetStats != null)
                    {
                        combat.Attack(targetStats);
                    }
                    //face target
                    FaceTarget();
                agent.enabled = false;
                obstacle.enabled = true;
            }
            else
            {
                agent.enabled = true;
                obstacle.enabled = false;
                agent.SetDestination(target.position);
            }
           
        }else if (isDetectionRange)
        {
            //回到原点
            agent.SetDestination(originalPosition);
            //切换音效
            ExitDetectionRange?.Invoke();
            isDetectionRange = false;
        }
        //回到原点回血
        if (!isDetectionRange && (stats.currentHealth<stats.maxHealth)&&Vector3.Distance(originalPosition, transform.position) <= agent.stoppingDistance)
        {
            OnBackCallback?.Invoke();
        }
        
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
         Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        //also
       // transform.LookAt(transform.position+ new Vector3(direction.x, 0, direction.z));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
