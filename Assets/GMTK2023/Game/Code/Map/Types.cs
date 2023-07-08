using System.Collections.Generic;

namespace GMTK2023.Game
{
    public interface ILocation
    {
        public string Name { get; }
    }

    public interface IMap
    {
        public IEnumerable<ILocation> Locations { get; }
    }
}