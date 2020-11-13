using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMotor))]
public class PalyerController : MonoBehaviour
{
    //当前选中的互动对象
    public Interactable focus;

    //可以移动的layer
    public LayerMask movmentMask;
    //跟随的相机
    Camera cam;
    //移动马达
    PlayerMotor motor;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();

    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        //左键移动
        if (Input.GetMouseButtonDown(0))
        {
            //创建数百哦从屏幕发射射线
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,100,movmentMask))
            {
                //Move out player to what we hit 
                motor.moveToPoint(hit.point);
                //Stop focusing  any objects 
                RemoveFocus();
            }
        }    
        //右键选中
        if (Input.GetMouseButtonDown(1))
        {
            //创建鼠标从屏幕发射射线
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,100))
            {
                //Check  if we  hit an interactable
              Interactable interactable =   hit.collider.transform.GetComponent<Interactable>();
                //if we did set it our foucs 
                if (interactable != null)
                {
                    //选中可交互对象
                    SetFocus(interactable);
                }

            }
        }
    }
    //interactable set focus and movement
     void SetFocus(Interactable newFocus)
    {
        //点中了新的interactable
        if (focus != newFocus)
        {
            if (focus != null)
                focus.OnDeFocused();
            //将选中的interactable保存
            focus = newFocus;
            //移动到新的interactable
            motor.FollowTarget(newFocus);
        }
        //将自己发送给interactable
        newFocus.OnFocused(transform);
    }

    //interactable remove focus
    void RemoveFocus()
    {
        if (focus != null)
        {
            //移除interactable的内存
            focus.OnDeFocused();
            //移除自己的内存
            focus = null;
        }
        motor.StopFollowingTarget();
    }
}
