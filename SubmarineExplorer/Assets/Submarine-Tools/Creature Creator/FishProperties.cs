using UnityEngine;
//using UnityEditor;

public enum FishFamily { Loner, School, Shark, Shellfish };

public class FishPropterties : ScriptableObject {
    
    [SerializeField]
    private FishFamily  family = FishFamily.Loner;
    [SerializeField]
    private string      type;
    [SerializeField]
    private string      description;
    [SerializeField]
    private float       swimSpeed;
    [SerializeField]
    private float       turnSpeed;
    [SerializeField]
    private int         minAmount = 1;
    [SerializeField]
    private int         maxAmount = 1;
    [SerializeField][Range(2.0f, 50.0f)]
    private float       positionInterval = 2.0f;
    [SerializeField]
    private GameObject  fishPrefab;
    [SerializeField]
    private Color       swimAreaColor = new Color(1, 0, 0, 0.5f);
    [SerializeField]
    private Color       targetColor = new Color(0, 1, 0, 1);
    [SerializeField]
    private bool        generateQuest = true;

    public string       Type            { get { return type;                } }
    public string       Description     { get { return description;         } }
    public float        SwimSpeed       { get { return swimSpeed;           } }
    public float        TurnSpeed       { get { return turnSpeed;           } }
    public FishFamily   Family          { get { return family;              } }
    public int          MaxAmount       { get { return maxAmount;           } }
    public int          MinAmount       { get { return minAmount;           } }
    public float        ReactionTime    { get { return positionInterval;    } }
    public GameObject   FishPrefab      { get { return fishPrefab;          } }
    public Color        SwimAreaColor   { get { return swimAreaColor;       } }
    public Color        TargetColor     { get { return targetColor;         } }
    public bool         GenerateQuest   { get { return generateQuest;       } }
    
    void OnInspectorGUI() {
        
    }
    
    public void Print() {
        Debug.Log("Fish type " + type);
        Debug.Log("Fish desciption " + description);
        Debug.Log("Fish speed " + swimSpeed);
        Debug.Log("Fish turn speed " + turnSpeed);
    }
}
