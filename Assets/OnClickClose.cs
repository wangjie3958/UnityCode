﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickClose : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            gameObject.SetActive(false);
        }
    }
}
