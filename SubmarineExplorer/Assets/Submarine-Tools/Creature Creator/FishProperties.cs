using UnityEngine;
using UnityEditor;

public enum FishFamily { Loner, School, Shark, Shellfish };

[CreateAssetMenu(fileName = "New Fish", menuName = "Sea Creatures/Fish")]
public class FishPropterties : ScriptableObject {
    
    [SerializeField]
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
    [SerializeField]
    private Material material;
    [SerializeField]
    private int minAmount = 1;
    [SerializeField]
    private int maxAmount = 1;
    [SerializeField][Range(2.0f, 50.0f)]
    private float reactionTime = 2.0f;

    public string       Type            { get { return type;            } }
    public string       Description     { get { return description;     } }
    public float        SwimSpeed       { get { return swimSpeed;       } }
    public float        TurnSpeed       { get { return turnSpeed;       } }
    public FishFamily   Family          { get { return family;          } }
    public Mesh         Model           { get { return model;           } }
    public Material FishMaterial        { get { return material;        } }
    public int          MaxAmount       { get { return maxAmount;       } }
    public int          MinAmount       { get { return minAmount;       } }
    public float        ReactionTime    { get { return reactionTime;    } }
    
    void OnInspectorGUI() {
        
    }
    
    public void Print() {
        Debug.Log("Fish type " + type);
        Debug.Log("Fish desciption " + description);
        Debug.Log("Fish speed " + swimSpeed);
        Debug.Log("Fish turn speed " + turnSpeed);
    }
}
