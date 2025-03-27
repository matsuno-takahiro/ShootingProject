using UnityEngine;

namespace MapAndRadarSystem
{
    public class MapAndRadarManager : MonoBehaviour
    {
        public Transform Actor;
        public Transform ActorCamera;
        public bool FindCameraAutomatically;
        public static MapAndRadarManager Instance;
        public bool ShowMapButtonOnUI = true;
        public bool ShowRadarOnUI = true;
        public KeyCode KeyCode_Map = KeyCode.M;

        public GameObject ButtonMap;
        public GameObject Panel_Map;
        public GameObject Panel_Radar;
        public GameObject TopDownCameraForMap;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if(FindCameraAutomatically && ActorCamera == null)
            {
                ActorCamera = Camera.main != null ? Camera.main.transform : null;
            }
            Panel_Radar.SetActive(ShowRadarOnUI);
            ButtonMap.SetActive(ShowMapButtonOnUI);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode_Map))
            {
                Click_Show_Map();
            }
        }

        public void Click_Show_Map()
        {
            if(Panel_Map.activeSelf)
            {
                // Let's close it:
                Panel_Map.SetActive(false);
                TopDownCameraForMap.SetActive(false);
                if (ShowRadarOnUI)
                {
                    Panel_Radar.SetActive(true);
                }
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Panel_Map.SetActive(true);
                TopDownCameraForMap.SetActive(true);
                if (ShowRadarOnUI)
                {
                    Panel_Radar.SetActive(false);
                }
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }
    }
}
