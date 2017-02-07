using System;
using System.IO;
using System.Windows.Forms;
using StellarisSaveGameEditor.Properties;
using System.IO.Compression;

namespace StellarisSaveGameEditor
{
    public partial class Main : Form
    {
        public string GamestateString { get; set; }
        public string MetaString { get; set; }
        public string ZipFileName { get; set; }
        public string InitialDirectory { get; set; }

        public Main()
        {
            InitializeComponent();

            saveToolStripMenuItem.Enabled = false;
            InitialDirectory = @"C:\Users\paulo\OneDrive\Documentos\Paradox Interactive\Stellaris\save games\gatineofoundation_-154020303";
            //InitialDirectory = "c:\\"
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
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
