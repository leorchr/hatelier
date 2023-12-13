using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkDustAdapter : MonoBehaviour
{
    S_Player_Anim_Manager anim;

    private void Awake()
    {
        anim = transform.parent.GetComponent<S_Player_Anim_Manager>();
    }
    public void WalkDust()
    {
        anim.walkDust();
    }

    public void disablePick()
    {
        anim.disablePickUp();
    }
}
