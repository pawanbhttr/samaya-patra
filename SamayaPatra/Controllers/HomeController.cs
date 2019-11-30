using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using SamayaPatra.Business;
using SamayaPatra.Helper;

namespace SamayaPatra.Controllers
{
    public class HomeController : BaseController
    {
        readonly BALTransaction bALTransaction = new BALTransaction();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserDetails()
        {
            dynamic model;
            try
            {
                var dt = await bALTransaction.GetActiveTripAsync(UserContext.UserID);
                var base64Image = string.Empty;
                if (dt != null && dt.Rows.Count > 0)
                {
                    base64Image = GenerateQRCode(dt.Rows[0]["TrasactionID"].ToText());
                    model = new { Message = "QR Code Generated Successfully", MessageType = "success", Data = dt, QrCode = base64Image };
                }
                else
                {
                    model = new
                    {
                        Message = "Record Not Found",
                        MessageType = "error"
                    };
                }
            }
            catch
            {
                model = new { Message = "Error While loading User details", MessageType = "error" };
            }
            return Json(model);
        }

        private string GenerateQRCode(string transactionId)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(transactionId, QRCodeGenerator.ECCLevel.Q);
            Base64QRCode qrCode = new Base64QRCode(qrCodeData);
            return qrCode.GetGraphic(20);
        }
    }
}