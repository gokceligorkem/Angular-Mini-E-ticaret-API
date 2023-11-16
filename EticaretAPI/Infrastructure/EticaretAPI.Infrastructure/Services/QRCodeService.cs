using EticaretAPI.Application.Abstraction.Services;
using QRCoder;

namespace EticaretAPI.Infrastructure.Services
{
    public class QRCodeService: IQRCodeService
    {

        public byte[] GeneratQRCode(string text)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] byteGraphip = qrCode.GetGraphic(20, new byte[] { 84, 99, 71 }, new byte[] { 240, 240, 240 });
            return byteGraphip;
        }
    }
}
