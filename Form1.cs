using System.Diagnostics;
using System.Windows.Forms;

namespace ImperialAgeLauncher;

public partial class Form1 : Form {

    private static bool dragging = false;
    private static Point dragCursorPoint;
    private static Point dragFormPoint;

    private void Background_MouseDown(object? sender, MouseEventArgs e) {
        dragging=true;
        dragCursorPoint=Cursor.Position;
        dragFormPoint=this.Location;
    }

    private void Background_MouseMove(object? sender, MouseEventArgs e) {
        if(dragging) {
            Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
            this.Location=Point.Add(dragFormPoint, new Size(diff));
        }
    }

    private void Background_MouseUp(object? sender, MouseEventArgs e) {
        dragging=false;
    }


    public Form1() {
        InitializeComponent();
        this.StartPosition=FormStartPosition.CenterScreen;
        this.FormBorderStyle=FormBorderStyle.None;
        this.Region=Region.FromHrgn(Utility.CreateRoundRectRgn(0, 0, this.Width, this.Height, 15, 15));
        this.background.MouseDown+=this.Background_MouseDown;
        this.background.MouseMove+=this.Background_MouseMove;
        this.background.MouseUp+=this.Background_MouseUp;

    }

    private void Form1_Load(object sender, EventArgs e) {
        closeButton.Parent=background;
        playButton.Parent=background;
        websiteButton.Parent=background;
        discordButton.Parent=background;
        FileUpdater.UpdateFilesAsync("https://imperialage.juvhost.com/data.json", progressBar1, playButton);
    }

    private void CloseButton_MouseEnter(object sender, EventArgs e) {
        closeButton.Image=Properties.Resources.exit2;
    }

    private void CloseButton_MouseLeave(object sender, EventArgs e) {
        closeButton.Image=Properties.Resources.exit1;
    }

    private void CloseButton_Click(object sender, EventArgs e) {
        DialogResult result = MessageBox.Show(
            "Você tem certeza que deseja cancelar?",
            "Confirmar saída",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );
        if(result==DialogResult.Yes) {
            Application.Exit();
        }
    }

    private void PlayButton_MouseEnter(object sender, EventArgs e) {
        playButton.Image=Properties.Resources.jogar2;
    }

    private void PlayButton_MouseLeave(object sender, EventArgs e) {
        playButton.Image=Properties.Resources.jogar1;
    }

    private void WebsiteButton_MouseEnter(object sender, EventArgs e) {
        websiteButton.Image=Properties.Resources.website2;
    }

    private void WebsiteButton_MouseLeave(object sender, EventArgs e) {
        websiteButton.Image=Properties.Resources.website1;
    }

    private void WebsiteButton_Click(object sender, EventArgs e) {
        Process.Start(new ProcessStartInfo("https://imperialage.com.br/") { UseShellExecute=true });
    }

    private void DiscordButton_MouseEnter(object sender, EventArgs e) {
        discordButton.Image=Properties.Resources.discord2;
    }

    private void DiscordButton_MouseLeave(object sender, EventArgs e) {
        discordButton.Image=Properties.Resources.discord1;
    }

    private void DiscordButton_Click(object sender, EventArgs e) {
        Process.Start(new ProcessStartInfo("https://discord.gg/AsY7kv8H") { UseShellExecute=true });
    }
}
