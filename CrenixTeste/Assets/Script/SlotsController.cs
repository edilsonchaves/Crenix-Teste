using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SlotsController : MonoBehaviour
{
    //Script com a função de controlar os 5 slots presentes no jogo;
    [Tooltip("Status das 5 posições no Slot Machine")]public SlotMachineSpace[] slotsMachine = new SlotMachineSpace[5];
    [Tooltip("Script Controlador Mascote")]public AnimalController animalController;
    [Tooltip("Game Objects dos Slots do Inventorio")] public GameObject[] positionsInventorySlot;
    private void Update()
    {
        animalController.ChangeTextAnimals(allSlotsBusy());

    }

    public bool allSlotsBusy()
    {
        foreach (SlotMachineSpace slot in slotsMachine)
        {
            if(!slot.IsBusy)
            return false;
        }
        return true;
    }
    public void ResetAllSlots()
    {
        foreach (SlotMachineSpace slot in slotsMachine)
        {
            if (slot.ObjetoSlot != null)
            {
                for(int i = 0; i< positionsInventorySlot.Length; i++)
                {
                    if (positionsInventorySlot[i].transform.childCount == 0)
                    {
                        slot.ObjetoSlot.GetComponent<Piece>().SetSlot(slot.ObjetoSlot, positionsInventorySlot[positionsInventorySlot[i].GetComponent<Slot>().NumSlot].GetComponent<Slot>());
                        i = positionsInventorySlot.Length;
                    }
                }
            }
        }
        foreach (SlotMachineSpace slot in slotsMachine)
        {
            slot.IsBusy = false;
        }
        RotationPieces(allSlotsBusy());
        foreach (SlotMachineSpace slot in slotsMachine)
        {
            //peça precisa retornar para o seu lugar de origem
            slot.ObjetoSlot = null;
        }
    }
    public void OcupySlot(GameObject piece,int numPosition)
    {
        slotsMachine[numPosition].ObjetoSlot = piece;
        slotsMachine[numPosition].IsBusy = true;
        RotationPieces(allSlotsBusy());
    }
    public void DesocupySlot(int numPosition)
    {
        slotsMachine[numPosition].ObjetoSlot = null;
        slotsMachine[numPosition].IsBusy = false;
        RotationPieces(allSlotsBusy());
    }
    public void RotationPieces(bool isAllSlotBusy)
    {
        int value=0;
       foreach (SlotMachineSpace slot in slotsMachine)
       {
            if (isAllSlotBusy)
            {
                if (value % 2 ==0)
                {
                    slot.ObjetoSlot.GetComponent<Piece>().DirectionRotation = -1;
                }
                else
                {
                    slot.ObjetoSlot.GetComponent<Piece>().DirectionRotation = 1;
                }
                value++;
            }
            else
            {
                if (slot.ObjetoSlot!= null)
                {
                    slot.ObjetoSlot.GetComponent<Piece>().DirectionRotation = 0;
                }
            }
            
       }
    }

    [System.Serializable]
    public class SlotMachineSpace
    {
        [SerializeField]GameObject _objetoSlot;
        [SerializeField] bool _isBusy;

        public GameObject ObjetoSlot
        {
            get { return _objetoSlot; }
            set { _objetoSlot = value; }
        }
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; }
        }

        public SlotMachineSpace()
        {
            _objetoSlot = null;
            _isBusy = false;
        }
    }
}
