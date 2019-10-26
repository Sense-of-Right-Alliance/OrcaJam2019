using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    [SerializeField] int InputIndex = 1;
    [SerializeField] Scarecrow ScarecrowSelected;

    [SerializeField] int partIndex = 0;
    Transform partSelected;

    bool inputReset = true; // have to set dir/pad back to 0 before moving again. This is weird and I'm sorry.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool madeInput = false;

        float horizontal = Input.GetAxis("Horizontal" + InputIndex.ToString());
        float vertical = Input.GetAxis("Vertical" + InputIndex.ToString());

        if (Mathf.Abs(horizontal) > 0)
        {
            Debug.Log("Player " + InputIndex.ToString() + " Moved horizontal! " + horizontal.ToString());
            if (inputReset) ChangeScarecrow(horizontal);
            madeInput = true;
        }
        if (Mathf.Abs(vertical) > 0)
        {
            Debug.Log("Player " + InputIndex.ToString() + " Moved vertically! " + vertical.ToString());
            if (inputReset) ChangeBodyPart(vertical);
            madeInput = true;
        }

        if (Input.GetButtonDown("A"+ InputIndex.ToString()))
        {
            Debug.Log("Player " + InputIndex.ToString() + " 'A' button pressed!");
            madeInput = true;
        }

        if (!madeInput)
        {
            inputReset = true;
        } else {
            inputReset = true;
        }
    }

    private void ChangeScarecrow(float horizontal)
    {
        if (horizontal > 0)
        {

        }
    }

    private void ChangeBodyPart(float vertical)
    {
        Transform[] parts = ScarecrowSelected.GetScarecrowPartTransforms();

        partIndex = vertical > 0 ? (partIndex + 1) % parts.Length : partIndex == 0 ? parts.Length : partIndex - 1;


        //Debug.Log(" part index = " + partIndex);
        partSelected = parts[partIndex];

        Vector3 newPos = new Vector3(partSelected.position.x, partSelected.position.y, transform.position.z);
        transform.position = newPos;
    }
}
