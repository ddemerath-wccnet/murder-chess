using UnityEngine;
using TMPro;
using System.Reflection;
using System;

public class StatVisualizer : MonoBehaviour
{
    public MonoBehaviour myScript;
    public string variableName;
    public TMP_Text text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myScript != null && !string.IsNullOrEmpty(variableName))
        {
            // Get the type of the script
            Type type = myScript.GetType();

            // First, try getting the field (public or private)
            FieldInfo fieldInfo = type.GetField(variableName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (fieldInfo != null)
            {
                // If the field is found, get the value from the instance
                object fieldValue = fieldInfo.GetValue(myScript);
                text.text = fieldValue.ToString();
            }
            else
            {
                // If no field, try getting the property (public or private)
                PropertyInfo propertyInfo = type.GetProperty(variableName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (propertyInfo != null)
                {
                    // If the property is found, get the value from the instance
                    object propertyValue = propertyInfo.GetValue(myScript);
                    text.text = propertyValue.ToString();
                }
                else
                {
                    // If neither is found, display "Variable not found"
                    text.text = "Variable not found!";
                }
            }
        }
    }
}
