using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Piece : MonoBehaviour,IDragHandler, IEndDragHandler,IBeginDragHandler
{
    [Tooltip("Define se vai ser uma rotação no eixo horario ou anti horario")] [Range(-1,1)]
    [SerializeField]int _directionRotation;
    [Tooltip("Define velocidade de rotação")]
    [SerializeField] float velRotation;
    [SerializeField] Transform selectedPieceTransform;
    public Slot slotStoragePiece;
    public static GameObject itemDrag;
    Vector3 lastInventoryPosition;
    Transform lastSlotParent;
    SlotsController gameController;
    CanvasGroup canvasGroup;
    bool chageEffective;
    public int DirectionRotation
    {
        set { _directionRotation = value;}
    }
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SlotsController>();
        lastInventoryPosition = this.transform.position;
        lastSlotParent = this.transform.parent;
        slotStoragePiece = transform.GetComponentInParent<Slot>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(this.transform.rotation.x, this.transform.rotation.y, _directionRotation * velRotation * Time.deltaTime));
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemDrag = this.gameObject;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(selectedPieceTransform);
        chageEffective = false;
        _directionRotation = 0;
        if (slotStoragePiece.TypeSlot.ToString() == Slot.TypeSlotEnum.Machine.ToString())
        {
            gameController.DesocupySlot(slotStoragePiece.NumSlot);
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        itemDrag = null;
        canvasGroup.blocksRaycasts = true;
        if (!chageEffective)
        {
            transform.position = lastInventoryPosition;
            SetParent(this.gameObject, slotStoragePiece);
        }
    }

    public void SetParent(GameObject objeto, Slot slot)
    {
        lastInventoryPosition = slot.gameObject.transform.position;
        lastSlotParent = slot.gameObject.transform;
        this.gameObject.transform.SetParent(slot.gameObject.transform);
        gameObject.GetComponent<RectTransform>().anchoredPosition = slot.gameObject.GetComponent<RectTransform>().anchoredPosition;
        slotStoragePiece = slot;
        if (slotStoragePiece.TypeSlot.ToString() == Slot.TypeSlotEnum.Machine.ToString())
        {
            gameController.OcupySlot(this.gameObject, slot.NumSlot);
        }

        chageEffective = true;
    }
}
