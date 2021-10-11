using UnityEngine;

public class Lantern : MonoBehaviour {
    public int reqIdUV;
    [SerializeField] private bool reqIdUVBool = false;
    public ControllerData playerController;
    private Light lanternLight;
    [SerializeField] private bool isLanternActive;
    [SerializeField] private float lanternInputCd = 0f;
    [SerializeField] private LayerMask UVLayer;
    [SerializeField] private LayerMask NotUVLayer;

    [SerializeField] private Color MainLanternColor;
    [SerializeField] private Color UVLanternColor;

    // [FMODUnity.EventRef]
    public string eventoSound = "event:/candado";
    private void Start() {
        
    }

    /*void OnEnable()
    {
        if(!GameController.current.database.GetProgressionState(2))
            gameObject.SetActive(false);
            
        lanternLight = GetComponent<Light>();
        if(GameController.current) UpdateChecks();
    }

    public void UpdateChecks()
    {
        reqIdUVBool = GameController.current.database.GetProgressionState(reqIdUV);
    }

    void Update() 
    {
        UpdateChecks();
        
        if (reqIdUVBool)
        {
            if(lanternInputCd > 0f) lanternInputCd -= Time.deltaTime;

            if(playerController.isLanternPressed &&
                lanternInputCd <= 0f)
            {
                TurnOnUV(isLanternActive);
            }

            if(playerController.isLanternPressed &&
                isLanternActive &&
                lanternInputCd <= 0f)
            {
                TurnOff();
            }
        }
    }

    private void TurnOnUV(bool isLanternOn = false)
    {   
        if(isLanternOn) {
            TurnOff();
            return;
        }
        
        lanternLight.color = UVLanternColor;
        Camera.main.cullingMask = ~(1 << UVLayer);
        lanternLight.cullingMask = ~(1 << UVLayer);
        isLanternActive = true;
        lanternInputCd = 1f;
        GameController.current.music.playMusic(eventoSound);
    }

    private void TurnOff() 
    {
        isLanternActive = false;
        lanternLight.color = MainLanternColor;
        Camera.main.cullingMask = NotUVLayer;
        lanternLight.cullingMask =  -1;
        lanternInputCd = 1f;
        GameController.current.music.StopMusic(eventoSound);
    }*/
}