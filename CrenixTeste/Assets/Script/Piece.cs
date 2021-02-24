using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Piece : MonoBehaviour,IDragHandler, IEndDragHandler,IBeginDragHandler
{
    [Tooltip("Define se vai ser uma rotação no eixo horario ou anti horario")] [Range(-1,1)]
    [SerializeField]int _directionRotation;
    [Tooltip("Slot em que a peça esta Armazenada")]
    [SerializeField] Slot _slotStoragePiece;
    [Tooltip("Define velocidade de rotação")]
    [SerializeField] float velRotation;
    [Tooltip("Transform para peça que foi selecionada pelo usuario")]
    [SerializeField] Transform selectedPieceTransform;
    public static GameObject itemDrag;
    Transform lastSlotParent;
    Vector3 lastInventoryPosition;
    SlotsController gameController;
    CanvasGroup canvasGroup;
    bool chageEffective;

    public int DirectionRotation
    {
        set { _directionRotation = value;}
    }

    public Slot SlotStoragePiece 
    {
        get
        {
            return _slotStoragePiece;
        }
        set
        {
            _slotStoragePiece = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SlotsController>();
        lastInventoryPosition = this.transform.position;
        lastSlotParent = this.transform.parent;
        _slotStoragePiece = transform.GetComponentInParent<Slot>();
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
        if (_slotStoragePiece.TypeSlot.ToString() == Slot.TypeSlotEnum.Machine.ToString())
        {
            gameController.DesocupySlot(_slotStoragePiece.NumSlot);
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
            SetSlot(this.gameObject, _slotStoragePiece);
        }
    }
    public void SetSlot(GameObject objeto, Slot slot)
    {
        lastInventoryPosition = slot.gameObject.transform.position;
        lastSlotParent = slot.gameObject.transform;
        this.gameObject.transform.SetParent(slot.gameObject.transform);
        gameObject.GetComponent<RectTransform>().anchoredPosition = slot.gameObject.GetComponent<RectTransform>().anchoredPosition;
        _slotStoragePiece = slot;
        if (_slotStoragePiece.TypeSlot.ToString() == Slot.TypeSlotEnum.Machine.ToString())
        {
            gameController.OcupySlot(this.gameObject, slot.NumSlot);
        }

        chageEffective = true;
    }
}
