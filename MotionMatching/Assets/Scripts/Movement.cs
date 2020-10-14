using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public struct SUserControl
{
    public Vector3 direction;
}

public struct SMovementModel
{
    public float idle_speed;
    public float run_speed;

    public float responsiveness_speed;
    public float responsiveness_direction;
}

public struct SActor
{
    public Vector3 position;
    public Vector3 direction;
    public float speed;
}

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController controller;
    public float speed = 6f;
    private Vector3 movementInput;
    public GameObject historDot;
    public List<GameObject> history = new List<GameObject>();
    SMovementModel model = new SMovementModel();
    SActor actor = new SActor();
    List<SActor> predictions = new List<SActor>();
    public bool moving = false;
    // public bool running;
    [SerializeField]public int steps = 10;

    void Start()
    {
        model.idle_speed = 0.0f;
        model.run_speed = 4.0f;
        model.responsiveness_speed = 0.04f;
        model.responsiveness_direction = 0.04f;
        InvokeRepeating("History", 0f, 0.2f);
        // running = false;
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
    

    // Update is called once per frame

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        movementInput = new Vector3(input.x, 0f, input.y); //this should be normalized in the input system itself.
    }

    public void OnRun()
    {
        // running = !running;
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


       static SActor Step(CharacterController controller, SActor current, Vector3 direction, float dt, bool moving)
    {
        SActor actor = current;
        float requested_speed;

        // if (running) 
        // {
        //     requested_speed = model.run_speed;
        // } else
        // {
        //    requested_speed = model.idle_speed; 
        // }
        if (moving) 
        {
            actor.speed = controller.velocity.magnitude;
        }
        else
        {
            actor.speed = 0;  
        }
         //Mathf.Lerp(actor.speed, requested_speed, model.responsiveness_speed); 
        actor.direction = direction; //Vector3.Lerp(actor.direction, direction, model.responsiveness_direction);

        actor.position = actor.position + actor.direction.normalized * actor.speed * dt;

        return actor;
    }

    static void Predict(CharacterController controller, SActor actor, Vector3 direction, List<SActor> destination, bool moving, int steps)
    {
        float dt = 0.2f;

        destination.Clear();
        destination.Add(actor);

        List<SActor> predictions = new List<SActor>();

        for(int step = 0; step < steps; ++step)
        {
            actor = Step(controller, actor, direction, dt, moving);
            destination.Add(actor);
        }
    }

    void Update()
    {

        if (movementInput.magnitude >= 0.1f)
        {
            
            float targetAngle = Mathf.Atan2(movementInput.x, movementInput.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            controller.Move(movementInput * speed * Time.deltaTime);
            moving = true;
        }
        else 
        {
            moving = false;
        }

        Debug.Log(controller.velocity.magnitude);
        actor = Step(controller, actor, this.transform.forward, Time.deltaTime, moving);
        Predict(controller, actor, this.transform.forward, predictions, moving, steps);   
    }

}


