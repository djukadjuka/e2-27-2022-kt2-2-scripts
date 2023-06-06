using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ObjectiveTriggerScripts
{
    public class PlayerMovementTrigger : MonoBehaviour
    {
        [HideInInspector] public bool PlayerMovedInto = false;
        [HideInInspector] public bool PlayerMovedOut = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerMovedInto = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerMovedOut = true;
            }
        }
    }
}
