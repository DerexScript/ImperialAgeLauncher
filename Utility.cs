using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ImperialAgeLauncher;
internal static class Utility {
    [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

    public static async Task EditClassicUOSettings() {
        await Task.Run(() => {
            try {
                // Obtém o diretório do executável atual
                string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string settingsFilePath = Path.Combine(currentDirectory, "ClassicUO", "settings.json");

                // Verifica se o arquivo settings.json existe
                if(!File.Exists(settingsFilePath)) {
                    // Se o arquivo não existir, cria com o valor padrão
                    var defaultSettings = new {
                        username = "",
                        password = "",
                        ip = "login.imperialage.com.br",
                        port = 2593,
                        ignore_relay_ip = false,
                        ultimaonlinedirectory = currentDirectory.TrimEnd('\\'), // Define o diretório do executável sem barra final
                        profilespath = "",
                        clientversion = "7.0.91.15",
                        lang = "IVL",
                        lastservernum = 1,
                        last_server_name = "",
                        fps = 250,
                        window_position = new { X = 0, Y = 0 },
                        window_size = new { X = 2560, Y = 1017 },
                        is_win_maximized = true,
                        saveaccount = false,
                        autologin = false,
                        reconnect = false,
                        reconnect_time = 1000,
                        login_music = true,
                        login_music_volume = 50,
                        fixed_time_step = true,
                        run_mouse_in_separate_thread = true,
                        force_driver = 0,
                        use_verdata = false,
                        maps_layouts = (object?)null,
                        encryption = 0,
                        plugins = new[] { "Razor\\Razor.exe" }
                    };

                    // Serializa o valor padrão e cria o arquivo settings.json
                    string jsonContent = JsonSerializer.Serialize(defaultSettings, new JsonSerializerOptions { WriteIndented=true });
                    File.WriteAllText(settingsFilePath, jsonContent);

                    // Saída da função após criar o arquivo
                    return;
                }

                // Lê o conteúdo do arquivo settings.json
                string existingJsonContent = File.ReadAllText(settingsFilePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive=true };
                var settings = JsonSerializer.Deserialize<Dictionary<string, object>>(existingJsonContent, options);

                if(settings==null) {
                    MessageBox.Show("Erro ao ler o arquivo settings.json.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Atualiza o atributo ip se necessário
                if(settings.ContainsKey("ip")&&settings["ip"]?.ToString()!="login.imperialage.com.br") {
                    settings["ip"]="login.imperialage.com.br";
                }

                // Atualiza o atributo clientversion se necessário
                if(settings.ContainsKey("clientversion")&&settings["clientversion"]?.ToString()!="7.0.91.15") {
                    settings["clientversion"]="7.0.91.15";
                }

                // Atualiza o atributo ultimaonlinedirectory se necessário
                if(settings.ContainsKey("ultimaonlinedirectory")&&settings["ultimaonlinedirectory"]?.ToString()!=currentDirectory) {
                    settings["ultimaonlinedirectory"]=currentDirectory.TrimEnd('\\');
                }

                // Atualiza o atributo plugins, removendo "ClassicAssist\\ClassicAssist.dll" se existir
                if(settings.ContainsKey("plugins")&&settings["plugins"] is JsonElement pluginsElement&&pluginsElement.ValueKind==JsonValueKind.Array) {
                    var pluginsList = JsonSerializer.Deserialize<List<string>>(pluginsElement.GetRawText(), options);
                    if(pluginsList!=null) {
                        // Remove "ClassicAssist\\ClassicAssist.dll" se estiver na lista
                        pluginsList.RemoveAll(p => p=="ClassicAssist\\ClassicAssist.dll");

                        // Atualiza o atributo plugins no settings
                        settings["plugins"]=pluginsList;
                    }
                }

                // Salva as mudanças de volta no arquivo settings.json
                string updatedJsonContent = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented=true });
                File.WriteAllText(settingsFilePath, updatedJsonContent);

            } catch(Exception ex) {
                MessageBox.Show($"Ocorreu um erro ao atualizar o arquivo settings.json: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        });
    }


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
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string settingsFilePath = Path.Combine(appDataPath, "ClassicUOLauncher", "launcher_settings.xml");
            string arguments = $"-plugin \"Data\\Plugins\\Razor\\Razor.exe\" -ip \"login.imperialage.com.br\" -port \"2593\" -clientversion \"7.0.91.15\"";

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

            // Obtém o diretório do executável (diretório do Ultima Online) sem a barra final
            string installDirectory = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');

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
                        string serverAttribute = (string?)profile.Attribute("server")??"";
                        // Se o atributo server tiver o valor "190.2.72.35", altera para "login.imperialage.com.br"
                        if(serverAttribute=="190.2.72.35") {
                            profile.SetAttributeValue("server", "login.imperialage.com.br");
                            serverAttribute="login.imperialage.com.br"; // Atualiza o valor para a próxima verificação
                        }

                        if(serverAttribute=="login.imperialage.com.br") {
                            profileExists=true;

                            // Verifica se o atributo client_version está vazio ou diferente de "7.0.91.15"
                            var clientVersionAttribute = profile.Attribute("client_version");
                            if(clientVersionAttribute==null||string.IsNullOrWhiteSpace(clientVersionAttribute.Value)||clientVersionAttribute.Value!="7.0.91.15") {
                                profile.SetAttributeValue("client_version", "7.0.91.15");
                            }

                            // Verifica se o elemento "plugins" existe
                            var pluginsElement = profile.Element("plugins");
                            if(pluginsElement!=null) {
                                // Verifica se o plugin "ClassicAssist\ClassicAssist.dll" existe
                                var classicAssistPlugin = pluginsElement.Elements("plugin")
                                    .FirstOrDefault(p => (string?)p.Attribute("path")=="ClassicAssist\\ClassicAssist.dll");

                                if(classicAssistPlugin!=null) {
                                    // Remove o plugin
                                    classicAssistPlugin.Remove();
                                }
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
            new XAttribute("server", "login.imperialage.com.br"),
            new XAttribute("port", "2593"),
            new XAttribute("charname", ""),
            new XAttribute("client_version", "7.0.91.15"),
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
                )/*,
                new XElement("plugin",
                    new XAttribute("path", "ClassicAssist\\ClassicAssist.dll"),
                    new XAttribute("enabled", "False") // Define o valor padrão como "False"
                )
                */
            )
        );
    }


}
