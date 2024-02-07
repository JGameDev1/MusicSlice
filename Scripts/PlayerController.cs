using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D PlayerBody; public CapsuleCollider2D PlayerBodyForm;
    public float MovementSpeed, JumpForce, JumpFromGround, HorizontalInput, VerticalInput, JumpInput, OnPowerUpCronometre, Cronometre, LimitsOfMovementX, NegLimitsOfMovementX, LimitsOfMovementY, NegLimitsOfMovementY;
    public Animator _Animator;
    public LayerMask Ground;
    public Vector3 LastMovement = Vector3.zero;
    public List<GameObject> Projectiles; public int Ammo;
    public GameObject StarShoot;
    public string ShootKey;
    public bool IsDeath, Iddle, IsJumping, TactilControll;
    private PlayerUI _PlayerUI;
    public Joystick joystick;

    private void Awake()
    { PlayerBody = GetComponent<Rigidbody2D>(); PlayerBodyForm = GetComponent<CapsuleCollider2D>(); }
    void Start()
    {CeroButton(); CreationOfBullets(); _Animator = GetComponent<Animator>(); Iddle = true; IsJumping = false; IsDeath = false; _PlayerUI = GameObject.FindObjectOfType<PlayerUI>(); Cronometre = OnPowerUpCronometre; }

    void Movement()
    {   HorizontalInput = Input.GetAxisRaw("Horizontal") * MovementSpeed; VerticalInput = Input.GetAxisRaw("Vertical");
        if (HorizontalInput > 0) { Iddle = false; LastMovement = new Vector3(1, LastMovement.y, LastMovement.z); } else if (HorizontalInput < 0) { Iddle = false; LastMovement = new Vector3(-1, LastMovement.y, LastMovement.z); }
        if (HorizontalInput == 0) { Iddle = true; }
        if (VerticalInput > 0) { LastMovement = new Vector3(LastMovement.x, 1, LastMovement.z); } else if (VerticalInput < 0) { LastMovement = new Vector3(LastMovement.x, -1, LastMovement.z); } else if (VerticalInput == 0) { LastMovement = new Vector3(LastMovement.x, 0, LastMovement.z); }
        PlayerBody.velocity = new Vector2(HorizontalInput, PlayerBody.velocity.y);

        JumpInput = Input.GetAxisRaw("Jump") * Time.fixedDeltaTime * JumpForce;
        if (Physics2D.Raycast(transform.position, Vector2.down, JumpFromGround, Ground.value) && JumpInput > 0) { PlayerBody.AddForce(Vector2.up * JumpInput, ForceMode2D.Impulse); IsJumping = true; _Animator.SetFloat("JumpInput", Input.GetAxisRaw("Jump")); }
    }

    void DontCrossTheLimits()
    {
        if (transform.position.x >= LimitsOfMovementX) { transform.position = new Vector3(LimitsOfMovementX, transform.position.y, transform.position.z); }
        if (transform.position.x <= NegLimitsOfMovementX) { transform.position = new Vector3(NegLimitsOfMovementX, transform.position.y, transform.position.z); }
        if (transform.position.y >= LimitsOfMovementY) { transform.position = new Vector3(transform.position.x, LimitsOfMovementY, transform.position.z); }
        if (transform.position.y <= NegLimitsOfMovementY) { transform.position = new Vector3(transform.position.x, NegLimitsOfMovementY, transform.position.z); }
    }
    //--------------------------------------------------------------CODIGOS PARA BOTONES-------------------------
    public void PointRightUpButton() { if (TactilControll) { LastMovement = new Vector3(1, 1, LastMovement.z); Iddle = false; IsJumping = false; PlayerBody.velocity = new Vector2(1 * MovementSpeed, PlayerBody.velocity.y); _Animator.SetFloat("HorizontalInput", PlayerBody.velocity.x); _Animator.SetFloat("LastX", LastMovement.x); _Animator.SetBool("Iddle", Iddle); _Animator.SetBool("IsJumping", IsJumping); } }
    public void PointRightDownButton() { if (TactilControll) { LastMovement = new Vector3(1, -1, LastMovement.z); Iddle = false; IsJumping = false; PlayerBody.velocity = new Vector2(1 * MovementSpeed, PlayerBody.velocity.y); _Animator.SetFloat("HorizontalInput", PlayerBody.velocity.x); _Animator.SetFloat("LastX", LastMovement.x); _Animator.SetBool("Iddle", Iddle); _Animator.SetBool("IsJumping", IsJumping); } }
    public void MoveToRightButton() { if (TactilControll) { LastMovement = new Vector3(1, 0, LastMovement.z); Iddle = false; IsJumping = false; PlayerBody.velocity = new Vector2(1 * MovementSpeed, PlayerBody.velocity.y); _Animator.SetFloat("HorizontalInput", PlayerBody.velocity.x); _Animator.SetFloat("LastX", LastMovement.x); _Animator.SetBool("Iddle", Iddle); _Animator.SetBool("IsJumping", IsJumping); } }
    public void CeroButton() { if (TactilControll) { LastMovement = new Vector3(0, 0, 0); Iddle = true; IsJumping = false; PlayerBody.velocity = new Vector2(0, PlayerBody.velocity.y); _Animator.SetFloat("HorizontalInput", PlayerBody.velocity.x); _Animator.SetFloat("LastX", LastMovement.x); _Animator.SetBool("Iddle", Iddle); _Animator.SetBool("IsJumping", IsJumping); } }
    public void MoveToLeftButton() { if (TactilControll) { LastMovement = new Vector3(-1, 0, LastMovement.z); Iddle = false; IsJumping = false; PlayerBody.velocity = new Vector2(-1 * MovementSpeed, PlayerBody.velocity.y); _Animator.SetFloat("HorizontalInput", PlayerBody.velocity.x); _Animator.SetFloat("LastX", LastMovement.x); _Animator.SetBool("Iddle", Iddle); _Animator.SetBool("IsJumping", IsJumping); } }
    public void PointLeftUpButton() { if (TactilControll) { LastMovement = new Vector3(-1, 1, LastMovement.z); Iddle = false; IsJumping = false; PlayerBody.velocity = new Vector2(-1 * MovementSpeed, PlayerBody.velocity.y); _Animator.SetFloat("HorizontalInput", PlayerBody.velocity.x); _Animator.SetFloat("LastX", LastMovement.x); _Animator.SetBool("Iddle", Iddle); _Animator.SetBool("IsJumping", IsJumping); } }
    public void PointLeftDownButton() { if (TactilControll) { LastMovement = new Vector3(-1, -1, LastMovement.z); Iddle = false; IsJumping = false; PlayerBody.velocity = new Vector2(-1 * MovementSpeed, PlayerBody.velocity.y); _Animator.SetFloat("HorizontalInput", PlayerBody.velocity.x); _Animator.SetFloat("LastX", LastMovement.x); _Animator.SetBool("Iddle", Iddle); _Animator.SetBool("IsJumping", IsJumping); } }
    public void JumpButton() { if (TactilControll && Physics2D.Raycast(transform.position, Vector2.down, JumpFromGround, Ground.value)) { PlayerBody.AddForce(Vector2.up * (JumpForce + 40) * Time.fixedDeltaTime, ForceMode2D.Impulse); if (transform.position.y > -4.532) { IsJumping = true; Iddle = false; _Animator.SetBool("IsJumping", IsJumping); _Animator.SetBool("Iddle", Iddle); } else { IsJumping = false; Iddle = true; _Animator.SetBool("IsJumping", IsJumping); _Animator.SetBool("Iddle", Iddle); } } }
    //-------------------------------------------------------------CODIGOS PARA JOYSTICK------------------------
    void JoystickMovement(){Vector2 JoystickInput=joystick.Vertical*Vector2.up+joystick.Horizontal*Vector2.right;
PlayerBody.velocity=new Vector2(JoystickInput.x*MovementSpeed,PlayerBody.velocity.y);
if(JoystickInput.x>0){Iddle = false; LastMovement = new Vector3(1, LastMovement.y, LastMovement.z); } else if (JoystickInput.x<0){Iddle=false;LastMovement=new Vector3(-1,LastMovement.y, LastMovement.z); }
if(JoystickInput.x==0){Iddle=true;}
if(JoystickInput.y > 0.4) { LastMovement = new Vector3(LastMovement.x, 1, LastMovement.z);} else if (JoystickInput.y < -0.4) { LastMovement = new Vector3(LastMovement.x, -1, LastMovement.z); } else if (JoystickInput.y == 0) { LastMovement = new Vector3(LastMovement.x, 0, LastMovement.z); }
_Animator.SetFloat("HorizontalInput",joystick.Horizontal);_Animator.SetFloat("LastX",LastMovement.x);_Animator.SetBool("Iddle",Iddle);
if(transform.position.y>-4.532){IsJumping = true;_Animator.SetBool("IsJumping",IsJumping);}else{IsJumping=false;_Animator.SetBool("IsJumping",IsJumping);}}

    //----------------------------------------------------------------------------------------------------------

    void MoveAnimation() { _Animator.SetFloat("HorizontalInput", Input.GetAxisRaw("Horizontal")); _Animator.SetFloat("LastX", LastMovement.x); _Animator.SetBool("Iddle", Iddle); _Animator.SetFloat("JumpInput", Input.GetAxisRaw("Jump")); _Animator.SetBool("IsJumping", IsJumping); }
    void DeathAnimation() { _Animator.SetBool("IsDeath", IsDeath); }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsJumping = false;
        if (collision.gameObject.name == "Beer(Clone)") { _PlayerUI.Beer.enabled = true; _PlayerUI.EnergyDrink.enabled = false; _PlayerUI.DiscoBall.enabled = false; collision.gameObject.SetActive(false); Cronometre = OnPowerUpCronometre; }
        if (collision.gameObject.name == "EnergyDrink(Clone)") { _PlayerUI.EnergyDrink.enabled = true; _PlayerUI.Beer.enabled = false; _PlayerUI.DiscoBall.enabled = false; collision.gameObject.SetActive(false); Cronometre = OnPowerUpCronometre; }
        if (collision.gameObject.name == "DiscoBall(Clone)") { _PlayerUI.DiscoBall.enabled = true; _PlayerUI.EnergyDrink.enabled = false; _PlayerUI.Beer.enabled = false; collision.gameObject.SetActive(false); Cronometre = OnPowerUpCronometre; 
        IsJumping=false;}
    }

    void CreationOfBullets()
    {
        for (int i = 0; i < Ammo; i++)
        {
            GameObject S = Instantiate(StarShoot);
            S.SetActive(false);
            Projectiles.Add(S);
        }
    }

    void StarComprobation() { for (int i = 0; i < Projectiles.Count; i++) if (Projectiles[i] == null) { GameObject Respuesto = Instantiate(StarShoot); Respuesto.SetActive(false); Projectiles[i] = (Respuesto); } }

    public GameObject RequestStar()
    {
        for (int i = 0; i < Projectiles.Count; i++)
        {
            if (!Projectiles[i].activeSelf)
            {
                Projectiles[i].SetActive(true); Projectiles[i].transform.position = transform.position + new Vector3(LastMovement.x, 2, 0);
                return Projectiles[i];
            }
        }
        return Projectiles[0];
    }

    void ShotStar()
    { if (Input.GetKeyDown(ShootKey)) { RequestStar(); } }
    public void ShotStarWithoutCondition() { if (LastMovement.x < 0 || LastMovement.x > 0) { RequestStar(); } }


    void Update()
    {
        StarComprobation();
        if (GameManager._SharedInstanceGameManager.CurrentGamestate == Gamestates.RunningGame){if(TactilControll==false){ShotStar();{MoveAnimation();}}}
        if (Cronometre <= OnPowerUpCronometre && _PlayerUI.Beer.enabled == true) { Cronometre -= Time.deltaTime; MovementSpeed = 2; }
        if (Cronometre <= 0) { _PlayerUI.Beer.enabled = false; MovementSpeed = 5; }
        if (Cronometre <= OnPowerUpCronometre && _PlayerUI.EnergyDrink.enabled == true) { Cronometre -= Time.deltaTime; MovementSpeed = 9; }
        if (Cronometre <= 0) { _PlayerUI.EnergyDrink.enabled = false; MovementSpeed = 5; }
        if (Cronometre <= OnPowerUpCronometre && _PlayerUI.DiscoBall.enabled == true) { Cronometre -= Time.deltaTime; MovementSpeed = 7; }
        if (Cronometre <= 0) { _PlayerUI.DiscoBall.enabled = false; MovementSpeed = 5; }
        if (Cronometre <= 0) { Cronometre = 0; }
        DontCrossTheLimits(); DeathAnimation();
    }

    void FixedUpdate(){if(GameManager._SharedInstanceGameManager.CurrentGamestate==Gamestates.RunningGame){if(!TactilControll){Movement();}else if(TactilControll&&MusicManager.MusicManagerSharedInstance.UseJoystick){JoystickMovement();}}
}}
