
//using CappTools;
//using UnityEngine;

////Mert kodu

//public class RayCastManager : MonoBehaviour
//{
//    //public RayableType rayableType;
//    public GameObject Shop;

//    [SerializeField] private LayerMask targetLayer;
//    [SerializeField] private LayerMask Draggable;
//    [SerializeField] private LayerMask GroundLayer;

//    private Camera mainCam;

//    private float speed = 15f;
//    private int vipLevel = 3;
//    public float cameraSpeed = 0.02f;

//    private Generator currentGenerator;
//    public Product currentProduct;
//    private ShopManager shopManager;
//    private Product secondProduct;
//    private Node node;
//    private GeneratorLevelButton generatorLevelButton;
//    private GeneratorUpgradeButton generatorUpgradeButton;
//    private Renderer shopRenderer;
//    public static RayCastManager Instance;

//    private void Awake()
//    {
//        if (Instance)
//        {
//            Destroy(this);
//            return;
//        }
//        Instance = this;
//    }

//    void Start()
//    {
//        Subscribe();
//        mainCam = Camera.main;
//    }

//    void Subscribe()
//    {
//        TouchManager.Instance.onTouchBegan += OnTouchBegan;
//        TouchManager.Instance.onTouchMoved += OnTouchMoved;
//        TouchManager.Instance.onTouchEnded += OnTouchEnded;
//    }

//    void Unsubscribe()
//    {
//        TouchManager.Instance.onTouchBegan -= OnTouchBegan;
//        TouchManager.Instance.onTouchMoved -= OnTouchMoved;
//        TouchManager.Instance.onTouchEnded -= OnTouchEnded;
//    }

//    private void OnTouchBegan(TouchInput touch)
//    {

//        Ray ray = mainCam.ScreenPointToRay(touch.ScreenPosition);
//        RaycastHit hit;

//        if (Physics.Raycast(ray, out hit, 100f, targetLayer))
//        {
//            hit.transform.GetComponent<IInteractable>().Interacted();
//        }
//    }

//    private void OnTouchMoved(TouchInput touch)
//    {
//        Renderer shopRenderer = Shop.GetComponent<Renderer>();
//        if (currentProduct)
//        {
//            Ray ray = mainCam.ScreenPointToRay(touch.ScreenPosition);
//            RaycastHit hit;

//            if (Physics.Raycast(ray, out hit, 100f, GroundLayer))
//            {
//                var newPos = new Vector3(hit.point.x, hit.point.y + 2f, hit.point.z);
//                currentProduct.Moved(newPos);
//                Debug.DrawLine(ray.origin, hit.point);
//            }
//            if (hit.transform.TryGetComponent<ShopManager>(out shopManager))
//            {
//                Debug.Log("Ã¼zerinde");
//                shopRenderer = shopManager.transform.GetComponent<Renderer>();
//                shopRenderer.material.color = Color.green;
//            }
//        }
//        else
//        {
//            var posX = -touch.DeltaScreenPosition.x * cameraSpeed;
//            var posY = -touch.DeltaScreenPosition.y * cameraSpeed;
//            mainCam.transform.position += Vector3.right * posX + Vector3.forward * posY;

//        }
//    }

//    private void OnTouchEnded(TouchInput touch)
//    {
//        Renderer shopRenderer = Shop.GetComponent<Renderer>();
//        if (currentProduct)
//        {
//            Ray ray = mainCam.ScreenPointToRay(touch.ScreenPosition);
//            RaycastHit hit;

//            if (Physics.Raycast(ray, out hit, 100f, targetLayer))
//            {
//                //  hit.transform.GetComponent<IInteractable>().Released();
//                if (hit.transform.TryGetComponent<Product>(out secondProduct))
//                {
//                    currentProduct.MergingProducts(secondProduct.gameObject);
//                }
//                if (hit.transform.TryGetComponent<ShopManager>(out shopManager))
//                {
//                    Shop.transform.GetComponent<ShopManager>().Shopping(currentProduct);
//                    shopRenderer = shopManager.transform.GetComponent<Renderer>();
//                    shopRenderer.material.color = Color.red;
//                }
//                else if (Vector3.Distance(mainCam.WorldToScreenPoint(currentProduct.transform.position), mainCam.WorldToScreenPoint(Shop.transform.position)) < 100f)
//                {
//                    //Shop.transform.GetComponent<ShopManager>().Shopping(currentProduct);
//                    shopRenderer.material.color = Color.red;
//                }
//                else
//                {
//                    GridManager.Instance.ClosestGridPos(currentProduct);
//                }
//            }
//            else
//            {
//                GridManager.Instance.ClosestGridPos(currentProduct);
//            }

//            //Ray ray = mainCam.ScreenPointToRay(touch.ScreenPosition);
//            //RaycastHit hit;

//            //if (Physics.Raycast(ray, out hit, 100f, Draggable))
//            //{

//            //    if (currentProduct.transform.TryGetComponent<Product>(out secondProduct))
//            //    {
//            //        currentProduct.MergingProducts(secondProduct.gameObject);

//            //    }
//            //}
//            //else if (Vector3.Distance(mainCam.WorldToScreenPoint(currentProduct.transform.position), mainCam.WorldToScreenPoint(Shop.transform.position)) < 100f)
//            //{
//            //    Shop.transform.GetComponent<ShopManager>().Shopping(currentProduct);
//            //    shopRenderer.material.color = Color.red;
//            //}
//            //else
//            //{
//            //    GridManager.Instance.ClosestGridPos(currentProduct);
//            //}

//        }
//        currentProduct = null;
//        currentGenerator = null;
//    }
//}