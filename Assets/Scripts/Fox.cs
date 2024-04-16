using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float sightDistance;  // how close a target needs to be to begin chase. Should be same size as circle radius.
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] int collisionID;
    [SerializeField] Rabbit[] preyList;

    private bool isMoving = false;  // stops from chasing a new target until movingTime elapses.
    private float movingTime;  // how long it will keep chasing the current target
    private Vector3 dest;  // the target it is currently chasing

    // Update is called once per frame
    void Update()
    {
        float minRabbitDistance = sightDistance + 1f;
        foreach (Rabbit prey in preyList)
        {
            if (prey.get_isHidden()) continue;

            Vector3 currentRabbit = prey.transform.position;
            float currentRabbitDistance = Vector3.Distance(transform.position, currentRabbit);
            if (sightDistance > currentRabbitDistance && currentRabbitDistance < minRabbitDistance)
            {
                minRabbitDistance = currentRabbitDistance;
                dest = currentRabbit;
                dest.z = transform.position.z;
                movingTime = 1.0f;
            }
        
        }
        
        // move the character towards the target position
        if (movingTime > 0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
            movingTime -= Time.deltaTime;
        }
    }
}
