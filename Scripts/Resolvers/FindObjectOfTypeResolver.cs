using Syrinj.Attributes;
using Syrinj.Injection;
using UnityEngine;

namespace Syrinj.Resolvers
{
    public class FindObjectOfTypeResolver : IResolver
    {
        public object Resolve(Injectable injectable)
        {
            var attribute = (FindObjectOfTypeAttribute)injectable.Attribute;
            if (attribute.ComponentType == null)
            {
#if UNITY_5 
                return Object.FindObjectOfType(injectable.Type);
#else
                return Object.FindFirstObjectByType(injectable.Type, FindObjectsInactive.Exclude);
#endif                
            }
            // else
            // {
#if UNITY_5 
                return Object.FindObjectOfType(attribute.ComponentType);
#else
                return Object.FindFirstObjectByType(attribute.ComponentType, FindObjectsInactive.Exclude);
#endif                 
            // }
        }
    }
}