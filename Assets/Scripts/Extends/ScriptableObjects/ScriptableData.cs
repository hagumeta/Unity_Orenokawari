using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extends.ScriptableDatas
{
    [CreateAssetMenu(fileName ="ScriptableData", menuName ="ScriptableDatas/default")]
    abstract public class ScriptableData : ScriptableObject
    {
        public string Id { get; private set; }

        public ScriptableData()
        {
            this.Id = System.Guid.NewGuid().ToString("N");
        }
    }
}
