using UnityEngine;
using CappTools;
using DG.Tweening;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;

        }
        Instance = this;
    }

    private new Camera camera;
    public LegoPiece SelectedPiece;

    public LayerMask DragableLayer;



    private void Start()
    {
        camera = Camera.main;

        Subscribe();



    }
    public void Subscribe()
    {

        TouchManager.Instance.onTouchBegan += OnTouchBegan;
        TouchManager.Instance.onTouchMoved += OnTouchMoved;
        TouchManager.Instance.onTouchEnded += OnTouchEnded;
    }
    public void Unsubscribe()
    {
        TouchManager.Instance.onTouchBegan -= OnTouchBegan;
        TouchManager.Instance.onTouchMoved -= OnTouchMoved;
        TouchManager.Instance.onTouchEnded -= OnTouchEnded;
    }

    public bool isGrabbed;


    public bool isMerging;

    public float OffsetY;
    private void OnTouchBegan(TouchInput touch)
    {
        

        if (isMerging) return;

        Ray ray = camera.ScreenPointToRay(touch.ScreenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, DragableLayer))
        {

            isGrabbed = true;

            SelectedPiece = hit.transform.GetComponent<LegoPiece>();

            //if (SelectedPiece && SelectedPiece.transform.position.y < 0.03f)



            SelectedPiece.rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            // var newPos = SelectedPiece.transform.position.y + 0.1f;
            var newPos = SelectedPiece.HeightY * 2f + OffsetY;
            SelectedPiece.transform.DOMoveY(newPos, 0.15f);

        }

    }

    public float MovementSpeed;
    private void OnTouchMoved(TouchInput touch)
    {
        if (!GamePlayManager.Instance.isGameStarted) return;
        if (!SelectedPiece) return;
        SelectedPiece.isGrabbed = true;
        var posX = -touch.DeltaScreenPosition.x * MovementSpeed;
        var PosZ = -touch.DeltaScreenPosition.y * MovementSpeed;

        SelectedPiece.rb.velocity = posX * Vector3.right + PosZ * Vector3.forward;

    }

   
    private void OnTouchEnded(TouchInput touch)
    {
        if (!SelectedPiece) return;

        if (!isGrabbed) return;


        Ray ray = camera.ScreenPointToRay(touch.ScreenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, DragableLayer) && hit.transform.GetComponent<LegoPiece>().Index == SelectedPiece.Index)
        {
            isGrabbed = false;

            var currontLego = hit.collider.GetComponent<LegoPiece>();

           

            if (currontLego.Index == SelectedPiece.Index && currontLego.gameObject != SelectedPiece.gameObject)
            {

                var newPos = new Vector3(currontLego.transform.position.x, transform.position.y, currontLego.transform.position.z);

                SelectedPiece.rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                SelectedPiece.rb.isKinematic = true;
                isMerging = true;
                SelectedPiece.gameObject.transform.DOMove(newPos, 0.4f).OnComplete(() =>
                {
                    SelectedPiece.rb.isKinematic = false;
                    SelectedPiece.rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

                    LeagoPieceManager.Instance.SpawnNewLegoPieces(currontLego);
                    Destroy(currontLego.gameObject);

                    Destroy(SelectedPiece.gameObject);

                    SelectedPiece = null;
                    isMerging = false;
                });

            }
            else 
            {

                Debug.Log("else if ");
                SelectedPiece.rb.velocity = Vector3.zero;
                SelectedPiece.rb.constraints = RigidbodyConstraints.None;
                SelectedPiece.rb.constraints = RigidbodyConstraints.FreezeRotation;
                SelectedPiece = null;

            }
        }
        
        else
        {
            Debug.Log("else");
            SelectedPiece.rb.velocity = Vector3.zero;
            SelectedPiece.rb.constraints = RigidbodyConstraints.None;
            SelectedPiece.rb.constraints = RigidbodyConstraints.FreezeRotation;
            SelectedPiece = null;

        }
    }
}

