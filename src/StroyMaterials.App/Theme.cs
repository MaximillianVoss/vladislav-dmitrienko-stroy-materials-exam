using System.Drawing.Drawing2D;

namespace StroyMaterials.App;

internal static class Theme
{
    public static readonly Color MainBackground = Color.White;
    public static readonly Color SecondaryBackground = ColorTranslator.FromHtml("#DAA520");
    public static readonly Color Accent = ColorTranslator.FromHtml("#B8860B");
    public static readonly Color DiscountBackground = ColorTranslator.FromHtml("#F4A460");
    public static readonly Color OutOfStockBackground = Color.LightBlue;

    public static Font Regular(float size = 10F) => new("Calibri", size, FontStyle.Regular);

    public static Font Bold(float size = 10F) => new("Calibri", size, FontStyle.Bold);

    public static Button AccentButton(string text)
    {
        return new Button
        {
            Text = text,
            BackColor = Accent,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = Bold(),
            Height = 34,
            Margin = new Padding(4)
        };
    }

    public static void ApplyForm(Form form, string title)
    {
        form.Text = title;
        form.BackColor = MainBackground;
        form.Font = Regular();
        form.StartPosition = FormStartPosition.CenterScreen;
        form.MinimumSize = new Size(900, 560);

        if (File.Exists(AppPaths.AppIcon))
        {
            form.Icon = new Icon(AppPaths.AppIcon);
        }
    }

    public static void StyleGrid(DataGridView grid)
    {
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.BackgroundColor = MainBackground;
        grid.BorderStyle = BorderStyle.None;
        grid.ColumnHeadersDefaultCellStyle.BackColor = SecondaryBackground;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        grid.ColumnHeadersDefaultCellStyle.Font = Bold();
        grid.DefaultCellStyle.SelectionBackColor = Accent;
        grid.DefaultCellStyle.SelectionForeColor = Color.White;
        grid.EnableHeadersVisualStyles = false;
        grid.MultiSelect = false;
        grid.ReadOnly = true;
        grid.RowHeadersVisible = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    }

    public static Image ResizeImage(Image image, int width, int height)
    {
        var bitmap = new Bitmap(width, height);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.Clear(Color.White);
        graphics.DrawImage(image, 0, 0, width, height);
        return bitmap;
    }
}
