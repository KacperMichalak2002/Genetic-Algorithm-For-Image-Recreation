using System.Windows.Input;

namespace Genetic_Algorithm_For_Image_Recreation.MVVM
{
    internal class RelayCommand : ICommand
    {
        private Action<Object> execute;
        private Func<object, bool> canExecute;


        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value;}
        }

        public RelayCommand(Action<Object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }
}
