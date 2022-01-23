using UnityEngine;
using BoxScripts;

public class RaycastPlayer : MonoBehaviour {
    public LayerMask TargetLayer;

    public void ExecuteRaycast(Vector3 pos, Vector3 dir, float dist, bool Execute = false)
    {
        RaycastHit? result = RaycastExtensions.RaycastFirst(pos, dir, dist, TargetLayer);

        if(result != null)
        {
            RaycastHit hit = (RaycastHit) result;
            // DBot.SendLog("RaycastPlayer", "RaycastHitting " + hit.transform.name); 

            if(Execute) 
            {
                DBot.SendLog("RaycastPlayer", "Executing " + hit.transform.name); 
                hit.transform.GetComponent<BObject>().Execute();
            }
        }
    }
}