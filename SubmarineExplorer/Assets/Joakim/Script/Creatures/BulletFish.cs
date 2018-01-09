using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFish : GenericCreature {

        private string type = "BulletFish";
        private string description = "We've heard rumors about a fish that looks kind of like a bullet. Be careful so you don't get shot!";

        public override string ReturnType()
        {
            return type;
        }

        public override string GetDescription()
        {
            return description;
        }
    }

