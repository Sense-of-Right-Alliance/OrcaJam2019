using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    [SerializeField] int inputIndex = 1;
    [SerializeField] int partIndex = 0;

    private Transform _partSelected;
    private Scarecrow _scarecrowSelected;

    private bool _inputReset = true; // have to set dir/pad back to 0 before moving again. This is weird and I'm sorry.

    private ScarecrowManager _scarecrowManager;
    private Player _player;

    private void Start()
    {
        _scarecrowManager = Utility.ScarecrowManager;

        _scarecrowSelected = _scarecrowManager.ScarecrowsLeftToRight.First();
        UpdateSelection();
    }

    private void Update()
    {
        bool madeInput = false;

        float horizontal = Input.GetAxis("Horizontal" + inputIndex);
        float vertical = Input.GetAxis("Vertical" + inputIndex);

        if (Mathf.Abs(horizontal) > 0)
        {
            //Debug.Log("Player " + InputIndex.ToString() + " Moved horizontal! " + horizontal.ToString());
            if (_inputReset) SelectNextScarecrow(horizontal > 0);
            _inputReset = false;
            madeInput = true;
        }
        if (Mathf.Abs(vertical) > 0)
        {
            //Debug.Log("Player " + InputIndex.ToString() + " Moved vertically! " + vertical.ToString());
            if (_inputReset) ChangeBodyPart(vertical);
            _inputReset = false;
            madeInput = true;
        }

        if (Input.GetButtonDown("A" + inputIndex))
        {
            RepairScarecrow();
            _inputReset = false;
            madeInput = true;
        }

        _inputReset = !madeInput;
    }

    public void Init(Player p)
    {
        _player = p;
        inputIndex = p.Id + 1;
    }

    private void RepairScarecrow()
    {
        _player.RepairPart(_scarecrowSelected, _scarecrowSelected.Parts[partIndex]);
    }

    private void SelectNextScarecrow(bool forward)
    {
        // create queue, ordered depending on whether we are searching forward or backward
        var scarecrows = new Queue<Scarecrow>(forward ? _scarecrowManager.ScarecrowsLeftToRight : _scarecrowManager.ScarecrowsRightToLeft);

        // rotate queue so the current scarecrow is last
        if (scarecrows.Contains(_scarecrowSelected))
        {
            var scarecrow = scarecrows.Dequeue();
            while (scarecrow != _scarecrowSelected)
            {
                scarecrows.Enqueue(scarecrow);
                scarecrow = scarecrows.Dequeue();
            }
            scarecrows.Enqueue(scarecrow);
        }

        // select the next scarecrow
        _scarecrowSelected = scarecrows.Dequeue();

        // if this scarecrow is not intact, keep looking
        while (!_scarecrowSelected.IsIntact && scarecrows.Any())
        {
            _scarecrowSelected = scarecrows.Dequeue();
        }

        UpdateSelection();
    }

    private void ChangeBodyPart(float vertical)
    {
        partIndex = vertical < 0 ? (partIndex + 1) % _scarecrowSelected.Parts.Length : partIndex == 0 ? _scarecrowSelected.Parts.Length - 1 : partIndex - 1;
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        _partSelected = _scarecrowSelected.Parts[partIndex].transform;

        var newPos = new Vector3(_partSelected.position.x, _partSelected.position.y, transform.position.z);
        transform.position = newPos;
    }
}
