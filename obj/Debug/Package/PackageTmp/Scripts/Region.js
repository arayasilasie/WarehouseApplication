function GetZones()
{
    var RegionId =$get("cboRegion").Value;
    var lang = "33673ac2-7888-42d5-9712-522941b3208c";
    ECXLookUp.ECXLookup.GetActiveZonesAsync(lang,RegionId,OnWSCall);
    
}

function OnWSCall(results)
{
    var res = $get("cboZone");
    res.Control.set_data(results);
}