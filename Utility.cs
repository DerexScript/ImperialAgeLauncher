using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ImperialAgeLauncher;
internal static class Utility {
    [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

    public static void RunClassicUO(PictureBox playButton, Form mainForm) {
        try {
            // Desativa o botão de jogo
            playButton.Enabled=false;

            // Minimiza o formulário principal
            mainForm.WindowState=FormWindowState.Minimized;

            // Obtém o diretório do executável atual
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Caminho completo para ClassicUO.exe
            string classicUOPath = Path.Combine(currentDirectory, "ClassicUO", "ClassicUO.exe");

            // Verifica se o arquivo existe
            if(!File.Exists(classicUOPath)) {
                MessageBox.Show("O arquivo ClassicUO.exe não foi encontrado no diretório esperado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                playButton.Enabled=true; // Reativa o botão
                mainForm.WindowState=FormWindowState.Normal; // Restaura a janela principal
                return;
            }

            // Configura os argumentos
            string arguments = "-plugin \"Data\\Plugins\\Razor\\Razor.exe\" "+
                               "-plugin \"Data\\Plugins\\ClassicAssist\\ClassicAssist.dll\" "+
                               "-ip \"190.2.72.35\" -port \"2593\"";

            // Configura o processo
            var processStartInfo = new ProcessStartInfo {
                FileName=classicUOPath,
                Arguments=arguments,
                WorkingDirectory=Path.GetDirectoryName(classicUOPath)??currentDirectory, // Define o diretório de trabalho
                UseShellExecute=false, // Não usa o shell do sistema para executar o processo
                CreateNoWindow=true // Não cria uma janela do console
            };

            // Inicia o processo
            using var process = new Process { StartInfo=processStartInfo };
            if(process.Start()) {
                // MessageBox.Show("ClassicUO foi iniciado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Você pode aguardar o processo iniciar completamente se necessário
                process.WaitForInputIdle();

                playButton.Enabled=true; // Reativa o botão após o jogo abrir
            } else {
                MessageBox.Show("Falha ao iniciar o ClassicUO.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                playButton.Enabled=true; // Reativa o botão
                mainForm.WindowState=FormWindowState.Normal; // Restaura a janela principal
            }
        } catch(Exception ex) {
            // Log do erro e exibição de uma mensagem para o usuário
            MessageBox.Show($"Ocorreu um erro ao tentar executar o ClassicUO: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            playButton.Enabled=true; // Reativa o botão
            mainForm.WindowState=FormWindowState.Normal; // Restaura a janela principal
        }
    }

    public static async Task EnsureLauncherSettings() {
        await Task.Run(() => {
            // Caminho do diretório e do arquivo
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string launcherDirectory = Path.Combine(appDataPath, "ClassicUOLauncher");
            string settingsFilePath = Path.Combine(launcherDirectory, "launcher_settings.xml");

            // Obtém o diretório do executável (diretório do Ultima Online)
            string installDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Verifica se o diretório existe, se não cria o diretório
            if(!Directory.Exists(launcherDirectory)) {
                Directory.CreateDirectory(launcherDirectory);
            }

            // Verifica se o arquivo existe
            if(!System.IO.File.Exists(settingsFilePath)) {
                // Cria o arquivo com o conteúdo padrão
                var defaultSettings = CreateDefaultSettingsXml(installDirectory);
                defaultSettings.Save(settingsFilePath);
            } else {
                // Abre e verifica o arquivo existente
                var settingsXml = XDocument.Load(settingsFilePath);
                var profiles = settingsXml.Root?.Element("profiles");

                bool profileExists = false;

                if(profiles!=null) {
                    foreach(var profile in profiles.Elements("profile")) {
                        if(((string?)profile.Attribute("server")??"")=="190.2.72.35") {
                            profileExists=true;

                            // Verifica se os plugins estão configurados corretamente
                            var pluginsElement = profile.Element("plugins");
                            if(pluginsElement==null) {
                                // Se não existir o elemento "plugins", cria e adiciona
                                pluginsElement=new XElement("plugins");
                                profile.Add(pluginsElement);
                            }

                            // Verifica se os plugins "ClassicAssist.dll" e "Razor.exe" estão presentes
                            bool classicAssistExists = pluginsElement.Elements("plugin")
                                .Any(p => (string?)p.Attribute("path")=="ClassicAssist\\ClassicAssist.dll"&&
                                          (string?)p.Attribute("enabled")=="True");

                            bool razorExists = pluginsElement.Elements("plugin")
                                .Any(p => (string?)p.Attribute("path")=="Razor\\Razor.exe"&&
                                          (string?)p.Attribute("enabled")=="True");

                            // Adiciona ou corrige os plugins se necessário
                            if(!classicAssistExists) {
                                pluginsElement.Add(new XElement("plugin",
                                    new XAttribute("path", "ClassicAssist\\ClassicAssist.dll"),
                                    new XAttribute("enabled", "True")));
                            }

                            if(!razorExists) {
                                pluginsElement.Add(new XElement("plugin",
                                    new XAttribute("path", "Razor\\Razor.exe"),
                                    new XAttribute("enabled", "True")));
                            }

                            break;
                        }
                    }

                    if(!profileExists) {
                        // Adiciona o novo profile se não existir
                        var newProfile = CreateProfileElement(installDirectory);
                        profiles.Add(newProfile);
                    }

                    // Salva as mudanças no arquivo XML
                    settingsXml.Save(settingsFilePath);
                }
            }
        });
    }

    private static XDocument CreateDefaultSettingsXml(string installDirectory) {
        return new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            new XElement("settings",
                new XAttribute("close_on_launch", "False"),
                new XAttribute("use_preview_package", "True"),
                new XAttribute("auto_apply_updates", "True"),
                new XAttribute("last_profile_name", "Imperial Age"),
                new XAttribute("driver_type", "0"),
                new XElement("profiles",
                    CreateProfileElement(installDirectory)
                )
            )
        );
    }

    private static XElement CreateProfileElement(string installDirectory) {
        return new XElement("profile",
            new XAttribute("name", "Imperial Age"),
            new XAttribute("cuo_path", ""),
            new XAttribute("username", ""),
            new XAttribute("password", ""),
            new XAttribute("server", "190.2.72.35"),
            new XAttribute("port", "2593"),
            new XAttribute("charname", ""),
            new XAttribute("client_version", ""),
            new XAttribute("uo_protocol", "0"),
            new XAttribute("uopath", installDirectory),
            new XAttribute("server_type", "0"),
            new XAttribute("last_server_index", "255"),
            new XAttribute("last_server_name", ""),
            new XAttribute("debug", "False"),
            new XAttribute("profiler", "False"),
            new XAttribute("save_account", "False"),
            new XAttribute("skip_login_screen", "False"),
            new XAttribute("autologin", "False"),
            new XAttribute("reconnect", "False"),
            new XAttribute("reconnect_time", "1000"),
            new XAttribute("has_music", "True"),
            new XAttribute("high_dpi", "False"),
            new XAttribute("use_verdata", "False"),
            new XAttribute("music_volume", "50"),
            new XAttribute("encryption_type", "0"),
            new XAttribute("force_driver", "0"),
            new XAttribute("packet_log", "False"),
            new XAttribute("args", ""),
            new XElement("plugins",
                new XElement("plugin",
                    new XAttribute("path", "Razor\\Razor.exe"),
                    new XAttribute("enabled", "True")
                ),
                new XElement("plugin",
                    new XAttribute("path", "ClassicAssist\\ClassicAssist.dll"),
                    new XAttribute("enabled", "True")
                )
            )
        );
    }

}
