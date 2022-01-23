using UnityEngine;

namespace BoxScripts
{
    public class DataPackage
    {
        public Vector3 position;
        public Vector3 euler;
        public Vector3 scale;

        public DataPackage (DataPackage pac)
        {
            position = pac.position;
            euler = pac.euler;
            scale = pac.scale;
        }
        public DataPackage (Transform copy)
        {
            position = copy.position;
            euler = copy.eulerAngles;
            scale = copy.localScale;
        }
        public DataPackage (Vector3 pos)
        {
            position = pos;
            euler = Vector3.zero;
            scale = Vector3.one;
        }
        public DataPackage (Vector3 pos, Vector3 angle)
        {
            position = pos;
            euler = angle;
            scale = Vector3.one;
        }
        public DataPackage (Vector3 pos, Vector3 angle, Vector3 sc)
        {
            position = pos;
            euler = angle;
            scale = sc;
        }

        public Vector3 LerpDistance(DataPackage pac, float timer = 0.5f)
        {
            return Vector3.Lerp(position, pac.position, timer);
        }
        public Vector3 LerpAngle(DataPackage pac, float timer = 0.5f)
        {
             return new Vector3(
                Mathf.LerpAngle(euler.x, pac.euler.x, timer),
                Mathf.LerpAngle(euler.y, pac.euler.y, timer),
                Mathf.LerpAngle(euler.z, pac.euler.z, timer));
        }
        public Vector3 LerpScale(DataPackage pac, float timer = 0.5f)
        {
            return Vector3.Lerp(scale, pac.scale, timer);
        }
        public DataPackage Swap(DataPackage pac)
        {
            DataPackage temp = new DataPackage(this);
            position = pac.position; euler = pac.euler; scale = pac.scale;
            return temp;
        }
    }
}