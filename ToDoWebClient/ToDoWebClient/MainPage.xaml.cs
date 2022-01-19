using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ToDoWeb
{
   /// <summary>
   /// An empty page that can be used on its own or navigated to within a Frame.
   /// </summary>
   public sealed partial class MainPage : Page
   {
      private readonly ItemListViewModel ViewModel;

      public MainPage()
      {
         InitializeComponent();
         DataContext = ViewModel = new ItemListViewModel();
      }

      private void AddItem_Click(object sender, RoutedEventArgs e)
      {
         ViewModel.AddItem();
      }

      private void EditItem_Click(object sender, RoutedEventArgs e)
      {
         ViewModel.EditItem();
      }

      private void DeleteItem_Click(object sender, RoutedEventArgs e)
      {
         ViewModel.DeleteItem();
      }

      private void Search_Click(object sender, RoutedEventArgs e)
      {
         ViewModel.Search(textBoxSearchTerm.Text);
      }

      private void Save_Click(object sender, RoutedEventArgs e)
      {
      }
   }
}