using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    Transform target;//Target to follow
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent =  GetComponent<NavMeshAgent>();
        
    }
     void Update()
    {
        if (target != null)
        {
            //走向互动品
            agent.SetDestination(target.position);
            //面向互动品
            FaceTarget();
        }
    }
    public void moveToPoint(Vector3 point)
    {
        agent.SetDestination(point);

    }

    public void FollowTarget(Interactable newTarget)
    {
        //在选中敌人的时候停止位置
        agent.stoppingDistance = newTarget.radius * .8f;
        agent.updateRotation = false;
        target = newTarget.interactionTransform;

    }
    public void StopFollowingTarget()
    {
        //失去敌人可以停在选中位置
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
       target = null;
    }

  void   FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }
}
