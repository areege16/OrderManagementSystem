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

namespace OrderManagementSystem.Application.Admin.Invoices.Queries.GetInvoiceDetails
{
    public class GetInvoiceDetailsHandler : IRequestHandler<GetInvoiceDetailsQuery, ResponseDto<InvoiceDetailsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetInvoiceDetailsHandler> _logger;
        private readonly IMapper _mapper;

        public GetInvoiceDetailsHandler(IUnitOfWork unitOfWork,
                                        ILogger<GetInvoiceDetailsHandler> logger,
                                        IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ResponseDto<InvoiceDetailsDto>> Handle(GetInvoiceDetailsQuery request, CancellationToken cancellationToken)
        {
            var invoiceRepository = _unitOfWork.Repository<Invoice>();

            try
            {
                var invoice = await invoiceRepository
                       .GetAllAsNoTracking()
                       .Where(i => i.Id == request.Id)
                       .ProjectTo<InvoiceDetailsDto>(_mapper.ConfigurationProvider)
                       .SingleOrDefaultAsync(cancellationToken);

                if (invoice == null)
                {
                    _logger.LogWarning("Invoice {InvoiceId} not found ", request.Id);

                    return ResponseDto<InvoiceDetailsDto>.Error(ErrorCode.NotFound, $" Invoice with ID {request.Id} not found");
                }

                _logger.LogInformation("Invoice with ID {InvoiceId} retrieved successfully", request.Id);
                return ResponseDto<InvoiceDetailsDto>.Success(invoice, $" Invoice with ID {request.Id} retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve invoice with ID {InvoiceId}", request.Id);
                return ResponseDto<InvoiceDetailsDto>.Error(ErrorCode.DatabaseError, "Failed to retrieve invoice.");
            }
        }
    }
}