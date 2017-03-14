using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace StellarisSaveGameEditor
{
    public class StringManager
    {
        internal static string GetStringByPosition(string line, int fromIndex, int toIndex)
        {
            return line.Substring(fromIndex, toIndex - fromIndex);
        }

        internal static string GetPlanetLine(string planetsBlock, int planetId)
        {
            var firstValue = planetId + "={name";
            var secondValue = (planetId + 1) + "={name";

            var from = planetsBlock.IndexOf(firstValue, StringComparison.Ordinal);
            var to = planetsBlock.IndexOf(secondValue, StringComparison.Ordinal) - from;

            if (to < 0) return string.Empty;

            var planetLine = planetsBlock.Substring(from, to);

            return planetLine;
        }

        internal static string GetPlanetListLine(string formattedGamestate)
        {
            var from = formattedGamestate.IndexOf("planet={", StringComparison.Ordinal);
            var to = formattedGamestate.IndexOf("country={", StringComparison.Ordinal) - from;

            var planetListLine = formattedGamestate.Substring(from, to);

            return planetListLine;
        }

        internal static string GetValueBetweenDoubleQuotes(string line)
        {
            var from = line.IndexOf("\"", StringComparison.Ordinal) + 1;
            var to = line.LastIndexOf("\"", StringComparison.Ordinal) - from;

            return line.Substring(from, to);
        }

        internal static string ClearValue(string line, string param)
        {
            return line.Replace(param, "");
        }

        internal static Coordinate GetCoordinate(string selectedString)
        {
            var coordData = GetValueBetweenBrackets(selectedString);

            var coordinateDictionary = new Dictionary<CoordinateEnum, int>
            {
                {CoordinateEnum.X, coordData.IndexOf("x=", StringComparison.Ordinal)},
                {CoordinateEnum.Y, coordData.IndexOf("y=", StringComparison.Ordinal)},
                {CoordinateEnum.Origin, coordData.IndexOf("origin=", StringComparison.Ordinal)},
            };

            var coordinate = new Coordinate
            {
                X = coordData.Substring(coordinateDictionary[CoordinateEnum.X] + 2, coordinateDictionary[CoordinateEnum.Y] - (coordinateDictionary[CoordinateEnum.X] + 2)),
                Y = coordData.Substring(coordinateDictionary[CoordinateEnum.Y] + 2, coordinateDictionary[CoordinateEnum.Origin] - (coordinateDictionary[CoordinateEnum.Y] + 2)),
                Origin = coordData.Substring(coordinateDictionary[CoordinateEnum.Origin] + 7),
            };

            return coordinate;
        }

        private static string GetValueBetweenBrackets(string selectedString)
        {
            //Sample: coordinate={x=0.000y=0.000origin=112}
            var from = selectedString.IndexOf("{", StringComparison.Ordinal) + 1;
            var to = selectedString.LastIndexOf("}", StringComparison.Ordinal) - from;

            return selectedString.Substring(from, to);
        }

        public static List<string> GetStringList(string selectedString)
        {
            return GetValueBetweenBrackets(selectedString).Split(' ').ToList().Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
        }



        public static Flags GetFlags(string fullString)
        {
            var flagsLine = GetValueBetweenBrackets(fullString);

            var flagDictionary = new Dictionary<FlagsEnum, int>
            {
                {FlagsEnum.PrescriptedIdeal, flagsLine.IndexOf("prescripted_ideal=", StringComparison.Ordinal)},
                {FlagsEnum.SeismicDisturbance,flagsLine.IndexOf("seismic_disturbance=", StringComparison.Ordinal)},
                {FlagsEnum.ColonyEvent,flagsLine.IndexOf("colony_event=", StringComparison.Ordinal)},
                {FlagsEnum.SeismicDisturbanceFriendly,flagsLine.IndexOf("seismic_disturbance_friendly=", StringComparison.Ordinal)},
                {FlagsEnum.SeismicDisturbanceQuake2,flagsLine.IndexOf("seismic_disturbance_quake2=", StringComparison.Ordinal)},
                {FlagsEnum.SeismicDisturbanceFamine,flagsLine.IndexOf("seismic_disturbance_famine=", StringComparison.Ordinal)},
                {FlagsEnum.SeismicDisturbanceRefugees,flagsLine.IndexOf("seismic_disturbance_refugees=", StringComparison.Ordinal)},
                {FlagsEnum.SeismicDisturbanceGift,flagsLine.IndexOf("seismic_disturbance_gift=", StringComparison.Ordinal)},
                {FlagsEnum.SeismicDisturbanceAnthropologists,flagsLine.IndexOf("seismic_disturbance_anthropologists=", StringComparison.Ordinal)},
                {FlagsEnum.PheromonePlanet,flagsLine.IndexOf("pheromone_planet=", StringComparison.Ordinal)},
                {FlagsEnum.UplOngoingPlanet,flagsLine.IndexOf("upl_ongoing_planet=", StringComparison.Ordinal)},
                {FlagsEnum.StoneAgePrimitives,flagsLine.IndexOf("stone_age_primitives=", StringComparison.Ordinal)},
                {FlagsEnum.NomadStartingPoint,flagsLine.IndexOf("nomad_starting_point=", StringComparison.Ordinal)},
                {FlagsEnum.AbandonedTerraformingPlanet,flagsLine.IndexOf("abandoned_terraforming_planet=", StringComparison.Ordinal)},
                {FlagsEnum.TerraformingMutantsWon,flagsLine.IndexOf("terraforming_mutants_won=", StringComparison.Ordinal)},
                {FlagsEnum.EnclavesEstablished,flagsLine.IndexOf("enclaves_established=", StringComparison.Ordinal)},
                {FlagsEnum.ReservationsEstablished,flagsLine.IndexOf("reservations_established=", StringComparison.Ordinal)},
                {FlagsEnum.FeThePreserve,flagsLine.IndexOf("fe_the_preserve=", StringComparison.Ordinal)},
                {FlagsEnum.PrimitiveAttack,flagsLine.IndexOf("primitive_attack=", StringComparison.Ordinal)},
                {FlagsEnum.OreGrinderPlanet,flagsLine.IndexOf("ore_grinder_planet=", StringComparison.Ordinal)},
                {FlagsEnum.FallenEmpireWorld,flagsLine.IndexOf("fallen_empire_world=", StringComparison.Ordinal)},
            };

            var flags = new Flags();

            foreach (var flag in flagDictionary.Where(kvp => kvp.Value == -1).ToList())
                flagDictionary.Remove(flag.Key);

            var sortedFlagDictionary = from entry in flagDictionary orderby entry.Value ascending select entry;

            foreach (var flag in sortedFlagDictionary.Select((value, i) => new { i, value }))
            {
                var nextIndex = flag.i == sortedFlagDictionary.Count() - 1
                    ? flagsLine.Length
                    : sortedFlagDictionary.ElementAt(flag.i + 1).Value;
                var selectedString = GetStringByPosition(flagsLine, flag.value.Value, nextIndex);

                switch (flag.value.Key)
                {
                    case FlagsEnum.PrescriptedIdeal:
                        flags.PrescriptedIdeal = ClearValue(selectedString, "prescripted_ideal=");
                        break;
                    case FlagsEnum.SeismicDisturbance:
                        flags.SeismicDisturbance = ClearValue(selectedString, "seismic_disturbance=");
                        break;
                    case FlagsEnum.ColonyEvent:
                        flags.ColonyEvent = ClearValue(selectedString, "colony_event=");
                        break;
                    case FlagsEnum.SeismicDisturbanceFriendly:
                        flags.SeismicDisturbanceFriendly = ClearValue(selectedString, "seismic_disturbance_friendly=");
                        break;
                    case FlagsEnum.SeismicDisturbanceQuake2:
                        flags.SeismicDisturbanceQuake2 = ClearValue(selectedString, "seismic_disturbance_quake2=");
                        break;
                    case FlagsEnum.SeismicDisturbanceFamine:
                        flags.SeismicDisturbanceFamine = ClearValue(selectedString, "seismic_disturbance_famine=");
                        break;
                    case FlagsEnum.SeismicDisturbanceRefugees:
                        flags.SeismicDisturbanceRefugees = ClearValue(selectedString, "seismic_disturbance_refugees=");
                        break;
                    case FlagsEnum.SeismicDisturbanceGift:
                        flags.SeismicDisturbanceGift = ClearValue(selectedString, "seismic_disturbance_gift=");
                        break;
                    case FlagsEnum.SeismicDisturbanceAnthropologists:
                        flags.SeismicDisturbanceAnthropologists = ClearValue(selectedString, "seismic_disturbance_anthropologists=");
                        break;
                    case FlagsEnum.PheromonePlanet:
                        flags.SeismicDisturbanceAnthropologists = ClearValue(selectedString, "pheromone_planet=");
                        break;
                    case FlagsEnum.UplOngoingPlanet:
                        flags.UplOngoingPlanet = ClearValue(selectedString, "upl_ongoing_planet=");
                        break;
                    case FlagsEnum.StoneAgePrimitives:
                        flags.StoneAgePrimitives = ClearValue(selectedString, "upl_ongoing_planet=");
                        break;
                    case FlagsEnum.NomadStartingPoint:
                        flags.NomadStartingPoint = ClearValue(selectedString, "nomad_starting_point=");
                        break;
                    case FlagsEnum.AbandonedTerraformingPlanet:
                        flags.AbandonedTerraformingPlanet = ClearValue(selectedString, "abandoned_terraforming_planet=");
                        break;
                    case FlagsEnum.TerraformingMutantsWon:
                        flags.TerraformingMutantsWon = ClearValue(selectedString, "terraforming_mutants_won=");
                        break;
                    case FlagsEnum.EnclavesEstablished:
                        flags.EnclavesEstablished = ClearValue(selectedString, "enclaves_established=");
                        break;
                    case FlagsEnum.ReservationsEstablished:
                        flags.ReservationsEstablished = ClearValue(selectedString, "reservations_established=");
                        break;
                    case FlagsEnum.FeThePreserve:
                        flags.FeThePreserve = ClearValue(selectedString, "fe_the_preserve=");
                        break;
                    case FlagsEnum.PrimitiveAttack:
                        flags.PrimitiveAttack = ClearValue(selectedString, "primitive_attack=");
                        break;
                    case FlagsEnum.OreGrinderPlanet:
                        flags.OreGrinderPlanet = ClearValue(selectedString, "ore_grinder_planet=");
                        break;
                    case FlagsEnum.FallenEmpireWorld:
                        flags.FallenEmpireWorld = ClearValue(selectedString, "fallen_empire_world=");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return flags;
        }

        public static TerraformProcess GetTerraformProcess(string fullString)
        {
            //terraform_process={progress=1536total=1800planet_class="pc_arctic"cost={}energy=2000.000who=2}

            var terraformLine = GetValueBetweenBrackets(fullString);

            var coordinateDictionary = new Dictionary<TerraformEnum, int>
            {
                {TerraformEnum.Progress, terraformLine.IndexOf("progress=", StringComparison.Ordinal)},
                {TerraformEnum.Total, terraformLine.IndexOf("total=", StringComparison.Ordinal)},
                {TerraformEnum.PlanetClass, terraformLine.IndexOf("planet_class=", StringComparison.Ordinal)},
                {TerraformEnum.Cost, terraformLine.IndexOf("cost=", StringComparison.Ordinal)},
                {TerraformEnum.Energy, terraformLine.IndexOf("energy=", StringComparison.Ordinal)},
                {TerraformEnum.Who, terraformLine.IndexOf("who=", StringComparison.Ordinal)},
            };

            var terraformProcess = new TerraformProcess
            {
                Progress = terraformLine.Substring(coordinateDictionary[TerraformEnum.Progress] + 9, coordinateDictionary[TerraformEnum.Total] - (coordinateDictionary[TerraformEnum.Progress] + 9)),
                Total = terraformLine.Substring(coordinateDictionary[TerraformEnum.Total] + 6, coordinateDictionary[TerraformEnum.PlanetClass] - (coordinateDictionary[TerraformEnum.Total] + 6)),
                PlanetClass = GetValueBetweenDoubleQuotes(terraformLine.Substring(coordinateDictionary[TerraformEnum.PlanetClass] + 13, coordinateDictionary[TerraformEnum.Cost] - (coordinateDictionary[TerraformEnum.PlanetClass] + 13))),
                Cost = terraformLine.Substring(coordinateDictionary[TerraformEnum.Cost] + 5, coordinateDictionary[TerraformEnum.Energy] - (coordinateDictionary[TerraformEnum.Cost] + 5)),
                Energy = terraformLine.Substring(coordinateDictionary[TerraformEnum.Energy] + 7, coordinateDictionary[TerraformEnum.Who] - (coordinateDictionary[TerraformEnum.Energy] + 7)),
                Who = terraformLine.Substring(coordinateDictionary[TerraformEnum.Who] + 4),
            };

            return terraformProcess;

        }

        public static List<TimedModifier> GetTimedModifier(string fullString)
        {
            //TODO: Complex
            //timed_modifier={modifier="capital"days=-1}timed_modifier={multiplier=3.000modifier="assist_research"days=-1}

            var timeModifierList = new List<TimedModifier>();

            var indexList = GetAllIndexes(fullString, "timed_modifier=").ToList();

            foreach (var index in indexList.Select((value, i) => new { i, value }))
            {
                var nextIndex = index.i == indexList.Count - 1 ? fullString.Length : indexList.ElementAt(index.i + 1);
                var selectedIndexString = GetValueBetweenBrackets(GetStringByPosition(fullString, index.value, nextIndex));

                var timedModifierDictionary = new Dictionary<TimedModifierEnum, int>
                {
                    {TimedModifierEnum.Multiplier, selectedIndexString.IndexOf("multiplier=", StringComparison.Ordinal)},
                    {TimedModifierEnum.Modifier, selectedIndexString.IndexOf("modifier=", StringComparison.Ordinal)},
                    {TimedModifierEnum.Days, selectedIndexString.IndexOf("days=", StringComparison.Ordinal)}
                };

                foreach (var timeModifier in timedModifierDictionary.Where(kvp => kvp.Value == -1).ToList())
                    timedModifierDictionary.Remove(timeModifier.Key);

                var sortedTimedModifierDictionary = from entry in timedModifierDictionary orderby entry.Value ascending select entry;

                var timedModifier = new TimedModifier();

                foreach (var timed in sortedTimedModifierDictionary.Select((valueTimed, iTimed) => new { iTimed, valueTimed }))
                {
                    var nextTimedIndex = timed.iTimed == sortedTimedModifierDictionary.Count() - 1
                        ? selectedIndexString.Length
                        : sortedTimedModifierDictionary.ElementAt(timed.iTimed + 1).Value;
                    var selectedString = StringManager.GetStringByPosition(selectedIndexString, timed.valueTimed.Value,
                        nextTimedIndex);

                    switch (timed.valueTimed.Key)
                    {
                        case TimedModifierEnum.Multiplier:
                            timedModifier.Multiplier = ClearValue(selectedString, "multiplier=");
                            break;
                        case TimedModifierEnum.Modifier:
                            timedModifier.Modifier = GetValueBetweenDoubleQuotes(selectedString);
                            break;
                        case TimedModifierEnum.Days:
                            timedModifier.Days = ClearValue(selectedString, "days=");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                timeModifierList.Add(timedModifier);
            }


            return timeModifierList;
        }

        public static IEnumerable<int> GetAllIndexes(string source, string matchString)
        {
            matchString = Regex.Escape(matchString);
            foreach (Match match in Regex.Matches(source, matchString))
            {
                yield return match.Index;
            }
        }

        public enum CoordinateEnum
        {
            X,
            Y,
            Origin
        }

        public enum FlagsEnum
        {
            PrescriptedIdeal,
            SeismicDisturbance,
            ColonyEvent,
            SeismicDisturbanceFriendly,
            SeismicDisturbanceQuake2,
            SeismicDisturbanceFamine,
            SeismicDisturbanceRefugees,
            SeismicDisturbanceGift,
            SeismicDisturbanceAnthropologists,
            PheromonePlanet,
            UplOngoingPlanet,
            StoneAgePrimitives,
            NomadStartingPoint,
            AbandonedTerraformingPlanet,
            TerraformingMutantsWon,
            EnclavesEstablished,
            ReservationsEstablished,
            FeThePreserve,
            PrimitiveAttack,
            OreGrinderPlanet,
            FallenEmpireWorld,
        }

        public enum TerraformEnum
        {
            Progress,
            Total,
            PlanetClass,
            Cost,
            Energy,
            Who,
        }

        public enum TimedModifierEnum
        {
            Multiplier,
            Modifier,
            Days
        }

    }
}