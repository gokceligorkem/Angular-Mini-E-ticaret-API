using MediatR;

namespace EticaretAPI.Application.Feature_Özellikler_.Products.Command.QRCodeUpdateProduct
{
    public class QRProductUpdateRequest:IRequest<QRProductUpdateResponse>
    {
        public int Stock { get; set; }
        public string productId { get; set; }
    }
}