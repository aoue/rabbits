using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private Rabbit[] preyList;
    [SerializeField] private Fox fox;

    [SerializeField] private int rabbitSpeed;
    [SerializeField] private int foxSpeed;

    public Rabbit[] get_preyList() {return preyList;}
    public Fox get_fox() {return fox;}

    private List<string> allSelectionIDs;
    void Start()
    {
        allSelectionIDs = new List<string>();
        foreach (Rabbit prey in get_preyList())
        {
            allSelectionIDs.Add(prey.get_selectionID());
            prey.set_speed(rabbitSpeed);
        }
        fox.set_speed(foxSpeed);
    }

    void Update()
    {
        // set rabbit selection
        foreach (string selectionID in allSelectionIDs)
        {
            if (Input.GetKeyDown(selectionID))
            {
                foreach (Rabbit prey in get_preyList())
                {
                    if (prey.get_selectionID() == selectionID) prey.set_selection(true);
                    else prey.set_selection(false);
                }
                break;
            }
        }
        
        
        
}
}
