using UnityEngine;
using UnityEngine.Events;

public class FloatToStringEvent: MonoBehaviour
{
    public UnityEvent<string> OnFloatToString = new UnityEvent<string>();
    public void ConvertToString(float value)
    {
        OnFloatToString.Invoke(value.ToString());
    }
}
