using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace StellarisSaveGameEditor
{
    public class Planet
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PlanetClass { get; set; }
        public Coordinate Coordinate { get; set; }
        public string Orbit { get; set; }
        public string PlanetSize { get; set; }
        public string FortificationHealth { get; set; }
        public string LastBombardment { get; set; }
        public string Entity { get; set; }
        public List<Tile> Tiles { get; set; }
        public string SurveyedBy { get; set; }
        public string Owner { get; set; }
        public string OriginalOwner { get; set; }
        public string Controller { get; set; }
        public string Pop { get; set; }
        public string Orbitals { get; set; }
        public string Leader { get; set; }
        public string SpaceportStation { get; set; }
        public string Army { get; set; }
        public string BuiltArmies { get; set; }
        public string TimedModifier { get; set; }
        public string PreventAnomaly { get; set; }
        public string OrbitalDepositTile { get; set; }
        public string NextBuildItemId { get; set; }
        public string MoonOf { get; set; }
        public string IsMoon { get; set; }
        public string ColonizeDate { get; set; }
        public Spaceport Spaceport { get; set; }
        public BuildQueueItem BuildQueueItem { get; set; }
    }

    public class Coordinate
    {
        public string X { get; set; }
        public string Y { get; set; }
        public string Origin { get; set; }
    }

    public class Tile
    {
    }

    public class Spaceport
    {
    }

    public class BuildQueueItem
    {
    }
}
