using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using AutoDealerz.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace AutoDealerz.Controls
{
    public partial class layoutSales : UserControl
    {
        List<fuel> Fuel = new List<fuel>();
        public layoutSales()
        {
            InitializeComponent();
            for (int i = 0; i < 5; i++)
            {
                switch (i)
                {
                    case 0:
                        Fuel.Add(new fuel() { id = i.ToString(), name = "All" });
                        break;
                    case 1:
                        Fuel.Add(new fuel() { id = i.ToString(), name = "Petrol" });
                        break;
                    case 2:
                        Fuel.Add(new fuel() { id = i.ToString(), name = "Diesel" });
                        break;
                    case 3:
                        Fuel.Add(new fuel() { id = i.ToString(), name = "LPG" });
                        break;
                    case 4:
                        Fuel.Add(new fuel() { id = i.ToString(), name = "CNG" });
                        break;
                }
            }
            CB_FuelType_used.ItemsSource = Fuel;
        }

        private void MainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainPivot.SelectedIndex == 1)
                LoadData2();
        }

        private void LoadBrandUsed()
        {
            if (CB_Brands_used.ItemsSource == null)
            {
                if (App.ListBrand.Count > 0)
                {
                    List<Brand> tempbrand = new List<Brand>();
                    foreach (var item in App.ListBrand)
                        tempbrand.Add(item);
                    tempbrand.Insert(0, new Brand() { brand_id = "-1", brand_name = "All" });
                    CB_Brands_used.ItemsSource = tempbrand;
                    return;
                }
                loading_brands_used.Visibility = System.Windows.Visibility.Visible;
                ListBrandModel model = new ListBrandModel();
                model.OKAction += () =>
                {
                    loading_brands_used.Visibility = System.Windows.Visibility.Collapsed;
                    List<Brand> tempbrand = new List<Brand>();
                    foreach (var item in App.ListBrand)
                        tempbrand.Add(item);
                    tempbrand.Insert(0, new Brand() { brand_id = "-1", brand_name = "All" });
                    CB_Brands_used.ItemsSource = tempbrand;
                };
                model.LoadData();
            }
        }

        private void GetBodyTypeUsed()
        {
            if (CB_BodyType_used.ItemsSource == null)
            {
                loading_BodyType_used.Visibility = System.Windows.Visibility.Visible;
                ListBodyType model = new ListBodyType();
                model.OKAction += () =>
                {
                    loading_BodyType_used.Visibility = System.Windows.Visibility.Collapsed;
                    model.ListBody.Insert(0, new BodyType() { body_type_id="-1",body_type="All"});
                    CB_BodyType_used.ItemsSource = model.ListBody;
                };
                model.LoadData();
            }
        }

        private void GetBugetUsed()
        {
            if (CB_Budget_used.ItemsSource == null)
            {
                loading_Budget_used.Visibility = System.Windows.Visibility.Visible;
                ListPriceModel model = new ListPriceModel();
                model.OKAction += () =>
                {
                    loading_Budget_used.Visibility = System.Windows.Visibility.Collapsed;
                    model.ListPrices.Insert(0, new Prices() { price = "All", price_id = "-1" });
                    CB_Budget_used.ItemsSource = model.ListPrices;
                };
                model.LoadData();
            }
        }
        private void LoadData2()
        {
            GetUsedCar();

            LoadBrandUsed();

            GetBodyTypeUsed();

            GetBugetUsed();

           

        }
        private void CB_Brands_used_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_Brands_used.SelectedItem == null) return;
            Brand data = CB_Brands_used.SelectedItem as Brand;
            loading_models_used.Visibility = System.Windows.Visibility.Visible;
            ListModelModel model = new ListModelModel();
            model.OKAction += () =>
            {
                loading_models_used.Visibility = System.Windows.Visibility.Collapsed;
                model.ListModel.Insert(0, new Model() {model_id="-1",model_name="All" });
                CB_Models_used.ItemsSource = model.ListModel;
            };
            model.LoadData(data.brand_id);
        }
        private void GetBrandNew()
        {
            if (CB_Brands_New.ItemsSource == null)
            {
                if (App.ListBrand.Count > 0)
                {
                    List<Brand> tempbrand = new List<Brand>();
                    foreach (var item in App.ListBrand)
                        tempbrand.Add(item);
                    tempbrand.Insert(0, new Brand() { brand_id = "-1", brand_name = "All" });

                    CB_Brands_New.ItemsSource = tempbrand;
                    return;
                }
                loading_branch_New.Visibility = System.Windows.Visibility.Visible;
                ListBrandModel model = new ListBrandModel();
                model.OKAction += () =>
                {
                    loading_branch_New.Visibility = System.Windows.Visibility.Collapsed;
                    List<Brand> tempbrand = new List<Brand>();
                    foreach (var item in App.ListBrand)
                        tempbrand.Add(item);
                    tempbrand.Insert(0, new Brand() { brand_id = "-1", brand_name = "All" });

                    CB_Brands_New.ItemsSource = tempbrand;
                };
                model.LoadData();
            }
        }

        private void GetBranchNew()
        {
            if (CB_Branch_New.ItemsSource == null)
            {
                loading_branch_New.Visibility = System.Windows.Visibility.Visible;
                ListBranchModel model = new ListBranchModel();
                model.OKAction += () =>
                {
                    loading_branch_New.Visibility = System.Windows.Visibility.Collapsed;
                    model.ListBranch.Insert(0, new Branch() { branch_name = "All", branch_id = "-1", branch_address = "All" });
                    CB_Branch_New.ItemsSource = model.ListBranch;
                };
                model.LoadData("");
            }
        }

        private void GetBugGetnew()
        {
            if (CB_Budget_New.ItemsSource == null)
            {
                loading_budget_New.Visibility = System.Windows.Visibility.Visible;
                ListPriceModel model = new ListPriceModel();
                model.OKAction += () =>
                {
                    loading_budget_New.Visibility = System.Windows.Visibility.Collapsed;
                    model.ListPrices.Insert(0, new Prices() { price = "All", price_id = "-1" });
                    CB_Budget_New.ItemsSource = model.ListPrices;
                };
                model.LoadData();
            }
        }

        public void LoadData()
        {
            GetNewCar();

            GetBrandNew();
            GetBranchNew();

            GetBugGetnew();
        }

        private void CB_Brands_New_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_Brands_New.SelectedItem == null) return;
            Brand data = CB_Brands_New.SelectedItem as Brand;
            loading_models_New.Visibility = System.Windows.Visibility.Visible;
            ListModelModel model = new ListModelModel();
            model.OKAction += () =>
            {
                loading_models_New.Visibility = System.Windows.Visibility.Collapsed;
                model.ListModel.Insert(0, new Model() { model_id = "-1", model_name = "All" });
                CB_Models_New.ItemsSource = model.ListModel;
            };
            model.LoadData(data.brand_id);
        }

        private void BT_search_Click(object sender, RoutedEventArgs e)
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            GetNewCar();
        }

        private async void GetNewCar()
        {
            List_search_new.ItemsSource = null;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),   
                        new KeyValuePair<string, string>("brand_id", CB_Brands_New.SelectedItem!=null?((CB_Brands_New.SelectedItem as Brand).brand_id!="-1"?(CB_Brands_New.SelectedItem as Brand).brand_id:""):""),
                       new KeyValuePair<string, string>("model_id", CB_Models_New.SelectedItem!=null?((CB_Models_New.SelectedItem as Model).model_id!="-1"?(CB_Models_New.SelectedItem as Model).model_id:""):""),
                       new KeyValuePair<string, string>("price", CB_Budget_New.SelectedItem!=null?((CB_Budget_New.SelectedItem as Prices).price!="All"?(CB_Budget_New.SelectedItem as Prices).price:""):""),   
                       new KeyValuePair<string, string>("branch_id", CB_Branch_New.SelectedItem!=null?((CB_Branch_New.SelectedItem as Branch).branch_id!="-1"?(CB_Branch_New.SelectedItem as Branch).branch_id:""):""), 
                    new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),   
                     new KeyValuePair<string, string>("brand_status", CB_Brands_New.SelectedItem!=null?((CB_Brands_New.SelectedItem as Brand).brand_id!="-1"?"1":"0"):"0"),   
                        new KeyValuePair<string, string>("price_status", CB_Budget_New.SelectedItem!=null?((CB_Budget_New.SelectedItem as Prices).price!="All"?"1":"0"):"0"),  
                         new KeyValuePair<string, string>("branch_status",  CB_Branch_New.SelectedItem!=null?((CB_Branch_New.SelectedItem as Branch).branch_id!="-1"?"1":"0"):"0"),

                        new KeyValuePair<string, string>("language", "en"),
                          new KeyValuePair<string, string>("model_status",CB_Models_New.SelectedItem!=null?((CB_Models_New.SelectedItem as Model).model_id!="-1"?"1":"0"):"0")
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiGetNewCar(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                CarObject data = JsonConvert.DeserializeObject<CarObject>(responseString);
                if (data.status == "true")
                {
                    List_search_new.ItemsSource = data.cars;
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;

            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private async void GetUsedCar()
        {
            List_search_used.ItemsSource = null;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {   new KeyValuePair<string, string>("language", "en"),
                          new KeyValuePair<string, string>("brand_id", CB_Brands_used.SelectedItem!=null?((CB_Brands_used.SelectedItem as Brand).brand_id!="-1"?(CB_Brands_used.SelectedItem as Brand).brand_id:""):""),
                      new KeyValuePair<string, string>("model_id", CB_Models_used.SelectedItem!=null?((CB_Models_used.SelectedItem as Model).model_id!="-1"?(CB_Models_used.SelectedItem as Model).model_id:""):""),
                       new KeyValuePair<string, string>("price", CB_Budget_used.SelectedItem!=null?((CB_Budget_used.SelectedItem as Prices).price!="All"?(CB_Budget_used.SelectedItem as Prices).price:""):""),   
                     new KeyValuePair<string, string>("body_type", CB_BodyType_used.SelectedItem!=null?((CB_BodyType_used.SelectedItem as BodyType).body_type_id!="-1"?(CB_BodyType_used.SelectedItem as BodyType).body_type_id:""):""), 
 new KeyValuePair<string, string>("fuel_type", CB_FuelType_used.SelectedItem!=null?(CB_FuelType_used.SelectedItem as fuel).id:"0"), 
 new KeyValuePair<string, string>("model_year", TB_eg.Text),   

                        new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),   
       new KeyValuePair<string, string>("brand_status", CB_Brands_used.SelectedItem!=null?((CB_Brands_used.SelectedItem as Brand).brand_id!="-1"?"1":"0"):"0"),
              new KeyValuePair<string, string>("price_status", CB_Budget_used.SelectedItem!=null?((CB_Budget_used.SelectedItem as Prices).price!="All"?"1":"0"):"0"),  

                     new KeyValuePair<string, string>("fuel_status", (CB_FuelType_used.SelectedItem as fuel).id=="0"?"0":"1"), 


                        new KeyValuePair<string, string>("body_status", CB_BodyType_used.SelectedItem!=null?((CB_BodyType_used.SelectedItem as BodyType).body_type_id!="-1"?"1":"0"):"0"), 

                          new KeyValuePair<string, string>("model_status",CB_Models_used.SelectedItem!=null?((CB_Models_used.SelectedItem as Model).model_id!="-1"?"1":"0"):"0")
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiGetUsedCar(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                CarObject data = JsonConvert.DeserializeObject<CarObject>(responseString);
                if (data.status == "true")
                {
                    List_search_used.ItemsSource = data.cars;
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;

            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void BT_search_used_Click(object sender, RoutedEventArgs e)
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            GetUsedCar();
        }

        private void List_search_new_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_search_new.SelectedIndex < 0) return;
            PhoneApplicationService.Current.State["new_vehicle"] = List_search_new.SelectedItem as Car;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/NewVehicles.xaml", UriKind.Relative));
            List_search_new.SelectedIndex = -1;
        }

        private void List_search_used_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_search_used.SelectedIndex < 0) return;
            PhoneApplicationService.Current.State["used_vehicle"] = List_search_used.SelectedItem as Car;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/UsedVehicleDetail.xaml", UriKind.Relative));
            List_search_used.SelectedIndex = -1;
        }

        private void CB_Brands_used_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (CB_Brands_used.ItemsSource == null)
                LoadBrandUsed();
        }

        private void CB_BodyType_used_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (CB_BodyType_used.ItemsSource == null)
                GetBodyTypeUsed();
        }

        private void CB_Budget_used_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (CB_Budget_used.ItemsSource == null)
                GetBugetUsed();
        }

        private void CB_Brands_New_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (CB_Brands_New.ItemsSource == null)
                GetBrandNew();
        }

        private void CB_Models_New_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        
                
        }

        private void CB_Branch_New_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (CB_Branch_New.ItemsSource == null)
                GetBranchNew();
        }

        private void CB_Budget_New_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (CB_Budget_New.ItemsSource == null)
                GetBugGetnew();
        }

    }
}
