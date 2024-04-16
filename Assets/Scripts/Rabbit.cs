using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] int collisionID;

    private float movingTime;  // how long it will keep moving to the current dest
    private Vector3 dest;  // the location it is currently moving to
    private bool isHidden = false;  // True when hiding in a grove

    // Update is called once per frame
    void Update()
    {
        // get mouse click location (where the rabbit should move towards)
        if (Input.GetMouseButton(0))
        {
            dest = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dest.z = transform.position.z;
            movingTime = 1.5f;
        }

        // move the rabbit towards the target position
        if (movingTime > 0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
            movingTime -= Time.deltaTime;
        }
    }


    public bool get_isHidden() {return isHidden;}

}
