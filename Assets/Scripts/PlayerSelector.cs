using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSelector : MonoBehaviour
{
    [SerializeField] int InputIndex = 1;
    [SerializeField] int scarecrowIndex = 0;

    [SerializeField] int partIndex = 0;
    Transform partSelected;
    Scarecrow ScarecrowSelected;

    bool inputReset = true; // have to set dir/pad back to 0 before moving again. This is weird and I'm sorry.

    GameManager gameManager;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = Utility.GameManager;

        ScarecrowSelected = gameManager.scarecrows[scarecrowIndex];
        UpdateSelection();
    }

    public void Init(Player p)
    {
        player = p;
    }

    // Update is called once per frame
    void Update()
    {
        bool madeInput = false;

        float horizontal = Input.GetAxis("Horizontal" + InputIndex.ToString());
        float vertical = Input.GetAxis("Vertical" + InputIndex.ToString());

        if (Mathf.Abs(horizontal) > 0)
        {
            //Debug.Log("Player " + InputIndex.ToString() + " Moved horizontal! " + horizontal.ToString());
            if (inputReset) ChangeScarecrow(horizontal);
            inputReset = false;
            madeInput = true;
        }
        if (Mathf.Abs(vertical) > 0)
        {
            //Debug.Log("Player " + InputIndex.ToString() + " Moved vertically! " + vertical.ToString());
            if (inputReset) ChangeBodyPart(vertical);
            inputReset = false;
            madeInput = true;
        }

        if (Input.GetButtonDown("A"+ InputIndex.ToString()))
        {
            RepairScarecrow();
            inputReset = false;
            madeInput = true;
        }

        if (!madeInput)
        {
            inputReset = true;
        } else {
            inputReset = false;
        }
    }

    private void RepairScarecrow()
    {
        ScarecrowPart[] parts = ScarecrowSelected.GetScarecrowPartTransforms();
        player.RepairPart(ScarecrowSelected, parts[partIndex]);
    }

    private void ChangeScarecrow(float horizontal)
    {
        Scarecrow[] scarecrows = gameManager.scarecrows;
        scarecrowIndex = horizontal > 0 ? (scarecrowIndex + 1) % scarecrows.Length : scarecrowIndex == 0 ? scarecrows.Length-1 : scarecrowIndex - 1;
        ScarecrowSelected = scarecrows[scarecrowIndex];
        UpdateSelection();
    }

    private void ChangeBodyPart(float vertical)
    {
        ScarecrowPart[] parts = ScarecrowSelected.GetScarecrowPartTransforms();
        partIndex = vertical < 0 ? (partIndex + 1) % parts.Length : partIndex == 0 ? parts.Length-1 : partIndex - 1;
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        ScarecrowPart[] parts = ScarecrowSelected.GetScarecrowPartTransforms();
        partSelected = parts[partIndex].transform;

        Vector3 newPos = new Vector3(partSelected.position.x, partSelected.position.y, transform.position.z);
        transform.position = newPos;
    }
}
