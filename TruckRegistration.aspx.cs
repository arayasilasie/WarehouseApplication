using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using WarehouseApplication.BLL;
using WarehouseApplication.DALManager;
using WarehouseApplication.GINLogic;
using WarehouseApplication.UserControls;

namespace WarehouseApplication
{
    public partial class TruckRegistration : System.Web.UI.Page
    {
        private IGINProcess ginProcess;
        private PageDataTransfer transferedData;
        private ErrorMessageDisplayer errorDisplayer;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            errorDisplayer = new ErrorMessageDisplayer(lblMessage);
            errorDisplayer.ClearErrorMessage();

            transferedData = new PageDataTransfer(Request.Path);

            PUNADataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckRegistration", "PUNAcknowledgement");
            DriverDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckRegistration", "RegisteredTruck");
            TruckDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckRegistration", "TruckData");
            TrailerDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckRegistration", "TrailerData");
            TruckGridViewer.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckRegistration", "RegisteredTruckGrid");
            PUNADataEditor.IsNew = false;
            try
            {
                ginProcess = GINProcessWrapper.GetGINProcess(IsPostBack);

                PUNADataEditor.Lookup = ginProcess.LookupSource;
                TruckDataEditor.Lookup = ginProcess.LookupSource;
                DriverDataEditor.Lookup = ginProcess.LookupSource;
                TrailerDataEditor.Lookup = ginProcess.LookupSource;
                TruckGridViewer.Lookup = ginProcess.LookupSource;

                PUNADataEditor.Setup();
                TruckDataEditor.Setup();
                DriverDataEditor.Setup();
                TrailerDataEditor.Setup();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            TruckGridViewer.DataSource = ginProcess.GINProcessInformation.RegisteredTrucks;
            TruckGridViewer.DataBind();

            var gridCommands = from command in TruckGridViewer.Driver.Columns
                               where command.IsCommand
                               select command.AttachedRenderer;
            foreach (LinkGINColumnRenderer linkCommand in gridCommands)
            {
                linkCommand.Command += new CommandEventHandler(linkCommand_Command);
            }

            bool bPunClosed = Math.Abs(ginProcess.GINProcessInformation.PledgedWeight - ginProcess.GINProcessInformation.IssuedWeight) <= 0.0001M;
            if (bPunClosed)
            {
                btnAddTruck.Text = "Close PUN";
            }

            if (!IsPostBack)
            {
                try
                {
                    PUNADataEditor.DataSource = ginProcess.GINProcessInformation.PUNAcknowledgement;
                    PUNADataEditor.DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected void linkCommand_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "EditTruck")
            {
                DriverDataEditor.IsNew = false;
                TruckDataEditor.IsNew = false;
                TrailerDataEditor.IsNew = false;
                var truckToEdit = from truck in ginProcess.GINProcessInformation.Trucks
                                  where truck.TruckId == new Guid((string)e.CommandArgument)
                                  select truck;
                //TruckDataEditor.DataSource = truckToEdit.ElementAt(0);
                //TruckDataEditor.DataBind();
                TruckRegistrationInfo truckRegistration = new TruckRegistrationInfo()
                {
                    GINTruck = truckToEdit.ElementAt(0)
                };
                DriverDataEditor.DataSource = truckRegistration.GINTruck;
                DriverDataEditor.DataBind();
                TruckDataEditor.DataSource = truckRegistration.Truck;
                TruckDataEditor.DataBind();
                TrailerDataEditor.DataSource = truckRegistration.Trailer;
                TrailerDataEditor.DataBind();
                mpeTruckDataEditorExtender.Show();
            }
        }

        protected void TruckDataEditor_Cancel(object sender, EventArgs e)
        {
            mpeTruckDataEditorExtender.Hide();
        }

        protected void TruckDataEditor_Ok(object sender, EventArgs e)
        {
            try
            {
                GINTruckInfo ginTruck = (GINTruckInfo)DriverDataEditor.DataSource;
                TruckInfo mainTruck = (TruckInfo)TruckDataEditor.DataSource;
                TruckInfo trailer = (TruckInfo)TrailerDataEditor.DataSource;
                if (mainTruck.IsNew && (mainTruck.PlateNo != string.Empty))
                {
                    if (!new TruckRegisterBLL() 
                    {
                        Id = mainTruck.TruckId,
                        IsTrailer = false,
                        Status = TruckStatus.Active,
                        TrackingNo = string.Empty,
                        TruckModelYearId = mainTruck.TruckModelYearId,
                        TruckNumber = mainTruck.PlateNo
                    }.Add());

                        //throw new Exception("Unable to register truck");
                }
                if (trailer.IsNew && (trailer.PlateNo != string.Empty))
                {
                    if (!new TruckRegisterBLL()
                    {
                        Id = trailer.TruckId,
                        IsTrailer = true,
                        Status = TruckStatus.Active,
                        TrackingNo = string.Empty,
                        TruckModelYearId = trailer.TruckModelYearId,
                        TruckNumber = trailer.PlateNo
                    }.Add()) ;
                       // throw new Exception("Unable to register trailer");
                }
                ginTruck.MainTruckId = mainTruck.TruckId;
                ginTruck.TrailerId = trailer.TruckId;
                ginTruck.PlateNo = mainTruck.PlateNo;
                ginTruck.TrailerNo = trailer.PlateNo;
                if (TruckDataEditor.IsNew)
                {
                    ginProcess.AddTruck(ginTruck);
                    TruckGridViewer.DataSource = ginProcess.GINProcessInformation.RegisteredTrucks;
                    TruckGridViewer.DataBind();
                    updatePanel.Update();
                }
                else
                {
                    var editedTruck = from truck in ginProcess.GINProcessInformation.Trucks
                                      where truck.TruckId == ginTruck.TruckId
                                      select truck;
                    if (editedTruck.Count() > 0)
                    {
                        editedTruck.ElementAt(0).Copy(ginTruck);
                        TruckGridViewer.DataSource = ginProcess.GINProcessInformation.RegisteredTrucks;
                        TruckGridViewer.DataBind();
                        updatePanel.Update();
                    }
                }
            }
            catch (Exception ex)
            {
                errorDisplayer.ShowErrorMessage(ex.Message);
            }
            mpeTruckDataEditorExtender.Hide();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                GINProcessWrapper.SaveTruckRegistration();

                PageDataTransfer reportTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/ReportViewerForm.aspx");
                reportTransfer.TransferData["TransactionId"] = ginProcess.GINProcessInformation.TransactionId;
                reportTransfer.TransferData["IsGINTransaction"] = false;
                reportTransfer.TransferData["RequestedReport"] = "rptGINTrackingReport";
                reportTransfer.TransferData["GINTrackingReportData"] = ginProcess.GINTrackingReportData;
                reportTransfer.TransferData["ReturnPage"] = transferedData.GetTransferedData("ReturnPage");
                reportTransfer.PersistToSession();
                ScriptManager.RegisterStartupScript(this,
                    this.GetType(),
                    "ShowReport",
                    "<script type=\"text/javascript\">" +
                        string.Format("javascript:window.open(\"ReportViewerForm.aspx?id={0}\", \"_blank\",\"height=400px,width=600px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +
                        string.Format("location.href = '{0}';", transferedData.GetTransferedData("ReturnPage")) +
                    "</script>",
                    false);

                GINProcessWrapper.RemoveGINProcessInformation();
            }
            catch (Exception ex)
            {
                btnSave.Enabled = true;
                errorDisplayer.ShowErrorMessage(ex.Message);
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            GINProcessWrapper.RemoveGINProcessInformation();
            transferedData.Return();
        }

        protected void btnAddTruck_Click(object sender, EventArgs e)
        {
            bool bPunClosed = (ginProcess.GINProcessInformation.RemainingWeight <= 0);
            if (bPunClosed)
            {
                GINProcessWrapper.ClosePun();
                GINProcessWrapper.RemoveGINProcessInformation();
                transferedData.Return();
            }
            else
            {
                DriverDataEditor.IsNew = true;
                TruckDataEditor.IsNew = true;
                TrailerDataEditor.IsNew = true;
                TruckRegistrationInfo truckRegistration = new TruckRegistrationInfo()
                {
                    GINTruck = ginProcess.GetBlankTruck()
                };
                DriverDataEditor.DataSource = truckRegistration.GINTruck;
                DriverDataEditor.DataBind();
                TruckDataEditor.DataSource = truckRegistration.Truck;
                TruckDataEditor.DataBind();
                TrailerDataEditor.DataSource = truckRegistration.Trailer;
                TrailerDataEditor.DataBind();
                mpeTruckDataEditorExtender.Show();
            }
        }
    }

}
