using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialAgeLauncher;
internal class FileUpdater {
    private static readonly HttpClient httpClient = new HttpClient();

    private static string CalculateMD5(string filePath) {
        using var md5 = System.Security.Cryptography.MD5.Create();
        using var stream = File.OpenRead(filePath);
        var hash = md5.ComputeHash(stream);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }

    public static async void UpdateFilesAsync(string jsonUrl, ProgressBar progressBar, PictureBox playButton, Label updateNoticeLabel) {
        try {
            // Baixa o JSON contendo as informações dos arquivos
            string json = await httpClient.GetStringAsync(jsonUrl);
            var files = JsonConvert.DeserializeObject<FileUpdate[]>(json);

            // Filtra apenas os arquivos com update = true
            var filesToUpdate = files!=null ? files.Where(file => file.Update).ToArray() : [];
            if(filesToUpdate.Length>0) {
                // Configura a barra de progresso
                progressBar.Maximum=filesToUpdate.Length;
                progressBar.Value=0;
                // Diretório do executável
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                foreach(var file in filesToUpdate) {
                    string installPath = Path.Combine(baseDir, file.InstallDir);
                    // Verifica se o arquivo já existe e se a hash coincide
                    if(File.Exists(installPath)) {
                        string existingFileHash = CalculateMD5(installPath);
                        if(existingFileHash==file.Hash) {
                            // Hashes coincidem; pula o download deste arquivo
                            progressBar.Invoke((Action)(() => progressBar.Value++));
                            continue;
                        }
                    }
                    progressBar.Visible=true;
                    updateNoticeLabel.Text="Baixando atualizações...";
                    // Cria o diretório, se necessário
                    string directory = Path.GetDirectoryName(installPath)??string.Empty;
                    if(!String.IsNullOrEmpty(directory)) {
                        Directory.CreateDirectory(directory);
                    }
                    // Baixa o arquivo e salva no caminho especificado
                    using(var response = await httpClient.GetAsync(file.FileUrl)) {
                        response.EnsureSuccessStatusCode();
                        using var fileStream = new FileStream(installPath, FileMode.Create, FileAccess.Write, FileShare.None);
                        await response.Content.CopyToAsync(fileStream);
                    }
                    // Atualiza a barra de progresso
                    progressBar.Invoke((Action)(() => progressBar.Value++));
                }
                //MessageBox.Show("All files updated successfully!");
            }
        } catch(Exception) {
            progressBar.Visible=false;
            playButton.Visible=true;
            //MessageBox.Show($"An error occurred: {ex.Message}");
        } finally {
            progressBar.Visible=false;
            playButton.Visible=true;
            updateNoticeLabel.Visible=false;
        }
    }
}

public class FileUpdate {
    public string FileUrl { get; set; } = string.Empty;
    public string InstallDir { get; set; } = string.Empty;
    public bool Update { get; set; }
    public string Hash { get; set; } = string.Empty;
}
