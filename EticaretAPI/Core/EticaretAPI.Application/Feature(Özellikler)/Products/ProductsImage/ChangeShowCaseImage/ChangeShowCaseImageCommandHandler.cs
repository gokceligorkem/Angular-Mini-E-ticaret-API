using EticaretAPI.Application.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Products.ProductsImage.ChangeShowCaseImage
{
    public class ChangeShowCaseImageCommandHandler : IRequestHandler<ChangeShowCaseImageCommandRequest, ChangeShowCaseImageCommandResponse>
    {
        readonly IFileImageWriteRepository _fileimagerepository;

        public ChangeShowCaseImageCommandHandler(IFileImageWriteRepository fileimagerepository)
        {
            _fileimagerepository = fileimagerepository;
        }

        public async Task<ChangeShowCaseImageCommandResponse> Handle(ChangeShowCaseImageCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _fileimagerepository.Table.Include(p => p.Products)
          .SelectMany(p => p.Products, (pif, p) => new
          {
              pif,
              p
          });
            var data = await query.FirstOrDefaultAsync(p => p.p.ID == Guid.Parse(request.productId) && p.pif.Showcase);
            if(data!=null)
            data.pif.Showcase = false;

            var image = await query.FirstOrDefaultAsync(p => p.pif.ID == Guid.Parse(request.imageId));
            if(image!=null)
            image.pif.Showcase = true;

            await _fileimagerepository.SaveAsync();
            return new();
        }
    }
}
