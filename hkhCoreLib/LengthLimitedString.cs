using System;

// See below class for a template to create derived classes from.

namespace hkhCoreLib
{
    /// <summary>
    /// Base class for length-limited, typesafe and immutable strings.
    /// </summary>
    public abstract class LengthLimitedString : IComparable
    {
        // ReSharper disable once MemberCanBePrivate.Global
        /// <summary>
        /// The maximum length of any string of this type.
        /// </summary>
        public int MaxLength { get; private set; }

        // ReSharper disable once MemberCanBePrivate.Global
        /// <summary>
        /// The object value as a string.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Create a new and empty object and set the maximum length.
        /// </summary>
        protected LengthLimitedString(int maxLength)
        {
            MaxLength = maxLength;
        }

        /// <summary>
        /// Create a new object with the passed string as value, and set the maximum length.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Exception thrown if passed string is longer than maximum length.</exception>
        protected LengthLimitedString(int maxLength, string value)
        {
            if (null != value)
            {
                string str = value.Trim();
                if (str.Length > maxLength)
                {
                    throw new ArgumentOutOfRangeException("value", string.Format("Can not be longer than {0} characters.", maxLength));
                }
            }
            MaxLength = maxLength;
            Value = value;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="other">The other object to make a copy of.</param>
        protected LengthLimitedString(LengthLimitedString other)
        {
            MaxLength = other.MaxLength;
            Value = other.Value;
        }

        public override string ToString()
        {
            return Value ?? string.Empty;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        /// <summary>
        /// Determine whether object represents a null string or an empty string.
        /// </summary>
        /// <returns>true if null or empty.</returns>
        public bool IsNullOrEmpty()
        {
            return string.IsNullOrEmpty(Value);
        }

        /// <summary>
        /// Determine whether object represents a null string, an empty string or a string consisting solely of whitespace.
        /// </summary>
        /// <returns>true if null, empty or whitespace only.</returns>
        public bool IsNullOrWhiteSpace()
        {
            return string.IsNullOrWhiteSpace(Value);
        }

        public override int GetHashCode()
        {
            return (null == Value ? 0 : Value.GetHashCode());
        }

        public int CompareTo(object obj)
        {
            if (null == obj) return 1;
            LengthLimitedString other = obj as LengthLimitedString;
            if (null == other) throw new ArgumentException("Must be LengthLimitedString");
            if (IsNullOrEmpty() && other.IsNullOrEmpty()) return 0;
            return String.Compare(Value, other.Value, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Determine whether two objects are equal.
        /// </summary>
        /// <param name="other">The other object to compare to.</param>
        /// <returns>true if both objects are null or empty, or has identical content.</returns>
        protected bool Equals(LengthLimitedString other)
        {
            if (null == other) return false;
            if (IsNullOrEmpty() && other.IsNullOrEmpty()) return true;
            return Value.Equals(other.Value);
        }
    }
}


// Here is a template for inheriting classes from the LengthLimitedString class.
// Just replace <YourClassName> with a real class name, and you are ready to go.
// If you use this template unchanged (except for the name, of course), there is no
// need to write unit tests - all functionality is found in the LengthLimitedString
// base class, and unit tests for that class already exists.

///// <summary>
///// A typesafe and immutable class for representing <some kind of string>.
///// </summary>
//public sealed class <YourClassName> : LengthLimitedString, IEquatable<LengthLimitedString>
//{
//    // ReSharper disable once MemberCanBePrivate.Global
//    /// <summary>
//    /// The maximum length of any string of this type.
//    /// </summary>
//    public const int MaximumLength = 25;

//    /// <summary>
//    /// Create am empty <YourClassName> object.
//    /// </summary>
//    public <YourClassName>()
//        : base(MaximumLength)
//    {
//    }

//    /// <summary>
//    /// Create a new <YourClassName> object, and initialize it with the passed string.
//    /// </summary>
//    /// <param name="value">Value for the new <YourClassName>.</param>
//    public <YourClassName>(string value)
//        : base(MaximumLength, value)
//    {
//    }

//    /// <summary>
//    /// Create a copy of an existing <YourClassName> object.
//    /// </summary>
//    /// <param name="other">The original to make a copy of.</param>
//    public <YourClassName>(<YourClassName> other)
//        : base(other)
//    {
//    }

//    public new bool Equals(LengthLimitedString other)
//    {
//        return base.Equals(other);
//    }

//    public override int GetHashCode()
//    {
//        return base.GetHashCode();
//    }

//    /// <summary>
//    /// Determines whether the specified <YourClassName> is equal to the current <YourClassName>.
//    /// </summary>
//    /// <returns>
//    /// true if the specified <YourClassName> is equal to the current <YourClassName>; otherwise, false.
//    /// </returns>
//    /// <param name="obj">The object to compare with the current object.</param>
//    // ReSharper disable once CSharpWarnings::CS0659
//    public override bool Equals(object obj)
//    {
//        return Equals(obj as <YourClassName>);
//    }
//}
