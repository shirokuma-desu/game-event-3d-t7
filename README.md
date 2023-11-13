# game-event-3d-t7

//Coding Style:
//public class MyCodeStyle : MonoBehaviour
{
    // Constants: UpperCase SnakeCase
    public const int CONSTANT_FIELD = 56;

    // Properties: PascalCase
    public static MyCodeStyle Instance { get; private set; }

    // Events: Pascal Case
    public event EventHandler OnSomeThingHappened;

    // Fields: camelCase
    private float memberVariable;

    // Function Names: PascalCase
    private void Awake()
    {
        Instance = this;
        DoSomething(10f);
    }

    // Function Params: camelCase
    private void DoSomething(float time)
    {
        memberVariable = time + time * CONSTANT_FIELD;
        if (OnSomeThingHappened != null)
        {
            //Do some thing
        }
    }
}
