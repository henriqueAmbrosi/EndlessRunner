using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Lane { Left, Middle, Right };
public enum Height { Roll, Ground, Air, Jump }

public class Player_ : MonoBehaviour
{
    public Animator animator;
    private Lane lane;
    private Height height;
    public float timeToSwitchLane = .2f;
    public float speed = 5;
    private float gravity = 10;
    private float yVelocity;
    public GameObject model;
    public RaycastHit groundHit;
    public LayerMask groudLayers;

    [Header("Jump")]
    public float jumpHeight = 1.5f;
    public float jumpTime = .4f;

    public Lane Lane
    {
        get => lane;
        set
        {
            lane = value;
            switch (lane)
            {
                case Lane.Left:
                    LeanTween.moveX(gameObject, -2, timeToSwitchLane).setEaseInOutQuad();
                    break;
                case Lane.Middle:
                    LeanTween.moveX(gameObject, 0, timeToSwitchLane).setEaseInOutQuad();
                    break;
                case Lane.Right:
                    LeanTween.moveX(gameObject, 2, timeToSwitchLane).setEaseInOutQuad();
                    break;
                default:
                    break;
            }
        }
    }

    public Height Height { get => height; set
        {
            if(height != value)
            {
                height = value;
            }
            switch (height)
            {
                case Height.Roll:
                    animator.SetTrigger("Roll");

                    break;
                case Height.Ground:
                    animator.SetTrigger("Running");
                    break;
                case Height.Air:
                    animator.SetTrigger("Falling");
                    break;
                case Height.Jump:
                    animator.SetTrigger("Jump");
                    LeanTween.moveY(model, model.transform.position.y + jumpHeight, jumpTime).setEaseOutQuad().setOnComplete(() => { 
                        if(Height == Height.Jump)
                        {
                            Height = Height.Air;
                        }
                    });
                    break;
                default: break;
            }
        }

    }

    private void Awake()
    { 
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lane = Lane.Middle;
       
        animator.SetTrigger("Running");
        yVelocity = 0;
        Height = Height.Ground;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;

        switch (Height)
        {
            case Height.Roll:
                if (Physics.Raycast(model.transform.position + Vector3.up * .3f, Vector3.down, out groundHit, .5f, groudLayers))
                {
                    yVelocity = 0;
                    model.transform.position = groundHit.point;
                }
                else
                {
                    yVelocity -= gravity * 5 * Time.deltaTime;
                    model.transform.position += Vector3.up * yVelocity * Time.deltaTime;
                }
                break;
            case Height.Ground:
                if (Physics.Raycast(model.transform.position + Vector3.up * .3f, Vector3.down, out groundHit, .5f, groudLayers))
                {
                    yVelocity = 0;
                    model.transform.position = groundHit.point;
                }
                else
                {
                    Height = Height.Air;
                }
                break;
            case Height.Air:
                if (Physics.Raycast(model.transform.position + Vector3.up * .3f, Vector3.down, out groundHit, .5f, groudLayers))
                {
                    yVelocity = 0;
                    Height = Height.Ground;
                }
                else
                {
                    yVelocity -= gravity * Time.deltaTime;
                    model.transform.position += Vector3.up * yVelocity * Time.deltaTime;
                }
                break;
            case Height.Jump:
                break;
            default:
                break;
        }

       
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Section")
        {
            Game_.instance.levelGenerator.SpawnLevelSection(true);
        }
    }

    public void Jump() {
        if (Height == Height.Ground || Height == Height.Roll)
        {
            Height = Height.Jump;
        }
    }

    public void Roll() {
        Height = Height.Roll;
    }

    public void Right() {
        switch (lane)
        {
            case Lane.Left:
                Lane = Lane.Middle;
                break;
            case Lane.Middle:
                Lane = Lane.Right;
                break;
            default:
                break;
        }
    }

    public void Left() {
        switch (lane)
        {
            case Lane.Right:
                Lane = Lane.Middle;
                break;
            case Lane.Middle:
                Lane = Lane.Left;
                break;
            default:
                break;
        }
    }
}
