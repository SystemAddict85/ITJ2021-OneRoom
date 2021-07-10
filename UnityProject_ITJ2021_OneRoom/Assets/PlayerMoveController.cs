using UnityEngine;

public class PlayerMoveController : MoveController
{
    private Animator _anim;
    private SpriteRenderer _rend;

    protected override void Awake()
    {
        base.Awake();
        _anim = GetComponent<Animator>();
        _rend = GetComponent<SpriteRenderer>();
    }

    protected override void NoInput()
    {
        base.NoInput();
        _anim.SetBool("isMoving", false);
    }

    protected override void Move()
    {
        base.Move();
        _anim.SetBool("isMoving", true);
        _rend.flipX = input.Horizontal < 0f;
        
    }

    protected override void Move3D()
    {
        base.Move3D();
        _anim.SetBool("isMoving", true);
        _rend.flipX = input.Horizontal < 0f;
    }
}