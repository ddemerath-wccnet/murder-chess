using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ElementalPiece : MonoBehaviour
{
    [SubclassSelector(typeof(BaseStatusEffect))]
    public string statusEffect;
    public float duration = 1;
    public int level = 1;

    public void EffectPlayer()
    {
        if (string.IsNullOrEmpty(statusEffect))
        {
            Debug.LogWarning("No status effect type selected.");
            return;
        }

        Type type = Type.GetType(statusEffect);
        if (type == null)
        {
            Debug.LogError($"Could not find type for statusEffect: {statusEffect}");
            return;
        }

        try
        {
            object instance = Activator.CreateInstance(type, GlobalVars.player.GetComponent<Player>(), duration, level);
            BaseStatusEffect effect = instance as BaseStatusEffect;

            if (effect == null)
            {
                Debug.LogError($"Type {type.Name} is not a BaseStatusEffect.");
            }
        }
        catch (MissingMethodException)
        {
            Debug.LogError($"Constructor (Player, float, int) not found on type {type.Name}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error instantiating {type.Name}: {ex.Message}");
        }
    }
}

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class SubclassSelectorAttribute : PropertyAttribute
{
    public Type BaseType;

    public SubclassSelectorAttribute(Type baseType)
    {
        this.BaseType = baseType;
    }
}

[CustomPropertyDrawer(typeof(SubclassSelectorAttribute))]
public class SubclassSelectorDrawer : PropertyDrawer
{
    private static readonly Dictionary<Type, TypeCache> cache = new();

    private class TypeCache
    {
        public string[] AssemblyQualifiedNames;
        public string[] DisplayNames;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attr = (SubclassSelectorAttribute)attribute;
        var baseType = attr.BaseType;

        // Check if we've already cached this base type
        if (!cache.TryGetValue(baseType, out var typeCache))
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.FullName.StartsWith("Unity")) // Skip Unity internals for speed
                .SelectMany(a => SafeGetTypes(a))
                .Where(t => baseType.IsAssignableFrom(t) && !t.IsAbstract && !t.IsGenericType)
                .OrderBy(t => t.Name)
                .ToArray();

            typeCache = new TypeCache
            {
                AssemblyQualifiedNames = types.Select(t => t.AssemblyQualifiedName).ToArray(),
                DisplayNames = types.Select(t => t.Name).ToArray()
            };

            cache[baseType] = typeCache;
        }

        var options = typeCache.DisplayNames;
        var values = typeCache.AssemblyQualifiedNames;

        int index = Mathf.Max(0, Array.IndexOf(values, property.stringValue));
        EditorGUI.BeginProperty(position, label, property);
        int newIndex = EditorGUI.Popup(position, label.text, index, options);
        if (newIndex != index)
        {
            property.stringValue = values[newIndex];
        }
        EditorGUI.EndProperty();
    }

    private static IEnumerable<Type> SafeGetTypes(System.Reflection.Assembly asm)
    {
        try
        {
            return asm.GetTypes();
        }
        catch
        {
            return Array.Empty<Type>();
        }
    }
}