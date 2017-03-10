using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using StellarisSaveGameEditor.Properties;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;

namespace StellarisSaveGameEditor
{
    public partial class Main : Form
    {
        public string GamestateString { get; set; }
        public string MetaString { get; set; }
        public string ZipFileName { get; set; }
        public string InitialDirectory { get; set; }
        public List<Planet> PlanetList { get; set; }

        public Main()
        {
            InitializeComponent();

            saveToolStripMenuItem.Enabled = false;
            InitialDirectory = @"C:\Users\paulo\OneDrive\Documentos\Paradox Interactive\Stellaris\save games\gatineofoundation_-154020303";
            //InitialDirectory = "c:\\"
            toolStripStatusLabel.Text = "Please open a file...";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = InitialDirectory,
                Filter = "sav files (*.sav)|*.sav",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                var zipFile = openFileDialog.OpenFile();

                ZipFileName = ((FileStream)zipFile).Name;

                using (zipFile)
                {
                    using (var zip = new ZipArchive(zipFile, ZipArchiveMode.Read))
                    {
                        foreach (var entry in zip.Entries)
                        {
                            using (var stream = entry.Open())
                            {
                                var reader = new StreamReader(stream);

                                if (entry.FullName == "meta")
                                    MetaString = reader.ReadToEnd();

                                if (entry.FullName == "gamestate")
                                    GamestateString = reader.ReadToEnd();
                            }
                        }
                    }
                }

                saveToolStripMenuItem.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.Main_openToolStripMenuItem_Click_Error__Could_not_read_file_from_disk__Original_error__ + ex.Message);
            }

            DeserializeGamestate(GamestateString);

            toolStripStatusLabel.Text = "File opened successfully!";
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var gamestateFile = archive.CreateEntry("gamestate");

                    using (var entryStream = gamestateFile.Open())
                    using (var streamWriter = new StreamWriter(entryStream))
                    {
                        streamWriter.Write(GamestateString);
                    }

                    var metaFile = archive.CreateEntry("meta");

                    using (var entryStream = metaFile.Open())
                    using (var streamWriter = new StreamWriter(entryStream))
                    {
                        streamWriter.Write(MetaString);
                    }
                }

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "sav files (*.sav)|*.sav",
                    Title = "Save"
                };
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName == "") return;

                using (var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    memoryStream.CopyTo(fileStream);
                }
            }

            MessageBox.Show(Resources.Main_saveToolStripMenuItem_Click_File_saved);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DeserializeGamestate(string gamestateString)
        {
            var formattedGamestate = Regex.Replace(gamestateString, @"\t|\n|\r", "");
            GetPlanetList(GetPlanetListLine(formattedGamestate));
        }

        private void GetPlanetList(string planetsLine)
        {
            var planetId = 0;
            PlanetList = new List<Planet>();

            while (true)
            {
                var planetLine = GetPlanetLine(planetsLine, planetId);
                if (planetLine == string.Empty) break;
                var planet = TransformLineToPlanet(planetLine, planetId.ToString());
                PlanetList.Add(planet);
                planetId++;
            }
        }

        private Planet TransformLineToPlanet(string planetLine, string planetId)
        {
            /*
            X ={
                name="Aeria"
                planet_class="pc_g_star"
                coordinate={
                    x=0.000
                    y=0.000
                    origin=111
                }
                orbit=0.000
                planet_size=25
                fortification_health=-1.000
                last_bombardment= 1.01.01"
                owner=0
                original_owner=0
                controller=0
                pop={
                    0 1 2 3 4 5 6 7 123 135 136 137 142 149 154 160 165 171 175 183 190 195 202 215 226 236 248 266 276 296 
                }
                orbitals={
                    33554562 2 1 398 4294967295 4294967295 4294967295 4294967295 
                }
                leader=13
                spaceport_station=0
                army={
                    16777364 16777360 16777367 50331772 33554604 16777216 
                }
                built_armies=2
                timed_modifier={
                    modifier="capital"
                    days=-1
                }
                timed_modifier={
                    multiplier=3.000
                    modifier="assist_research"
                    days=-1
                }
                entity=0
                tiles={
                    0={
                        active=yes
                    }
                    1={
                        active=yes
                    }
                }
                spaceport={
                    build_queue_item={
                        item={
                            type=colony_ship
                            pop=0
                        }
                        progress=142.000
                        sector=4294967296				cost={
                            minerals=249.000
                        }
                        id=95
                    }
                    level=6
                    modules={
                        0=laser_weapon				1=observatory				2=fleet_academy				3=synchronized_defenses			}
                    next_build_item_id=96
                }
                prevent_anomaly=yes

                surveyed_by=0
                orbital_deposit_tile=1125917086711808   next_build_item_id=4 }
            */

            var planetStructureDictionary = new Dictionary<PlanetEnum, int>
            {
                {PlanetEnum.Name, planetLine.IndexOf("name", StringComparison.Ordinal)},
                {PlanetEnum.PlanetClass, planetLine.IndexOf("planet_class", StringComparison.Ordinal)},
                {PlanetEnum.Coordinate, planetLine.IndexOf("coordinate", StringComparison.Ordinal)},
                {PlanetEnum.Orbit, planetLine.IndexOf("orbit", StringComparison.Ordinal)},
                {PlanetEnum.PlanetSize, planetLine.IndexOf("planet_size", StringComparison.Ordinal)},
                {PlanetEnum.FortificationHealth, planetLine.IndexOf("fortification_health", StringComparison.Ordinal)},
                {PlanetEnum.LastBombardment, planetLine.IndexOf("last_bombardment", StringComparison.Ordinal)},
                {PlanetEnum.Owner, planetLine.IndexOf("owner", StringComparison.Ordinal)},
                {PlanetEnum.OriginalOwner, planetLine.IndexOf("original_owner", StringComparison.Ordinal)},
                {PlanetEnum.Controller, planetLine.IndexOf("controller", StringComparison.Ordinal)},
                {PlanetEnum.Pop, planetLine.IndexOf("pop", StringComparison.Ordinal)},
                {PlanetEnum.Orbitals, planetLine.IndexOf("orbitals", StringComparison.Ordinal)},
                {PlanetEnum.Leader, planetLine.IndexOf("leader", StringComparison.Ordinal)},
                {PlanetEnum.Spaceport, planetLine.IndexOf("spaceport", StringComparison.Ordinal)},
                {PlanetEnum.SpaceportStation, planetLine.IndexOf("spaceport_station", StringComparison.Ordinal)},
                {PlanetEnum.BuildQueueItem, planetLine.IndexOf("build_queue_item", StringComparison.Ordinal)},
                {PlanetEnum.Army, planetLine.IndexOf("army", StringComparison.Ordinal)},
                {PlanetEnum.BuiltArmies, planetLine.IndexOf("built_armies", StringComparison.Ordinal)},
                {PlanetEnum.TimedModifier, planetLine.IndexOf("timed_modifier", StringComparison.Ordinal)},
                {PlanetEnum.Entity, planetLine.IndexOf("entity", StringComparison.Ordinal)},
                {PlanetEnum.Tiles, planetLine.IndexOf("tiles", StringComparison.Ordinal)},
                {PlanetEnum.PreventAnomaly, planetLine.IndexOf("prevent_anomaly", StringComparison.Ordinal)},
                {PlanetEnum.SurveyedBy, planetLine.IndexOf("surveyed_by", StringComparison.Ordinal)},
                {PlanetEnum.OrbitalDepositTile, planetLine.IndexOf("orbital_deposit_tile", StringComparison.Ordinal)},
                {PlanetEnum.NextBuildItemId, planetLine.IndexOf("next_build_item_id", StringComparison.Ordinal)},
                {PlanetEnum.ColonizeDate, planetLine.IndexOf("colonize_date", StringComparison.Ordinal)},
                {PlanetEnum.IsMoon, planetLine.IndexOf("is_moon", StringComparison.Ordinal)},
                {PlanetEnum.MoonOf, planetLine.IndexOf("moon_of", StringComparison.Ordinal)},
            };

            foreach (var planetStructure in planetStructureDictionary.Where(kvp => kvp.Value == -1).ToList())
                planetStructureDictionary.Remove(planetStructure.Key);

            var sortedPlanetStructureDictionary = from entry in planetStructureDictionary orderby entry.Value ascending select entry;

            var planet = new Planet { Id = planetId };

            //foreach (var structure in sortedPlanetStructureDictionary)
            foreach (var structure in sortedPlanetStructureDictionary.Select((value, i) => new { i, value }))
            {
                var nextIndex = structure.i == sortedPlanetStructureDictionary.Count() - 1 ? planetLine.Length - 1 : sortedPlanetStructureDictionary.ElementAt(structure.i + 1).Value;
                var selectedString = GetStringByPosition(planetLine, structure.value.Value, nextIndex);

                switch (structure.value.Key)
                {
                    case PlanetEnum.Name:
                        planet.Name = GetLineValue(selectedString);
                        break;
                    case PlanetEnum.PlanetClass:
                        planet.PlanetClass = GetLineValue(selectedString);
                        break;
                    case PlanetEnum.Coordinate:
                        // Complex
                        break;
                    case PlanetEnum.Orbit:
                        planet.Orbit = GetLineValue(selectedString, "orbit=");
                        break;
                    case PlanetEnum.PlanetSize:
                        planet.PlanetSize = GetLineValue(selectedString, "planet_size=");
                        break;
                    case PlanetEnum.FortificationHealth:
                        planet.FortificationHealth = GetLineValue(selectedString, "fortification_health=");
                        break;
                    case PlanetEnum.LastBombardment:
                        planet.LastBombardment = GetLineValue(selectedString);
                        break;
                    case PlanetEnum.Owner:
                        planet.Owner = GetLineValue(selectedString, "owner=");
                        break;
                    case PlanetEnum.OriginalOwner:
                        planet.OriginalOwner = GetLineValue(selectedString, "original_owner=");
                        break;
                    case PlanetEnum.Controller:
                        planet.Controller = GetLineValue(selectedString, "controller=");
                        break;
                    case PlanetEnum.Pop:
                        // Complex
                        break;
                    case PlanetEnum.Orbitals:
                        // Complex
                        break;
                    case PlanetEnum.Leader:
                        planet.Leader = GetLineValue(selectedString, "leader=");
                        break;
                    case PlanetEnum.SpaceportStation:
                        planet.SpaceportStation = GetLineValue(selectedString, "spaceport_station=");
                        break;
                    case PlanetEnum.Army:
                        // Complex
                        break;
                    case PlanetEnum.BuiltArmies:
                        planet.BuiltArmies = GetLineValue(selectedString, "built_armies=");
                        break;
                    case PlanetEnum.TimedModifier:
                        // Complex
                        break;
                    case PlanetEnum.MoonOf:
                        planet.MoonOf = GetLineValue(selectedString, "moon_of=");
                        break;
                    case PlanetEnum.IsMoon:
                        planet.IsMoon = GetLineValue(selectedString, "is_moon=");
                        break;
                    case PlanetEnum.Entity:
                        planet.Entity = GetLineValue(selectedString, "entity=");
                        break;
                    case PlanetEnum.Tiles:
                        // Complex
                        break;
                    case PlanetEnum.PreventAnomaly:
                        planet.PreventAnomaly = GetLineValue(selectedString, "prevent_anomaly=");
                        break;
                    case PlanetEnum.SurveyedBy:
                        planet.SurveyedBy = GetLineValue(selectedString, "surveyed_by=");
                        break;
                    case PlanetEnum.OrbitalDepositTile:
                        planet.OrbitalDepositTile = GetLineValue(selectedString, "orbital_deposit_tile=");
                        break;
                    case PlanetEnum.NextBuildItemId:
                        planet.NextBuildItemId = GetLineValue(selectedString, "next_build_item_id");
                        break;
                    case PlanetEnum.Spaceport:
                        // Complex
                        break;
                    case PlanetEnum.ColonizeDate:
                        planet.ColonizeDate = GetLineValue(selectedString);
                        break;
                    case PlanetEnum.BuildQueueItem:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return planet;
        }

        private string GetStringByPosition(string planetLine, int fromIndex, int toIndex)
        {
            return planetLine.Substring(fromIndex, toIndex - fromIndex);
        }

        private string GetPlanetLine(string planetsLine, int planetId)
        {
            var firstValue = planetId + "={name";
            var secondValue = (planetId + 1) + "={name";

            var from = planetsLine.IndexOf(firstValue, StringComparison.Ordinal);
            var to = planetsLine.IndexOf(secondValue, StringComparison.Ordinal) - from;

            if (to < 0) return string.Empty;

            var planetLine = planetsLine.Substring(from, to);

            return planetLine;
        }

        private string GetPlanetListLine(string formattedGamestate)
        {
            var from = formattedGamestate.IndexOf("planet={", StringComparison.Ordinal);
            var to = formattedGamestate.IndexOf("country={", StringComparison.Ordinal) - from;

            var planetListLine = formattedGamestate.Substring(from, to);

            return planetListLine;
        }

        private string GetLineValue(string line)
        {
            var from = line.IndexOf("\"", StringComparison.Ordinal) + 1;
            var to = line.LastIndexOf("\"", StringComparison.Ordinal) - from;

            return line.Substring(from, to);
        }

        private string GetLineValue(string line, string param)
        {
            return line.Replace(param, "");
        }
    }

    public enum BlocksEnum
    {
        Root,
        Planets
    }

    public enum PlanetEnum
    {
        Name,
        PlanetClass,
        Coordinate,
        Orbit,
        PlanetSize,
        FortificationHealth,
        LastBombardment,
        Owner,
        OriginalOwner,
        Controller,
        Pop,
        Orbitals,
        Leader,
        Spaceport,
        SpaceportStation,
        Army,
        BuiltArmies,
        TimedModifier,
        Entity,
        Tiles,
        PreventAnomaly,
        SurveyedBy,
        OrbitalDepositTile,
        NextBuildItemId,
        MoonOf,
        IsMoon,
        ColonizeDate,
        BuildQueueItem
    }

}
