using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    [SerializeField] string selectionID;
    [SerializeField] SpriteRenderer rabbitRenderer;
    [SerializeField] SpriteRenderer selectionRenderer;

    private float speed;
    private float movingTime;  // how long it will keep moving to the current dest
    private Vector3 dest;  // the location it is currently moving to
    private bool isHidden = false;  // True when hiding in a grove
    private bool isCaught = false;  // True when caught by the fox
    private bool isSelected = false;  // True when the player is controlling this rabbit in particular

    [SerializeField] CharacterManager characters;

    // Update is called once per frame
    void Update()
    {
        // If caught, can never act again.
        if (isCaught) return;

        // get mouse click location (where the rabbit should move towards)
        if (isSelected && Input.GetMouseButton(0))
        {
            dest = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dest.z = transform.position.z;
            movingTime = 1f;
        }

        // move the rabbit towards the target position
        if (movingTime > 0.02f)
        {
            transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
            movingTime -= Time.deltaTime;
            selectionRenderer.color = new Color(1f, 1f, 0f, 1f);  // Yellow when moving
        }
        else
        {
            selectionRenderer.color = new Color(1f, 1f, 1f, 1f);  // color back to white when stationary
        }
    }

    // Setters
    public void set_isHidden(bool b) {isHidden = b;}
    public void set_isCaught() 
    {
        isCaught = true;
        set_isHidden(true);  // stops fox from going after caught rabbits
        rabbitRenderer.color = new Color(0f, 0f, 0f, 1f);
        selectionRenderer.enabled = false;
    }
    public void set_selection(bool b) {isSelected = b; selectionRenderer.enabled = b;}
    public void set_speed(float s) {speed = s;}

    // Getters
    public bool get_isHidden() {return isHidden;}
    public bool get_isCaught() {return isCaught;}
    public string get_selectionID() {return selectionID;}

}
