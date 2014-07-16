﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Business.WebApi.Controllers
{
    public class VisitorRecordController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage visitorRecordSave([FromUri]Business.WebApi.Models.VisitorRecordSaveQuery query)
        {
            string purchaserIp = Request.GetClientIpAddress();
            string purchaserProduct = string.Empty;
            string language = string.Empty;
            string purchaserDomain = string.Empty;
            string targetEmail = string.Empty;

            if (!string.IsNullOrWhiteSpace(query.PurchaserProduct))
            {
                purchaserProduct = query.PurchaserProduct;
            }
            if (!string.IsNullOrWhiteSpace(query.Language))
            {
                language = query.Language;
            }
            if (!string.IsNullOrWhiteSpace(query.PurchaserDomain))
            {
                purchaserDomain = query.PurchaserDomain;
            }
            if (!string.IsNullOrWhiteSpace(query.TargetEmail))
            {
                targetEmail = query.TargetEmail;
            }

            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;//纯真IP数据文件路径..
            QQWry.NET.QQWryLocator2 qqWry2 = new QQWry.NET.QQWryLocator2(basePath + "\\Models\\QQWry.dat");
            QQWry.NET.IPLocation ip2 = qqWry2.Query(purchaserIp);  //查询一个IP地址
            string puchaserCountry = ip2.Country;

            Business.Serives.VisitRecordService.VisitRecordSave(purchaserIp, purchaserProduct, language, puchaserCountry, purchaserDomain, targetEmail);
            List<string> retList = new List<string>();
            var returnObj = new Business.WebApi.Models.ResultObject<List<string>>();
            //retList.Add("save record success!");
            returnObj.ReturnData = retList;
            returnObj.Status = Business.WebApi.Models.ServerStatus.SaveSuccess;
            return Request.CreateResponse<Business.WebApi.Models.ResultObject<List<string>>>(HttpStatusCode.OK, returnObj);
        }
    }


}