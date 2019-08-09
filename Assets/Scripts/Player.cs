using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action Hit;
    public event Action Shot;
    public event Action<int> Damaged;
    public event Action Death;
    public event Action Kill;

    [SerializeField]
    private AudioSource hitSource;

    [SerializeField]
    private AudioSource shotSource;

    [SerializeField]
    private AudioClip hitSound;

    [SerializeField]
    private AudioClip shotSound;

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float raycastOffset = 10;

    [SerializeField]
    private float raycastDistance = 1000;

    [SerializeField]
    private float rotationAngle = 90;

    [SerializeField]
    private float rotationTimeSpan = 0.5f;

    [SerializeField]
    private float factor = 0.4f;

    [SerializeField]
    private bool rotating = false;

    [SerializeField]
    private float boxRaycastDistance = 1;

    [SerializeField]
    private int shotDamage = 10;

    private Coroutine rotationCoroutine = null;


    [SerializeField]
    private Animator weaponAnimator;

    private ShootingStateMachineBehaviour shootingState;

    [SerializeField]
    private bool shooting = false;

    [SerializeField]
    private int health = 100;   
    public int Health
    {
        get { return health; }
//        set { health = value; }
    }


    private void Awake()
    {
   
        
    }

    private void Start()
    {

            
    }

    private void OnEnable()
    {
        shootingState = weaponAnimator.GetBehaviour<ShootingStateMachineBehaviour>();
        shootingState.Exit += OnShootingStateExit;

    }

    private void OnDisable()
    {
        shootingState.Exit -= OnShootingStateExit;
        
    }

    private void OnShootingStateExit()
    {
        weaponAnimator.SetBool("Shot", false);
        shooting = false;
    }

    private void Update()
    {
        //transform.position Physics.gravity
    }


    public void Move(Vector3 direction)
    {
        Vector3 localDirection = transform.TransformDirection(direction);

        RaycastHit hit;
        // Check collision first
        Physics.BoxCast(transform.position, transform.localScale / 2, localDirection, out hit, transform.rotation, factor);
        Extensions.DrawBoxCastBox(transform.position, transform.localScale / 2, transform.rotation, localDirection, factor, Color.magenta);

        if (hit.collider == null)
            transform.position += localDirection * speed * Time.deltaTime;
        else
        {
            //float projection = Vector3.Dot(localDirection, hit.normal);
            //if (projection < 0)
            //{
            //    localDirection = localDirection - projection * hit.normal;
            //}

            //Debug.DrawRay(transform.position, localDirection * 10, Color.cyan);
            //Debug.DrawRay(hit.collider.transform.position, hit.normal * 10, Color.cyan);

            //Vector3 reflection = Vector3.Cross(localDirection, hit.normal);
            //transform.position += localDirection * speed * Time.deltaTime;

            //Debug.DrawRay(transform.position, Vector3.Cross(localDirection, hit.normal) * 10, Color.cyan);

        }
    }

 

    public void Rotate(Vector3 rotation)
    {
        if (rotation.Equals(Vector3.zero))
            return;

        // Check collision
        Vector3 currentRotation = transform.rotation.eulerAngles;
        //Debug.Log(String.Format("rotating ... : {0}", currentRotation.y + (rotation.y * rotationAngle)));
        Quaternion toRotation = Quaternion.Euler(currentRotation.x + (rotation.x * rotationAngle), currentRotation.y + (rotation.y * rotationAngle), currentRotation.z + (rotation.z * rotationAngle));


        Collider[] hits;
        // Check collision first
        hits = Physics.OverlapBox(transform.position, transform.localScale / 2, toRotation);
        Extensions.DrawBox(transform.position, transform.localScale / 2, toRotation, Color.magenta, 1.5f);

        if (!rotating)
        {
            if (hits.Length <= 1)
            {
                rotationCoroutine = StartCoroutine(TimedRotation(transform.rotation, toRotation, rotationTimeSpan));
                //if (hit.collider == null)
                //    rotationCoroutine = StartCoroutine(TimedRotation(transform.rotation, toRotation, rotationTimeSpan));
                //else
                //    Debug.Log(hit.collider.name);
            }
            else
            {

            }


        }


        //StopCoroutine(rotationCoroutine);


    }

    public void Shoot()
    {
        if (shooting)
            return;

        Shot?.Invoke();

        RaycastHit hit;
        Physics.Raycast(transform.position + (transform.forward * raycastOffset), transform.forward, out hit, raycastDistance);
        Debug.DrawRay(transform.position + (transform.forward * raycastOffset), transform.forward * raycastDistance, Color.magenta, 2.5f);

        shotSource.Play();


        // animation
        weaponAnimator.SetBool("Shot", true);
        shooting = true;

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Hit?.Invoke();
            hitSource.Play();
            Player playerHit = hit.collider.GetComponent<Player>();
            playerHit.DealDamage(shotDamage);


            if (playerHit.Health == 0)
            {
                OnKill();
                playerHit.OnDeath();
            }               

        }
    }

    private IEnumerator TimedRotation(Quaternion start, Quaternion end, float duration)
    {
        if (duration < 0f)
            yield break;

        float elapsedTime = 0f;
        rotating = true;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = end;

        rotating = false;
    }

    private void DealDamage(int amount)
    {
        health -= 10;
        if (health - amount < 0)
            health = 0;

        Damaged?.Invoke(amount);
     
    }

    private void OnDeath()
    {
        Death?.Invoke();
    }

    private void OnKill()
    {
        Kill?.Invoke();
    }
}
