using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


namespace BoxScripts
{
    public struct MovGroup
    {
        public Transform t;
        public DataPackage data1;
        public DataPackage data2;
        public Mov mov;

        public MovGroup (Transform t, DataPackage data1, DataPackage data2, Mov mov)
        {
            this.t = t;
            this.data1 = data1;
            this.data2 = data2;
            this.mov = mov;
        }
    }
    public class Mov : MonoBehaviour {
        public bool isExecuting = false;

        public float Speed = 1f;
        public bool rotate = false;
        public bool scalate = false;

        public Mov(float sp = 1f, bool rot = false, bool sc = false)
        {
            rotate = rot; scalate = sc;
            Speed = sp;
        }
        
        public IEnumerator Execute(Transform target, DataPackage pac1, DataPackage pac2)
        {
            isExecuting = true;

            float timer = 0f;
            bool isAtDest = false;

            while(!isAtDest)
            {
                if(timer < 1f)
                    timer += Time.deltaTime * Speed;
                else 
                {
                    isAtDest = true;
                    timer = 1f;
                }

                if(rotate) target.eulerAngles = pac1.LerpAngle(pac2, timer);
                if(scalate) target.localScale = pac1.LerpScale(pac2, timer);
                target.position = pac1.LerpDistance(pac2, timer);
                yield return null;
            }

            yield return null;
            isExecuting = false;
        }

    }
}
