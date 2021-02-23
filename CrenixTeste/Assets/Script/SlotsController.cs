using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SlotsController : MonoBehaviour
{
    //Script com a função de controlar os 5 slots presentes no jogo;
    public Slot[] slots = new Slot[5];
    public AnimalController animalController;



    public Transform[] positionsSlot;
    private void Update()
    {
        animalController.ChangeTextAnimals(allSlotsBusy());

    }

    public bool allSlotsBusy()
    {
        foreach (Slot slot in slots)
        {
            if(!slot.IsBusy)
            return false;
        }
        return true;
    }
    public void ResetAllSlots()
    {
        foreach (Slot slot in slots)
        {
            slot.IsBusy = false;
        }
        RotationPieces(allSlotsBusy());
        foreach (Slot slot in slots)
        {
            //peça precisa retornar para o seu lugar de origem
            slot.ObjetoSlot = null;
        }
    }
    public void OcupySlot(GameObject piece,int numPosition)
    {
        slots[numPosition].ObjetoSlot = piece;
        slots[numPosition].IsBusy = true;
        RotationPieces(allSlotsBusy());
    }
    public void DesocupySlot(int numPosition)
    {
        slots[numPosition].ObjetoSlot = null;
        slots[numPosition].IsBusy = false;
        RotationPieces(allSlotsBusy());
    }
    public void RotationPieces(bool isAllSlotBusy)
    {
        int value=0;
       foreach (Slot slot in slots)
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
    public class Slot
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

        public Slot()
        {
            _objetoSlot = null;
            _isBusy = false;
        }
    }
}
