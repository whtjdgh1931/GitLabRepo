using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Animator playerAnim;

    public MapMgr mapMgr;

    public bool isMove;

    public int backCnt;

    public void Awake()
    {
        playerAnim = GetComponentInChildren<Animator>();

    }

    public void OnUp()
    {
        if (isMove) return;

        mapMgr.PlayerMove(Vector2Int.up);

    }

    public void OnDown()
    {
        if (isMove) return;
        backCnt++;
        if (backCnt >= 3) mapMgr.GameOver();
        mapMgr.PlayerMove(Vector2Int.down);

    }

    public void OnLeft()
    {
        if (isMove) return;

        mapMgr.PlayerMove(Vector2Int.left);

    }

    public void OnRight()
    {
        if (isMove) return;

        mapMgr.PlayerMove(Vector2Int.right);

     
    }

    public void SetAnimTrigger()
    {
        playerAnim.SetTrigger("IsMove");
    }
}
