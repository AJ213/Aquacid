using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float baseSpeed = 1;
    [SerializeField] float dashMultiplier = 1;
    [SerializeField] float dashCooldown = 3;

    [Header("Sword")]
    public float swordDamage = 1;
    [SerializeField] int stabAmountNeeded = 3;
    [SerializeField] float swordCooldown = 3;
    [SerializeField] float tornadoSpeed = 3f;

    [Header("For Testing")]
    public static int kills = 0;
    public static int stabCount;
    [SerializeField] bool isReloadingDash = false;
    [SerializeField] bool isReloadingStab = false;
    [SerializeField] float swordDisappearTime = 0.25f;
    [SerializeField] float tornadoDisappearTime = 1f;
    
    [SerializeField] float directionSpeed;
    [SerializeField] Vector2 movementDirection;
    [SerializeField] Vector3 mousePosition;
    [SerializeField] Vector2 mouseDirection;
    [SerializeField] Rigidbody2D rb = null;
    [SerializeField] Text points = null;
    [SerializeField] AudioSource attackSound = null;
    [SerializeField] AudioSource dashSound = null;
    [SerializeField] AudioSource tornadoSound = null;
    [SerializeField] AudioSource characterDeath = null;
    [SerializeField] Animator characterAnimator = null;
    [SerializeField] GameObject swordPivotPoint = null;
    [SerializeField] GameObject tornado = null;

    private void Awake()
    {
        swordPivotPoint.SetActive(false);
        tornado.SetActive(false);
    }

    void Start()
    {
        kills = 0;
        stabCount = 0;
    }

    void MovementInputs()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        directionSpeed = Mathf.Clamp(movementDirection.magnitude, 0, 1);
        movementDirection.Normalize();
    }
    void Move()
    {
        if (Input.GetMouseButton(1) && !isReloadingDash)
        {
            rb.AddForce(mouseDirection * dashMultiplier * Time.fixedDeltaTime);
            StartCoroutine(DashReload());
        }
        
        rb.velocity = movementDirection * directionSpeed * baseSpeed;
    }

    void Attack()
    {

        if (Input.GetMouseButton(0) && !isReloadingStab)
        {
            float angle = Projectile.CalculateAngle(mouseDirection);

            swordPivotPoint.transform.eulerAngles = new Vector3(0, 0, angle);
            swordPivotPoint.SetActive(true);
            isReloadingStab = true;
            
            if (stabCount >= stabAmountNeeded)
            {
                stabCount = 0;
                tornado.transform.position = this.transform.position;
                tornado.SetActive(true);
                tornado.GetComponent<Rigidbody2D>().AddForce(mouseDirection * tornadoSpeed, ForceMode2D.Impulse);
                StartCoroutine(TornadoReload());
            }
            StartCoroutine(SwordReload());
        }
    }
    IEnumerator TornadoReload()
    {
        tornadoSound.Play();
        yield return new WaitForSeconds(tornadoDisappearTime);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        tornado.SetActive(false);
    }

    IEnumerator DashReload()
    {
        dashSound.Play();
        isReloadingDash = true;
        yield return new WaitForSeconds(dashCooldown);
        isReloadingDash = false;
    }
    
    IEnumerator SwordReload()
    {
        attackSound.Play();
        yield return new WaitForSeconds(swordDisappearTime);
        swordPivotPoint.SetActive(false);
        yield return new WaitForSeconds(swordCooldown);
        isReloadingStab = false;
    }


    void FixedUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseDirection = Projectile.CalculateDirection(this.transform.position, mousePosition);

        MovementInputs();
        Move();
        Attack();
        Animate();
    }

    private void Update()
    {
        if (Input.GetKeyDown("f")) //suicide
        {
            this.gameObject.GetComponent<Health>().TakeDamage(3);
        }
        
        points.text = "Points " + kills;
    }

    private void OnDestroy()
    {
        characterDeath.Play();
    }

    void Animate()
    {
        characterAnimator.SetFloat("Horizontal", movementDirection.x);
        characterAnimator.SetFloat("Vertical", movementDirection.y);
        characterAnimator.SetFloat("Speed", directionSpeed);
    }
}
