﻿using EticaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Repository.Invoice
{
    public interface IInvoiceWriteRepository:IWriteRepository<InvoiceFile>
    {
    }
}