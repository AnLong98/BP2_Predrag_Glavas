using Client.Commands.RunnerCommands;
using Client.Contracts;
using Client.Helpers;
using Service.Model;
using Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModel
{
    public class RunnerViewModel : INotifyPropertyChanged, IViewModelBase
    {
        #region constructor
        public RunnerViewModel(RunnersService runnerService)
        {
            RunnerService = runnerService;
            AddRunnerCommand = new AddRunnerCommand(this, runnerService);
            UpdateRunnerCommand = new UpdateRunnerCommand(this, runnerService);
            DeleteRunnerCommand = new DeleteRunnerCommand(this, runnerService);
            GetRunnerExpensesCommand = new GetRunnerExpensesCommand(runnerService);
            LoadAll();
        }
        #endregion

        #region property changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region get set
        public RunnersService RunnerService { get; set; }

        internal void SelectedChanged(Runner runner)
        {
            Runner = runner;
        }
        #endregion

        #region commands
        public ICommand AddRunnerCommand { get; set; }
        public ICommand UpdateRunnerCommand { get; set; }
        public ICommand DeleteRunnerCommand { get; set; }
        public ICommand GetRunnerExpensesCommand { get; set; }

        public ICommand ChangeSelectedRunnerCommand { get; set; }
        #endregion

        #region binding 
        public Runner runner = new Runner();
        public Runner Runner
        {
            get
            {
                return runner;
            }
            set
            {
                if (value == null)
                    return;
                runner = value;
                OnPropertyChanged("Runner");
            }
        }

        public Runner selected = new Runner();
        public Runner Selected
        {
            get
            {
                return selected;
            }
            set
            {
                if (value == null)
                    return;
                selected = value;
                selected.Registrations = null;
                Runner = CopyHelper.DeepCopy<Runner>(selected);
                OnPropertyChanged("Selected");
            }
        }


        private BindingList<Runner> runners;
        public BindingList<Runner> AllRunners
        {
            get
            {
                return runners;
            }
            set
            {
                runners = value;
                OnPropertyChanged("AllRunners");
            }
        }
        #endregion

        public void LoadAll()
        {
            AllRunners = new BindingList<Runner>(RunnerService.GetAll());
        }
    }
}
