using UnityEngine;

[CreateAssetMenu(menuName = "Sea Creatures/School Fish")]
public class SchoolFishPropterties : FishPropterties {

    private FishFamily family = FishFamily.School;

    [SerializeField]
    private int minAmount;
    [SerializeField]
    private int maxAmount;

    public int MinAmount { get { return minAmount; } }
    public int MaxAmount { get { return maxAmount; } }
        
}
