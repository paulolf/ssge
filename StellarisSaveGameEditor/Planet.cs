using System.Collections.Generic;

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
        public List<string> Pop { get; set; }
        public List<string> Orbitals { get; set; }
        public string Leader { get; set; }
        public string SpaceportStation { get; set; }
        public List<string> Army { get; set; }
        public string BuiltArmies { get; set; }
        public List<TimedModifier> TimedModifierList { get; set; }
        public string PreventAnomaly { get; set; }
        public string OrbitalDepositTile { get; set; }
        public string NextBuildItemId { get; set; }
        public string MoonOf { get; set; }
        public string IsMoon { get; set; }
        public string ColonizeDate { get; set; }
        public Spaceport Spaceport { get; set; }
        public string BuildQueueItem { get; set; }
        public List<string> Moons { get; set; }
        public string HasRing { get; set; }
        public string TerraformedBy { get; set; }
        public string ColonyTitle { get; set; }
        public Flags Flags { get; set; }
        public string OwnSpeciesSlavery { get; set; }
        public string FoodDeficit { get; set; }
        public string ShipclassOrbitalStation { get; set; }
        public string EntityName { get; set; }
        public string ExplicitEntity { get; set; }
        public TerraformProcess TerraformProcess { get; set; }
        public string IsTerraformed { get; set; }
        public string PlanetModifier { get; set; }
        public string GroundSupportStance { get; set; }
        public string BuildingConstructionQueue { get; set; }
    }

    public class Coordinate
    {
        public string X { get; set; }
        public string Y { get; set; }
        public string Origin { get; set; }
    }

    public class Flags
    {
        public string PrescriptedIdeal { get; set; }
        public string SeismicDisturbance { get; set; }
        public string ColonyEvent { get; set; }
        public string SeismicDisturbanceFriendly { get; set; }
        public string SeismicDisturbanceQuake2 { get; set; }
        public string SeismicDisturbanceFamine { get; set; }
        public string SeismicDisturbanceRefugees { get; set; }
        public string SeismicDisturbanceGift { get; set; }
        public string SeismicDisturbanceAnthropologists { get; set; }
        public string PheromonePlanet { get; set; }
        public string UplOngoingPlanet { get; set; }
        public string StoneAgePrimitives { get; set; }
        public string NomadStartingPoint { get; set; }
        public string AbandonedTerraformingPlanet { get; set; }
        public string TerraformingMutantsWon { get; set; }
        public string EnclavesEstablished { get; set; }
        public string ReservationsEstablished { get; set; }
        public string FeThePreserve { get; set; }
        public string PrimitiveAttack { get; set; }
        public string OreGrinderPlanet { get; set; }
        public string FallenEmpireWorld { get; set; }
    }

    public class TerraformProcess
    {
        public string Progress { get; set; }
        public string Total { get; set; }
        public string PlanetClass { get; set; }
        public string Cost { get; set; }
        public string Energy { get; set; }
        public string Who { get; set; }
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

    public class TimedModifier
    {
        public string Multiplier { get; set; }
        public string Modifier { get; set; }
        public string Days { get; set; }
    }
}
