using UnityEngine;

namespace GameWork.Unity.Inspector.PropertyAttributes
{
    /// <summary>
    /// For use on an Enum property.
    ///
    /// The enum type must be decorated with the [Flags] attribute and must have a 0 value Flag:
    /// <example>
    /// <code>
    /// [Flags]
    /// public enum MyEnum
    /// {
    ///     None =  1 << 0,
    ///     A =     1 << 1,
    ///     B =     1 << 2,
    ///     C =     1 << 3
    /// }
    /// </code>
    /// </example>
    /// Usage:
    /// <example>
    /// <code>
    /// [SerializeField] [EnumFlags] private MyEnum _myEnum;
    /// </code>
    /// </example>
    /// </summary>
    public class EnumFlagsAttribute : PropertyAttribute
    {
    }
}
