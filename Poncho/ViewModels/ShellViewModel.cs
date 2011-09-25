namespace Poncho.ViewModels
{
    using Caliburn.Micro;

    public class ShellViewModel : PropertyChangedBase
    {
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    RaisePropertyChangedEventImmediately("Title");
                }
            }
        }

        public ShellViewModel()
        {
            Title = "Hello Caliburn.Micro";
        }
    }
}
