using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;
    [Header("offset")]
    public Vector3 Offset;
    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    public virtual void Interact()
    {
        //this method is meat to be overwritten
       // Debug.Log("Interacting whth" + transform.name);

    }
    void Update()
    {
        //如果被选中并且没有交互
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance( new Vector3(player.position.x,0, player.position.z), new Vector3(interactionTransform.position.x,0,interactionTransform.position.z));
            //当距离小于interactable的作用半径的时候
            if (distance <= radius)
            {
                //交互方法
                Interact();
                //设置正在交互中
                hasInteracted = true;
            }
        }

    }
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        //重复触发交互信息
        hasInteracted = false;
    }
    public void OnDeFocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position+ Offset, radius);


    }
}
