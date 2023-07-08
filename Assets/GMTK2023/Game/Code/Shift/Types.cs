using System.Collections.Generic;

namespace GMTK2023.Game
{
    /// <summary>
    /// Describes a shift. Equivalent to a level or night in FNAF
    /// </summary>
    public interface IShiftDescription
    {
        /// <summary>
        /// The adventurers that will be active during this shift
        /// </summary>
        public IReadOnlyList<IAdventurerInfo> Adventurers { get; }
    }
}