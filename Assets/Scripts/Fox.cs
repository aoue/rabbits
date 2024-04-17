using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    [SerializeField] float sightDistance;  // how close a target needs to be to begin chase. Should be same size as circle radius.
    [SerializeField] float catchDistance;  // how close a target needs to be for it to be caught and disabled.
    [SerializeField] CharacterManager characters;
    [SerializeField] SpriteRenderer renderer;

    private float speed;
    private bool inHole = false;  // stops the fox from moving when true
    private bool isMoving = false;  // stops from chasing a new target until movingTime elapses.
    private float movingTime;  // how long it will keep chasing the current target
    private Vector3 dest;  // the target it is currently chasing
    private Rabbit targetRabbit;  // the rabbit being currently chased.
    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inHole) return;

        // move the character towards the target position
        if (movingTime > 0f)
        {
            // check if target suddenly went into hiding
            if (targetRabbit != null && targetRabbit.get_isHidden())
            {
                // in that case, become confused and pick a random place to start moving to in the near area
                Vector3 confused_dest = UnityEngine.Random.insideUnitCircle * 3f;
                movingTime = 2f;
                speed = 1.5f;
                dest = transform.position + confused_dest;
                targetRabbit = null;
            }
            // otherwise, keep moving towards the target
            else
            {
                // transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
                agent.SetDestination(dest);
                movingTime -= Time.deltaTime;
            }
        }

        float minRabbitDistance = sightDistance + 1f;
        foreach (Rabbit prey in characters.get_preyList())
        {
            if (prey.get_isHidden() || prey.get_isCaught()) continue;

            Vector3 currentRabbit = prey.transform.position;
            float currentRabbitDistance = Vector3.Distance(transform.position, currentRabbit);
            
            // check if target is in danger circle with math
            if (currentRabbitDistance < catchDistance)
            {
                // if caught, then disable the rabbit
                prey.set_isCaught();
                continue;
            }

            // check if target enters collision circle with math
            if (sightDistance > currentRabbitDistance && currentRabbitDistance < minRabbitDistance)
            {
                minRabbitDistance = currentRabbitDistance;
                dest = currentRabbit;
                targetRabbit = prey;
                dest.z = transform.position.z;
                movingTime = 1.0f;
                speed = 3f;
            }
        }
        
        

        
    }

    public void fellInHole()
    {
        // the fox fell into the hole. It can't do anything now. Game over.
        inHole = true;
        renderer.color = new Color(0f, 0f, 0f, 1f);
    }

    public void set_speed(float s) {speed = s;}
}
