using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int FacingDirection { get; private set; } = 1;
    public Rigidbody2D RB {  get; private set; }
    public EnemyConfig Config;
    public Enemy_Senses Senses {  get; private set; }
    public StateMachine StateMachine { get; private set; }

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        StateMachine = new StateMachine();
        Senses = GetComponent<Enemy_Senses>();
    }

    public void Start()
    {
        StateMachine.Initialize(new PatrolState(this));
    }

    private void Update() => StateMachine.CurrentState?.Update();
    private void FixedUpdate() => StateMachine.CurrentState?.FixedUpdate();
    
    public void Flip()
    {
        FacingDirection *= -1;

        Vector3 scale = transform.localScale;
        scale.x = FacingDirection;
        transform.localScale = scale;
    }
}
