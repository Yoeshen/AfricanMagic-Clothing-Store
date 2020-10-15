using AfricanMagicSystem.Models;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using Nexmo.Api.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfricanMagicSystem.Controllers
{
    public class SupplierShipController : Controller
    {
        // GET: SupplierShip
        public ActionResult Index()
        {
            var sched = new DHXScheduler(this);
            sched.Skin = DHXScheduler.Skins.Terrace;
            sched.LoadData = true;
            sched.EnableDataprocessor = true;
            sched.InitialDate = new DateTime(2020,10,15);
            return View(sched);
        }

        public ContentResult Data()
        {
            return new SchedulerAjaxData(new ApplicationDbContext().supplierShippings.Select(e => new { e.ShippingID, e.Subject, e.StartTime, e.EndTime }));
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            var changedEvent = DHXEventsHelper.Bind<SupplierShipping>(actionValues);
            var entities = new ApplicationDbContext();
            try
            {
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        entities.supplierShippings.Add(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        changedEvent = entities.supplierShippings.FirstOrDefault(ev => ev.ShippingID == action.SourceId);
                        entities.supplierShippings.Remove(changedEvent);
                        break;
                    default:// "update"
                        var target = entities.supplierShippings.Single(e => e.ShippingID == changedEvent.ShippingID);
                        DHXEventsHelper.Update(target, changedEvent, new List<string> { "id" });
                        break;
                }
                entities.SaveChanges();
                action.TargetId = changedEvent.ShippingID;
            }
            catch (Exception a)
            {
                action.Type = DataActionTypes.Error;
            }
            
       return (new AjaxSaveResponse(action));
        }
    }
}