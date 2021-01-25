using UnityEngine;

public class LightChanger : MonoBehaviour
{
    public Light light; 

    Color defaultColor;
    
    void Start()
    {
        defaultColor = light.color;
    }

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
