using UnityEngine;
using System.Collections;

[System.Serializable]
public class AttackBase {

    public string name;
    public int power;
    public int spCost = 0;
    public bool healSP = false;
    public enum attackRange { single, all };
    public attackRange attkRng = attackRange.single;
    public enum attackType { attack, heal, magic, skill, other };
    public attackType attkType;
    public enum attackElement { normal, fire, ice, electric, shadow, light}
    public attackElement attkElement = attackElement.normal;
}
