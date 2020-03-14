using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    public int diamonds;

    private Rigidbody2D _rigid;
    [SerializeField]
    private float _jumpForce = 5.0f;
    private bool _resetJunp = false;
    private bool _grounded = false;
    [SerializeField]
    private float _speed = 2.5f;

    private PlayerAnimations _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite; 


    private bool resetJumpNeeded = false;

    public int Health { get; set; }

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimations>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Health = 4;
    }


    void Update()
    {


        Movement();
        if(CrossPlatformInputManager.GetButtonDown("A_Button") && IsGrounded() == true) //Input.GetMouseButtonDown(0)
        {
            _playerAnim.Attack();
        }


    }
    void Movement()
    {
        float move = CrossPlatformInputManager.GetAxis("Horizontal"); // Input.GetAxisRaw("Horizontal");// Возвращает значение по горизонтале(по X оси)
        _grounded = IsGrounded();

        if (move > 0)
        {
            Flip(true);
        }
        else if (move < 0)
        {
            Flip(false);
        }

        if (  CrossPlatformInputManager.GetButtonDown("B_Button")  && IsGrounded() == true) // Input.GetKeyDown(KeyCode.Space)
        {
        //Jump
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.Jump(true);
        }
        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y); // Передвижение игрока
        _playerAnim.Move(move);
    }
    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);

        if (hitInfo.collider != null)
        {
            if (_resetJunp == false)
            {
                _playerAnim.Jump(false);
                return true;
            }
                
        }
        return false;
    }

    void Flip (bool faceRight)
    {
        if (faceRight == true)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if (faceRight == false)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        
    }
    IEnumerator ResetJumpRoutine()
    {
        _resetJunp = true;
        yield return new WaitForSeconds(0.1f);
        _resetJunp = false;
    }

    public void Damage()
    {
        if (Health < 1)
        {
            return;
        }
        Health--;
        UIManager.Instance.UpdateLive(Health);
        if (Health < 1)
        {
            _playerAnim.Death();
        }
    } 
    public void AddGems(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }

}


