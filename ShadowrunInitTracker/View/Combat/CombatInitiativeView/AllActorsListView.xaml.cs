using ShadowrunInitTracker.Model;
using ShadowrunInitTracker.ViewModel;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShadowrunInitTracker.View
{
    /// <summary>
    /// Interaction logic for PhasePassListBox.xaml
    /// </summary>
    public partial class AllActorsListView : UserControl
    {
        CombatViewModel vm { get { return DataContext as CombatViewModel; } }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(AllActorsListView));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(AllActorsListView));
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public AllActorsListView()
        {
            InitializeComponent();
        }

        public static readonly RoutedUICommand AddActorCmd = new RoutedUICommand(
            "Add Actor To Combat", "AddActorCmd", typeof(AllActorsListView));
        private void AddActor(object sender, ExecutedRoutedEventArgs e)
        {
            //todo implement
        }

        public static readonly RoutedUICommand EditActorCmd = new RoutedUICommand(
            "Add Actor To Combat", "AddActorCmd", typeof(AllActorsListView));
        private void EditActor(object sender, ExecutedRoutedEventArgs e)
        {
            //todo implement
        }

        public static readonly RoutedUICommand RemoveActorCmd = new RoutedUICommand(
            "Remove Actor From Combat", "RemoveActorCmd", typeof(AllActorsListView));
        private void RemoveActor(object sender, ExecutedRoutedEventArgs e)
        {
            vm.Combat.RemoveActor((e.Parameter as ActorInitiativeEntry).Source as Actor);
        }
    }
}
