using UnityEngine;
using BoxScripts;

[CreateAssetMenu]
public class GameControllerObject : ScriptableObject {
    public GameState state = GameState.LOADGAME;
    public Camera camera;
    public bool requireFocus = true;
    public bool justChangedState = false;
    public string playerTargetTag = "";
    public Vector3 playerTargetPosition = Vector3.zero;

    public bool targetAllLayers = false;
    public bool isInPuzzle = false;

    private GameState prevState = GameState.PLAYING;

    public bool hasGoodEnd = false;

    public bool isFirstFloor = true;

    public string lang = "ES";

    public void ChangeState(GameState gs)
    {
        justChangedState = true;
        Debug.Log("[GCO] Requesting state change to " + gs + " and just changed? " + justChangedState);
        if(gs == GameState.OPENNOTEBOOK) prevState = state;
        state = gs;
    }

    public void ReturnToPreviousState()
    {
        Debug.Log("[GCO] Returning to " + prevState);
        justChangedState = true;
        state = prevState;
    }
}