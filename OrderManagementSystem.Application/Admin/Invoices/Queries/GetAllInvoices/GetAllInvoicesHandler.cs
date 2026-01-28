using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Invoice.Admin;
using OrderManagementSystem.Application.UnitOfWorkContract;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Models;

namespace OrderManagementSystem.Application.Admin.Invoices.Queries.GetAllInvoices
{
    public class GetAllInvoicesHandler : IRequestHandler<GetAllInvoicesQuery, ResponseDto<List<AdminInvoiceSummaryDto>>>
    {
        private readonly ILogger<GetAllInvoicesHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllInvoicesHandler(ILogger<GetAllInvoicesHandler> logger,
                                     IUnitOfWork unitOfWork,
                                     IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseDto<List<AdminInvoiceSummaryDto>>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoiceRepository = _unitOfWork.Repository<Invoice>();
            try
            {
                var invoices = await invoiceRepository
                 .GetAllAsNoTracking()
                 .OrderBy(i => i.InvoiceDate)
                 .ProjectTo<AdminInvoiceSummaryDto>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);

                if (invoices.Count == 0)
                {
                    _logger.LogWarning("No invoices found.");

                    return ResponseDto<List<AdminInvoiceSummaryDto>>.Error(ErrorCode.NotFound, "No invoices found.");
                }

                _logger.LogInformation("Retrieved {Count} invoices ", invoices.Count);

                return ResponseDto<List<AdminInvoiceSummaryDto>>.Success(invoices, "Invoices retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve invoices.");
                return ResponseDto<List<AdminInvoiceSummaryDto>>.Error(ErrorCode.DatabaseError, "Failed to retrieve invoices.");
            }
        }
    }
}