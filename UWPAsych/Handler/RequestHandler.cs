using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Web.Http;
using Newtonsoft.Json;
using UWPAsych.Model;

namespace UWPAsych.Handler
{
   public class RequestHandler<T>  where T : class 
    {
        // dependency Injection technique
        public T ViewModel { get; set; }
        public RequestHandler(T viewModel)
        {
            ViewModel = viewModel;
        }

       

        public static ObservableCollection<T> FetchAllData()
        {
            ObservableCollection<T> objects = null;
            using (var client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                {
                    // sends GET request
                    // ReSharper disable once AccessToDisposedClosure
                    response = await client.GetStringAsync(new Uri("http://localhost:5676/api/BookingInfoes")); 
                });
                // Wait
                task.Wait(); 
                // convert Json into Objects
                objects = JsonConvert.DeserializeObject<ObservableCollection<T>>(response);
            }

            return objects;
        }
        //public static async void GetById()
        //{

        //}

        //public static async void Post()
        //{
            
        //    //Event newEvent = new Event()
        //    //{
        //    //    Id = EventVm.AddEvent.Id,
        //    //    Name = EventVm.AddEvent.Name,
        //    //    Place = EventVm.AddEvent.Place,
        //    //    Description = EventVm.AddEvent.Description,
        //    //    DateTime = DateTimeConverter.DateTimeOffsetAndTimeSetToDateTime(EventVm.Date, EventVm.Time)
        //    //};
        //    //// EventVm --> EventCatalogSingleton --> Add
        //    //EventVm.EventCatalogSingleton.Add(newEvent);

        //    //// Display successful message
        //    //var messageDialog = new MessageDialog("Your have been successfully Added your new Event in calender: " + EventVm);
        //    //await messageDialog.ShowAsync();

        //}
        public static async void Delete()
        {

            // Create the message dialog and set its content
            //var messageDialog = new MessageDialog("Are you sure you want to Delete the Event: " + EventVm.SelectedEvent.Name + " ?");

            //// Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            //messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandInvokedHandler)));
            //messageDialog.Commands.Add(new UICommand("No", null));

            //// Set the command that will be invoked by default
            //messageDialog.DefaultCommandIndex = 0;

            //// Set the command to be invoked when escape is pressed
            //messageDialog.CancelCommandIndex = 1;

            //// Show the message dialog
            //await messageDialog.ShowAsync();

        }

        private static void CommandInvokedHandler(IUICommand command)
        {
           // EventVm.EventCatalogSingleton.Remove(EventVm.SelectedEvent);
        }
        public static DateTime DateTimeOffsetAndTimeSetToDateTime(DateTimeOffset date, TimeSpan time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, 0);
        }
    }
}
