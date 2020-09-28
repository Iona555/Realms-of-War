using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    // Combat Stats
    [SerializeField] private int baseHealthPoints = 1000;
    [Range(0, 150)] [SerializeField] private int baseArmor = 30;              // Each point of armor amounts to a 0.75% physical damage reduction, capping at 150
    [Range(0, 150)] [SerializeField] private int baseMagicResist = 30;        // Each point of magic resist amounts to a 0.75% magic damage reduction, capping at 150
    [SerializeField] private int basePhysicalDamage = 100;
    [SerializeField] private int baseMagicDamage = 100;
    [SerializeField] private int baseTrueDamage = 0;
    [Range(0, 100)] [SerializeField] private int baseCriticalChance = 25;     // Each point of critical chance amounts to 1% chance for an attack to be a critical strike, capping at 100
    [SerializeField] private int baseCriticalRate = 50;                       // Each point of critical rate amounts to 1% damage increase for critical strikes compared to normal ones

    // Current Stats
    int currentHealthPoints;
    int currentArmor;
    int currentMagicResist;
    int currentPhysicalDamage;
    int currentMagicDamage;
    int currentTrueDamage;
    int currentCriticalChance;
    int currentCriticalRate;

    // Buffs
    // Can be implemented into a bitmask
    public bool buffHealth;                 // Add ?% to baseHealthPoints cap, and instantly add that difference to the currentHealthPoints
    public bool buffArmor;                  // Add ? to currentArmor
    public bool buffMagicResist;            // Add ? to currentMagicResist
    public bool buffPhysicalDamage;         // Add ?% to currentPhysicalDamage
    public bool buffMagicDamage;            // Add ?% to currentMagicDamage
    public bool buffTrueDamage;             // % of the damage dealt becomes true damage
    public bool buffCriticalChance;         // Add ? to currentCriticalChance
    public bool buffCriticalRate;           // Add ? to currentCriticalRate

    // Other Stats
    public bool alive;
    public int weight;

    // Floating Text Feature
    public GameObject FloatingTextPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Set up all the Combat Stats
        currentHealthPoints = baseHealthPoints;
        currentArmor = baseArmor;
        currentMagicResist = baseMagicResist;
        currentPhysicalDamage = basePhysicalDamage;
        currentMagicDamage = baseMagicDamage;
        currentTrueDamage = baseTrueDamage;
        currentCriticalChance = baseCriticalChance;
        currentCriticalRate = baseCriticalRate;

        // Set up all the Buffs
        buffHealth = false;
        buffArmor = false;
        buffMagicResist = false;
        buffPhysicalDamage = false;
        buffMagicDamage = false;
        buffTrueDamage = false;
        buffCriticalChance = false;
        buffCriticalRate = false;

        // Set up all the Other Stats
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReceiveDamage(int receivedPhysicalDamage, int receivedMagicDamage, int receivedTrueDamage)
    {
        // Calculating the total of received damage
        int receivedTotalDamage = (int)(receivedPhysicalDamage * (100 - (0.75 * currentArmor)))
                                + (int)(receivedMagicDamage * (100 - (0.75 * currentMagicResist)))
                                + receivedTrueDamage;
        // Currently showing the text color related only to the total damage
        // Text color related to damage type to be implemented
        if (FloatingTextPrefab)
        {
            float gcg = 2.55f * receivedTotalDamage;
            if (gcg > 255.0f)
                gcg = 255.0f;
            var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
            go.GetComponent<TextMesh>().color = new Color(255.0f / 255.0f, gcg / 255.0f, 0.0f / 255.0f);
            go.GetComponent<TextMesh>().text = ("-" + receivedTotalDamage.ToString());
        }

        // The Character takes damage
        if (currentHealthPoints > receivedTotalDamage)
        {
            currentHealthPoints -= receivedTotalDamage;
        }
        else
        {
            alive = false;
        }
    }

    public void takeHeal(int receivedHeal)
    {
        // The text color for the healing effect is green
        if (FloatingTextPrefab)
        {
            var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
            go.GetComponent<TextMesh>().color = new Color(0.0f / 255.0f, 250.0f / 255.0f, 0.0f / 255.0f);
            go.GetComponent<TextMesh>().text = ("+" + receivedHeal.ToString());
        }

        currentHealthPoints += receivedHeal;
        if (currentHealthPoints > baseHealthPoints)
        {
            currentHealthPoints = baseHealthPoints;
        }
    }

}
