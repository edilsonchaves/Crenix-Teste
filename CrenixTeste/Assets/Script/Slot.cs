using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour, IDropHandler
{
    public enum TypeSlotEnum { Inventory, Machine }
    [SerializeField] [Tooltip("Tipo de Slot no jogo")] TypeSlotEnum _typeSlot;
    [SerializeField] [Tooltip("ID posicao Slot")]int _numSlot;

    public int NumSlot
    {
        get { return _numSlot; }
    }
    public TypeSlotEnum TypeSlot
    {
        get { return _typeSlot; }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (this.gameObject.transform.childCount == 0)
        {
            Piece.itemDrag.GetComponent<Piece>().SetSlot(Piece.itemDrag, this);
        }
        else
        {
            Piece.itemDrag.GetComponent<Piece>().SetSlot(Piece.itemDrag.GetComponent<Piece>().gameObject, Piece.itemDrag.GetComponent<Piece>().SlotStoragePiece);
        }
    }
}
