using UnityEngine;

public enum FishFamily { Loner, School, Shark, Shellfish };

[CreateAssetMenu(fileName = "New Fish", menuName = "Sea Creatures/Fish")]
public class FishPropterties : ScriptableObject {

    private FishFamily family = FishFamily.Loner;

    [SerializeField]
    private string type;
    [SerializeField]
    private string description;
    [SerializeField]
    private float swimSpeed;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private Mesh model;

    public string Type { get { return type; } }
    public string Description { get { return description; } }
    public float SwimSpeed { get { return swimSpeed; }  }
    public float TurnSpeed { get { return turnSpeed; } }
    public FishFamily Family { get { return family; } }
    public Mesh Model { get { return model; } }
    
    public void Print() {
        Debug.Log("Fish type " + type);
        Debug.Log("Fish desciption " + description);
        Debug.Log("Fish speed " + swimSpeed);
        Debug.Log("Fish turn speed " + turnSpeed);
    }    
}
