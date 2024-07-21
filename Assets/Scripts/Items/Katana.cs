using UnityEngine;

[SelectionBase]
public class Katana : Item, IUsable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider _attackCollider;
    [SerializeField] private AudioClip _swingSound;

    private readonly string _isAttackingParameter = "IsAttacking";

    private PlayerInputActions _playerInputActions;
    private bool _isAttacking;

    public void Use()
    {
        Attack();
    }

    private new void Awake()
    {
        base.Awake();
        InitTakeable();
        _playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _playerInputActions.Enable();
        StopAttacking();
    }

    private void OnDisable()
    {
        _playerInputActions.Disable();
        StopAttacking();
    }

    public override void Drop()
    {
        base.Drop();
        _animator.enabled = false;
        transform.rotation = Quaternion.identity;
    }

    public override void Take()
    {
        base.Take();
        _animator.enabled = true;
    }

    private void Attack()
    {
        if(!_isAttacking)
        {
            _isAttacking = true;
            _animator.SetBool(_isAttackingParameter, true);
            GlobalSoundsPlayer.Instance.PlayOneShot(_swingSound);
            _attackCollider.enabled = true;
        }
    }

    public void StopAttacking()
    {
        _animator.SetBool(_isAttackingParameter, false);
        _attackCollider.enabled = false;
    }

    //�������� � Update, �.�. ��� �����, ��� �������� Set � Get � ����������� ����������� �����,
    //�� �� ���� � ������ StopAttacking ���� _isAttacking �������� �� false ������� ��� 
    //�������� IS_ATTACKING � ���������, � ��������� ���� �������� �������� �����.

    private void Update()
    {
        if(!_animator.GetBool(_isAttackingParameter))
            _isAttacking = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(_isAttacking == false)
            return;

        IDamagable dieable;

        if(collider.TryGetComponent<IDamagable>(out dieable))
            dieable.Die();
    }

    protected override void InitTakeable()
    {
        _takeable = new PickupableBehaviour(this);
    }
}
