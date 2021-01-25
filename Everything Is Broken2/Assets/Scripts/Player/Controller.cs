using UnityEditor.UIElements;
using UnityEngine;
using System.Collections;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{
    public Queue<GameObject> RoboArms = new Queue<GameObject>();
    public EverythingIsBroken inputActions;
    public InputAction fireAction;
    public InputAction lookAction;

    public Transform player;
    public Transform camera;
    public Transform roboboy;

    public DampedTransform[] dampedTransforms;

    public ToolAnimation toolAnimation;

    [SerializeField] ParticleSystem collisionParticles;
    [SerializeField] GameObject worldRoboPart;
    [SerializeField] Transform partsParent;

    Rigidbody rigidbody;
    Vector3 move;
    float moveSpeed = 2f;
    float sensitivity = 500f;




    void Awake()
    {
        inputActions = new EverythingIsBroken();

        inputActions.Player.Move.performed += context => move = context.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += context => move = Vector2.zero;

        inputActions.Player.Fire.performed += Fire;

        DampedTransform[] dampedTransform = FindObjectsOfType<DampedTransform>();
        dampedTransforms = dampedTransform;
    }

    private void Start()
    {
        foreach (var dampedT in dampedTransforms)
        {
            dampedT.weight = moveSpeed / 0.5f;
            dampedT.data.dampPosition = /*moveSpeed **/ 0.05f; //0.1f;
            dampedT.data.dampRotation =/* moveSpeed **/ 0.05f; //0.1f;
        }

        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        inputActions.Player.Move.Enable();
        inputActions.Player.Look.Enable();
        inputActions.Player.Fire.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Move.Disable();
        inputActions.Player.Look.Disable();
        inputActions.Player.Fire.Disable();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (ToolAnimation.attacking)
        {
            moveSpeed =3f;
            transform.Rotate(0, move.x* sensitivity * Time.deltaTime, 0, Space.Self);
            collisionParticles.gameObject.SetActive(false);
        }
        else
        {
            moveSpeed = 1.5f; //or 2
            transform.Rotate(0, move.x * sensitivity * Time.deltaTime, 0, Space.Self);
            collisionParticles.gameObject.SetActive(true);
        }

        Vector3 m = new Vector3(/*move.x*/0, 0, move.y) * moveSpeed * Time.deltaTime;
        transform.Translate(m, Space.Self);
    }

    void Fire(InputAction.CallbackContext context)
    {
        roboboy.GetComponent<ToolAnimation>().RotateCharacter(); //GetRoboboy's ToolAnimation Component in Awake() to serialize;
    }

    public void OnChildCollisionEntered(Collision collision, Rigidbody childRigidbody)
    {
        SpawnRoboPartAtCollision();
    }

    public void SpawnRoboPartAtCollision()
    {
        GameObject roboPartCopy = Instantiate(worldRoboPart, partsParent, false);

        roboPartCopy.transform.localScale = new Vector3(.2f, .3f, .2f);

        roboPartCopy.transform.parent = null;
    }
}
