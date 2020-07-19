using Library.Core.Controls;
using Library.Core.Security;
using Library.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Web.Api
{

    public class DashBoardController : AdminAPIBaseController
    {
        private readonly IManageEmployeService _manageEmployeService;
        public DashBoardController(IManageEmployeService manageEmployeService)
        {
            _manageEmployeService = manageEmployeService;
        }
        [HttpGet]
        public HttpResponseMessage DownloadFile([FromUri] string exportid, int fileType)
        {

            var csv = _manageEmployeService.ExportTOCSV(exportid);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            string fileName = string.Empty;

            if (fileType == 1)
            {
                result.Content = new StringContent(csv);
                var headers = result.Content.Headers;
                fileName = "Download-" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + ".xls";
                headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                headers.ContentDisposition.FileName = fileName;
                headers.ContentType = new MediaTypeHeaderValue("text/csv");
            }
            else if (fileType == 2)
            {
                result.Content = new StringContent(csv);
                var headers = result.Content.Headers;
                fileName = "Download-" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + ".csv";
                headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                headers.ContentDisposition.FileName = fileName;
                headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
            }
            else
            {
                fileName = "Download-" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + ".pdf";
                string filePath = _manageEmployeService.ExportTOPDF(exportid);
                var fileBytes = File.ReadAllBytes(filePath);
                var fileMemstream = new MemoryStream(fileBytes);
                result.Content = new StreamContent(fileMemstream);
                var headers = result.Content.Headers;

                headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                headers.ContentDisposition.FileName = fileName;
                headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            }

            return result;
        }
    }
}
