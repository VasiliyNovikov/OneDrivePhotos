using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace OneDrivePhotos
{
    class Program : NUIApplication
    {
        private static readonly string[] Images =
        {
            "mass-effect.jpg",
            "halo-infinite.jpg",
            "starfield.jpg",
        };

        private int _currentImageIndex = 0;
        private TextLabel _text;
        private ImageView _imageView;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        private void Initialize()
        {
            var window = Window.Instance;
            window.KeyEvent += OnKeyEvent;
            CreateUI(window);
        }

        private void CreateUI(Window window)
        {
            _imageView = new ImageView
            {
                Size = new Size(window.Size.Width, window.Size.Height),  // Reserving 100 pixels for buttons at the bottom
                Position2D = new Position2D(0, 0),
                ResourceUrl = Images[_currentImageIndex]
            };
            window.Add(_imageView);

            _text = new TextLabel("<No Image>")
            {
                Size = new Size(window.Size.Width, 100),
                Position = new Position(0, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                TextColor = Color.White,
            };
            window.Add(_text);

            var prevButton = new Button
            {
                Text = "Prev",
                Size = new Size(window.Size.Width / 2, 100),
                Position = new Position(0, window.Size.Height - 100),
                BackgroundColor = new Color(0, 0, 0, 0.5f),
                TextColor = Color.White,
            };
            prevButton.Clicked += OnPrev;
            window.Add(prevButton);

            var nextButton = new Button
            {
                Text = "Next",
                Size = new Size(window.Size.Width / 2, 100),
                Position = new Position(window.Size.Width / 2, window.Size.Height - 100),
                BackgroundColor = new Color(0, 0, 0, 0.5f),
                TextColor = Color.White,
            };
            nextButton.Clicked += OnNext;
            window.Add(nextButton);

            prevButton.RightFocusableView = nextButton;
            nextButton.LeftFocusableView = prevButton;

            FocusManager.Instance.SetCurrentFocusView(nextButton);

            UpdateCurrentImage();
        }

        private void UpdateCurrentImage()
        {
            var image = Images[_currentImageIndex];
            _imageView.ResourceUrl = System.IO.Path.Join(DirectoryInfo.Resource, image);
            _text.Text = image;
        }

        private void OnPrev(object sender, ClickedEventArgs e)
        {
            -- _currentImageIndex;
            if (_currentImageIndex < 0)
                _currentImageIndex = Images.Length - 1;
            UpdateCurrentImage();
        }

        private void OnNext(object sender, ClickedEventArgs e)
        {
            ++_currentImageIndex;
            if (_currentImageIndex >= Images.Length)
                _currentImageIndex = 0;
            UpdateCurrentImage();
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                switch (e.Key.KeyPressedName)
                {
                    case "XF86Back":
                    case "Escape":
                        Exit();
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
