using System;

namespace Poem
{
    /// <summary>
    /// The Poem framework allows developers to provide hints using a variety of mechanisms.
    /// Here, we are using an <see cref="Attribute"/> to indicate that the particular class is 
    /// suitable to become a separate service.
    /// </summary>
    public sealed class PotentialPoemServiceAttribute : Attribute
    {
    }
}
