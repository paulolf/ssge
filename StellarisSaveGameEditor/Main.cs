using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using StellarisSaveGameEditor.Properties;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms.DataVisualization.Charting;

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
            toolStripStatusLabel.Text = Resources.Main_Main_Please_open_a_file;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "";

            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = InitialDirectory,
                Filter = Resources.Main_openToolStripMenuItem_Click_sav_files,
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            if (!string.IsNullOrEmpty(ZipFileName))
                Text = Text.Replace($" - {ZipFileName}", "");

            toolStripStatusLabel.Text = Resources.Main_openToolStripMenuItem_Click_Opening_file;

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

            LoadComboboxPlanetList();

            Text += $" - {ZipFileName}";

            //chart1.ChartAreas[0].Axes[0].Title = "X";
            //chart1.ChartAreas[0].Axes[1].Title = "Y";
            //chart1.Series[0].ChartType = SeriesChartType.Point;
            //chart1.Series[0].MarkerStyle = MarkerStyle.Circle;
            ////chart1.Series[0].LegendText = "Stars";

            //foreach (var planet in PlanetList)
            //    chart1.Series[0].Points.Add(new DataPoint(double.Parse(planet.Coordinate.X), double.Parse(planet.Coordinate.Y)));

            //chart1.Update();

            //double deltaY = chart1.ChartAreas[0].AxisY.Maximum - chart1.ChartAreas[0].AxisY.Minimum;
            //double deltaX = chart1.ChartAreas[0].AxisX.Maximum - chart1.ChartAreas[0].AxisX.Minimum;

            //double Lenght_Y = chart1.ClientSize.Height;
            //double Lenght_X = chart1.ClientSize.Width;

            //double ratioX = deltaX / Lenght_X;
            //double ratioY = deltaY / Lenght_Y;
            //double diference = Math.Abs(ratioX - ratioY) / Math.Max(ratioX, ratioY);

            //if (diference > 0.02)
            //{
            //    if (chart1.ChartAreas[0].AxisX.Maximum > chart1.ChartAreas[0].AxisY.Maximum)
            //        chart1.ChartAreas[0].AxisY.Maximum = chart1.ChartAreas[0].AxisX.Maximum;
            //    else
            //        chart1.ChartAreas[0].AxisX.Maximum = chart1.ChartAreas[0].AxisY.Maximum;

            //    if (chart1.ChartAreas[0].AxisX.Minimum < chart1.ChartAreas[0].AxisY.Minimum)
            //        chart1.ChartAreas[0].AxisY.Minimum = chart1.ChartAreas[0].AxisX.Minimum;
            //    else
            //        chart1.ChartAreas[0].AxisX.Minimum = chart1.ChartAreas[0].AxisY.Minimum;


            //    chart1.Update();
            //}


            toolStripStatusLabel.Text = Resources.Main_openToolStripMenuItem_Click_File_opened_successfully;

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
                    Filter = Resources.Main_openToolStripMenuItem_Click_sav_files,
                    Title = Resources.Main_saveToolStripMenuItem_Click_Save
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
            GetPlanetList(StringManager.GetPlanetListLine(formattedGamestate));
        }

        private void GetPlanetList(string planetsLine)
        {
            var planetId = 0;
            PlanetList = new List<Planet>();

            while (true)
            {
                var planetLine = StringManager.GetPlanetLine(planetsLine, planetId);
                if (planetLine == string.Empty) break;
                var planet = TransformLineToPlanet(planetLine, planetId.ToString());
                PlanetList.Add(planet);
                //Debug.WriteLine(planetLine);
                planetId++;
            }
        }

        private void LoadComboboxPlanetList()
        {
            comboBoxChoosePlanet.DisplayMember = "Text";
            comboBoxChoosePlanet.ValueMember = "Value";

            var sortedPlanetList = from entry in PlanetList orderby entry.Name ascending select entry;

            foreach (var planet in sortedPlanetList)
            {
                comboBoxChoosePlanet.Items.Add(new { Text = planet.Name, Value = planet.Id });
            }
        }

        private Planet TransformLineToPlanet(string planetLine, string planetId)
        {
            var planetStructureDictionary = new Dictionary<PlanetEnum, int>
            {
                {PlanetEnum.Name, planetLine.IndexOf("name=", StringComparison.Ordinal)},
                {PlanetEnum.PlanetClass, planetLine.IndexOf("planet_class=", StringComparison.Ordinal)},
                {PlanetEnum.Coordinate, planetLine.IndexOf("coordinate=", StringComparison.Ordinal)},
                {PlanetEnum.Orbit, planetLine.IndexOf("orbit=", StringComparison.Ordinal)},
                {PlanetEnum.PlanetSize, planetLine.IndexOf("planet_size=", StringComparison.Ordinal)},
                {PlanetEnum.FortificationHealth, planetLine.IndexOf("fortification_health=", StringComparison.Ordinal)},
                {PlanetEnum.LastBombardment, planetLine.IndexOf("last_bombardment=", StringComparison.Ordinal)},
                {PlanetEnum.Moons, planetLine.IndexOf("moons=", StringComparison.Ordinal)},
                {PlanetEnum.HasRing, planetLine.IndexOf("has_ring=", StringComparison.Ordinal)},
                {PlanetEnum.Owner, planetLine.IndexOf("owner=", StringComparison.Ordinal)},
                {PlanetEnum.OriginalOwner, planetLine.IndexOf("original_owner=", StringComparison.Ordinal)},
                {PlanetEnum.TerraformedBy, planetLine.IndexOf("terraformed_by=", StringComparison.Ordinal)},
                {PlanetEnum.Controller, planetLine.IndexOf("controller=", StringComparison.Ordinal)},
                {PlanetEnum.ShipclassOrbitalStation, planetLine.IndexOf("shipclass_orbital_station=", StringComparison.Ordinal)},
                {PlanetEnum.Pop, planetLine.IndexOf("pop=", StringComparison.Ordinal)},
                {PlanetEnum.ColonyTitle, planetLine.IndexOf("colony_tile=", StringComparison.Ordinal)},
                {PlanetEnum.Orbitals, planetLine.IndexOf("orbitals=", StringComparison.Ordinal)},
                {PlanetEnum.Leader, planetLine.IndexOf("leader=", StringComparison.Ordinal)},
                {PlanetEnum.Spaceport, planetLine.IndexOf("spaceport=", StringComparison.Ordinal)},
                {PlanetEnum.SpaceportStation, planetLine.IndexOf("spaceport_station=", StringComparison.Ordinal)},
                {PlanetEnum.Flags, planetLine.IndexOf("flags=", StringComparison.Ordinal)},
                {PlanetEnum.Army, planetLine.IndexOf("army=", StringComparison.Ordinal)},
                {PlanetEnum.BuiltArmies, planetLine.IndexOf("built_armies=", StringComparison.Ordinal)},
                {PlanetEnum.OwnSpeciesSlavery, planetLine.IndexOf("own_species_slavery=", StringComparison.Ordinal)},
                {PlanetEnum.TimedModifier, planetLine.IndexOf("timed_modifier=", StringComparison.Ordinal)},
                {PlanetEnum.Entity, planetLine.IndexOf("entity=", StringComparison.Ordinal)},
                {PlanetEnum.EntityName, planetLine.IndexOf("entity_name=", StringComparison.Ordinal)},
                {PlanetEnum.ExplicitEntity, planetLine.IndexOf("explicit_entity=", StringComparison.Ordinal)},
                {PlanetEnum.TerraformProcess, planetLine.IndexOf("terraform_process=", StringComparison.Ordinal)},
                {PlanetEnum.IsTerraformed, planetLine.IndexOf("is_terraformed=", StringComparison.Ordinal)},
                {PlanetEnum.GroundSupportStance, planetLine.IndexOf("ground_support_stance=", StringComparison.Ordinal)},
                {PlanetEnum.PlanetModifier, planetLine.IndexOf("planet_modifier=", StringComparison.Ordinal)},
                {PlanetEnum.Tiles, planetLine.IndexOf("tiles=", StringComparison.Ordinal)},
                {PlanetEnum.PreventAnomaly, planetLine.IndexOf("prevent_anomaly=", StringComparison.Ordinal)},
                {PlanetEnum.SurveyedBy, planetLine.IndexOf("surveyed_by=", StringComparison.Ordinal)},
                {PlanetEnum.OrbitalDepositTile, planetLine.IndexOf("orbital_deposit_tile=", StringComparison.Ordinal)},
                {PlanetEnum.NextBuildItemId, planetLine.LastIndexOf("next_build_item_id=", StringComparison.Ordinal)},
                {PlanetEnum.MoonOf, planetLine.IndexOf("moon_of=", StringComparison.Ordinal)},
                {PlanetEnum.IsMoon, planetLine.IndexOf("is_moon=", StringComparison.Ordinal)},
                {PlanetEnum.ColonizeDate, planetLine.IndexOf("colonize_date=", StringComparison.Ordinal)},
                {PlanetEnum.BuildingConstructionQueue, planetLine.IndexOf("building_construction_queue=", StringComparison.Ordinal)},
                {PlanetEnum.BuildQueueItem, planetLine.IndexOf("build_queue_item=", StringComparison.Ordinal)},
                {PlanetEnum.FoodDeficit, planetLine.IndexOf("food_deficit=", StringComparison.Ordinal)},
            };

            var orbitalDepositeTitleStringPosition = planetStructureDictionary[PlanetEnum.OrbitalDepositTile];
            var nextBuildItemIdStringPosition = planetStructureDictionary[PlanetEnum.NextBuildItemId];
            var spaceportStringPosition = planetStructureDictionary[PlanetEnum.Spaceport];

            if (nextBuildItemIdStringPosition > -1 && spaceportStringPosition > -1 && nextBuildItemIdStringPosition < orbitalDepositeTitleStringPosition)
                planetStructureDictionary[PlanetEnum.NextBuildItemId] = -1;

            // Just a verification of NextBuildItemId
            //if (nextBuildItemIdStringPosition > -1 && spaceportStringPosition == -1 && nextBuildItemIdStringPosition < orbitalDepositeTitleStringPosition)
            //{
            //    Debug.WriteLine($"****** PLANETA ENCONTRADO SEM NEXTBUILD FINAL - PLANET ID: {planetId} ******");
            //    Debug.WriteLine($"OrbitalDepositTile Position: {orbitalDepositeTitleStringPosition}");
            //    Debug.WriteLine($"NextBuildItemId Position: {nextBuildItemIdStringPosition}");
            //    Debug.WriteLine($"****************************************************************************");
            //}

            foreach (var planetStructure in planetStructureDictionary.Where(kvp => kvp.Value == -1).ToList())
                planetStructureDictionary.Remove(planetStructure.Key);

            var sortedPlanetStructureDictionary = from entry in planetStructureDictionary orderby entry.Value ascending select entry;

            var planet = new Planet { Id = planetId };

            //foreach (var structure in sortedPlanetStructureDictionary)
            foreach (var structure in sortedPlanetStructureDictionary.Select((value, i) => new { i, value }))
            {
                var nextIndex = structure.i == sortedPlanetStructureDictionary.Count() - 1 ? planetLine.Length - 1 : sortedPlanetStructureDictionary.ElementAt(structure.i + 1).Value;
                var selectedString = StringManager.GetStringByPosition(planetLine, structure.value.Value, nextIndex);

                // Debug test
                //if (selectedString.IndexOf("=", StringComparison.Ordinal) != selectedString.LastIndexOf("=", StringComparison.Ordinal)
                //&& selectedString.IndexOf("coordinate=", StringComparison.Ordinal) == -1
                //&& selectedString.IndexOf("timed_modifier=", StringComparison.Ordinal) == -1
                //&& selectedString.IndexOf("building_construction_queue=", StringComparison.Ordinal) == -1
                //&& selectedString.IndexOf("planet_modifier=", StringComparison.Ordinal) == -1
                //&& selectedString.IndexOf("build_queue_item=", StringComparison.Ordinal) == -1
                //&& selectedString.IndexOf("terraform_process=", StringComparison.Ordinal) == -1
                //&& selectedString.IndexOf("flags=", StringComparison.Ordinal) == -1
                //&& selectedString.IndexOf("pop=", StringComparison.Ordinal) == -1
                //&& selectedString.IndexOf("spaceport=", StringComparison.Ordinal) == -1
                //&& selectedString.IndexOf("tiles", StringComparison.Ordinal) == -1)
                //Debug.WriteLine(selectedString);

                switch (structure.value.Key)
                {
                    case PlanetEnum.Name:
                        planet.Name = StringManager.GetValueBetweenDoubleQuotes(selectedString);
                        break;
                    case PlanetEnum.PlanetClass:
                        planet.PlanetClass = StringManager.GetValueBetweenDoubleQuotes(selectedString);
                        break;
                    case PlanetEnum.Coordinate:
                        DebugLine(planetId, selectedString);
                        planet.Coordinate = StringManager.GetCoordinate(selectedString);
                        break;
                    case PlanetEnum.Orbit:
                        planet.Orbit = StringManager.ClearValue(selectedString, "orbit=");
                        break;
                    case PlanetEnum.PlanetSize:
                        planet.PlanetSize = StringManager.ClearValue(selectedString, "planet_size=");
                        break;
                    case PlanetEnum.FortificationHealth:
                        planet.FortificationHealth = StringManager.ClearValue(selectedString, "fortification_health=");
                        break;
                    case PlanetEnum.LastBombardment:
                        planet.LastBombardment = StringManager.GetValueBetweenDoubleQuotes(selectedString);
                        break;
                    case PlanetEnum.Moons:
                        planet.Moons = StringManager.GetStringList(selectedString);
                        break;
                    case PlanetEnum.HasRing:
                        planet.HasRing = StringManager.ClearValue(selectedString, "has_ring=");
                        break;
                    case PlanetEnum.Owner:
                        planet.Owner = StringManager.ClearValue(selectedString, "owner=");
                        break;
                    case PlanetEnum.OriginalOwner:
                        planet.OriginalOwner = StringManager.ClearValue(selectedString, "original_owner=");
                        break;
                    case PlanetEnum.TerraformedBy:
                        planet.TerraformedBy = StringManager.ClearValue(selectedString, "terraformed_by=");
                        break;
                    case PlanetEnum.Controller:
                        planet.Controller = StringManager.ClearValue(selectedString, "controller=");
                        break;
                    case PlanetEnum.ShipclassOrbitalStation:
                        planet.ShipclassOrbitalStation = StringManager.ClearValue(selectedString, "shipclass_orbital_station=");
                        break;
                    case PlanetEnum.Pop:
                        planet.Pop = StringManager.GetStringList(selectedString);
                        break;
                    case PlanetEnum.ColonyTitle:
                        planet.ColonyTitle = StringManager.ClearValue(selectedString, "colony_tile=");
                        break;
                    case PlanetEnum.Orbitals:
                        planet.Orbitals = StringManager.GetStringList(selectedString);
                        break;
                    case PlanetEnum.Leader:
                        planet.Leader = StringManager.ClearValue(selectedString, "leader=");
                        break;
                    case PlanetEnum.SpaceportStation:
                        planet.SpaceportStation = StringManager.ClearValue(selectedString, "spaceport_station=");
                        break;
                    case PlanetEnum.Flags:
                        planet.Flags = StringManager.GetFlags(selectedString);
                        break;
                    case PlanetEnum.Army:
                        planet.Army = StringManager.GetStringList(selectedString);
                        break;
                    case PlanetEnum.BuiltArmies:
                        planet.BuiltArmies = StringManager.ClearValue(selectedString, "built_armies=");
                        break;
                    case PlanetEnum.OwnSpeciesSlavery:
                        planet.OwnSpeciesSlavery = StringManager.ClearValue(selectedString, "own_species_slavery=");
                        break;
                    case PlanetEnum.TimedModifier:
                        planet.TimedModifierList = StringManager.GetTimedModifier(selectedString);
                        break;
                    case PlanetEnum.MoonOf:
                        planet.MoonOf = StringManager.ClearValue(selectedString, "moon_of=");
                        break;
                    case PlanetEnum.IsMoon:
                        planet.IsMoon = StringManager.ClearValue(selectedString, "is_moon=");
                        break;
                    case PlanetEnum.Entity:
                        planet.Entity = StringManager.ClearValue(selectedString, "entity=");
                        break;
                    case PlanetEnum.EntityName:
                        planet.EntityName = StringManager.GetValueBetweenDoubleQuotes(selectedString);
                        break;
                    case PlanetEnum.ExplicitEntity:
                        planet.ExplicitEntity = StringManager.ClearValue(selectedString, "explicit_entity=");
                        break;
                    case PlanetEnum.TerraformProcess:
                        planet.TerraformProcess = StringManager.GetTerraformProcess(selectedString);
                        break;
                    case PlanetEnum.IsTerraformed:
                        planet.IsTerraformed = StringManager.ClearValue(selectedString, "is_terraformed=");
                        break;
                    case PlanetEnum.GroundSupportStance:
                        planet.GroundSupportStance = StringManager.ClearValue(selectedString, "ground_support_stance=");
                        break;
                    case PlanetEnum.PlanetModifier:
                        planet.PlanetModifier = StringManager.GetValueBetweenDoubleQuotes(selectedString);
                        break;
                    case PlanetEnum.Tiles:
                        //TODO: Complex
                        //planet.Tiles = StringManager.ClearValue(selectedString, "planet_modifier=");
                        break;
                    case PlanetEnum.PreventAnomaly:
                        planet.PreventAnomaly = StringManager.ClearValue(selectedString, "prevent_anomaly=");
                        break;
                    case PlanetEnum.SurveyedBy:
                        planet.SurveyedBy = StringManager.ClearValue(selectedString, "surveyed_by=");
                        break;
                    case PlanetEnum.OrbitalDepositTile:
                        planet.OrbitalDepositTile = StringManager.ClearValue(selectedString, "orbital_deposit_tile=");
                        break;
                    case PlanetEnum.NextBuildItemId:
                        planet.NextBuildItemId = StringManager.ClearValue(selectedString, "next_build_item_id=");
                        break;
                    case PlanetEnum.Spaceport:
                        //TODO: Complex
                        //planet.Spaceport = StringManager.ClearValue(selectedString, "spaceport=");
                        break;
                    case PlanetEnum.ColonizeDate:
                        planet.ColonizeDate = StringManager.GetValueBetweenDoubleQuotes(selectedString);
                        break;
                    case PlanetEnum.BuildingConstructionQueue:
                        //DebugLine(planetId, selectedString);
                        //TODO: Complex
                        //building_construction_queue={{tile=562962838323219time=144type=buildingbuilding={type="building_power_plant_4"}start="2252.07.05"sector=4294967297resources={minerals={127.500 150.000 0.000}}}}
                        planet.BuildingConstructionQueue = StringManager.ClearValue(selectedString, "building_construction_queue=");
                        break;
                    case PlanetEnum.BuildQueueItem:
                        //TODO: Complex
                        //build_queue_item={item={type=shipship_design=83886733}progress=95.400sector=4294967302cost={minerals=652.000}id=55}level=5modules={0=laser_weapon1=solar_panel_network2=corvette_assembly_yards3=observatory4=synchronized_defenses}next_build_item_id=56}edicts={{edict="infrastructure_projects"date="2257.01.09"} }
                        planet.BuildQueueItem = StringManager.ClearValue(selectedString, "build_queue_item=");
                        break;
                    case PlanetEnum.FoodDeficit:
                        planet.FoodDeficit = StringManager.ClearValue(selectedString, "food_deficit=");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return planet;
        }

        private void DebugLine(string planetId, string selectedString)
        {
            Debug.WriteLine("");
            Debug.WriteLine($"==== Planet: {planetId} =====================================================================================");
            Debug.WriteLine(selectedString);
        }

        private void comboBoxChoosePlanet_SelectedIndexChanged(object sender, EventArgs e)
        {
            var nameOfProperty = "Value";
            var propertyInfo = comboBoxChoosePlanet.SelectedItem.GetType().GetProperty(nameOfProperty);
            var value = propertyInfo.GetValue(comboBoxChoosePlanet.SelectedItem, null).ToString();

            var selectedPlanet = PlanetList.FirstOrDefault(x => x.Id == value);

            if (selectedPlanet == null)
            {
                MessageBox.Show(Resources.Main_comboBoxChoosePlanet_SelectedIndexChanged_There_was_an_error_when_loading_the_selected_planet_data);
                return;
            }

            textBoxPlanetId.Text = selectedPlanet.Id;
            textBoxPlanetName.Text = selectedPlanet.Name;
            textBoxPlanetClass.Text = selectedPlanet.PlanetClass;
            textBoxPlanetSize.Text = selectedPlanet.PlanetSize;
            textBoxOrbit.Text = selectedPlanet.Orbit;
            textBoxPlanetSize.Text = selectedPlanet.PlanetSize;
            textBoxFortificationHealth.Text = selectedPlanet.FortificationHealth;
            textBoxLastBombardment.Text = selectedPlanet.LastBombardment;
            textBoxEntity.Text = selectedPlanet.Entity;

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
        Moons,
        HasRing,
        Owner,
        OriginalOwner,
        TerraformedBy,
        Controller,
        ShipclassOrbitalStation,
        Pop,
        ColonyTitle,
        Orbitals,
        Leader,
        Spaceport,
        SpaceportStation,
        Flags,
        Army,
        BuiltArmies,
        TimedModifier,
        Entity,
        EntityName,
        ExplicitEntity,
        TerraformProcess,
        IsTerraformed,
        GroundSupportStance,
        PlanetModifier,
        Tiles,
        PreventAnomaly,
        SurveyedBy,
        OrbitalDepositTile,
        NextBuildItemId,
        MoonOf,
        IsMoon,
        ColonizeDate,
        BuildingConstructionQueue,
        BuildQueueItem,
        FoodDeficit,
        OwnSpeciesSlavery
    }

}
