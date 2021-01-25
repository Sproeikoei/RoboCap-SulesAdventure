using UnityEngine;

public class LightChanger : MonoBehaviour
{
    public Light light;

    Color defaultColor;
    // Start is called before the first frame update
    void Start()
    {
        defaultColor = light.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(!ToolAnimation.attacking)
        {
            light.color = Color.red + Color.yellow;
        }
        else
        {
            light.color = defaultColor;
        }
    }
}
