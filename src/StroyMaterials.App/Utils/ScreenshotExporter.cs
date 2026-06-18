using StroyMaterials.App.Models;

namespace StroyMaterials.App;

internal static class ScreenshotExporter
{
    public static void Export(string outputDirectory)
    {
        Directory.CreateDirectory(outputDirectory);

        Capture(new LoginForm(), Path.Combine(outputDirectory, "01_login.png"));
        Capture(new MainForm(UserSession.Guest), Path.Combine(outputDirectory, "02_guest_products.png"));
        Capture(new MainForm(new UserSession
        {
            UserId = 1,
            FullName = "Ворсин Петр Евгеньевич",
            RoleName = "Администратор"
        }), Path.Combine(outputDirectory, "03_admin_products.png"));
        Capture(new OrderListForm(new UserSession
        {
            UserId = 1,
            FullName = "Ворсин Петр Евгеньевич",
            RoleName = "Администратор"
        }), Path.Combine(outputDirectory, "04_orders.png"));
    }

    private static void Capture(Form form, string path)
    {
        using (form)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-32000, -32000);
            form.ShowInTaskbar = false;
            form.Show();
            form.Refresh();
            Application.DoEvents();

            using var bitmap = new Bitmap(form.Width, form.Height);
            form.DrawToBitmap(bitmap, new Rectangle(Point.Empty, form.Size));
            bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            form.Close();
        }
    }
}
