using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFeeder : MonoBehaviour
{
    enum type { gold,iron,copper}
    [SerializeField] private type typeOfOre;
    [SerializeField] private PortalProperty portalProperty;

    public void Feed() {
        switch (typeOfOre) {
            case type.gold:
                portalProperty.FeedGold();
                break;
            case type.iron:
                portalProperty.FeedIron();
                break;
            case type.copper:
                portalProperty.FeedCopper();
                break;
        }
    }
}
