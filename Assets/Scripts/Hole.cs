using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] float fallDistance;  // how close a character needs to be to fall in. Should be same size as circle radius.
    [SerializeField] CharacterManager characters;

    // Update is called once per frame
    void Update()
    {
        // check if rabbits fell into hole
        foreach (Rabbit prey in characters.get_preyList())
        {
            float currentRabbitDistance = Vector3.Distance(transform.position, prey.transform.position);
            if (currentRabbitDistance <= fallDistance) prey.set_isCaught();
        }

        // check if fox fell into hole
        float currentFoxDistance = Vector3.Distance(transform.position, characters.get_fox().gameObject.transform.position);
        if (currentFoxDistance <= fallDistance) characters.get_fox().fellInHole();

    }
}
