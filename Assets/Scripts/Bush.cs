using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    [SerializeField] float hideDistance;  // how close a rabbit needs to be to enter hidden state. Should be same size as circle radius.
    [SerializeField] CharacterManager characters;

    // Update is called once per frame
    void Update()
    {
        // hide rabbits inside bush range with math
        foreach (Rabbit prey in characters.get_preyList())
        {
            float currentRabbitDistance = Vector3.Distance(transform.position, prey.transform.position);
            if (currentRabbitDistance <= hideDistance) prey.set_isHidden(true);
            else prey.set_isHidden(false);
        }

    }
}
