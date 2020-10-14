using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AdvancedMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;
    // Start is called before the first frame update
    private Vector3 movementInput;
    public GameObject historDot;
    public List<GameObject> history = new List<GameObject>();
    List<SActor> predictions = new List<SActor>();
    SActor actor = new SActor();
    public bool moving = false;
    [SerializeField]public int steps = 10;

    void Start()
    {
        InvokeRepeating("History", 0f, 0.2f);
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        movementInput = new Vector3(input.x, 0f, input.y); //this should be normalized in the input system itself.
    }

    void OnDrawGizmos()
    {
        if(predictions.Count == 0) return;
        float radius = 0.4f;
        Gizmos.DrawSphere(predictions[0].position, radius);

        foreach(SActor prediction in predictions)
        {
            radius = 0.1f;
            Gizmos.DrawSphere(prediction.position, radius);
        }
    }

    static SActor Step(CharacterController controller, SActor current, Vector3 direction, float dt, bool moving, Transform cam)
    {
        SActor actor = current;
        float requested_speed;

        if (moving) 
        {
            actor.speed = controller.velocity.magnitude;
        }
        else
        {
            actor.speed = 0;  
        }
         //Mathf.Lerp(actor.speed, requested_speed, model.responsiveness_speed); 
        actor.direction = Vector3.Lerp(direction, cam.forward, 0.1f); //Vector3.Lerp(actor.direction, direction, model.responsiveness_direction);

        actor.position = actor.position + actor.direction.normalized * actor.speed * dt;

        return actor;
    }

    static void Predict(CharacterController controller, SActor actor, Vector3 direction, List<SActor> destination, bool moving, int steps, Transform cam)
    {
        float dt = 0.2f;

        destination.Clear();
        destination.Add(actor);

        List<SActor> predictions = new List<SActor>();

        for(int step = 0; step < steps; ++step)
        {
            actor = Step(controller, actor, direction, dt, moving, cam);
            destination.Add(actor);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (movementInput.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movementInput.x, movementInput.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle , 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            moving = true;
            actor = Step(controller, actor, moveDir, Time.deltaTime, moving, cam);
            Predict(controller, actor, moveDir, predictions, moving, steps, cam); 
        }
        else
        {
            moving = false;
            actor = Step(controller, actor, this.transform.forward, Time.deltaTime, moving, cam);
            Predict(controller, actor, this.transform.forward, predictions, moving, steps, cam); 
        }

         
    }

    
    void History()
    {
        GameObject newPosition = Instantiate(historDot, transform.position, transform.rotation);
        history.Add(newPosition);

        if (history.Count > 5)
        {
            GameObject outdatedPosition = history[0];
            history.RemoveAt(0);
            Destroy(outdatedPosition);
        }
    }
}
