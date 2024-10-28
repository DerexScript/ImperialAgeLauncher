using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialAgeLauncher;
internal class FileUpdater {
    private static readonly HttpClient httpClient = new HttpClient();

    public static async void UpdateFilesAsync(string jsonUrl, ProgressBar progressBar, PictureBox playButton) {
        try {
            // Baixa o JSON contendo as informações dos arquivos
            string json = await httpClient.GetStringAsync(jsonUrl);
            var files = JsonConvert.DeserializeObject<FileUpdate[]>(json);

            // Filtra apenas os arquivos com update = true
            var filesToUpdate = files.Where(file => file.update).ToArray();
            if(filesToUpdate.Length>0) {


                // Configura a barra de progresso
                progressBar.Maximum=filesToUpdate.Length;
                progressBar.Value=0;

                // Diretório do executável
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;

                foreach(var file in filesToUpdate) {
                    string installPath = Path.Combine(baseDir, file.installDir);

                    // Cria o diretório, se necessário
                    string directory = Path.GetDirectoryName(installPath);
                    if(!Directory.Exists(directory)) {
                        Directory.CreateDirectory(directory);
                    }

                    // Baixa o arquivo e salva no caminho especificado
                    using(var response = await httpClient.GetAsync(file.fileUrl)) {
                        response.EnsureSuccessStatusCode();
                        using var fileStream = new FileStream(installPath, FileMode.Create, FileAccess.Write, FileShare.None);
                        await response.Content.CopyToAsync(fileStream);
                    }

                    // Atualiza a barra de progresso
                    progressBar.Invoke((Action)(() => progressBar.Value++));
                }

                //MessageBox.Show("All files updated successfully!");
            }
        } catch(Exception ex) {
            progressBar.Visible=false;
            playButton.Visible=true;
            //MessageBox.Show($"An error occurred: {ex.Message}");
        } finally {
            progressBar.Visible=false;
            playButton.Visible=true;
        }
    }
}

public class FileUpdate {
    public string fileUrl { get; set; }
    public string installDir { get; set; }
    public bool update { get; set; }
}
