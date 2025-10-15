using UnityEngine;

public abstract class PlayerState
{
    protected Player player;
    protected Animator anim;
    protected Rigidbody2D rb;

    protected bool JumpPressed { get => player.jumpPressed; set => player.jumpPressed = value; }
    protected bool JumpReleased { get => player.jumpReleased; set => player.jumpReleased = value; }
    protected bool RunPressed => player.runPressed;
    protected Vector2 MoveInput => player.moveInput;


    public PlayerState (Player player)
    {
        this.player = player;
        this.anim = player.anim;
        this.rb = player.rb;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }

    public virtual void Update() { }
    public virtual void FixedUpdate() { }    
}
