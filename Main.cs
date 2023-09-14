using System;
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

        private View _loginView;
        private Button _loginButton;

        private View _presentationView;

        private int _currentImageIndex = 0;
        private TextLabel _text;
        private ImageView _image;
        private Button _nextButton;
        private Button _prevButton;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        private void Initialize()
        {
            var window = Window.Instance;
            window.KeyEvent += OnKeyEvent;
            CreateLoginView(window);
            CreatePresentationView(window);

            _loginView.Show();
            FocusManager.Instance.SetCurrentFocusView(_loginButton);
        }

        private void CreatePresentationView(Window window)
        {
            _presentationView = new View
            {
                Size = new Size(window.Size.Width, window.Size.Height),
                Position = new Position(0, 0),
                BackgroundColor = Color.Black,
                Focusable = true,
                Layout = new AbsoluteLayout(),
            };

            _image = new ImageView
            {
                Size = new Size(window.Size.Width, window.Size.Height),  // Reserving 100 pixels for buttons at the bottom
                Position2D = new Position2D(0, 0),
                BackgroundColor = Color.Black,
            };
            _presentationView.Add(_image);

            _text = new TextLabel("<No Image>")
            {
                Size = new Size(window.Size.Width, 100),
                Position = new Position(0, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                TextColor = Color.White,
            };
            _presentationView.Add(_text);

            _prevButton = new Button
            {
                Text = "Prev",
                Size = new Size(window.Size.Width / 2, 100),
                Position = new Position(0, window.Size.Height - 100),
                BackgroundColor = new Color(0, 0, 0, 0.5f),
                TextColor = Color.White,
            };
            _prevButton.Clicked += OnPrev;
            _presentationView.Add(_prevButton);

            _nextButton = new Button
            {
                Text = "Next",
                Size = new Size(window.Size.Width / 2, 100),
                Position = new Position(window.Size.Width / 2, window.Size.Height - 100),
                BackgroundColor = new Color(0, 0, 0, 0.5f),
                TextColor = Color.White,
            };
            _nextButton.Clicked += OnNext;
            _presentationView.Add(_nextButton);

            _prevButton.RightFocusableView = _nextButton;
            _nextButton.LeftFocusableView = _prevButton;

            _presentationView.Hide();
            window.Add(_presentationView);
        }

        private void CreateLoginView(Window window)
        {
            _loginView = new View
            {
                Size = new Size(window.Size.Width, window.Size.Height),
                Position = new Position(0, 0),
                BackgroundColor = Color.Black,
                Focusable = true,
                Layout = new AbsoluteLayout()
            };

            _loginButton = new Button
            {
                Text = "Login",
                Position = new Position(window.Size.Width / 4, window.Size.Height / 2),
                Size = new Size(window.Size.Width / 2, 100),
                BackgroundColor = new Color(0, 0, 0, 0.5f),
                TextColor = Color.White,
            };
            _loginButton.Clicked += OnLogin;
            _loginView.Add(_loginButton);

            _loginView.Hide();
            window.Add(_loginView);
        }

        private void OnLogin(object sender, EventArgs e)
        {
            _loginView.Hide();
            _presentationView.Show();
            FocusManager.Instance.SetCurrentFocusView(_nextButton);
            UpdateCurrentImage();
        }

        private void UpdateCurrentImage()
        {
            var image = Images[_currentImageIndex];
            _image.ResourceUrl = System.IO.Path.Join(DirectoryInfo.Resource, image);
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
