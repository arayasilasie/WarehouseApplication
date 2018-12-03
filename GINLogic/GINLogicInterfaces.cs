using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseApplication.DALManager;
using System.Xml;
using System.Xml.Serialization;
using System.Data.SqlClient;

namespace WarehouseApplication.GINLogic
{
    public interface IGINProcess
    {
        ILookupSource LookupSource { get; }
        GINProcessInfo GINProcessInformation { get; set; }
        SqlTransaction SaveAvailabilityVerification(PUNAcknowledgementInformation punaInformation);
        SqlTransaction SaveTruckRegistration();
        SqlTransaction CreateTruck(GINTruckInfo truck);
        SqlTransaction SaveTruck(GINTruckInfo truck);
        SqlTransaction SaveTruck(GINTruckInfo truck, SqlTransaction transaction);
        SqlTransaction SaveTruckInformation(Guid truckId);
        SqlTransaction SaveLoading(Guid truckId);
        SqlTransaction SaveScaling(Guid truckId);
        SqlTransaction SaveGIN(Guid truckId);
        SqlTransaction SaveGIN(Guid truckId, SqlTransaction transaction);
        void ValidateAvailabilityConfirmation(PUNAcknowledgementInformation punaInformation);
        void ValidateTruckRegistration();
        void ValidateGINProcess(GINProcessInfo ginProcess);
        void ValidateTruck(GINTruckInfo truck);
        void ValidateGIN(GINInfo gin);
        void ValidateTruckLoading(TruckLoadInfo load);
        void ValidateTruckWeight(TruckWeightInfo weight);
        void ValidateWorker(WorkerInformation worker);
        void ValidateStack(TruckStackInfo stack);
        SqlTransaction CompleteAvailabilityVerification();
        SqlTransaction CompleteLoading(Guid truckId);
        SqlTransaction CompleteScaling(Guid truckId);
        SqlTransaction GINGenerated(Guid truckId);
        SqlTransaction GINSigned(Guid truckId);
        SqlTransaction GINApproved(Guid truckId);
        SqlTransaction CompleteGINProcess();
        SqlTransaction AbortGINProcess();
        GINTruckInfo GetBlankTruck();
        WorkerInformation GetBlankLoader(Guid truckId);
        WorkerInformation GetBlankWeigher(Guid truckId);
        TruckStackInfo GetBlankTruckStack(Guid truckId);
        ReturnedBagsInfo GetBlankReturnedBags(Guid truckId);
        SampleInfo GetBlankSample();
        void DeleteLoader(Guid id);
        void DeleteWeigher(Guid id);
        void DeleteTruck(Guid id);
        void DeleteTruckStack(Guid truckId, Guid id);
        void AddLoader(Guid truckId, WorkerInformation loader);
        void AddStack(Guid truckId, TruckStackInfo stack);
        void AddReturnedBags(Guid truckId, ReturnedBagsInfo returnedBags);
        void AddSample(SampleInfo sample);
        void AddTruck(GINTruckInfo truck);
        void AddWeigher(Guid truckId, WorkerInformation weigher);
        GINReportInfo GetGINReport(Guid ginId);
        //Guid CurrentUser { get; }
        decimal CalculateNetWeight(Guid truckId);
        SqlTransaction SaveGINEditingRequest(GINEditingRequest request);
        TrackingReportData PUNTrackingReportData { get; }
        List<GINTrackingReportData> GINTrackingReportData { get; }
    }

    public interface IPickupNotice
    {
        ILookupSource LookupSource { get; }
        PickupNoticeInformation PickupNoticeInformation { get; set; }
        PUNAcknowledgementInformation PUNAInformation { get; }
        SqlTransaction AcknowledgePickupNotice(PUNAcknowledgementInformation acknowledgement);
        SqlTransaction AcknowledgePickupNotice(PUNAcknowledgementInformation acknowledgement, SqlTransaction transaction);
        SqlTransaction Aborted();
        void GINIssued(SqlTransaction transaction);
        void Aborted(SqlTransaction transaction);
        PUNReportData GetPUNReport(Guid punId);
    }

}
