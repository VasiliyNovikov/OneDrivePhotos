using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace OneDrivePhotos
{
    internal class LoginView : View
    {
        public LoginView(Window window, string code, string url)
        {
            Size = new Size(window.Size.Width, window.Size.Height);
            Position = new Position(0, 0);
            BackgroundColor = Color.Black;
            Layout = new LinearLayout
            {
                LinearOrientation = LinearLayout.Orientation.Vertical,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                CellPadding = new Size2D(20, 20),
            };

            var codeLabel = new TextLabel(code)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                TextColor = Color.White,
            };
            Add(codeLabel);

            var urlLabel = new TextLabel(url)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                TextColor = Color.White,
            };
            Add(urlLabel);

            window.Add(this);
        }
    }
}
