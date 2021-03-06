using AutoMapper;
using DataLayer;
using ServiceModel;
using System;
using System.Threading.Tasks;
using Utility;

namespace ProcessLayer
{
    public class CustomerProcess : ICustomerProcess
    {
        private readonly ICustomerData _customerData;
        private readonly IMapper _mapper;
        public CustomerProcess(IMapper mapper, ICustomerData customerData)
        {
            this._mapper = mapper;
            this._customerData = customerData;
        }

        public async Task<IResponse> Add(Customer customer)
        {
            try
            {
                var customerDomain = _mapper.Map<DomainModel.Customer>(customer);

                var response = await _customerData.Add(customerDomain);

                return new SuccessResponse() { Message = $"Created new customer with ID {response}" };
            }
            catch (Exception e)
            {
                return new FailResponse() { Message = $"Failed to create new customer - {e.Message}" };
            }
        }

        public async Task<Customer> Get(int customerId)
        {
            try
            {
                var response = await _customerData.Get(customerId);

                return response.GetT<Customer>();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
