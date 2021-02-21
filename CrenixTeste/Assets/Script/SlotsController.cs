using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsController : MonoBehaviour
{
    //Script com a função de controlar os 5 slots presentes no jogo;
    public Slot[] slots = new Slot[5];
    public AnimalController animalController;

    private void Update()
    {
        Debug.Log(gameObject.name);
        animalController.ChangeTextAnimals(allSlotsBusy());
        Debug.Log("Taquepariu");
    }

    public bool allSlotsBusy()
    {
        Debug.Log("OLA");
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
            //peça precisa retornar para o seu lugar de origem
            slot.ObjetoSlot = null;
            slot.IsBusy = false;
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
