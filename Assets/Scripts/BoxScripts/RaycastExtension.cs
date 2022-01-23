using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

namespace BoxScripts
{
    public static class RaycastExtensions
    {
        public static bool Raycast (Vector3 origin, Vector3 direction, float distance, LayerMask targetLayer, Action<RaycastHit> action)
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(origin, direction, distance, targetLayer);

            if(hits.Length <= 0) return false;

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                action(hit);
            }
            return true;
        }

        public static bool Raycast (Vector3 origin, Vector3 direction, LayerMask targetLayer, Action<RaycastHit> action)
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(origin, direction, 20f, targetLayer);

            if(hits.Length <= 0) return false;

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                action(hit);
            }
            return true;
        }

        public static RaycastHit? RaycastFirst (Vector3 origin, Vector3 direction, float distance, LayerMask targetLayer)
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(origin, direction, distance, targetLayer);

            if(hits.Length <= 0) return null;
            return hits[0];
        }

        public static RaycastHit? RaycastFirst (Vector3 origin, Vector3 direction, LayerMask targetLayer)
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(origin, direction, 20f, targetLayer);

            if(hits.Length <= 0) return null;
            return hits[0];
        }
    }
}