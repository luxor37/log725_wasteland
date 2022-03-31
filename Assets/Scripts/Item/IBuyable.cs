using System.Collections;
using UnityEngine;

namespace Item
{
    public interface IBuyable
    {
        void OnTriggerEnter(Collider other);
        void OnTriggerExit(Collider other);
    }
}