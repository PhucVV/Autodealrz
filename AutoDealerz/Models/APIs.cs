using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public static class APIs
    {
        public static string root = "http://72.167.41.165/";

        public static string login = "autodealer/webservices/login.php";
        public static string forgotpassword = "autodealer/webservices/forget_password.php";
        public static string getcountry = "autodealer/webservices/getcountry.php";
        public static string getstate = "autodealer/webservices/getstate.php";
        public static string getcity = "autodealer/webservices/getcity.php";
        public static string register1 = "autodealer/webservices/customer_register.php";
        public static string register2 = "autodealer/webservices/customer_vehicle_info.php";
        public static string getbrand = "autodealer/webservices/getbrand.php";
        public static string getmodel = "autodealer/webservices/getmodel_no.php";
        public static string changepass = "autodealer/webservices/change_password.php";
        public static string getmyrequest = "autodealer/webservices/all_service_request.php";
        public static string deletemyrequest = "autodealer/webservices/service_request_delete.php";
        public static string myprofile = "autodealer/webservices/my_profile.php";
        public static string updateprofile = "autodealer/webservices/edit_profile.php";
        public static string getdialerinfo = "autodealer/webservices/get_dealer_info.php";
        public static string addmorevehicle = "autodealer/webservices/add_more_cars.php";
        public static string customervehicle = "autodealer/webservices/get_customer_vehicles.php";
        public static string sendfeedback = "autodealer/webservices/send_feedback.php";
        public static string getservicecity = "autodealer/webservices/get_service_cities.php";
        public static string getbranches = "autodealer/webservices/get_branches.php";
        public static string logout = "autodealer/webservices/logout.php";
        public static string getnews = "autodealer/webservices/get_news.php";
        public static string dealcount = "autodealer/webservices/deal_count.php";
        public static string servicetype = "autodealer/webservices/get_service_type.php";
        public static string servicerequest = "autodealer/webservices/customer_service_request.php";
        public static string sendparts = "autodealer/webservices/send_parts_detail.php";
        public static string getpartsrequest = "autodealer/webservices/all_parts_request.php";
        public static string deletepartrequest = "autodealer/webservices/parts_request_delete.php";
        public static string getdealer = "autodealer/webservices/get_dealer.php";
        public static string listdeals = "autodealer/webservices/deal.php";
        public static string notification = "autodealer/webservices/get_all_notifications.php";
        public static string updatenotification = "autodealer/webservices/update_notifications_readstatus.php";
        public static string sellvehicle = "autodealer/webservices/sell_your_car.php";
        public static string getnotificount = "autodealer/webservices/get_notifications_count.php";
        public static string getprices = "autodealer/webservices/get_prices.php";
        public static string getnewcar = "autodealer/webservices/new_cars.php";
        public static string bodytype = "autodealer/webservices/get_body_type.php";
        public static string getusedcar = "autodealer/webservices/used_cars.php";
        public static string newcarvarian = "autodealer/webservices/get_new_cars_variants_detail.php";
        public static string newcardetail = "autodealer/webservices/get_new_cars_detail.php";
        public static string testdrive = "autodealer/webservices/test_drive.php";
        public static string enquiry = "autodealer/webservices/enquiry.php";
        public static string UsdeCarDetail = "autodealer/webservices/get_used_cars_detail.php";
        public static string branchemployee = "autodealer/webservices/branch_employees.php";
        public static string getsettingstatus = "autodealer/webservices/get_notification_onoff_status.php";
        static string roadasisst = "autodealer/webservices/road_assist.php";
        public static string getcompleteservice = "autodealer/webservices/get_completed_services.php";
        public static string SettingMailOnOff = "autodealer/webservices/email_notificaton_onoff.php";
        public static string deletenotifi = "autodealer/webservices/notification_delete.php";

        public static string ApiDeleteNotifi()
        {
            return root + deletenotifi;
        }

        public static string ApiRoadAsisst()
        {
            return root + roadasisst;
        }
        public static string ApiGetCompleteService()
        {
            return root + getcompleteservice;
        }
        public static string ApiSettingMailOnOff()
        {
            return root + SettingMailOnOff;
        }
       
        public static string ApiSettingStatus()
        {
            return root + getsettingstatus;
        }
        public static string ApiUpdateNorStatus()
        {
            return root + updatenotification;
        }

        public static string ApiBranchEmployee()
        {
            return root + branchemployee;
        }

        public static string ApiUsedCarDetail()
        {
            return root + UsdeCarDetail;

        }
        public static string ApiEnquiry()
        {
            return root + enquiry;
        }
        public static string ApiTestDrive()
        {
            return root + testdrive;
        }
        public static string ApiNewcarDetail()
        {
            return root + newcardetail;
        }
        public static string ApiNewCarvarian()
        {
            return root + newcarvarian;
        }

        public static string ApiGetUsedCar()
        {
            return root + getusedcar;
        }
        public static string ApiBodyType()
        {
            return root + bodytype;
        }
        public static string ApiGetNewCar()
        {
            return root + getnewcar;
        }
        public static string ApiGetprices()
        {
            return root + getprices;
        }
        public static string ApiGetNotifiCount()
        {
            return root + getnotificount;
        }

        public static string ApiSellVehicle()
        {
            return root + sellvehicle;
        }
        public static string ApiAllNotification()
        {
            return root + notification;
        }
        public static string ApiListDeals()
        {
            return root + listdeals;
        }
        public static string ApiGetDealer()
        {
            return root + getdealer;
        }

        public static string ApiDeletePartRequest()
        {
            return root + deletepartrequest;
        }
        public static string ApiGetPartRequest()
        {
            return root + getpartsrequest;
        }
        public static string ApiSendParts()
        {
            return root + sendparts;
        }
        public static string SendServiceRequest()
        {
            return root + servicerequest;
        }
        public static string ApiGetServiceType()
        {
            return root + servicetype;
        }
        public static string ApiDealCount()
        {
            return root + dealcount;
        }
        public static string ApiGetNews()
        {
            return root + getnews;
        }
        public static string ApiDeleteMyrequest()
        {
            return root + deletemyrequest;
        }
        public static string ApiLogout()
        {
            return root + logout;
        }
        public static string ApiGetBranches()
        {
            return root + getbranches;
        }
        public static string ApiGetServiceCity()
        {
            return root + getservicecity;
        }
        public static string ApiSendFeedback()
        {
            return root + sendfeedback;
        }
        public static string ApiCustomerVehicle()
        {
            return root + customervehicle;
        }
        public static string ApiAddVehicle()
        {
            return root + addmorevehicle;
        }

        public static string ApiGetdialerInfo()
        {
            return root + getdialerinfo;
        }

        public static string ApiUpdateProfile()
        {
            return root + updateprofile;
        }
        public static string ApiMyprofile()
        {
            return root + myprofile;
        }
        public static string ApiGetMyRequest()
        {
            return root + getmyrequest;
        }
        public static string ApiChangePass()
        {
            return root + changepass;
        }

        public static string ApiGetmodel()
        {
            return root + getmodel;
        }
        public static string ApiGetbrand()
        {
            return root + getbrand;
        }
        public static string ApiRegister2()
        {
            return root + register2;
        }

        public static string ApiRegister()
        {
            return root + register1;
        }
        public static string ApiGetcity()
        {
            return root + getcity;
        }
        public static string ApiGetstate()
        {
            return root + getstate;
        }
        public static string ApiGetCountry()
        {
            return root + getcountry;
        }
        public static string ApiForgotPass()
        {
            return root + forgotpassword;
        }
        public static string ApiLogin()
        {
            return root + login;
        }
    }
}
