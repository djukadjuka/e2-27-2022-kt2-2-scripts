using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class PlainItemData
    {
        /// Weight of the item in kilograms
        public float Weight { get; set; }

        /// Dimensions of the item in x=Width and y=Height
        public Vector2 Dimensions { get; set; }

        /// Name of the item to display
        public string Name { get; set; }
        
        [SerializeField]
        public IconAttribute Icon { get; set; }
    }
}
